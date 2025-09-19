"use client";
import { useEffect, useState } from "react";

export default function CountryPostalDropdown() {
    const [countries, setCountries] = useState([]);
    const [selectedCountry, setSelectedCountry] = useState("");
    const [postalCodes, setPostalCodes] = useState([]);
    const [selectedPostalCode, setSelectedPostalCode] = useState("");
    const [loadingCountries, setLoadingCountries] = useState(true);
    const [loadingPostalCodes, setLoadingPostalCodes] = useState(false);

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
                if (selectedCountry != null) {
                    const response = await fetch(
                        `https://localhost:7265/api/country/${selectedCountry}/postal-codes`
                    );
                    if (!response.ok) throw new Error("Network response was not ok");
                    const data = await response.json();
                    setPostalCodes(data);
                } 
            } catch (error) {
                console.error("Error fetching postal codes:", error);
            } finally {
                setLoadingPostalCodes(false);
            }
        }

        fetchPostalCodes();
    }, [selectedCountry]);

    if (loadingCountries) return <p>Loading countries...</p>;

    return (
        <div>
            <label htmlFor="country">Country:</label>
            <select
                id="country"
                value={selectedCountry}
                onChange={(e) => setSelectedCountry(e.target.value)}
            >
                <option value="">--Select Country--</option>
                {countries.map((c) => (
                    <option key={c.code} value={c.code}>
                        {c.name}
                    </option>
                ))}
            </select>

            {selectedCountry && (
                <>
                    <label htmlFor="postalCode">Postal Code:</label>
                    {loadingPostalCodes ? (
                        <p>Loading postal codes...</p>
                    ) : (
                            <select
                                id="postalCode"
                                value={selectedPostalCode}
                                onChange={(e) => setSelectedPostalCode(e.target.value)}
                            >
                                <option value="">--Select Postal Code--</option>
                                {postalCodes.map((p) => (
                                    <option key={p.postalCode} value={p.postalCode}>
                                        {p.displayName}
                                    </option>
                                ))}
                            </select>
                    )}
                </>
            )}

            {selectedPostalCode && <p>Selected postal code: {selectedPostalCode}</p>}
        </div>
    );
}
