
import {House, CircleUser, MessageCircleMore, CircleQuestionMark, CalendarDays, LogOut, Settings, Trophy} from "lucide-react";
import Link from "next/link";
import "./NavBar.css"

export default function NavBar(){
    return(
        <aside className="bg-white w-70 min-h-screen flex flex-col items-center py-8 space-y-6">
            <nav className="max-w-6x1 mx-auto flex flex-col space-y-10 py-4 space-x-8">
                <Link
                href="/home"
                className="nav-link">
                    <House className="nav-icon"/>
                    <span className="text-colour-blue"> Hjem </span>
                </Link>
                
                <Link href="/get-events"
                className="nav-link">
                    <Trophy className="nav-icon"/>
                    <span className="text-colour-blue"> Stævner </span>
                </Link>

                <Link href="/profil"
                className="nav-link">
                    <CircleUser className="nav-icon"/>
                    <span className="text-colour-blue"> Profil </span>
                </Link>

                <Link href="/get-horses"
                className="nav-link">
                    <span className="text-colour-blue">Mine Heste</span>
                </Link>

                <Link href="/calendar"
                className="nav-link">
                    <CalendarDays className="nav-icon"/>
                    <span className="text-colour-blue">Kalender</span>
                </Link>

                <Link href="/chatbot"
                className="nav-link">
                    <MessageCircleMore className="nav-icon-p"/>
                    <span className="text-colour-purple">Chat Bot</span>
                </Link>

                <Link href="/settings"
                className="nav-link">
                    <Settings className="nav-icon"/>
                    <span className="text-colour-blue">Indstillinger</span>
                </Link>

                <Link href="/help"
                className="nav-link">
                    <CircleQuestionMark className="nav-icon"/>
                    <span className="text-colour-blue">Hjælp & Support</span>
                </Link>

                <Link href="/logout"
                className="nav-link">
                    <LogOut className="nav-icon-p"/>
                    <span className="text-colour-purple"> Log ud </span>
                </Link>
            </nav>
        </aside>
    );
}