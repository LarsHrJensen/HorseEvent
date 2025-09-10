'use client';
import "./signin.css";
import Image from "next/image";

export default function Page() {
  return (
    <main className= "signin-wrapper"> 
    <div className= "signin-content">

      <h1>Log in her</h1>
            <div className= "signin-image">
              <Image
                src="/kvindehest.webp"
                alt="kvindehest"
                width={600}
                height={500}
               />
            </div>
        </div>
        
        <div className= "signin-box">    
          <h1 className="signin-title">Opret dig som bruger i Hesteland! </h1>

          <div className="form-group">
          <label htmlFor="email">Email:</label>
          <input type="text" id="email" name="email"/>
            </div>
        
          <div className="form-group">
          <label htmlFor="password">Password:</label>
          <input type="password" id="password" name="password"/>
            </div>


            <form className="signin-form">
           <button className="signin-button">Opret</button>
           </form>
       </div>
         
    </main>
    );
}