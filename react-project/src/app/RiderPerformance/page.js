'use client';
import { useEffect, useState } from "react";
import './page.css';
import { ResponsiveBar } from '@nivo/bar';
import { ResponsiveLine } from '@nivo/line';

export default function RiderPerformanceTable({ drfLicense }) {
    const [performance, setPerformance] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");
    

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await fetch(
                    `https://localhost:7265/api/riderperformance/${encodeURIComponent("DRF-000038")}`
                );
                if (!response.ok) throw new Error("Fejl ved hentning af data");
                const data = await response.json();
                setPerformance(data);
            } catch (err) {
                setError(err.message);
            } finally {
                setLoading(false);
            }
        };

        fetchData();
    }, [drfLicense]);

    if (loading) return <p>Loading...</p>;
    if (error) return <p>Error: {error}</p>;
    if (performance.length === 0) return <p>Ingen data fundet for denne rytter</p>;

    const maxPlacement = Math.max(...performance.map(p => p.placements ?? 0));

    const scoreData = performance.map((p, index) => ({
        competitionClass: `${p.competitionName} - ${p.classLevel}`,
        score: p.score ?? 0
    }));

    const faultsData = performance.map((p, index) => ({
        competitionClassHorse: `${p.competitionName} - ${p.classLevel} - ${p.horseName}`,
        faults: p.faults ?? 0
    }));

    const placementsData = performance.map((p, index) => ({
        competitionClassHorse: `${p.competitionName} - ${p.classLevel} - ${p.horseName}`,
        placements: p.placements !== null ? maxPlacement - p.placements + 1 : 0
    }));

    const averageScoreData = Object.values(
        performance.reduce((acc, p) => {
            if (!acc[p.horseName]) {
                acc[p.horseName] = {
                    horse: p.horseName,
                    totalScore: 0,
                    count: 0
                };
            }

            if (p.score !== null && p.score !== undefined) {
                acc[p.horseName].totalScore += p.score;
                acc[p.horseName].count += 1;
            }

            return acc;
        }, {})
    ).map(h => ({
        horse: h.horse,
        averageScore: h.count > 0
            ? Number((h.totalScore / h.count).toFixed(2))
            : 0
    }));

    const sortedPerformance = [...performance].sort(
        (a, b) => new Date(a.startTime) - new Date(b.startTime)
    );

    let accumulated = 0;

    const accumulatedScoreData = [
        {
            id: "Akkumuleret Score",
            data: sortedPerformance.map(p => {
                accumulated += p.score ?? 0; // hvis score mangler, brug 0
                return {
                    x: new Date(p.startTime).toLocaleDateString(), // eller p.competitionName
                    y: accumulated
                };
            })
        }
    ];


    return (
        <div>
            <h1 style={{ frontweight: 'bold', textAlign: 'center', marginTop: '1rem' }}>
                Rytterperformance for {performance[0]?.riderName ?? drfLicense}
            </h1>
            <table style={{ borderCollapse: "collapse", width: "100%" }}>
                <thead>
                    <tr>
                        <th>Hest</th>
                        <th>Konkurrence</th>
                        <th>Start #</th>
                        <th>Start Tid</th>
                        <th>Klasse</th>
                        <th>Disciplin</th>
                        <th>Score</th>
                        <th>Fejl</th>
                        <th>Placering</th>
                    </tr>
                </thead>
                <tbody>
                    {performance.map((item, index) => (
                        <tr key={index}>
                            <td>{item.horseName}</td>
                            <td>{item.competitionName}</td>
                            <td>{item.startNumber ?? "-"}</td>
                            <td>{item.startTime ? new Date(item.startTime).toLocaleString() : "-"}</td>
                            <td>{item.classLevel}</td>
                            <td>{item.disciplineName}</td>
                            <td>{item.score ?? "-"}</td>
                            <td>{item.faults ?? "-"}</td>
                            <td>{item.placements ?? "-"}</td>
                        </tr>
                    ))}
                </tbody>
            </table>

            


           {/* Score per stævne/klasse*/}
            <div className="dashboard-grid">
                <div style={{ height: 350 }}>
                    <ResponsiveBar
                        data={scoreData}
                        keys={['score']}
                        indexBy="competitionClass"
                        margin={{ top: 50, right: 50, bottom: 100, left: 60 }}
                        padding={0.3}
                        valueScale={{ type: 'linear' }}
                        indexScale={{ type: 'band', round: true }}
                        colors={{ scheme: 'nivo' }}
                        axisBottom={{
                            tickRotation: -45,
                        }}
                        axisLeft={{ legend: 'Score', legendPosition: 'middle', legendOffset: -40 }}
                        labelsSkipWidth={12}
                        labelsSkipHeight={12}
                    />
                </div>

                {/*Fejl per hest/klasse*/}
                <div style={{ height: 350 }}>
                    <ResponsiveBar
                        data={faultsData}
                        keys={['faults']}
                        indexBy="competitionClassHorse"
                        margin={{ top: 50, right: 50, bottom: 100, left: 60 }}
                        padding={0.3}
                        colors={{ scheme: 'red_yellow_blue' }}
                        axisBottom={{
                            tickRotation: -45,
                        }}
                        axisLeft={{ legend: 'Fejl', legendPosition: 'middle', legendOffset: -40 }}
                    />
                </div>

                {/*Placering per hest/klasse*/}
                <div style={{ height: 350 }}>
                    <ResponsiveBar
                        data={placementsData}
                        keys={['placements']}
                        indexBy="competitionClassHorse"
                        margin={{ top: 50, right: 50, bottom: 100, left: 60 }}
                        valueScale={{ type: 'linear' }}
                        padding={0.3}
                        colors={{ scheme: 'greens' }}
                        axisBottom={{ tickRotation: -45 }}
                        axisLeft={{ legend: 'Placering (1 = bedste)', legendPosition: 'middle', legendOffset: -40 }}

                    />
                </div>

                {/*Gennemsnitsscore pr hest pr rytter*/}
                <div style={{ height: 350 }}>
                    <ResponsiveBar
                        data={averageScoreData}
                        keys={['averageScore']}
                        indexBy="horse"
                        margin={{ top: 50, right: 50, bottom: 100, left: 60 }}
                        padding={0.3}
                        valueScale={{ type: 'linear' }}
                        colors={{ scheme: 'greens' }}
                        axisBottom={{
                            tickRotation: -45,
                        }}
                        axisLeft={{
                            legend: 'Gennemsnitsscore',
                            legendPosition: 'middle',
                            legendOffset: -40
                        }}
                        labelSkipWidth={12}
                        labelsSkipHeight={12}
                    />
                </div>

                {/*Akummuleret score over tid*/}
                <div style={{ height: 350 }}>
                    <ResponsiveLine
                        data={accumulatedScoreData}
                        margin={{ top: 50, right: 50, bottom: 100, left: 60 }}
                        xScale={{ type: 'point' }}
                        yScale={{ type: 'linear', min: 0 }}
                        axisBottom={{ tickRotation: -45 }}
                        axisLeft={{ legend: 'Akkumuleret Score', legendPosition: 'middle', legendOffset: -40 }}
                        pointSize={10}
                        pointColor={{ theme: 'background' }}
                        pointBorderWidth={2}
                        pointBorderColor={{ from: 'serieColor' }}
                        enableGridX={false}
                        enableGridY={true}
                    />
                </div>

            </div>
        </div>
    );
}