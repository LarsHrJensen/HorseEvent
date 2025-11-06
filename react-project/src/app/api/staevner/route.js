import axios from 'axios';
import cheerio from 'cheerio';

export async function GET() {
  try {
    const res = await axios.get('https://www.heste-nettet.dk/staevner/');
    const $ = cheerio.load(res.data);
    const staevner = [];

    $('.staevne-element').each((i, e) => {
      const navn = $(e).find('.staevne-navn').text().trim();
      const dato = $(e).find('.staevne-dato').text().trim();
      if (navn && dato) {
        staevner.push({ navn, dato });
      }
    });

    return new Response(JSON.stringify(staevner), {
      status: 200,
      headers: { 'Content-Type': 'application/json' },
    });
  } catch (error) {
    console.error('Fejl i scraping:', error.message);
    return new Response(JSON.stringify({ error: 'Kunne ikke hente stævner' }), {
      status: 500,
      headers: { 'Content-Type': 'application/json' },
    });
  }
}