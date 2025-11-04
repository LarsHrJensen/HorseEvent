"use client";

import React, { useState } from "react";

export default function CreateEventPage() {
    // Hardcoded choices (you asked hardcoded for now)
    const clubs = [
        { id: "club-1", name: "Randers Riding Club" },
        { id: "club-2", name: "East Valley Equestrian" },
        { id: "club-3", name: "North Field Riders" },
    ];

    const disciplines = [
        { id: "dressage", label: "Dressage" },
        { id: "jumping", label: "Jumping" },
    ];

    // Event-level: only level E allowed now
    const eventLevels = ["E"];

    // Class-levels per discipline
    const dressageLevels = ["LD1", "LA5", "LA3", "MB"];
    const jumpingLevels = ["LB*", "LB", "BOM (on ground)", "MB"];

    const statuses = [
        { id: "draft", label: "Draft" },
        { id: "open", label: "Open for entries" },
        { id: "closed", label: "Closed" },
        { id: "cancelled", label: "Cancelled" },
    ];

    // Event state
    const [event, setEvent] = useState({
        name: "",
        clubId: clubs[0].id,
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
        discipline: disciplines[0].id,
        classLevel: dressageLevels[0],
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
        setClassForm((s) => ({
            ...s,
            discipline: val,
            // reset classLevel depending on discipline
            classLevel: val === "dressage" ? dressageLevels[0] : jumpingLevels[0],
        }));
    };

    const saveEvent = () => {
        // basic validation
        if (!event.name) return alert("Please give the event a name.");
        if (!event.startDate || !event.endDate) return alert("Please set start and end dates.");
        if (new Date(event.startDate) > new Date(event.endDate))
            return alert("Start date must be before end date.");

        const payload = { ...event, classes };
        // For now: show in console (later: POST to API / persist to DB)
        console.log("Save event payload:", payload);
        alert("Event saved — check console for payload (dev mode).\nYou can now extend this to POST to an API endpoint.");
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
                                value={event.clubId}
                                onChange={(e) => updateEventField("clubId", e.target.value)}
                            >
                                {clubs.map((c) => (
                                    <option key={c.id} value={c.id}>
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
                                    <select
                                        className="w-full border rounded px-3 py-2"
                                        value={classForm.discipline}
                                        onChange={(e) => handleDisciplineChange(e.target.value)}
                                    >
                                        {disciplines.map((d) => (
                                            <option key={d.id} value={d.id}>
                                                {d.label}
                                            </option>
                                        ))}
                                    </select>
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
                                        {(classForm.discipline === "dressage" ? dressageLevels : jumpingLevels).map((lv) => (
                                            <option key={lv} value={lv}>
                                                {lv}
                                            </option>
                                        ))}
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

            <div className="max-w-3xl mx-auto mt-4 text-xs text-gray-500">
                <p>
                    Notes: This is a fairly raw implementation (client-side only) with hardcoded reference data. Hook it up to an
                    API or a database (e.g. using a POST to /api/events) and add server-side validation for production.
                </p>
            </div>
        </div>
    );
}
