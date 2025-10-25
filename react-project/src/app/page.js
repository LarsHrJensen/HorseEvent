//import Image from "next/image";
import {House, CircleUser, MessageCircleMore, CircleQuestionMark, Trophy} from "lucide-react";
import "./page.css"

export default function Home() {
  return (
    <main className="flex flex-col items-center justify-center h-screen bg-[#f0f4ff]">
      <div className="max-w-2xl bg-white shadow-2xl rounded-2xl p-10 text-center">
        <h1 className="text-4xl font-bold mb-4 text-[#5d80f1]">
          Hesteland! 
        </h1>
        <h2 className="text-2xl font-bold mb-4 text-black">
          Velkommen til hesteland
        </h2>
        <p className="text-lg text-gray-700 mb-6">
          Hvad vil du gerne nu?
        </p>
        
        <div className="grid grid-cols-3 gap-4">
          <a href="/home" className="btn-icon">
          <House className="icon"/>
          <span> Hjem </span>
          </a>

          <a href="/stævner" className="btn-icon">
          <Trophy className="icon"/>
          <span> Stævner </span>
          </a>

          <a href="/profil" className="btn-icon">
          <CircleUser className="icon"/>
          <span> Profil </span>
          </a>

          <a href="/hest" className="btn-icon">
          <span> Hest </span>
          </a>

          <a href="/chatbot" className="btn-icon">
          <MessageCircleMore className="icon"/>
          <span> Chatbot </span>
          </a>

          <a href="/hjælp-og-support" className="btn-icon">
          <CircleQuestionMark className="icon"/>
          <span> Hjælp & Support </span>
          </a>
        </div>
      </div>
    </main>
  );
}