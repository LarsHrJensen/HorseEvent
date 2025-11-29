"use client";
import { useState, useEffect } from "react";
import SearchableSelect from "../../components/SearchableSelect";

function groupClassesByDate(classes) {
    const groups = {};
    classes.forEach(c => {
        const dateKey = new Date(c.date).toLocaleDateString("da-DK");
        if (!groups[dateKey]) groups[dateKey] = [];
        groups[dateKey].push(c);
    });
    return Object.entries(groups).sort((a, b) => new Date(a[0]) - new Date(b[0]));
}

export default function EventProfilePage({ params }) {
    const { eventId } = params;

    const [event, setEvent] = useState(null);
    const [eventClub, setEventClub] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    const [formData, setFormData] = useState({
        horseId: "",
        riderId: "",
        clubId: "",
        comment: ""
    });

    const [horses, setHorses] = useState([]);
    const [riders, setRiders] = useState([]);
    const [clubs, setClubs] = useState([]);

    useEffect(() => {
        if (!eventId) return;
        const controller = new AbortController();

        async function fetchData() {
            try {
                // Hent event
                const eventRes = await fetch(`https://localhost:7265/api/event/${eventId}`, { signal: controller.signal });
                if (!eventRes.ok) throw new Error(`Failed to fetch event: ${eventRes.status}`);
                const eventData = await eventRes.json();
                setEvent(eventData);

                // Hent arrangørklub baseret på event.clubId
                if (eventData.clubId) {
                    const clubRes = await fetch(`https://localhost:7265/api/club/${eventData.clubId}`);
                    if (!clubRes.ok) throw new Error(`Failed to fetch club: ${clubRes.status}`);
                    const clubData = await clubRes.json();
                    setEventClub(clubData);
                }

                // Hent heste
                const horsesRes = await fetch("https://localhost:7265/api/horse");
                if (!horsesRes.ok) throw new Error("Fejl ved hentning af heste");
                const horsesData = await horsesRes.json();
                setHorses(horsesData);

                // Hent ryttere
                const ridersRes = await fetch("https://localhost:7265/api/rider");
                if (!ridersRes.ok) throw new Error("Fejl ved hentning af ryttere");
                const ridersData = await ridersRes.json();
                setRiders(ridersData);

                // Hent klubber
                const clubsRes = await fetch("https://localhost:7265/api/club");
                if (!clubsRes.ok) throw new Error("Fejl ved hentning af klubber");
                const clubsData = await clubsRes.json();
                setClubs(clubsData);

            } catch (err) {
                if (err.name !== "AbortError") setError(err.message);
            } finally {
                setLoading(false);
            }
        }

        fetchData();

        return () => controller.abort();
    }, [eventId]);

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setFormData(prev => ({ ...prev, [name]: value }));
    };

    const handleAddToClass = async (classId) => {
        if (!formData.horseId || !formData.riderId) {
            alert("Vælg først hest og rytter");
            return;
        }

        try {
            const res = await fetch(`https://localhost:7265/api/classes/${classId}/signup`, {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(formData)
            });
            if (!res.ok) throw new Error("Kunne ikke tilmelde ekvipagen");
            alert(`Ekvipagen er tilmeldt klassen!`);
        } catch (err) {
            alert(err.message);
        }
    };

    if (loading) return <div>Loading...</div>;
    if (error) return <div>Error: {error}</div>;
    if (!event) return <div>Event not found.</div>;

    const classGroups = groupClassesByDate(event.classes || []);

    return (
        <div className="min-h-screen bg-gray-100 p-0">
            {/* TOPBAR */}
            <div className="w-full bg-white shadow p-6 mb-6">
                <h1 className="text-3xl font-bold">{event.name} ({event.level})</h1>

                <div className="mt-2 text-gray-700">

                    <div className="w-full bg-white shadow p-6 mb-6 flex items-start gap-6">
                        <div className="mt-3 text-sm">
                            <div>
                                <strong>Dato:</strong>{" "}
                                {new Date(event.startDate).toLocaleDateString("da-DK") === new Date(event.endDate).toLocaleDateString("da-DK")
                                    ? new Date(event.startDate).toLocaleDateString("da-DK")
                                    : `${new Date(event.startDate).toLocaleDateString("da-DK")} til ${new Date(event.endDate).toLocaleDateString("da-DK")}`}
                            </div>
                            <div><strong>Tilmeldingsfrist:</strong> {new Date(event.entryDeadline).toLocaleDateString("da-DK")}</div>
                            <div>
                                <strong>Status:</strong>{" "}
                                {event.isOpen ? (
                                    <span className="text-green-600 font-semibold">Åben for tilmelding</span>
                                ) : (
                                    <span className="text-red-600 font-semibold">Lukket</span>
                                )}
                            </div>
                        </div>

                        {/* Klublogo placeholder */}
                        <div className="w-32 h-32 flex items-center justify-center border rounded bg-gray-100">
                            <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" strokeWidth={1.5} stroke="gray" className="w-16 h-16 opacity-60">
                                <path strokeLinecap="round" strokeLinejoin="round" d="M3 21h18M4 15l5-9 4 6 2-3 5 6" />
                            </svg>
                        </div>

                        {/* Info-tekst til højre */}
                        <div className="flex-1">
                            <div><strong>Arrangør:</strong> {eventClub?.name || "Ikke angivet"}</div>
                            <div><strong>Distrikt:</strong> {eventClub?.districtId || "Ikke angivet"}</div>
                            <div>
                                <strong>Adresse:</strong>{" "}
                                {eventClub?.address
                                    ? `${eventClub.address.streetName} ${eventClub.address.streetNumber}, ${eventClub.address.postalCode}, ${eventClub.address.city}`
                                    : "Ikke angivet"}
                            </div>
                        </div>
                    </div>

                </div>
            </div>

            <div className="p-6 flex gap-6">
                {/* SIDEBAR */}
                <div className="w-80 p-4 border rounded bg-white sticky top-4 space-y-4 h-fit">
                    <h3 className="font-semibold">Vælg ekvipage</h3>

                    <SearchableSelect label="Vælg hest" name="horseId" value={formData.horseId} onChange={handleInputChange} options={horses || []} />
                    <SearchableSelect label="Vælg rytter" name="riderId" value={formData.riderId} onChange={handleInputChange} options={riders || []} />
                    <SearchableSelect
                        label="Vælg klub"
                        name="clubId"
                        value={formData.clubId}
                        onChange={handleInputChange}
                        options={clubs || []}
                        idKey="clubId"
                    />

                    <textarea name="comment" value={formData.comment} onChange={handleInputChange} placeholder="Kommentar" className="w-full border rounded p-2" />
                </div>

                {/* HOVEDOMRÅDE */}
                <div className="flex-1 space-y-6">
                    {classGroups.map(([date, classList]) => (
                        <div key={date} className="space-y-3">
                            <h2 className="text-xl font-semibold border-b pb-1">{date}</h2>

                            {classList.map(c => (
                                <div key={c.id} className="border rounded p-4 bg-white flex justify-between items-center">
                                    <div>
                                        <div className="font-medium">{c.name} ({c.disciplineName})</div>
                                        <div className="text-sm text-gray-600">Level: {c.classLevelName} • Pris: {c.price} DKK</div>
                                    </div>
                                    <button onClick={() => handleAddToClass(c.id)} className="px-3 py-2 bg-blue-600 text-white rounded hover:bg-blue-700">
                                        Tilføj ekvipage til klasse
                                    </button>
                                </div>
                            ))}
                        </div>
                    ))}

                    {/* BEMÆRKNINGER */}
                    <div className="mt-10 p-4 border rounded bg-white">
                        <h3 className="font-semibold mb-1">Bemærkninger</h3>
                        <p className="text-sm text-gray-700">
                            Her kan du evt. vise regler, kontaktinfo, generelle noter om stævnet eller lignende.
                        </p>
                    </div>
                </div>
            </div>
        </div>
    );
}

