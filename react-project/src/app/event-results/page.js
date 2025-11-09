'use client'

import { useEffect, useState } from "react";
import "./page.css"


export default function ResultsPage() {
    const [results, setResults] = useState ([]);

    useEffect (() => {
        fetch("link")
        .then((res) => res.json())
        .then((data) => setResults(data))
        .catch((err) => console.error("Error fecting results>", err));

        // TEMP DATA for now (so it displays before backend is live)
        setResults([
            {
                id: 1,
                name: "Mie Pedersen",
                horse: {
                    name:"BILLESKÆRS MONIQUE",
                    category: "1",
                    gender: "Hoppe",
                    pedigree: "LU",
                    color: "brun",
                    birthYear: "2018",
                    father: "VARIETE ECETERA",
                    mothersFather: "METTERNICH",
                    breeder: "Karin & Svend Erik Høgh Ra"
                },
                program:"DRF LD1 - B 2025",
                total: 85.234,
            },

            {
                id: 2,
        name: "Anna Frederiksen",
        horse: {
            name: "HEJNINGES BELLA",
            category: "1",
            gender: "Hoppe",
            pedigree: "SP",
            color: "brun",
            birthYear: "2021",
            father: "MISTER MALTHE",
            mothersFather: "KLOOSTER'S ELTINO",
            breeder: "Rikke Pedersen"
                },
                program:"DRF LD1 - B 2025",
                total: 85.234,
            },

            {
                id: 3,
                name: "Simon Lund Nielsen",
                horse: {
                    name: "MIRANDA",
                    category: "3",
                    gender: "Hoppe",
                    pedigree: "SP",
                    color: "brun",
                    birthYear: "2008",
                    father: "MISTER MALTHE",
                    mothersFather: "UNKNOWN",
                    breeder: "N/A"
                    },
                program: "DRF LD1 - B 2025",
                total: 82.456,
            },

            {
                id: 4,
                name: "Andrea Baunsgaard",
                horse: {
                    name: "BOUNTY",
                    category: "3",
                    gender: "Vallak",
                    pedigree: "SP",
                    color: "brun",
                    birthYear: "2002",
                    father: "UNKNOWN",
                    mothersFather: "UNKNOWN",
                    breeder: "N/A"
                    },
                program: "DRF LD1 - B 2025",
                total: 81.723,
            },
        ]);
    }, []);

    return(
        <main className="results-page">
            {/* HEADER */}
            <section className="event-header">
                <h1> *Navnet på stævnet* </h1>
                <div className="event-details">
                    <div><span className="icon"> 📍 </span> *Placering på stævnet* </div>
                    <div><span className="icon"> 📅 </span> *Dato for stævnet* </div>
                    <div><span className="icon"> 🏆 </span> * Stævne type</div>
                </div>
            </section>

            {/* Result Section */}
            <section className="results-section">
                {results.length > 0 ? (
                    results.map((rider, index) => {
                        let rankClass = "";
                        if (index === 0) rankClass = "rank-1";
                        else if (index === 1) rankClass = "rank-2"
                        else if (index === 2) rankClass = "rank-3"

                        return (
                            <div key={rider.id} className={`result-card ${rankClass}`}>
                                <div className="left-column">
                                    <h2> {rider.name} </h2>
                                    <p className="horse-name"> {rider.horse.name} ({rider.horse.category}) </p>
                                    <p className="horse-info">
                                        {rider.horse.gender} | {rider.horse.pedigree} | {rider.horse.birthYear} |
                                        {rider.horse.father} x {rider.horse.mothersFather} | {rider.horse.breeder}
                                    </p>
                                </div>

                                <div className="right-column">
                                    <div className="program-info"> {rider.program} </div>
                                    <div className="scores">
                                        <span> Total </span>
                                        <strong> {rider.total} </strong>
                                    </div>
                                </div>
                            </div>
                        );
                    })
                ) : (
                        <p className="loading"> Loading results... </p>
                )}
            </section>
        </main>
    );
}