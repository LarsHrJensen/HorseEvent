import Image from "next/image";

export default function Profil() {
  return (
    <div>
      <h1>Din profil</h1>
      <p>Velkommen til min profil</p>

      <Image
        src="/Hest.png"
        alt="Hest"
        width={300}
        height={200}
      />
    </div>
  );
}