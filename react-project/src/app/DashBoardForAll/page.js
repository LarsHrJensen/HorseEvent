'use client';
import { useEffect, useState } from "react";
import { ResponsiveBar } from '@nivo/bar';

export default function TopRidersChart() {
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");

    useEffect(() => {
        const fetchTopRiders = async () => {
            try {
                const response = await fetch('https://localhost:7265/api/dashboard/TopRiders/2025?top=10');
                if (!response.ok) throw new Error('Fejl ved hentning af top-ryttere');
                const json = await response.json();
                setData(json);
            } catch (err) {
                setError(err.message);
            } finally {
                setLoading(false);
            }
        };

        fetchTopRiders();
    }, []);

    if (loading) return <p>Loading...</p>;
    if (error) return <p>Error: {error}</p>;
    if (data.length === 0) return <p>Ingen data fundet</p>;

    // Til Nivo Bar: omsæt data til {riderName, averageScore}
    const chartData = data.map(r => ({
        rider: r.riderName,
        averageScore: r.averageScore
    }));

    return (
        <div style={{ height: 400 }}>
            <h1 style={{ textAlign: 'center' }}>Top 10 Ryttere 2025</h1>
            <ResponsiveBar
                data={chartData}
                keys={['averageScore']}
                indexBy="rider"
                margin={{ top: 50, right: 50, bottom: 100, left: 60 }}
                padding={0.3}
                valueScale={{ type: 'linear' }}
                colors={{ scheme: 'greens' }}
                axisBottom={{
                    tickRotation: -45
                }}
                axisLeft={{
                    legend: 'Gennemsnitsscore',
                    legendPosition: 'middle',
                    legendOffset: -40
                }}
                labelSkipWidth={12}
                labelSkipHeight={12}
                valueFormat={v => Number(v).toFixed(2)}
            />
        </div>
    );
}
