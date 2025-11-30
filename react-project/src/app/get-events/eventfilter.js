"use client";

import { useState } from "react";

export default function EventFilter({ clubs = [], onFilter }) {
    const disciplines = ["Spring", "Dressur"];
    const levels = ["E"]; // kan udvides senere til D, C, B, FEI, etc.
    const types = ["Hest", "Pony"];

    const districtGroups = [
        { label: "Distrikt 01-05 (Sjælland)", range: [1, 2, 3, 4, 5] },
        { label: "Distrikt 06 (Bornholm)", range: [6] },
        { label: "Distrikt 07 (Fyn)", range: [7] },
        { label: "Distrikt 08-14 (Jylland)", range: [8, 9, 10, 11, 12, 13, 14] },
    ];

    const [search, setSearch] = useState("");
    const [selectedClub, setSelectedClub] = useState("");
    const [selectedDisciplines, setSelectedDisciplines] = useState([]);
    const [selectedDistricts, setSelectedDistricts] = useState([]);

    // Sæt default dates
    const today = new Date();
    const threeMonthsLater = new Date();
    threeMonthsLater.setMonth(today.getMonth() + 3);

    // Funktion til at formatere som YYYY-MM-DD
    const formatDate = (date) => date.toISOString().split("T")[0];

    const [dateFrom, setDateFrom] = useState(formatDate(today));
    const [dateTo, setDateTo] = useState(formatDate(threeMonthsLater));
    const [isOpen, setIsOpen] = useState(false);

    // Nye filtre som checkbox-grupper
    const [selectedLevels, setSelectedLevels] = useState([...levels]); // kun E nu, valgt som default
    const [selectedTypes, setSelectedTypes] = useState([...types]); // begge valgt som default

    const toggleDiscipline = (disc) => {
        setSelectedDisciplines(prev =>
            prev.includes(disc) ? prev.filter(d => d !== disc) : [...prev, disc]
        );
    };

    const toggleDistrict = (dist) => {
        setSelectedDistricts(prev =>
            prev.includes(dist) ? prev.filter(d => d !== dist) : [...prev, dist]
        );
    };

    const toggleLevel = (level) => {
        setSelectedLevels(prev =>
            prev.includes(level) ? prev.filter(l => l !== level) : [...prev, level]
        );
    };

    const toggleType = (type) => {
        setSelectedTypes(prev =>
            prev.includes(type) ? prev.filter(t => t !== type) : [...prev, type]
        );
    };

    const handleSubmit = () => {
        if (onFilter) {
            onFilter({
                search,
                selectedClub,
                selectedDisciplines,
                selectedDistricts,
                dateFrom,
                dateTo,
                isOpen,
                selectedLevels,
                selectedTypes,
            });
        }
    };

    return (
        <div className="w-full mb-6 p-4 bg-white border border-gray-200 rounded-xl shadow-sm">
            {/* Første linje: navn, arrangør, datoer */}
            <div className="flex gap-3 flex-wrap items-end">
                <label className="sr-only" htmlFor="search">Søg stævne</label>
                <input
                    id="search"
                    type="text"
                    value={search}
                    onChange={e => setSearch(e.target.value)}
                    placeholder="Søg stævne..."
                    className="flex-1 p-2 border rounded-lg"
                />
                <label className="sr-only" htmlFor="club">Vælg klub</label>
                <select
                    id="club"
                    value={selectedClub}
                    onChange={e => setSelectedClub(e.target.value)}
                    className="w-48 p-2 border rounded-lg"
                >
                    <option value="">Alle klubber</option>
                    {clubs.map(c => <option key={c} value={c}>{c}</option>)}
                </select>
                <label className="sr-only" htmlFor="dateFrom">Fra dato</label>
                <input
                    id="dateFrom"
                    type="date"
                    value={dateFrom}
                    onChange={e => setDateFrom(e.target.value)}
                    className="p-2 border rounded-lg w-40"
                />
                <label className="sr-only" htmlFor="dateTo">Til dato</label>
                <input
                    id="dateTo"
                    type="date"
                    value={dateTo}
                    onChange={e => setDateTo(e.target.value)}
                    className="p-2 border rounded-lg w-40"
                />
            </div>

            {/* Anden linje: disciplin, distrikter, niveau, type, åbne stævner, filter */}
            <div className="flex gap-6 mt-3 flex-wrap items-start">

                {/* Type */}
                <div className="flex flex-col gap-1">
                    <span className="font-semibold text-sm">Type</span>
                    <div className="flex gap-2">
                        {types.map(t => (
                            <label key={t} className="flex items-center gap-1 cursor-pointer">
                                <input
                                    type="checkbox"
                                    checked={selectedTypes.includes(t)}
                                    onChange={() => toggleType(t)}
                                    id={`type-${t}`}
                                />
                                <span htmlFor={`type-${t}`}>{t}</span>
                            </label>
                        ))}
                    </div>
                </div>

                {/* Discipliner */}
                <div className="flex flex-col gap-1">
                    <span className="font-semibold text-sm">Discipliner</span>
                    <div className="flex gap-2">
                        {disciplines.map(d => (
                            <label key={d} className="flex items-center gap-1 cursor-pointer">
                                <input
                                    type="checkbox"
                                    checked={selectedDisciplines.includes(d)}
                                    onChange={() => toggleDiscipline(d)}
                                    id={`disc-${d}`}
                                />
                                <span htmlFor={`disc-${d}`}>{d}</span>
                            </label>
                        ))}
                    </div>
                </div>

                {/* Niveau */}
                <div className="flex flex-col gap-1">
                    <span className="font-semibold text-sm">Niveau</span>
                    <div className="flex gap-2">
                        {levels.map(l => (
                            <label key={l} className="flex items-center gap-1 cursor-pointer">
                                <input
                                    type="checkbox"
                                    checked={selectedLevels.includes(l)}
                                    onChange={() => toggleLevel(l)}
                                    id={`level-${l}`}
                                />
                                <span htmlFor={`level-${l}`}>{l}</span>
                            </label>
                        ))}
                    </div>
                </div>


                {/* Kun åbne stævner */}
                <div className="flex items-center gap-2 mt-2">
                    <input
                        type="checkbox"
                        checked={isOpen}
                        onChange={() => setIsOpen(!isOpen)}
                        id="openOnly"
                    />
                    <label htmlFor="openOnly" className="font-semibold cursor-pointer">Kun åbne stævner</label>
                </div>

                {/* Distrikter */}
                <div className="flex flex-col gap-1">
                    <span className="font-semibold text-sm">Distrikter</span>
                    <div className="flex flex-wrap gap-4">
                        {districtGroups.map(group => (
                            <div key={group.label} className="flex flex-col gap-1">
                                <span className="text-xs">{group.label}</span>
                                <div className="flex gap-1 flex-wrap">
                                    {group.range.map(d => (
                                        <label key={d} className="flex items-center gap-1 w-8 cursor-pointer">
                                            <input
                                                type="checkbox"
                                                checked={selectedDistricts.includes(d)}
                                                onChange={() => toggleDistrict(d)}
                                                id={`dist-${d}`}
                                            />
                                            <span htmlFor={`dist-${d}`}>{d}</span>
                                        </label>
                                    ))}
                                </div>
                            </div>
                        ))}
                    </div>
                </div>

               

                {/* Filtrer-knap */}
                <div className="ml-auto mt-2">
                    <button
                        onClick={handleSubmit}
                        className="px-4 py-2 bg-blue-600 text-white rounded-lg hover:bg-blue-700 transition"
                    >
                        Filtrer
                    </button>
                </div>
            </div>
        </div>
    );
}


