import { useState } from "react";
import "./page.css";

export default function CreateHorsePage() {
    // Data for horses = basis for input fields for create object
    const [horseData, setHorseData] = useState({
        name: "",
        id: "",
        height: "", 
        birthyear: ""
    });

    // Handle changes of input
    const handleChange = (e) => {
        const {name, value} = e.target;
        setHorseData({...horseData, [name]: value});
    };
    
    
    const handleSumbit = async (e) => {
        e.preventDefault();

        try{
            const response = await fetch("https://localhost:5001/api/horses", { //opdater med rigtig URL
                method: "POST",
                headers: {
                    "Conent-Type": "application/json"
                },
                body: JSON.stringify(horseData)
            });

            if(response.ok) {
                alert("Hest oprettet!");
                setHorseData({name: "", id: "", height: "", birthyear: ""})
            } else {
                alert("Der opstod en fejl.")
            }
        } catch (error) {
            console.error(error);
            alert("Something went wrong, womp womp")
        }
    };
    
    
    return (
        <div className="createHorseConatiner">
            <h1> opret hest </h1>
            <p>Udfyld detaljerne nedenfor for at oprette en ny hest</p>
            <form>
                <input
                type="text"
                name="name"
                placeholder="Horse name"
                value={horseData.name}
                onChange={handleChange}
                required>
                </input>
                <input
                type="text"
                name="horseId"
                placeholder="horse id"
                value={horseData.horseId}
                onChange={handleChange}
                required>
                </input>
                <input
                type="number"
                name="height"
                placeholder="height"
                value={horseData.height}
                onChange={handleChange}
                required>
                </input>
                <input
                type="number"
                name="birthyear"
                placeholder="birthyear"
                value={horseData.birthyear}
                onChange={handleChange}
                required>
                </input>
                <button type="sumbit">Opret Hest</button>
            </form>
        </div>
    );
}