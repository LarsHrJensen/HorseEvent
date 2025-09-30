"use client";

import { useState } from "react";
import { useRouter } from "next/navigation";
import Image from "next/image";
import "./page.css";

export default function Page() {
    const router = useRouter();
    const [username, setUsername] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");
    const [message, setMessage] = useState("");

    const handleSubmit = async (e) => {
        e.preventDefault();
        setMessage("");

        if (password !== confirmPassword) {
            setMessage("Adgangskoderne matcher ikke");
            return;
        }

        try {
            const response = await fetch("https://localhost:7265/api/user", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    UserName: username,
                    Password: password,
                    Email: email
                })
            });

            const data = await response.json();

            if (response.ok) {
                setMessage("Bruger oprettet!");
                setUsername("");
                setEmail("");
                setPassword("");
                setConfirmPassword("");
                // router.push('/login'); // evt. redirect
            } else {
                setMessage(data.message || "Fejl ved oprettelse");
            }
        } catch (error) {
            console.error(error);
            setMessage("Serverfejl");
        }
    };

    return (
        <main className="signup-wrapper">
            <div className="signup-content">
                <div className="signup-image">
                    <Image src="/kvindehest.webp" alt="kvindehest" width={600} height={500} />
                </div>
            </div>

            <div className="signup-box">
                <h1 className="signup-title">Opret dig som bruger i Hesteland!</h1>

                <form className="signup-form" onSubmit={handleSubmit}>
                    <div className="form-group">
                        <label htmlFor="username">Brugernavn:</label>
                        <input
                            type="text"
                            id="username"
                            name="username"
                            value={username}
                            onChange={(e) => setUsername(e.target.value)}
                            required
                        />
                    </div>

                    <div className="form-group">
                        <label htmlFor="email">Email:</label>
                        <input
                            type="email"
                            id="email"
                            name="email"
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                            required
                        />
                    </div>

                    <div className="form-group">
                        <label htmlFor="password">Adgangskode:</label>
                        <input
                            type="password"
                            id="password"
                            name="password"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                            required
                        />
                    </div>

                    <div className="form-group">
                        <label htmlFor="confirmPassword">Gentag adgangskode:</label>
                        <input
                            type="password"
                            id="confirmPassword"
                            name="confirmPassword"
                            value={confirmPassword}
                            onChange={(e) => setConfirmPassword(e.target.value)}
                            required
                        />
                    </div>

                    <div className="signup-button">
                        <button className="signup-button" type="submit">Opret</button>
                    </div>

                    {message && <p style={{ marginTop: '15px' }}>{message}</p>}
                </form>
            </div>
        </main>
    );
}
