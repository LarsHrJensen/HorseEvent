'use client'
import { useEffect, useState } from "react";

export default function StartList({ competitionName }) {
    const [startList, setStartList] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await fetch(
                    `https://localhost:7265/api/startlist/${encodeURIComponent("Aalborg Springtour")}`
                );
                if (!response.ok) throw new Error("Fejl ved hentning af startlisten");
                const data = await response.json();
                setStartList(data);
            } catch (err) {
                setError(err.message);
            } finally {
                setLoading(false);
            }
        };

        fetchData();
    }, [competitionName]);

    if (loading) return <p>Loading...</p>;
    if (error) return <p>Error: {error}</p>;
    if (startList.length === 0) return <p>Ingen startliste fundet</p>;

    return (
        <table style={{ borderCollapse: "collapse", width: "100%" }}>
            <thead>
                <tr>
                    <th>#</th>
                    <th>Start Time</th>
                    <th>Rider</th>
                    <th>Horse</th>
                    <th>Competition</th>
                    <th>Class Level</th>
                    <th>Discipline</th>
                </tr>
            </thead>
            <tbody>
                {startList.map((item) => (
                    <tr key={item.startListId}>
                        <td>{item.startNumber}</td>
                        <td>{item.startTime ? new Date(item.startTime).toLocaleString() : "Ingen tid"}</td>
                        <td>{item.riderName}</td>
                        <td>{item.horseName}</td>
                        <td>{item.competitionName}</td>
                        <td>{item.classLevel}</td>
                        <td>{item.disciplineName}</td>
                    </tr>
                ))}
            </tbody>

        </table>
    );

}
