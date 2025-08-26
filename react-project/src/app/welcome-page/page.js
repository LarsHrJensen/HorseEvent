
export default function Home() {
    return (
    <main className="flex flex-col items-center justify-center min-h-screen bg-sky-100">
      <div className="max-w-2xl bg-white shadow-2xl rounded-2xl p-10 text-center">
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
      </div>
    </main>
  );
}


          //Hest
          //Chatbot
          //help og support