'use client';
import{ useRouter} from "next/navigation";
import "./forgotpassword.css";
import Topbar from "./topbar";

export default function ForgotPassword() {
    const router = useRouter();
    return (

        <main>
            <Topbar></Topbar>
            <div className="reset-container">
                <h2>Nulstil din adgangskode </h2>
    <form
        id="resetForm"
        onSubmit={(e) => {
            e.preventDefault();
            const email = e.target.email.value;

            fetch("/api/resetpassword", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify({ email }),
            })
                .then((res) => {
                    if (res.ok)
                        document.getElementById("message").textContent =
                            "Hvis denne e-mail er registreret, har vi sendt instruktioner til nulstilling af din adgangskode.";
                    else {
                        document.getElementById("message").textContent =
                            "Der opstod en fejl. Prøv igen senere.";
                    }
                })
                .catch(() => {
                    document.getElementById("message").textContent =
                        "Der opstod en fejl. Prøv igen senere.";
                });
        }}
    >
        <label htmlFor="email">Indtast din e-mail her:</label>
        <input type="email" id="email" name="email" required />
        <button type="submit">Nulstil adgangskode</button>
    </form>
    <p id="message"></p>
</div>





        </main>
    )
    }     
    
