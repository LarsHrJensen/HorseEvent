'use client';
import{ useRouter} from "next/navigation";
import Image from "next/image";
import "./profil.css";



export default function Profilpage() {
const router = useRouter();

const handleSignupClick = ()  => {
  router.push('/signup');
};

  return (
    
    <main className= "profil-wrapper">  
    <div className= "profil-box">    
      <h1 className="profil-title">Velkommen til min profil </h1>

    <div className= "profil-content">
      <div className= "profil-image">
        <Image
          src="/Hest.png"
          alt="Hest"
          width={600}
          height={500}
        />
      </div>
      

      <div className="profil-buttons">
          <button className="profilknap"onClick={handleSignupClick}>Opret profil</button>
          <button className="profilknap">Log ind</button>
         <button className="profilknap">Glemt password?</button>
        </div>
      </div>
      </div>
    
    </main>

  
  );
}