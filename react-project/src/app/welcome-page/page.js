import Link from "next/link";

//tror bare den her fil skal slettes?
export default function Home() {
  return (
    <main className="relative min-h-screen bg-sky-100">

      {/* Øverste højre hjørne */}
      <div className="fixed top-4 right-4 z-50 flex gap-2">
        <Link href="/indstillinger">
          <button className="bg-blue-500 text-white px-4 py-2 rounded-lg hover:bg-blue-600 transition">
            Indstillinger
          </button>
        </Link>
      </div>

      {/* Øverste venstre hjørne */}
       <div className="fixed top-4 left-4 z-50 flex gap-2">
        <Link href="/login">
          <button className="bg-red-500 text-white px-4 py-2 rounded-lg hover:bg-red-600 transition">
            Log ind/Opret profil
          </button>
        </Link>
      </div>

      <div className="max-w-2xl mx-auto mt-20 bg-white shadow-2xl rounded-2xl p-10 text-center relative z-10">
        <h1 className="text-4xl font-bold mb-4 text-blue-400">
          🐎Hesteland! 
        </h1>
        <h2 className="text-2xl font-bold mb-4 text-black">
          Velkommen til hesteland
        </h2>
        <p className="text-lg text-gray-700 mb-6">
          Hvad vil du gerne nu? 
        </p>

        <a href="/hjem"
          className="inline-block bg-purple-300 text-black px-6 py-3 rounded-xl text-lg font-medium shadow-md hover:bg-purple-400 transition-transform hover:scale-105">
          Hjem
        </a>
        <a href="/stævner"
          className="inline-block bg-purple-300 text-black px-6 py-3 rounded-xl text-lg font-medium shadow-md hover:bg-purple-400 transition-transform hover:scale-105">
          Stævner
        </a>
        <a href="/profil"
          className="inline-block bg-purple-300 text-black px-6 py-3 rounded-xl text-lg font-medium shadow-md hover:bg-purple-400 transition-transform hover:scale-105">
          Profil
        </a>
        <a href="/hest"
          className="inline-block bg-purple-300 text-black px-6 py-3 rounded-xl text-lg font-medium shadow-md hover:bg-purple-400 transition-transform hover:scale-105">
          Hest
        </a>
        <a href="/chatbot"
          className="inline-block bg-purple-300 text-black px-6 py-3 rounded-xl text-lg font-medium shadow-md hover:bg-purple-400 transition-transform hover:scale-105">
          Chatbot
        </a>
        <a href="/hjælp-og-support"
        className="inline-block bg-purple-300 text-black px-6 py-3 rounded-xl text-lg font-medium shadow-md hover:bg-purple-400 transition-transform hover:scale-105">
          Hjælp & Support
        </a>

        <a href="/Log ind"
        className="inline-block bg-purple-300 text-black px-6 py-3 rounded-xl text-lg font-medium shadow-md hover:bg-purple-400 transition-transform hover:scale-105">
          Log ind
        </a>

        <a href="/opret-profil"
        className="inline-block bg-purple-300 text-black px-6 py-3 rounded-xl text-lg font-medium shadow-md hover:bg-purple-400 transition-transform hover:scale-105">
          Opret profil
        </a>

        <a href="/Glemt-password"
        className="inline-block bg-purple-300 text-black px-6 py-3 rounded-xl text-lg font-medium shadow-md hover:bg-purple-400 transition-transform hover:scale-105">
          Glemt password
        </a>
      </div>
    </main>
  );
}


          //Hest
          //Chatbot
          //help og support