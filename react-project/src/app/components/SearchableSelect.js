"use client";
import { useState, useEffect } from "react";

export default function SearchableSelect({ label, name, value, onChange, options = [], idKey = "id" }) {
    const [query, setQuery] = useState("");
    const [open, setOpen] = useState(false);

    // Find den valgte option baseret på value
    const selected = options.find(i => i[idKey] == value); // Brug == så type ikke blokerer

    // Synk query med selected
    useEffect(() => {
        if (selected && selected.name !== query) {
            setQuery(selected.name);
        }
        if (!selected && query !== "") {
            setQuery("");
        }
    }, [selected]);

    const filtered = options.filter(i =>
        i.name.toLowerCase().startsWith(query.toLowerCase())
    );

    const handleSelect = (item) => {
        onChange({ target: { name, value: item[idKey] } });
        setQuery(item.name);
        setOpen(false);
    };

    const handleInputChange = (e) => {
        const val = e.target.value;
        setQuery(val);
        setOpen(true);
        if (val === "") {
            onChange({ target: { name, value: null } });
        }
    };

    return (
        <div className="relative space-y-1">
            <label className="block text-sm font-medium">{label}</label>
            <input
                type="text"
                value={query}
                onChange={handleInputChange}
                onFocus={() => setOpen(true)}
                placeholder={`Søg ${label.toLowerCase()}...`}
                className="w-full border rounded p-2"
            />

            {open && (
                <div className="absolute left-0 right-0 mt-1 bg-white border rounded shadow z-10 max-h-40 overflow-y-auto">
                    {filtered.length > 0 ? (
                        filtered.map(item => (
                            <div
                                key={item[idKey]}
                                onClick={() => handleSelect(item)}
                                className="p-2 hover:bg-gray-100 cursor-pointer"
                            >
                                {item.name}
                            </div>
                        ))
                    ) : (
                        <div className="p-2 text-gray-500">Ingen resultater</div>
                    )}
                </div>
            )}
        </div>
    );
}