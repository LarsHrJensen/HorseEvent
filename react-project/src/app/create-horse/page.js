"use client";

import { useState } from "react";
import "./page.css";

export default function CreateHorsePage() {
    // Hardcoded values – kan senere hentes fra API/databasen
    const breeds = ["DV", "Oldenborg", "Fjordhest", "Hannoveraner", "Arabian"];
    const existingHorses = ["Chess", "Flipper", "Marjolein", "Sander", "Onslow", "The Flying Dutchmann"];

    const [horseData, setHorseData] = useState({
        Name: "",
        HorseId: "",
        Height: "",
        BirthYear: "",
        Gender: "",
        Color: "",
        Breed: "",
        Breeder: "",
        Sire: "",
        Dam: ""
    });

    const [showOptional, setShowOptional] = useState(false);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setHorseData({ ...horseData, [name]: value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        try {
            const response = await fetch("https://localhost:7265/api/horse", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(horseData)
            });

            if (response.ok) {
                alert("Hest oprettet!");
                setHorseData({
                    Name: "",
                    HorseId: "",
                    Height: "",
                    BirthYear: "",
                    Gender: "",
                    Color: "",
                    Breed: "",
                    Breeder: "",
                    Sire: "",
                    Dam: ""
                });
            } else {
                alert("Der opstod en fejl.");
            }
        } catch (error) {
            console.error(error);
            alert("Something went wrong, womp womp");
        }
    };

    return (
        <div className="createHorseContainer">
            <h1>Opret konkurrencehest/pony</h1>
            <p>Udfyld detaljerne nedenfor for at oprette en ny hest.</p>

            <form onSubmit={handleSubmit}>

                {/* Obligatoriske felter */}
                <div className="form-section">
                    <h2>Obligatoriske oplysninger</h2>
                    <div className="form-grid">
                        <label>Navn:</label>
                        <input type="text" name="Name" value={horseData.Name} onChange={handleChange} required />

                        <label>Horse ID:</label>
                        <input type="text" name="HorseId" value={horseData.HorseId} onChange={handleChange} required />

                        <label>Højde:</label>
                        <input type="number" name="Height" value={horseData.Height} onChange={handleChange} required />

                        <label>Fødselsår:</label>
                        <input type="number" name="BirthYear" value={horseData.BirthYear} onChange={handleChange} required />

                        <label>Køn:</label>
                        <select name="Gender" value={horseData.Gender} onChange={handleChange} required>
                            <option value="">Vælg køn</option>
                            <option value="Hoppe">Hoppe</option>
                            <option value="Vallak">Vallak</option>
                            <option value="Hingst">Hingst</option>
                        </select>
                    </div>
                </div>

                {/* Toggle frivillige felter */}
                <button type="button" className="toggle-button" onClick={() => setShowOptional(!showOptional)}>
                    {showOptional ? "Skjul frivillige oplysninger" : "Udfyld frivillige oplysninger"}
                </button>

                {/* Frivillige felter */}
                {showOptional && (
                    <div className="form-section">
                        <h2>Frivillige oplysninger</h2>
                        <div className="form-grid">
                            <label>Farve:</label>
                            <input type="text" name="Color" value={horseData.Color} onChange={handleChange} />

                            <label>Race / Avlsforbund:</label>
                            <select name="Breed" value={horseData.Breed} onChange={handleChange}>
                                <option value="">Vælg race</option>
                                {breeds.map((b) => <option key={b} value={b}>{b}</option>)}
                            </select>

                            <label>Far:</label>
                            <select name="Sire" value={horseData.Sire} onChange={handleChange}>
                                <option value="">Vælg far</option>
                                {existingHorses.map((h) => <option key={h} value={h}>{h}</option>)}
                            </select>

                            <label>Mor:</label>
                            <select name="Dam" value={horseData.Dam} onChange={handleChange}>
                                <option value="">Vælg mor</option>
                                {existingHorses.map((h) => <option key={h} value={h}>{h}</option>)}
                            </select>

                            <label>Avler:</label>
                            <input type="text" name="Breeder" value={horseData.Breeder} onChange={handleChange} />
                        </div>
                    </div>
                )}

                <button type="submit" className="submit-button">Opret Hest</button>
            </form>
        </div>
    );
}
