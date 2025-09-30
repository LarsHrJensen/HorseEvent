'use client';

import { useState } from "react";
import "./page.css";
import Image from "next/image";

export default function Page() {
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [message, setMessage] = useState("");

    const handleLoginClick = async (e) => {
        e.preventDefault();
        setMessage("");

        if (!username || !password) {
            setMessage("Udfyld både brugernavn og adgangskode");
            return;
        }

        try {
            const response = await fetch("https://localhost:7265/api/user/login", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    UserName: username,
                    Password: password
                })
            });

            if (response.ok) {
                const data = await response.json();
                console.log("Login succesful:", data);
                setMessage(`Velkommen, ${data.userName}!`);
                // evt. redirect eller gem JWT-token her
            } else if (response.status === 401) {
                setMessage("Forkert brugernavn eller adgangskode");
            } else {
                setMessage("Fejl ved login");
            }
        } catch (error) {
            console.error(error);
            setMessage("Serverfejl");
        }
    };

    return (
        <main className="login-wrapper">
            <div className="login-content">
                <div className="login-image">
                    <Image
                        src="/Horse_girl.png"
                        alt="Horse_girl"
                        width={600}
                        height={500}
                    />
                </div>
            </div>

            <div className="login-box">
                <h1 className="login-title">Log ind på Hesteland!</h1>

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

                <div className="login-form">
                    <button className="login-button" onClick={handleLoginClick}>
                        Log ind
                    </button>
                </div>

                {message && <p style={{ marginTop: '15px' }}>{message}</p>}
            </div>
        </main>
    );
}