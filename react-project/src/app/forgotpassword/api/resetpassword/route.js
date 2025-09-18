   
    export async function POST(req) {
        const { email } = await req.json();
        console.log("Modtaget e-mail:", email);
    
        return new Response("Email modtaget", { status: 200 });
    }