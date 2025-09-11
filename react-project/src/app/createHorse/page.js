import { useState } from "react";
import "./page.css";

export default function CreateHorsePage() {
    return (
        <div className="createHorseConatiner">
            <h1> opret hest </h1>
            <p>Udfyld detaljerne nedenfor for at oprette en ny hest</p>
            <form>
                <input
                type="text"
                name="name"
                placeholder="Horse name"
                value={HorseData.name}
                onChange={handleChange}
                required>
                </input>
                <input
                type="text"
                name="id"
                placeholder="horse id"
                value={HorseData.horseId}
                onChange={handleChange}
                required>
                </input>
                <input
                type="number"
                name="height"
                placeholder="height"
                value={HorseData.height}
                onChange={handleChange}
                required>
                </input>
                <input
                type="number"
                name="birthyear"
                placeholder="birthyear"
                value={HorseData.birthyear}
                onChange={handleChange}
                required>
                </input>
            </form>
        </div>
    )
}