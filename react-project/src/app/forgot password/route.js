    // Move the API handler outside the component and form
    export async function POST(req) {
        const { email } = await req.json();
    
        // Her skal du tilføje logik til at sende e-mail
        console.log("Modtaget e-mail:", email);
    
        return new Response("Email modtaget", { status: 200 });
    }