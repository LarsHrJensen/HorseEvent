'use client';
import { useRouter } from "next/navigation";
import './Topbar.css';

export default function Topbar() {
    const router= useRouter();

    return (
        <div className="topbar">
            <div className="topbar-content">
               
                <button className="login-button" onClick={() => router.push('/login')}>Log ind</button>
                 <h1 className="topbar-title">Opret din profil</h1>
                <div className="spacer"></div>
            </div>
        </div>
    );
}