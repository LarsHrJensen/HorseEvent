"use client";

import { useEffect, useState } from "react";
import { useParams } from "next/navigation";

export default function HorseProfilePage() {
    const params = useParams();
    // useParams() can initially be undefined/null in some environments — normalize it
    const rawId = params ? params.id : undefined;
    const id = Array.isArray(rawId) ? rawId[0] : rawId; // ensure string or undefined

    const [horse, setHorse] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        let isMounted = true; // avoid state updates after unmount
        const controller = new AbortController();

        // If there's no valid id yet, show a friendly message instead of attempting fetch
        if (!id) {
            // Only clear loading if we're mounted — otherwise leave it to the first successful fetch
            if (isMounted) {
                setLoading(false);
            }
            return () => {
                isMounted = false;
                controller.abort();
            };
        }

        async function fetchHorse() {
            try {
                if (isMounted) {
                    setLoading(true);
                    setError(null);
                }

                const res = await fetch(`https://localhost:7265/api/horse/${encodeURIComponent(id)}`, {
                    signal: controller.signal,
                });

                if (!res.ok) {
                    // try to get server-provided message
                    let serverMsg = null;
                    try {
                        const payload = await res.json();
                        serverMsg = payload?.message || payload?.error || null;
                    } catch { /* ignore json parse errors */ }

                    throw new Error(serverMsg ?? `Server returned ${res.status}`);
                }

                const data = await res.json();

                if (!isMounted) return;
                setHorse(data);
            } catch (err) {
                if (!isMounted) return;
                if (err.name === "AbortError") return; // fetch was aborted
                setError(err?.message ?? String(err));
                setHorse(null);
            } finally {
                if (isMounted) setLoading(false);
            }
        }

        fetchHorse();

        return () => {
            isMounted = false;
            controller.abort();
        };
    }, [id]);

    // Render states
    if (!id) return <p className="p-6 text-center">Ingen hest id i URL — tjek ruten (fx /horse-profile/3)</p>;
    if (loading) return <p className="p-6 text-center">Henter hest...</p>;
    if (error) return <p className="p-6 text-center text-red-600">{error}</p>;
    if (!horse) return <p className="p-6 text-center">Ingen hest fundet.</p>;

    return (
        <div className="max-w-4xl mx-auto p-6 space-y-6">
            {/* Top: Image + Info */}
            <div className="shadow-xl rounded-2xl bg-white">
                <div className="p-6 grid grid-cols-1 md:grid-cols-2 gap-6">
                    {/* Image */}
                    <div className="w-full h-64 bg-gray-200 rounded-lg flex items-center justify-center">
                        <span className="text-gray-500">(Billede af hesten)</span>
                    </div>

                    {/* Info */}
                    <div>
                        <h1 className="text-3xl font-bold mb-4">{horse.name}</h1>
                        <ul className="space-y-1 text-lg">
                            <li><strong>UELN:</strong> {horse.ueln}</li>
                            <li><strong>Køn:</strong> {horse.gender}</li>
                            <li><strong>Højde:</strong> {horse.height} cm</li>
                            <li><strong>Fødselsår:</strong> {horse.birthYear}</li>
                            <li><strong>Farve:</strong> {horse.color ?? "Ukendt"}</li>
                            <li><strong>Race:</strong> {horse.breedName ?? "Ukendt"}</li>
                            <li><strong>Avler:</strong> {horse.breeder ?? "Ukendt"}</li>
                        
                        </ul>
                    </div>
                </div>
            </div>

            {/* Sections under info */}
            <section className="p-4 border rounded-xl bg-white shadow">
                <h2 className="text-xl font-semibold mb-2">Stamtavle</h2>
                <p>Hardcoded stamtavle-info her…</p>

                <div className="w-full flex flex-col gap-6 p-4">
                    {/* 3 kolonner */}
                    <div className="grid grid-cols-3 gap-4">
                        {/* Kolonne 1: Far / Mor */}
                        <div className="grid grid-rows-4 gap-4">
                            <div className="border p-4 rounded-xl shadow bg-white row-span-2 flex flex-col justify-center">
                                <h3 className="font-bold">Far</h3>
                                <p>{horse?.sireName ?? "Ukendt"}</p>
                            </div>
                            <div className="border p-4 rounded-xl shadow bg-white row-span-2 flex flex-col justify-center">
                                <h3 className="font-bold">Mor</h3>
                                <p>{horse?.damName ?? "Ukendt"}</p>
                            </div>
                        </div>

                        {/* Kolonne 2: Morfar / Mormor / Farfar / Farmor */}
                        <div className="grid grid-rows-4 gap-4">
                            <div className="border p-4 rounded-xl shadow bg-white">
                                <h3 className="font-bold">Morfar</h3>
                                <p>{horse?.damSireName ?? "Ukendt"}</p>
                            </div>
                            <div className="border p-4 rounded-xl shadow bg-white">
                                <h3 className="font-bold">Mormor</h3>
                                <p>{horse?.damDamName ?? "Ukendt"}</p>
                            </div>
                            <div className="border p-4 rounded-xl shadow bg-white">
                                <h3 className="font-bold">Farfar</h3>
                                <p>{horse?.sireSireName ?? "Ukendt"}</p>
                            </div>
                            <div className="border p-4 rounded-xl shadow bg-white">
                                <h3 className="font-bold">Farmor</h3>
                                <p>{horse?.sireDamName ?? "Ukendt"}</p>
                            </div>
                        </div>

                        {/* Kolonne 3: 8 oldeforældre */}
                        <div className="grid grid-rows-8 gap-4">
                            {[...Array(8)].map((_, i) => (
                                <div key={i} className="border p-4 rounded-xl shadow bg-white">
                                    <h3 className="font-bold">Oldeforælder {i + 1}</h3>
                                    <p>Ukendt</p>
                                </div>
                            ))}
                        </div>
                    </div>
                </div>
            </section>

            <section className="p-4 border rounded-xl bg-white shadow">
                <h2 className="text-xl font-semibold mb-2">Resultater</h2>
                <p>Hardcoded resultater her…</p>
            </section>

            <section className="p-4 border rounded-xl bg-white shadow mb-6">
                <h2 className="text-xl font-semibold mb-2">Vaccinationer</h2>
                <p>Hardcoded vaccinationsdata her…</p>
            </section>
        </div>
    );
}
