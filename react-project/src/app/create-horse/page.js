"use client";

import { useState } from "react";
import "./page.css";

export default function CreateHorsePage() {
    const [horseData, setHorseData] = useState({
        Name: "",
        HorseId: "",
        Height: "",
        BirthYear: ""
    });


    // Handle changes of input
    const handleChange = (e) => {
        const { name, value } = e.target;
        setHorseData({ ...horseData, [name]: value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        try {
            const response = await fetch("https://localhost:7265/api/horse", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(horseData)
            });

            if (response.ok) {
                alert("Hest oprettet!");
                setHorseData({ name: "", id: "", height: "", birthyear: "" });
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
            <h1>Opret hest</h1>
            <p>Udfyld detaljerne nedenfor for at oprette en ny hest.</p>
            <form onSubmit={handleSubmit}>
                <input type="text" name="Name" placeholder="Hestens navn" value={horseData.Name} onChange={handleChange} required />
                <input type="text" name="HorseId" placeholder="Hestens ID nummer" value={horseData.HorseId} onChange={handleChange} required />
                <input type="number" name="Height" placeholder="Højde i cm" value={horseData.Height} onChange={handleChange} required />
                <input type="number" name="BirthYear" placeholder="Hestens fødselsår" value={horseData.BirthYear} onChange={handleChange} required />

                <button type="submit">Opret Hest</button>
            </form>
        </div>
    );
}
