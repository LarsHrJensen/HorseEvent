'use client';
import { useRouter } from "next/navigation";
import Image from "next/image";
import "./page.css";
import { useEffect, useState } from "react";


export default function Profilpage() {
  const router = useRouter();

  const [nyheder, setNyheder] = useState([]);
  const [loading, setLoading] = useState(true);
  const [staevner, setStaevner] = useState([]);
  const [konkurrencer, setKonkurrencer] = useState(5);
  const [læringsmoduler, setLæringsmoduler] = useState(34);
  const [opnådeMål, setOpnådeMål] = useState(11);
  const [træningstimer, setTræningstimer] = useState(97);


  const handleSave = () => {
          console.log("Gemte værdier:", {
            konkurrencer,
            læringsmoduler,
            træningstimer,
            opnådeMål
          });
             alert("Dine ændringer er gemt!");
          };



  useEffect(() => {
    async function fetchNyheder() {
      try {
        const response = await fetch('https://newsdata.io/api/1/latest?apikey=pub_0a5be89711d14f809cc661475950e602&q=horse OR pony OR hest OR ridning');
        const data = await response.json();
        setNyheder((data.results || []).slice(0, 2));
      } catch (error) {
        console.error('Error fetching news:', error);
      } finally{
        setLoading(false);
      }
    }
    fetchNyheder();
  }, []);

  useEffect(() => {
    const dummyStaevner = [
      { navn: 'Springstævne i København', dato: '2025-07-15' },
      { navn: 'Dressurstævne i Aarhus', dato: '2025-08-20' },
      { navn: 'Terrænstævne i Odense', dato: '2025-09-10' },
      { navn: 'Springstævne i Aalborg', dato: '2025-11-30' },
      { navn: 'Dressurstævne i Tim', dato: '2025-01-09' },
      { navn: 'Terrænstævne i Esbjerg', dato: '2025-09-10' },
      { navn: 'Springstævne i Fredericia', dato: '2025-09-15' },
      { navn: 'Springstævne i Højslev', dato: '2025-08-02' },
      { navn: 'Hestestævne på Mors', dato: '2025-02-14' }
    ];

    const twoRandom = dummyStaevner
      .sort(() => 0.5 - Math.random())
      .slice(0, 2);

    setStaevner(twoRandom);
  }, []);

  return (
    <main className="profil-wrapper">

    
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
          <div className="profil-extra-boxes">
            <div className="profil-info-box">
              <h1>Personlig information</h1>
              <p className="profil-info-text">
                Navn: Hestevennen<br />
                Alder: 43 år<br />
                Interesser: rideture, hesteshows, dyrepleje<br />
              </p>
            </div>
            
           <div className="mini-box">
            <span className="box-title">Konkurrencer</span>
            <select className="box-number" 
            value={konkurrencer} 
            onChange={(e) => setKonkurrencer(e.target.value)}>
              {[...Array(100)].map((_, index) => (
                <option key={index} value={index + 1}>
                  {index + 1}
                </option>
              ))}
            </select>

          </div>

          <div className="mini-box">
            <span className="box-title">Læringsmoduler</span>
             <select className="box-number" 
            value={læringsmoduler} 
            onChange={(e) => setLæringsmoduler(e.target.value)}>
              {[...Array(500)].map((_, index) => (
                <option key={index} value={index + 1}>
                  {index + 1}
                </option>
              ))}
            </select>
          </div>

          <div className="mini-box">
            <span className="box-title">Træningstimer</span>
             <select className="box-number" 
            value={træningstimer} 
            onChange={(e) => setTræningstimer(e.target.value)}>
              {[...Array(500)].map((_, index) => (
                <option key={index} value={index + 1}>
                  {index + 1}
                </option>
              ))}
            </select>
          </div>

          <div className="mini-box">
            <span className="box-title">Opnåede mål</span>
             <select className="box-number" 
            value={opnådeMål} 
            onChange={(e) => setOpnådeMål(e.target.value)}>
              {[...Array(100)].map((_, index) => (
                <option key={index} value={index + 1}>
                  {index + 1}
                </option>
              ))}
            </select>
          </div>
          </div>
              
          <button className="save-button" onClick={handleSave}>Gem ændringer</button>


          </div>

          {/* Højre sektion */}
          <div className="profil-sidebokse">

            <div className="sideboks topboks">
              <h3>Kommende stævner</h3>
              <ul className="staevne-liste">
                {Array.isArray(staevner) && staevner.length > 0 ? (
                  staevner.map((staevne, index) => (
                    <li key={index}>
                      <strong>{staevne.navn}</strong> - {staevne.dato}
                    </li>
                  ))
                ) : (
                  <li>Ingen stævner fundet.</li>
                )}
              </ul>
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
                <div className="sideboks hestebokse">
                 <h3>Mine heste</h3>
                 <div className="heste-container">
                   <div className="heste-boks">
                    <Image
                    src="/Horse1.webp"
                    alt="Karamel"
                    width={200}
                    height={150}
                    className="hest-1"
                    />
                   </div>
                   <div className="heste-boks">
                    <Image
                    src="/Horse2.avif"
                    alt="Chokolade"
                    width={200}
                    height={150}
                    className="hest-2"
                    />
                   </div>
                 </div>
                </div>


          </div>
        </div>
      </div>
    </main>
  );

}