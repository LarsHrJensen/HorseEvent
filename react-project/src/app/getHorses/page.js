"use client";

import { useEffect, useState } from "react";
import "./page.css";

export default function HorseListPage() {
    const [horses, setHorses] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        async function fetchHorses() {
            try {
                const response = await fetch("https://localhost:7265/api/horse"); // Din API
                if (!response.ok) throw new Error("Fejl ved hentning af heste");
                const data = await response.json();
                setHorses(data);
            } catch (err) {
                console.error(err);
                alert("Der opstod en fejl ved hentning af heste");
            } finally {
                setLoading(false);
            }
        }

        fetchHorses();
    }, []);

    if (loading) return <p>Loader heste...</p>;

    return (
        <div className="createHorseContainer">
            <h1>Liste over heste</h1>
            <table>
                <thead>
                    <tr>
                        <th>Navn</th>
                        <th>ID</th>
                        <th>Højde (cm)</th>
                        <th>Fødselsår</th>
                    </tr>
                </thead>
                <tbody>
                    {horses.map((horse) => (
                        <tr key={horse.horseId}>
                            <td>{horse.name}</td>
                            <td>{horse.horseId}</td>
                            <td>{horse.height}</td>
                            <td>{horse.birthYear}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}
