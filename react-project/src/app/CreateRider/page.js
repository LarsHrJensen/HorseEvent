'use client'

import { useState } from "react";
import "./page.css";

export default function CreateRiderPage() {
    const [riderData, setRiderData] = useState({
        Name: "",
        DRFId: "",
        BirthYear: "",
        Email: "", 
    })

    // Handle changes of input
const handleChange = (e) => {
    const {name, value} = e.target;
    setRiderData({...riderData, [name]: value});
};


const handleSubmit = async (e) => {
    e.preventDefault();

    try{
        const response = await fetch("httpa://localhost", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(riderData)
        });

        if(response.ok) {
            alert("Rytter er oprettet!");
            setRiderData({name: "", DRFId: "", BirthYear:"", Email:""});
        } else {
            alert("Der opstod en fehl.");
        }
    } catch (error) {
        console.error(error);
        alert("Der gik sku noget galt med at oprette rytter")
    }
}
    
    return(
        <div className="createRiderContainer">
            <h1>Opret rytter</h1>
            <p>Udfyld detaljerne nedenfor for at oprette en ny rytter.</p>
            <form onSubmit={handleSubmit}>
                <input type="text" name="Name" placeholder="Rytters navn" value={riderData.Name} onChange={handleChange} required />
                <input type="text" name="DRFId" placeholder="DRF licens nummber" value={riderData.DRFId} onChange={handleChange} required />
                <input type="number" name="BirthYear" placeholder="Rytterens fødselsår" value={riderData.BirthYear} onChange={handleChange} required />
                <input type="number" name="" placeholder="Rytterens email" value={riderData.Email} onChange={handleChange} required />

                <button type="submit">Opret Rytter</button>
            </form>
        </div>
    )
}