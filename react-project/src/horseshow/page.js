'use client'
import React from 'react';
import "./page.css";
import Link from 'next/link';
import{ useRouter} from "next/navigation";


export default function Page() {
  const router = useRouter();
  return (
    
  <div className="container">
      <div className="columns">
        <div className="column">Placering</div>
        <div className="column">Ekvipage</div>
        <div className="column">Program</div>
        <div className="column">Point</div>
      </div>

      <div className="box">
        <h1>Hestestævner</h1>
        <button onClick={() => router.push('/horseshow')}>
          Stævner
        </button>
      </div>
    </div>

    
  );
}

    



