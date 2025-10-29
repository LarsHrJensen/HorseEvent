'use client';
import{ useRouter} from "next/navigation";
import Image from "next/image";
import "./page.css";




export default function Profilpage() {
const router = useRouter();

const handleSignupClick = ()  => {
  router.push('/signup');
};
const handleForgotPasswordClick = ()  => {
  router.push('/forgotpassword');
};

  return (
    
    <main className= "profil-wrapper">  
    <div className= "login-button-container"> 
     {/* <button className="login-button" onClick={() => router.push('/login')}>Log ind</button> */}
    </div>

    <div className="profil-box">
    <div className= "profil-content">
      <div className= "profil-image">
        <Image
          src="/Hest.png"
          alt="Hest"
          width={400}
          height={200}
        />
      </div>
      

      <div className="profil-buttons">
          <button className="profilknap"onClick={handleSignupClick}>Opret profil</button>
          <button className="profilknap"onClick={handleForgotPasswordClick}>Glemt password?</button>
        </div>
      </div>
      </div>
    
    </main>

  
  );
}