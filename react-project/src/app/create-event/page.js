"use client";
import { useState, useEffect } from "react";

export default function CreateEventPage() {

    const [clubs, setClubs] = useState([]);
    const [loadingClubs, setLoadingClubs] = useState(true);

    useEffect(() => {
        async function fetchClubs() {
            try {
                const response = await fetch("https://localhost:7265/api/club");
                if (!response.ok) throw new Error("Network response was not ok");
                const data = await response.json();
                setClubs(data);
            } catch (error) {
                console.error("Error fetching clubs:", error);
            } finally {
                setLoadingClubs(false);
            }
        }
        fetchClubs();
    }, []);

    const [disciplines, setDisciplines] = useState([]);
    const [loadingDisciplines, setLoadingDisciplines] = useState(true);

    useEffect(() => {
        async function fetchDisciplines() {
            try {
                const response = await fetch("https://localhost:7265/api/disciplin");
                if (!response.ok) throw new Error("Network response was not ok");
                const data = await response.json();
                setDisciplines(data);
            } catch (error) {
                console.error("Error fetching disciplines:", error);
            } finally {
                setLoadingDisciplines(false);
            }
        }
        fetchDisciplines();
    }, []);

    const [classLevels, setClassLevels] = useState([]);
    const [loadingClassLevels, setLoadingClassLevels] = useState(true);

    useEffect(() => {
        async function fetchClassLevels() {
            try {
                const response = await fetch("https://localhost:7265/api/classlevel");
                if (!response.ok) throw new Error("Network response was not ok");
                const data = await response.json();
                setClassLevels(data);
            } catch (error) {
                console.error("Error fetching disciplinesLevels:", error);
            } finally {
                setLoadingClassLevels(false);
            }
        }
        fetchClassLevels();
        
    }, []);


    // Event-level: only level E allowed now
    const eventLevels = ["E"];


    const statuses = [
        { id: "draft", label: "Draft" },
        { id: "open", label: "Open for entries" },
        { id: "closed", label: "Closed" },
        { id: "cancelled", label: "Cancelled" },
    ];

    // Event state
    const [event, setEvent] = useState({
        name: "",
        clubId: 0,
        level: eventLevels[0],
        startDate: "",
        endDate: "",
        entryDeadline: "",
        status: "draft",
    });

    // Classes state
    const [classes, setClasses] = useState([]);
    const [adding, setAdding] = useState(false);

    // Temp class form
    const emptyClass = {
        id: null,
        name: "",
        level: "E",
        discipline: null,
        classLevel: null,
        date: "",
        price: "",
        maxParticipants: "",
    };

    const [classForm, setClassForm] = useState(emptyClass);

    // Handlers
    const updateEventField = (field, value) =>
        setEvent((s) => ({ ...s, [field]: value }));

    const startAddClass = () => {
        setClassForm({ ...emptyClass, id: Date.now().toString() });
        setAdding(true);
    };

    const cancelAdd = () => {
        setClassForm(emptyClass);
        setAdding(false);
    };

    const saveClass = () => {
        // basic validation
        if (!classForm.name || !classForm.date || !classForm.price) {
            alert("Please fill name, date and price for the class.");
            return;
        }
        setClasses((c) => [...c, classForm]);
        setClassForm(emptyClass);
        setAdding(false);
    };

    const removeClass = (id) => setClasses((c) => c.filter((x) => x.id !== id));

    const handleDisciplineChange = (val) => {
        const levelsForDiscipline = classLevels.filter(cl => cl.disciplineId === val);
        setClassForm((s) => ({
            ...s,
            discipline: val,
            classLevel: levelsForDiscipline.length > 0 ? levelsForDiscipline[0].id : null,
        }));
    };

    const saveEvent = async () => {
        // Basic validation
        if (!event.name) return alert("Please give the event a name.");
        if (!event.startDate || !event.endDate) return alert("Please set start and end dates.");
        if (!event.clubId || event.clubId==0) return alert("Please select a club.");
        if (new Date(event.startDate) > new Date(event.endDate))
            return alert("Start date must be before end date.");

        if (classes.length === 0) return alert("Please add at least one class.");

        // Byg payload
        const payload = {
            ...event,
            clubId: Number(event.clubId), // <--- sikrer int
            classes: classes.map(c => ({
                ...c,
                discipline: Number(c.discipline),
                classLevel: Number(c.classLevel),
                price: Number(c.price),
                maxParticipants: c.maxParticipants ? Number(c.maxParticipants) : null
            }))
        };

        try {
            const response = await fetch("https://localhost:7265/api/event", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(payload),
            });

            if (!response.ok) {
                const errorText = await response.text();
                throw new Error(errorText || "Failed to save event");
            }

            const result = await response.json();
            alert(`Event saved successfully! Event ID: ${result.id}`);
            console.log("Saved event:", result);

            setClasses([]);
        } catch (error) {
            console.error("Error saving event:", error);
            alert("Error saving event: " + error.message);
        }
    };


    return (
        <div className="min-h-screen bg-gray-50 p-6">
            <div className="max-w-3xl mx-auto bg-white shadow rounded-lg p-6">
                <h1 className="text-2xl font-semibold mb-4">Create Riding Competition</h1>

                <section className="space-y-3">
                    <label className="block">
                        <div className="text-sm font-medium mb-1">Event name</div>
                        <input
                            className="w-full border rounded px-3 py-2"
                            value={event.name}
                            onChange={(e) => updateEventField("name", e.target.value)}
                            placeholder="e.g. Autumn Cup 2026"
                        />
                    </label>

                    <div className="grid grid-cols-1 md:grid-cols-3 gap-3">
                        <label className="block">
                            <div className="text-sm font-medium mb-1">Organising club</div>
                            <select
                                className="w-full border rounded px-3 py-2"
                                value={event.clubId ?? ""}
                                onChange={(e) => updateEventField("clubId", Number(e.target.value))}
                            >
                                <option value="" disabled>Select a club</option>
                                {loadingClubs && <option>Loading clubs...</option>}
                                {!loadingClubs && clubs.length === 0 && <option>No clubs found</option>}
                                {clubs.map((c) => (
                                    <option key={c.id} value={Number(c.id)}>
                                        {c.name}
                                    </option>
                                ))}
                            </select>

                        </label>

                        <label>
                            <div className="text-sm font-medium mb-1">Event level</div>
                            <select
                                className="w-full border rounded px-3 py-2"
                                value={event.level}
                                onChange={(e) => updateEventField("level", e.target.value)}
                            >
                                {eventLevels.map((l) => (
                                    <option key={l} value={l}>
                                        {l}
                                    </option>
                                ))}
                            </select>
                            <p className="text-xs text-gray-500 mt-1">Currently only E is allowed.</p>
                        </label>

                        <label>
                            <div className="text-sm font-medium mb-1">Status</div>
                            <select
                                className="w-full border rounded px-3 py-2"
                                value={event.status}
                                onChange={(e) => updateEventField("status", e.target.value)}
                            >
                                {statuses.map((s) => (
                                    <option key={s.id} value={s.id}>
                                        {s.label}
                                    </option>
                                ))}
                            </select>
                        </label>
                    </div>

                    <div className="grid grid-cols-1 md:grid-cols-3 gap-3">
                        <label>
                            <div className="text-sm font-medium mb-1">Start date</div>
                            <input
                                type="date"
                                className="w-full border rounded px-3 py-2"
                                value={event.startDate}
                                onChange={(e) => updateEventField("startDate", e.target.value)}
                            />
                        </label>

                        <label>
                            <div className="text-sm font-medium mb-1">End date</div>
                            <input
                                type="date"
                                className="w-full border rounded px-3 py-2"
                                value={event.endDate}
                                onChange={(e) => updateEventField("endDate", e.target.value)}
                            />
                        </label>

                        <label>
                            <div className="text-sm font-medium mb-1">Entry deadline</div>
                            <input
                                type="date"
                                className="w-full border rounded px-3 py-2"
                                value={event.entryDeadline}
                                onChange={(e) => updateEventField("entryDeadline", e.target.value)}
                            />
                        </label>
                    </div>
                </section>

                <hr className="my-6" />

                <section>
                    <div className="flex items-center justify-between mb-4">
                        <h2 className="text-lg font-medium">Classes</h2>
                        <div>
                            <button
                                onClick={startAddClass}
                                className="inline-flex items-center px-3 py-2 bg-blue-600 text-white rounded hover:bg-blue-700"
                            >
                                + Add class
                            </button>
                        </div>
                    </div>

                    {adding && (
                        <div className="border rounded p-4 mb-4 bg-gray-50">
                            <div className="grid grid-cols-1 md:grid-cols-3 gap-3">
                                <label>
                                    <div className="text-sm font-medium mb-1">Class name</div>
                                    <input
                                        className="w-full border rounded px-3 py-2"
                                        value={classForm.name}
                                        onChange={(e) => setClassForm((s) => ({ ...s, name: e.target.value }))}
                                        placeholder="e.g. Youth Dressage"
                                    />
                                </label>

                                <label>
                                    <div className="text-sm font-medium mb-1">Level (event level)</div>
                                    <input className="w-full border rounded px-3 py-2" value={classForm.level} readOnly />
                                </label>

                                <label>
                                    <div className="text-sm font-medium mb-1">Discipline</div>
                                    {loadingDisciplines ? (
                                        <div className="text-gray-500 text-sm italic">Loading disciplines...</div>
                                    ) : (
                                        <select
                                            className="w-full border rounded px-3 py-2"
                                            value={classForm.discipline}
                                            onChange={(e) => handleDisciplineChange(Number(e.target.value))}
                                        >
                                            {disciplines.map((d) => (
                                                <option key={d.id} value={d.id}>
                                                    {d.name}
                                                </option>
                                            ))}
                                        </select>
                                    )}
                                </label>
                            </div>

                            <div className="grid grid-cols-1 md:grid-cols-4 gap-3 mt-3">
                                <label>
                                    <div className="text-sm font-medium mb-1">Class level</div>
                                    <select
                                        className="w-full border rounded px-3 py-2"
                                        value={classForm.classLevel}
                                        onChange={(e) => setClassForm((s) => ({ ...s, classLevel: e.target.value }))}
                                    >
                                        {
                                            classLevels
                                                .filter(cl => cl.disciplineId === classForm.discipline)
                                                .map(cl => (
                                                    <option key={cl.id} value={cl.id}>{cl.name}</option>
                                                ))
                                        }
                                    </select>
                                </label>

                                <label>
                                    <div className="text-sm font-medium mb-1">Date</div>
                                    <input
                                        type="date"
                                        className="w-full border rounded px-3 py-2"
                                        value={classForm.date}
                                        onChange={(e) => setClassForm((s) => ({ ...s, date: e.target.value }))}
                                    />
                                </label>

                                <label>
                                    <div className="text-sm font-medium mb-1">Price (DKK)</div>
                                    <input
                                        type="number"
                                        className="w-full border rounded px-3 py-2"
                                        value={classForm.price}
                                        onChange={(e) => setClassForm((s) => ({ ...s, price: e.target.value }))}
                                        min="0"
                                    />
                                </label>

                                <label>
                                    <div className="text-sm font-medium mb-1">Max participants</div>
                                    <input
                                        type="number"
                                        className="w-full border rounded px-3 py-2"
                                        value={classForm.maxParticipants}
                                        onChange={(e) => setClassForm((s) => ({ ...s, maxParticipants: e.target.value }))}
                                        min="1"
                                    />
                                </label>
                            </div>

                            <div className="flex gap-2 mt-4">
                                <button onClick={saveClass} className="px-3 py-2 bg-green-600 text-white rounded">
                                    Save class
                                </button>
                                <button onClick={cancelAdd} className="px-3 py-2 border rounded">
                                    Cancel
                                </button>
                            </div>
                        </div>
                    )}

                    <div className="space-y-3">
                        {classes.length === 0 && <p className="text-sm text-gray-500">No classes added yet.</p>}

                        {classes.map((c) => (
                            <div key={c.id} className="border rounded p-3 bg-white flex justify-between items-center">
                                <div>
                                    <div className="font-medium">{c.name} <span className="text-xs text-gray-500">({c.discipline})</span></div>
                                    <div className="text-sm text-gray-600">Level: {c.classLevel} • Date: {c.date} • Price: {c.price} DKK • Max: {c.maxParticipants || '—'}</div>
                                </div>
                                <div className="flex gap-2 items-center">
                                    <button onClick={() => removeClass(c.id)} className="px-2 py-1 border rounded text-sm">Remove</button>
                                </div>
                            </div>
                        ))}
                    </div>
                </section>

                <div className="mt-6 flex justify-end gap-3">
                    <button onClick={saveEvent} className="px-4 py-2 bg-blue-600 text-white rounded">Save event</button>
                </div>
            </div>
        </div>
    );
}
