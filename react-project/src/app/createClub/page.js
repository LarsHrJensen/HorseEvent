"use client";
import { useEffect, useState } from "react";

export default function CreateClubForm() {
    const [countries, setCountries] = useState([]);
    const [selectedCountry, setSelectedCountry] = useState("");
    const [postalCodes, setPostalCodes] = useState([]);
    const [selectedPostalCode, setSelectedPostalCode] = useState("");
    const [loadingCountries, setLoadingCountries] = useState(true);
    const [loadingPostalCodes, setLoadingPostalCodes] = useState(false);

    // Klub info
    const [clubName, setClubName] = useState("");
    const [streetName, setStreetName] = useState("");
    const [streetNumber, setStreetNumber] = useState("");

    const [statusMessage, setStatusMessage] = useState("");

    // Hent lande
    useEffect(() => {
        async function fetchCountries() {
            try {
                const response = await fetch("https://localhost:7265/api/country");
                if (!response.ok) throw new Error("Network response was not ok");
                const data = await response.json();
                setCountries(data);
            } catch (error) {
                console.error("Error fetching countries:", error);
            } finally {
                setLoadingCountries(false);
            }
        }
        fetchCountries();
    }, []);

    // Hent postnumre, når land vælges
    useEffect(() => {
        if (!selectedCountry) return;

        async function fetchPostalCodes() {
            setLoadingPostalCodes(true);
            try {
                const response = await fetch(
                    `https://localhost:7265/api/country/${selectedCountry}/postal-codes`
                );
                if (!response.ok) throw new Error("Network response was not ok");
                const data = await response.json();
                setPostalCodes(data);
            } catch (error) {
                console.error("Error fetching postal codes:", error);
            } finally {
                setLoadingPostalCodes(false);
            }
        }

        fetchPostalCodes();
    }, [selectedCountry]);

    async function handleSubmit(e) {
        e.preventDefault();
        setStatusMessage("Creating club...");

        const newClub = {
            name: clubName,
            address: {
                streetName,
                streetNumber,
                countryCode: selectedCountry,
                countryName: countries.find(c => c.code === selectedCountry)?.name || "",
                postalCode: selectedPostalCode,
                city: postalCodes.find(p => p.postalCode === selectedPostalCode)?.city || ""
            },
        }

        try {
            const response = await fetch("https://localhost:7265/api/club", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(newClub),
            });

            console.log("HTTP status:", response.status);
            const text = await response.text();
            console.log("Response body:", text);

            if (!response.ok) {
                throw new Error(`Failed to create club. Status: ${response.status}`);
            }
            

            setStatusMessage("Club created successfully!");
            setClubName("");
            setStreetName("");
            setStreetNumber("");
            setSelectedCountry("");
            setSelectedPostalCode("");
        } catch (error) {
            console.error(error);
            setStatusMessage("Error creating club: " + error.message);
        }
    }
     

    if (loadingCountries) return <p>Loading countries...</p>;

    return (
        <div className="max-w-2xl mx-auto mt-12 p-8 bg-white rounded-2xl shadow-lg">
            <h1 className="text-2xl font-semibold text-gray-800 mb-6">Create a Club</h1>
            <form onSubmit={handleSubmit} className="flex flex-col gap-6">
                <div>
                    <label className="block text-sm font-medium text-gray-700 mb-1">Club Name</label>
                    <input
                        type="text"
                        value={clubName}
                        onChange={(e) => setClubName(e.target.value)}
                        required
                        className="w-full border border-gray-300 rounded-lg px-4 py-2 text-lg focus:ring-2 focus:ring-blue-400 focus:outline-none"
                    />
                </div>

                <div className="grid grid-cols-2 gap-6">
                    <div>
                        <label className="block text-sm font-medium text-gray-700 mb-1">Street Name</label>
                        <input
                            type="text"
                            value={streetName}
                            onChange={(e) => setStreetName(e.target.value)}
                            required
                            className="w-full border border-gray-300 rounded-lg px-4 py-2 text-lg focus:ring-2 focus:ring-blue-400 focus:outline-none"
                        />
                    </div>
                    <div>
                        <label className="block text-sm font-medium text-gray-700 mb-1">Street Number</label>
                        <input
                            type="text"
                            value={streetNumber}
                            onChange={(e) => setStreetNumber(e.target.value)}
                            required
                            className="w-full border border-gray-300 rounded-lg px-4 py-2 text-lg focus:ring-2 focus:ring-blue-400 focus:outline-none"
                        />
                    </div>
                </div>

                <div>
                    <label className="block text-sm font-medium text-gray-700 mb-1">Country</label>
                    <select
                        value={selectedCountry}
                        onChange={(e) => setSelectedCountry(e.target.value)}
                        required
                        className="w-full border border-gray-300 rounded-lg px-4 py-2 text-lg bg-white focus:ring-2 focus:ring-blue-400 focus:outline-none"
                    >
                        <option value="">--Select Country--</option>
                        {countries.map((c) => (
                            <option key={c.code} value={c.code}>
                                {c.name}
                            </option>
                        ))}
                    </select>
                </div>

                {selectedCountry && (
                    <div>
                        <label className="block text-sm font-medium text-gray-700 mb-1">Postal Code</label>
                        {loadingPostalCodes ? (
                            <p className="text-gray-500">Loading postal codes...</p>
                        ) : (
                            <select
                                value={selectedPostalCode}
                                onChange={(e) => setSelectedPostalCode(e.target.value)}
                                required
                                className="w-full border border-gray-300 rounded-lg px-4 py-2 text-lg bg-white focus:ring-2 focus:ring-blue-400 focus:outline-none"
                            >
                                <option value="">--Select Postal Code--</option>
                                {postalCodes.map((p) => (
                                    <option key={p.postalCode} value={p.postalCode}>
                                        {p.displayName}
                                    </option>
                                ))}
                            </select>
                        )}
                    </div>
                )}

                <button
                    type="submit"
                    className="self-end bg-blue-500 text-white px-6 py-2 rounded-lg text-lg font-bold hover:bg-blue-600 transition"
                >
                    Create Club
                </button>

                {statusMessage && <p className="text-sm text-gray-700">{statusMessage}</p>}
            </form>
        </div>

    );
}
