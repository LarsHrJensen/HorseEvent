"use client";

import { useEffect, useState } from "react";
import { useRouter } from "next/navigation";
import "./page.css";

export default function HorseListPage() {
    const [horses, setHorses] = useState([]);
    const [races, setRaces] = useState([]);
    const [loading, setLoading] = useState(true);
    const router = useRouter();

    useEffect(() => {
        async function fetchHorses() {
            try {
                const response = await fetch("https://localhost:7265/api/horse");
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

        async function fetchRaces() {
            try {
                const response = await fetch("https://localhost:7265/api/horseBreed");
                if (!response.ok) throw new Error("Fejl ved hentning af racer");
                const data = await response.json();
                setRaces(data);
            } catch (err) {
                console.error(err);
            }
        }

        fetchHorses();
        fetchRaces();
    }, []);

    if (loading) return <p>Loader heste...</p>;

    return (
        <div className="createHorseContainer">
            <div className="topBar" style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '1.5rem' }}>
                <h1>Liste over heste</h1>
                <button className="createHorseButton" onClick={() => router.push("/create-horse")}>Opret hest</button>
            </div>

            <div className="searchCriteriaBox" style={{ marginBottom: '2rem' }}>
                <h3>Søgekriterier</h3>
                <div className="form-grid">
                    <input type="text" placeholder="Navn" name="name" />
                    <input type="text" placeholder="UELN" name="ueln" />
                    <input type="number" placeholder="Fødselsår" name="birthYear" />
                    <select name="race">
                        <option value="">--Vælg race--</option>
                        {races.map((race) => (
                            <option key={race.id} value={race.name}>{race.name}</option>
                        ))}
                    </select>
                    <button className="searchButton" style={{ gridColumn: 'span 2', justifySelf: 'end' }}>Søg</button>
                </div>
            </div>

            <table>
                <thead>
                    <tr>
                        <th style={{ textAlign: 'left' }}>Navn(UELN)</th>
                        <th style={{ textAlign: 'left' }}>Fødselsår</th> 
                        <th style={{ textAlign: 'left' }}>Kategori</th>
                        <th style={{ textAlign: 'left' }}>Avlsforbund</th>
                    </tr>
                </thead>
                <tbody>
                    {horses.map((horse) => (
                        <tr key={horse.id}>
                            <td style={{ textAlign: 'left' }}>{horse.name} ({horse.ueln})</td>
                            <td style={{ textAlign: 'left' }}>{horse.birthYear}</td>
                            <td style={{ textAlign: 'left' }}>{horse.category}</td>
                            <td style={{ textAlign: 'left' }}>{horse.breedName ?? "Ukendt"}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}