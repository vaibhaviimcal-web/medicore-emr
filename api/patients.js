import { createClient } from '@supabase/supabase-js';

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
    // Create Supabase client
    const supabase = createClient(SUPABASE_URL, SUPABASE_KEY);
    
    console.log('Querying patients table...');
    
    // Query patients
    const { data, error, count } = await supabase
      .from('patients')
      .select('*', { count: 'exact' });

    console.log('Query result:', { data, error, count });

    if (error) {
      console.error('Supabase error:', error);
      return res.status(400).json({ 
        error: 'Supabase query failed',
        message: error.message,
        details: error.details,
        hint: error.hint,
        code: error.code
      });
    }

    return res.status(200).json({ 
      success: true, 
      count: data.length,
      data 
    });

  } catch (error) {
    console.error('Server error:', error);
    return res.status(500).json({ 
      error: 'Server error', 
      message: error.message,
      stack: error.stack,
      name: error.name
    });
  }
}
