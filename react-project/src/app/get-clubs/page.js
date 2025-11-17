"use client"; // Next.js specielt: fortæller at denne komponent kører på client-side

import { useEffect, useState } from "react";
import { useRouter } from "next/navigation";

export default function GetClubsPage() {

    // -------------------------
    // 1. State til data og loading
    // -------------------------
    const [clubs, setClubs] = useState([]);         // Liste af klubber hentet fra API
    const [loading, setLoading] = useState(true);   // Loader status
    const [error, setError] = useState(null);       // Fejlbesked (hvis API fejler)
    const router = useRouter();

    // -------------------------
    // 2. Hent klubdata fra API ved mount
    // -------------------------
    useEffect(() => {
        async function fetchClubs() {
            try {
                const response = await fetch("https://localhost:7265/api/club");
                if (!response.ok) throw new Error("Fejl ved hentning af klubber");

                const data = await response.json();
                setClubs(data);
            } catch (err) {
                console.error("Fejl ved hentning af klubdata:", err);
                setError("Kunne ikke hente klubber fra serveren.");
            } finally {
                setLoading(false);
            }
        }

        fetchClubs();
    }, []); // Tomt array → kun kør ved første render

    // -------------------------
    // 3. Renderer
    // -------------------------
    if (loading) {
        return (
            <div className="min-h-screen bg-gray-50 flex items-center justify-center text-gray-600">
                <p>Henter klubber...</p>
            </div>
        );
    }

    if (error) {
        return (
            <div className="min-h-screen bg-gray-50 flex items-center justify-center text-red-600">
                <p>{error}</p>
            </div>
        );
    }

    // -------------------------
    // 4. HTML / JSX output
    // -------------------------
    return (
        <div className="min-h-screen bg-gray-50 p-6">
            <div className="max-w-4xl mx-auto bg-white rounded-lg shadow p-6">
                {/* Header */}
                <div className="flex justify-between items-center mb-6">
                    <h1 className="text-2xl font-semibold text-gray-800">Klubber</h1>
                    <button
                        onClick={() => router.push("/createClub")}
                        className="px-4 py-2 bg-blue-600 text-white rounded hover:bg-blue-700"
                    >
                        + Opret klub
                    </button>
                </div>

                {/* Listevisning i tabelform */}
                {clubs.length === 0 ? (
                    <p className="text-gray-500">Der er ingen klubber at vise.</p>
                ) : (
                    <table className="min-w-full border border-gray-300 text-left text-sm">
                        <thead className="bg-gray-100">
                            <tr>
                                <th className="py-2 px-3 border-b">ID</th>
                                <th className="py-2 px-3 border-b">Navn</th>
                                <th className="py-2 px-3 border-b">Adresse</th>
                                <th className="py-2 px-3 border-b">Land</th>
                            </tr>
                        </thead>
                        <tbody>
                            {clubs.map((club) => (
                                <tr key={club.id} className="hover:bg-gray-50">
                                    <td className="py-2 px-3 border-b">{club.id}</td>
                                    <td className="py-2 px-3 border-b font-medium text-gray-800">
                                        {club.name}
                                    </td>
                                    <td className="py-2 px-3 border-b text-gray-600">
                                        {club.address?.streetName} {club.address?.streetNumber},{" "}
                                        {club.address?.postalCode}
                                    </td>
                                    <td className="py-2 px-3 border-b text-gray-600">
                                        {club.address?.countryCode}
                                    </td>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                )}
            </div>
        </div>
    );
}
