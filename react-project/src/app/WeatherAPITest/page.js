'use client';

import { useEffect, useState } from 'react';

export default function WeatherPage() {
    const [weather, setWeather] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        fetch('https://localhost:7265/WeatherForecast') // Din backend URL
            .then((res) => {
                if (!res.ok) throw new Error('Network response was not ok');
                return res.json();
            })
            .then((data) => {
                setWeather(data);
                setLoading(false);
            })
            .catch((err) => {
                setError(err.message);
                setLoading(false);
            });
    }, []);

    if (loading) return <p className="p-4">Loading...</p>;
    if (error) return <p className="p-4 text-red-500">Error: {error}</p>;

    return (
        <div className="p-4">
            <h1 className="text-2xl font-bold mb-4">Weather Forecast</h1>
            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
                {weather.map((item, index) => (
                    <div
                        key={index}
                        className="border rounded p-4 shadow hover:shadow-lg transition"
                    >
                        <p className="font-semibold">{item.date}</p>
                        <p>Temp: {item.temperatureC}°C</p>
                        <p>Summary: {item.summary}</p>
                    </div>
                ))}
            </div>
        </div>
    );
}