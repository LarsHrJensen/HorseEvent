'use client';
import { useEffect, useState } from "react";
import './page.css';
import { ResponsiveBar } from '@nivo/bar'; 

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

    return (
        <div>
            <h2>Rytter Performance</h2>
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

                <div style={{ height: 350 }}>
                    <ResponsiveBar
                        data={faultsData}
                        keys={['faults']}
                        indexBy="competitionClassHorse"
                        margin={{ top: 50, right: 50, bottom: 100, left: 60 }}
                        padding={0.3}
                        colers={{ scheme: 'red_yellow_blue' }}
                        axisBottom={{
                            tickRotation: -45,
                        }}
                        axisLeft={{ legend: 'Fejl', legendPosition: 'middle', legendOffset: -40 }}
                    />
                </div>

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
            </div>

        </div>
    );
}