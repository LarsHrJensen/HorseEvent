"use client"; // gør komponenten til en klientkomponent (Next.js)

import { useState, useEffect, useRef } from "react";
import "./chatbot.css"; // importerer styling til chatbotten

// URL til backend-API'et
const API_URL = "http://localhost:8000";

export default function ChatbotPage() {
    // State til at gemme beskeder i chatten
    const [messages, setMessages] = useState([
        {
            id: "welcome", // unik ID
            sender: "bot", // afsender (bot eller user)
            text:
                "Hej. Jeg er din HorseEvent AI-assistent.\n",
        },
    ]);

    // State til brugerens inputfelt
    const [input, setInput] = useState("");

    // State til at vise om der sendes/loading
    const [loading, setLoading] = useState(false);

    // Reference til chatvinduet (for at kunne auto-scrrolle)
    const chatWindowRef = useRef(null);

    // Når messages ændres → scroll automatisk til bunden
    useEffect(() => {
        if (chatWindowRef.current) {
            chatWindowRef.current.scrollTop = chatWindowRef.current.scrollHeight;
        }
    }, [messages]);

    // Funktion der håndterer når brugeren trykker "Send"
    const handleSend = async (e) => {
        e.preventDefault(); // forhindrer sideopdatering
        const text = input.trim();
        if (!text || loading) return; // stop hvis tomt input eller loader

        // ➕ Tilføj brugerens besked til chatten
        const userMsg = { id: String(Date.now()), sender: "user", text };
        setMessages((prev) => [...prev, userMsg]);
        setInput(""); // tøm inputfeltet
        setLoading(true); // vis "sender" status

        try {
            // Send besked til backend
            const res = await fetch(`${API_URL}/ask`, {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ question: text }),
            });

            if (!res.ok) {
                throw new Error(`HTTP ${res.status}`);
            }

            // Modtag og fortolk svar fra backend
            const data = await res.json();
            const reply =
                (data && typeof data.answer === "string" && data.answer.trim()) ||
                "Jeg kunne ikke finde et svar ";

            // Tilføj chatbot-besked
            const botMsg = {
                id: String(Date.now() + 1),
                sender: "bot",
                text: reply,
            };
            setMessages((prev) => [...prev, botMsg]);
        } catch (err) {
            // Fejlhåndtering hvis serveren ikke svarer
            console.error("Chat error:", err);
            setMessages((prev) => [
                ...prev,
                {
                    id: String(Date.now() + 2),
                    sender: "bot",
                    text:
                        "Jeg kunne ikke forbinde til serveren.\n" +
                        "Tjek at backenden kører på http://localhost:8000.",
                },
            ]);
        } finally {
            setLoading(false); // færdig med at loade
        }
    };

    // HTML-layout til chatgrænsefladen
    return (
        <div className="chatbotContainer">
            <h1>HorseEvent AI-chatbot</h1>
            <p>
                Stil mig et spørgsmål og jeg vil forsøge mit bedste at hjælpe dig!

            </p>

            {/* Besked-vindue */}
            <div className="chatWindow" ref={chatWindowRef}>
                {messages.map((m) => (
                    <div
                        key={m.id}
                        className={`chatMessage ${m.sender === "user" ? "chatMessageUser" : "chatMessageBot"
                            }`}
                    >
                        <p>{m.text}</p>
                    </div>
                ))}
            </div>

            {/* Input-felt og send-knap */}
            <form onSubmit={handleSend} className="chatInputRow">
                <input
                    className="chatInput"
                    type="text"
                    placeholder="Skriv dit spørgsmål..."
                    value={input}
                    onChange={(e) => setInput(e.target.value)} // opdater input-state
                    disabled={loading} // lås feltet mens der sendes
                />
                <button type="submit" className="chatSendButton" disabled={loading}>
                    {loading ? "Sender…" : "Send"}
                </button>
            </form>
        </div>
    );
}