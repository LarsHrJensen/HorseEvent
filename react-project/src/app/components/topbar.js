'use client';
import { useRouter } from "next/navigation";
import "./Topbar.css"

export default function Topbar() {
    const router= useRouter();

    function handleSignupClick(e) {
        e.preventDefault();
        console.log("Signup clicked");
    }

    return (
        <aside className="topbar">
            <div className="topbar-content">
                
                <h1 className="topbar-title"> Hesteland </h1>
                {/*<button className="btn-default"
                        onClick={handleSignupClick}>
                    Sign Up
                </button> */}
                <button className="btn-default"
                        onClick={() => router.push('/login')}>
                    Log ind
                </button>
            </div>
        </aside>
    );
}