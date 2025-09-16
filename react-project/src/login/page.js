'use client';
import "./login.css";
import Image from "next/image";

export default function Page() {
  return (
    <main className= "login-wrapper"> 
    <div className= "login-content">

      <h1>Log in her</h1>
            <div className= "login-image">
              <Image
                src="/Horse_girl.png"
                alt="Horse_girl"
                width={600}
                height={500}
               />
            </div>
        </div>
        
        <div className= "login-box">    
          <h1 className="login-title">Log ind på Hesteland! </h1>

          <div className="form-group">
          <label htmlFor="email">Email:</label>
          <input type="text" id="email" name="email"/>
            </div>
        
          <div className="form-group">
          <label htmlFor="password">Password:</label>
          <input type="password" id="password" name="password"/>
            </div>


            <form className="login-form">
           <button className="login-button"onClick={handleSignupClick}>Log ind</button>

           </form>
       </div>
         
    </main>
    );
}