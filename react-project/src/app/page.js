//import Image from "next/image";

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
        <a
          href="/events"
          className="inline-block bg-purple-600 text-white px-6 py-3 rounded-xl text-lg font-medium shadow-md hover:bg-purple-700 transition-transform hover:scale-105"
        >
          View Events
        </a>
      </div>
    </main>
  );
}

