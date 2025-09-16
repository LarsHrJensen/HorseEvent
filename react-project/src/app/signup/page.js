'use client';
import{ useRouter} from "next/navigation";
import "./signup.css";
import Image from "next/image";


export default function Page() {
  return (
    <main className= "signup-wrapper"> 

      <div className= "signup-content">
            <div className= "signup-image">
              <Image
                src="/kvindehest.webp"
                alt="kvindehest"
                width={600}
                height={500}
               />
            </div>
        </div>
        
        <div className= "signup-box">    
          <h1 className="signup-title">Opret dig som bruger i Hesteland! </h1>

        <form className="signup-form">   
          <div className="form-group">
            <label htmlFor="name">Navn:</label>
            <input type="text" id="name"name="name"/>
          </div>

          <div className="form-group">
          <label htmlFor="adresse">Adresse:</label>
          <input type="text" id="adresse" name="adresse"/>
          </div>

          <div className="form-group">
          <label htmlFor="email">Email:</label>
          <input type="text" id="email" name="email"/>
            </div>
        
          <div className="form-group">
          <label htmlFor="phone">Telefonnummer:</label>
          <input type="text" id="phone" name="phone"/>
            </div>
          <div className="form-group">
          <label htmlFor="password">Password:</label>
          <input type="password" id="password" name="password"/>
            </div>
       
       <div className="signup-button">
           <button className="signup-button">Opret</button>
       </div>
         </form>
        </div>
    </main>
    );
}
