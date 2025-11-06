'use client';
import { useRouter } from "next/navigation";
import Image from "next/image";
import "./page.css";
import Topbar from "../components/Header";
import { useEffect, useState } from "react";


export default function Profilpage() {
  const router = useRouter();

 const [nyheder, setNyheder] = useState([]);
const [loading, setLoading] = useState(true);
const [staevner, setStaevner] = useState([]);


  useEffect(() => {
    async function fetchNyheder() {
      try {
        const response = await fetch('https://newsdata.io/api/1/latest?apikey=pub_0a5be89711d14f809cc661475950e602&q=horse OR pony OR hest OR ridning');
        const data = await response.json();
        setNyheder(data.results);
        setLoading(false);
      } catch (error) {
        console.error('Error fetching news:', error);
        setLoading(false);
      }
    }
    fetchNyheder();
    
  }, []);

  useEffect(() => {
    async function fetchStaevner() {
      try {
        const response = await fetch('/api/staevner');
        const data = await response.json();
        setNyheder(data.result);
        setLoading(false);
      } catch (error) {
        console.error('Error fetching events:', error);
        setLoading(false);
      }
    }
      

    const dummyStaevner = [
      { navn: 'Springstævne i København', dato: '2024-07-15' },
      { navn: 'Dressurstævne i Aarhus', dato: '2024-08-20' },
      { navn: 'Terrænstævne i Odense', dato: '2024-09-10' },
    ];

    const twoRandom = dummyStaevner
    .sort(() => 0.5 - Math.random())
    .slice(0, 2);

    setStaevner(twoRandom);


  }, []);

return (
    <main className="profil-wrapper">
      <Topbar />
      <div className="login-button-container">
        <button className="login-button" onClick={() => router.push('/login')}>Log ud</button>
      </div>

      <div className="profil-box">
              <h1 className="profil-title">Min Profil</h1>

        <div className="profil-content">
          {/* Venstre sektion */}
          <div className="profile-left-section">
            <div className="profil-image">
              <Image
                src="/Hest.png"
                alt="Hest"
                width={200}
                height={100}
                className="profile-billede"
              />
            </div>
            <div className="profil-info-box">
              <p className="profil-info-text">
                Navn: Hestevennen<br />
                Alder: 43 år<br />
                Interesser: rideture, hesteshows, dyrepleje<br />
              </p>
            </div>
          </div>

          {/* Højre sektion */}
          <div className="profil-sidebokse">

            <div className="sideboks topboks">
              <h3>Kommende stævner</h3>
              <ul className="staevne-liste">
                {Array.isArray(staevner) ? (
                  staevner.map((staevne, index) => (
                  <li key={index}>
                    <strong>{staevne.navn}</strong> - {staevne.dato}
                  </li>
                ))
              ) :(
                <li>Ingen stævner fundet.</li>
              )}
              </ul>
             : (
             
            )
            </div>
            <div className="sideboks bundboks">
              <h3>Seneste nyheder</h3>
              {loading ? (
                <p>Indlæser nyheder...</p>
              ) : (
                <ul className="nyheds-liste">
                  {nyheder.map((nyhed, index) => (
                    <li key={index}>
                      <a href={nyhed.link} target="_blank" rel="noopener noreferrer">{nyhed.title}</a>
                    </li>
                  ))}
                </ul>
              )}
            </div>
          </div>
        </div>
      </div>
    </main>
  );

}