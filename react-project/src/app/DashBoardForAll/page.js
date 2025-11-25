'use client';
import { useEffect, useState } from "react";
import { ResponsiveBar } from '@nivo/bar';
import { ResponsiveScatterPlot } from '@nivo/scatterplot';

export default function Dashboard() {
    const [topRiders, setTopRiders] = useState([]);
    const [riderConsistency, setRiderConsistency] = useState([]);
    const [riderHorse, setRiderHorse] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");

    useEffect(() => {
        const fetchData = async () => {
            try {
                // TopRiders
                const topRidersRes = await fetch('https://localhost:7265/api/dashboard/topriders/2025?top=10');
                if (!topRidersRes.ok) throw new Error('Fejl ved hentning af top-ryttere');
                const topRidersJson = await topRidersRes.json();
                setTopRiders(topRidersJson);

                // RiderConsistency
                const consistencyRes = await fetch('https://localhost:7265/api/dashboard/riderconsistency');
                if (!consistencyRes.ok) throw new Error('Fejl ved hentning af rider consistency');
                const consistencyJson = await consistencyRes.json();
                setRiderConsistency(consistencyJson);

                // Rider-Horse Performance
                const riderHorseresponse = await fetch('https://localhost:7265/api/dashboard/RiderHorsePerformance');
                if (!riderHorseresponse.ok) throw new Error('Fejl ved hentning af rider-horse performance');
                const riderHorsejson = await riderHorseresponse.json();
                setRiderHorse(riderHorsejson);


            } catch (err) {
                setError(err.message);
            } finally {
                setLoading(false);
            }
        };

        fetchData();
    }, []);

    if (loading) return <p>Loading...</p>;
    if (error) return <p>Error: {error}</p>;
    if (topRiders.length === 0 && riderConsistency.length === 0) return <p>Ingen data fundet</p>;

    // Nivo Bar data
    const topRidersData = topRiders.map(r => ({
        rider: r.riderName,
        averageScore: r.averageScore
    }));

    // ScatterPlot data
    const scatterData = [
        {
            id: "Ryttere",
            data: riderConsistency.map(r => ({
                x: r.averageScore,
                y: r.consistency,
                label: r.riderName
            }))
        }
    ];

    const scatterDataRiderHorse = [
        {
            id: "Ryttere-Heste",
            data: riderHorse.map(r => ({
                x: r.avgScore,
                y: r.diffFromHorseAvg,
                label: `${r.riderName} / ${r.horseName}`
            }))
        }
    ];


    return (
        <div style={{ padding: '20px' }}>
            {/* TopRiders Bar Chart */}
            <div style={{ height: 400 }}>
                <h2 style={{ textAlign: 'center' }}>Top 10 Ryttere – Gennemsnitsscore</h2>
                <ResponsiveBar
                    data={topRidersData}
                    keys={['averageScore']}
                    indexBy="rider"
                    margin={{ top: 50, right: 50, bottom: 100, left: 60 }}
                    padding={0.3}
                    valueScale={{ type: 'linear' }}
                    colors={{ scheme: 'greens' }}
                    axisBottom={{ tickRotation: -45 }}
                    axisLeft={{ legend: 'Gennemsnitsscore', legendPosition: 'middle', legendOffset: -40 }}
                    labelSkipWidth={12}
                    labelSkipHeight={12}
                    valueFormat={v => Number(v).toFixed(2)}

                />
            </div>

            {/* Rider Consistency ScatterPlot */}
            <div style={{ height: 500, marginTop: 50 }}>
                <h2 style={{ textAlign: 'center' }}>Rytter Konsistens – Gennemsnit vs Stabilitet</h2>
                <ResponsiveScatterPlot
                    data={scatterData}
                    margin={{ top: 60, right: 140, bottom: 70, left: 90 }}
                    xScale={{ type: 'linear', min: 60, max: 80 }}
                    yScale={{ type: 'linear', min: 0 }}
                    axisBottom={{ legend: 'Gennemsnitsscore', legendPosition: 'middle', legendOffset: 46 }}
                    axisLeft={{ legend: 'Konsistens (STD)', legendPosition: 'middle', legendOffset: -60 }}
                    colors={{ scheme: 'paired' }}
                    nodeSize={10}
                    tooltip={({ node }) => (
                        <strong>{node.data.label}: Score={node.data.x?.toFixed(2)}, STD={node.data.y?.toFixed(2)}</strong>
                    )}
                />
            </div>

            {/* Rider Consistency Table */}
            <div style={{ marginTop: 50 }}>
                <h2 style={{ textAlign: 'center' }}>Rytter Konsistens – Tabel</h2>
                <table border="1" style={{ width: '100%', textAlign: 'center', borderCollapse: 'collapse' }}>
                    <thead>
                        <tr>
                            <th>Rytter</th>
                            <th>Gennemsnitsscore</th>
                            <th>Konsistens (STD)</th>
                        </tr>
                    </thead>
                    <tbody>
                        {riderConsistency.map(r => (
                            <tr key={r.riderName}>
                                <td>{r.riderName}</td>
                                <td>{r.averageScore?.toFixed(2)} </td>
                                <td>{r.consistency?.toFixed(2) ?? '0.00'} </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>

            {/* Rytter-Hest performarnce. Hvilken rytter har det højeste gnmsnt på hvilken hest? */}
            <div style={{ height: 500, marginTop: 50 }}>
                <h2 style={{ textAlign: 'center' }}>Rytter-Hest performarnce. Hvilken rytter har det højeste gennemsnit på hvilken hest?</h2>
                <ResponsiveScatterPlot
                    data={scatterDataRiderHorse}
                    margin={{ top: 60, right: 140, bottom: 70, left: 90 }}
                    xScale={{ type: 'linear', min: 60 }}
                    yScale={{ type: 'linear' }}
                    axisBottom={{ legend: 'Gennemsnitsscore', legendPosition: 'middle', legendOffset: 46 }}
                    axisLeft={{ legend: 'Diff fra hestens gennemsnit', legendPosition: 'middle', legendOffset: -60 }}
                    colors={{ scheme: 'paired' }}
                    nodeSize={10}
                    tooltip={({ node }) => (
                        <strong>{node.data.label}: Avg={node.data.x.toFixed(2)}, Diff={node.data.y.toFixed(2)}</strong>
                    )}
                    />
            </div>

            <div style={{ marginTop: 50 }}>
                <h2 style={{ textAlign: 'center' }}>Rytter-Hest Performance – Tabel</h2>
                    <table border="1" style={{ width: '100%', textAlign: 'center', borderCollapse: 'collapse' }}>
                    <thead>
                        <tr>
                            <th>Rytter</th>
                            <th>Hest</th>
                            <th>Gennemsnitsscore</th>
                            <th>Diff fra hestens gennemsnit</th>
                        </tr>
                    </thead>
                    <tbody>
                        {riderHorse.map(r => (
                            <tr key={`${r.riderName}-${r.horseName}`}>
                                <td>{r.riderName}</td>
                                <td>{r.horseName}</td>
                                <td>{r.avgScore?.toFixed(2)}</td>
                                <td>{r.diffFromHorseAvg?.toFixed(2)}</td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>

        </div>
    );
}
