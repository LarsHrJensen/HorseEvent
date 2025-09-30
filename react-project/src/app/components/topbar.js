'use client';
import { useRouter } from "next/navigation";
import Image from "next/image";

export default function Topbar() {
    const router= useRouter();

    function handleSignupClick(e) {
        e.preventDefault();
        console.log("Signup clicked");
    }

    return (
        <>
            <button className="login-button" onClick={() => router.push('/login')}>Log ind</button>
            <button className="signup-button" onClick={handleSignupClick}>Sign up</button>
            <h1 className="topbar-title">Opret din profil</h1>
            <div className="spacer"></div>
        </>
    );
}