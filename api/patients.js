// Vercel Serverless Function to proxy Supabase requests
// This bypasses CORS issues by making server-side requests

export default async function handler(req, res) {
  // Enable CORS
  res.setHeader('Access-Control-Allow-Origin', '*');
  res.setHeader('Access-Control-Allow-Methods', 'GET, POST, PUT, DELETE, OPTIONS');
  res.setHeader('Access-Control-Allow-Headers', 'Content-Type');

  // Handle preflight
  if (req.method === 'OPTIONS') {
    return res.status(200).end();
  }

  const SUPABASE_URL = 'https://admsbebpvpnqcycrydxy.supabase.co';
  const SUPABASE_KEY = 'sb_publishable_NzJc47DMr2O-rOB2RhVDkA_cBCCiUBu';

  try {
    const url = `${SUPABASE_URL}/rest/v1/patients?select=*`;
    
    const response = await fetch(url, {
      method: 'GET',
      headers: {
        'apikey': SUPABASE_KEY,
        'Authorization': `Bearer ${SUPABASE_KEY}`,
        'Content-Type': 'application/json'
      }
    });

    const data = await response.json();

    if (!response.ok) {
      return res.status(response.status).json({ 
        error: data.message || 'Supabase request failed',
        details: data 
      });
    }

    return res.status(200).json({ 
      success: true, 
      count: data.length,
      data 
    });

  } catch (error) {
    return res.status(500).json({ 
      error: 'Server error', 
      message: error.message 
    });
  }
}
