'use client';
import { useState } from "react";
import "./page.css";
import Image from "next/image";
import { useRouter } from 'next/navigation';

export default function Page() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const router = useRouter();

  const handleLogin = async (e) => {
    e.preventDefault();
    console.log("Login clicked");

      try {
          const response = await fetch("https://localhost:7265/api/user/login", {
              method: "POST",
              headers: { "Content-Type": "application/json" },
              body: JSON.stringify({
                  Username: username,
                  Password: password,
                  rememberMe: true
              })
          });

      let data = null;
      const text = await response.text();
      try {
        data = JSON.parse(text);
      } catch (error) {
        console.error("Failed to parse response:", error);
      }

      if (response.ok) {
          console.log("Login successful", data);
          alert("Login successful!")
        router.push('/home');
      } else {
        console.error("Login failed", data?.message || "Unknown error");
      }
    } catch (error) {
      console.error("An error occurred during login:", error);
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
        <h1 className="login-title">Log ind på Hesteland! </h1>

        <div className="form-group">
          <label htmlFor="username">Brugernavn:</label>
          <input
            type="text"
            id="username"
            name="username"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
          />
        </div>

        <div className="form-group">
          <label htmlFor="password">Password:</label>
          <input
            type="password"
            id="password"
            name="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
        </div>

        <div className="login-form">
          <button className="login-button" onClick={handleLogin}>Log ind</button>
        </div>
      </div>
    </main>
  );
}

