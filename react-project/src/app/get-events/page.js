"use client";

import { useEffect, useState } from "react";
import EventFilter from "./eventfilter";

export default function EventsPage() {
    const [events, setEvents] = useState([]);
    const [loading, setLoading] = useState(true);
    const [search, setSearch] = useState("");

    useEffect(() => {
        async function fetchEvents() {
            try {
                const response = await fetch("https://localhost:7265/api/event");
                if (!response.ok) throw new Error("Fejl ved hentning af stævner");
                const data = await response.json();
                setEvents(data);
            } catch (err) {
                console.error(err);
            } finally {
                setLoading(false);
            }
        }

        fetchEvents();
    }, []);

    if (loading) return <p className="text-center mt-10 text-gray-600">Loader stævner...</p>;

    const filteredEvents = events.filter(evt =>
        evt.name.toLowerCase().includes(search.toLowerCase())
    );

    return (
        <div className="max-w-5xl mx-auto p-6">
            <h1 className="text-3xl font-bold mb-6">Liste over kommende stævner</h1>

            {/* søgefelt */}
            <EventFilter
                clubs={[...new Set(events.map(e => e.clubName).filter(Boolean))]}
                onFilter={(f) => setFilters(f)}
            />


            <div className="overflow-x-auto rounded-xl border shadow-sm">
                <table className="w-full text-left border-collapse">
                    <thead className="bg-gray-100">
                        <tr>
                            <th className="p-3">ID</th>
                            <th className="p-3">Navn</th>
                            <th className="p-3">Niveau</th>
                            <th className="p-3">Klubnavn</th>
                            <th className="p-3">Distrikt</th>
                            <th className="p-3">Start</th>
                            <th className="p-3">Slut</th>
                            <th className="p-3">Status</th>
                            <th className="p-3">Detaljer</th>
                        </tr>
                    </thead>

                    <tbody>
                        {filteredEvents.map(event => (
                            <tr key={event.id} className="border-t hover:bg-gray-50">
                                <td className="p-3">{event.id}</td>
                                <td className="p-3">{event.name}</td>
                                <td className="p-3">{event.level}</td>

                                {/* Klubnavn i stedet for clubId */}
                                <td className="p-3">
                                    {event.clubName ?? "Ukendt klub"}
                                </td>

                                {/* Distrikt (int?) */}
                                <td className="p-3">
                                    {event.clubDistrictId ?? "—"}
                                </td>

                                <td className="p-3">
                                    {new Date(event.startDate).toLocaleDateString()}
                                </td>

                                <td className="p-3">
                                    {new Date(event.endDate).toLocaleDateString()}
                                </td>

                                <td className="p-3">{event.status}</td>

                                <td className="p-3">
                                    <button
                                        onClick={() => window.location.href = `/event-details/${event.id}`}
                                        className="px-4 py-2 bg-blue-600 text-white rounded-lg hover:bg-blue-700 transition"
                                    >
                                        Se detaljer
                                    </button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        </div>
    );
}
