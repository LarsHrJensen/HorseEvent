import { Header } from "next/dist/lib/load-custom-routes";
import Link from "next/link";
import { StringifyOptions } from "querystring";

interface HeaderProps {
    className?: string;
}


export default function Header({className}: HeaderProps){
    return(
        <aside className={"bg-white w-70 min-h-screen flex flex-col items-center py-8 space-y-6 ${className}"}>
            <nav className="max-w-6x1 mx-auto flex flex-col space-y-10 py-4 space-x-8">
                <Link
                href="/"
                className="text-[#6da8ff] hover:text-[#FFA500] font-semibold transition-colors duration-300">
                    Hjem
                </Link>
                <Link href="/events"
                className="text-[#6da8ff] hover:text-[#FFA500] font-semibold transition-colors duration-300">
                    Stævner
                </Link>
                <Link href="/horses"
                className="text-[#6da8ff] hover:text-[#FFA500] font-semibold transition-colors duration-300">
                    Mine Heste
                </Link>
                <Link href="/calendar"
                className="text-[#6da8ff] hover:text-[#FFA500] font-semibold transition-colors duration-300">
                    Kalender
                </Link>
                <Link href="/chatbot"
                className="text-[#6da8ff] hover:text-[#FFA500] font-semibold transition-colors duration-300">
                    Chat Bot
                </Link>
                <Link href="/settings"
                className="text-[#6da8ff] hover:text-[#FFA500] font-semibold transition-colors duration-300">
                    Indstillinger
                </Link>
                <Link href="/help"
                className="text-[#6da8ff] hover:text-[#FFA500] font-semibold transition-colors duration-300">
                    Hjælp & Support
                </Link>
                <Link href="/logout"
                className="text-[#6da8ff] hover:text-[#FFA500] font-semibold transition-colors duration-300">
                    Log ud
                </Link>
                <Link href="/startandresults"
                className="text-[#6da8ff] hover:text-[#FFA500] font-semibold transition-colors duration-300">
                    Startlister & Resultater
                </Link>
            </nav>
        </aside>
    );
}