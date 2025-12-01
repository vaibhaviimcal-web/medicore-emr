import fs from 'fs';
import path from 'path';

export default async function handler(req, res) {
  // Enable CORS
  res.setHeader('Access-Control-Allow-Origin', '*');
  res.setHeader('Access-Control-Allow-Methods', 'GET, POST, PUT, DELETE, OPTIONS');
  res.setHeader('Access-Control-Allow-Headers', 'Content-Type');

  // Handle preflight
  if (req.method === 'OPTIONS') {
    return res.status(200).end();
  }

  try {
    // Read patients data from JSON file
    const filePath = path.join(process.cwd(), 'data', 'patients.json');
    const fileData = fs.readFileSync(filePath, 'utf8');
    const patients = JSON.parse(fileData);

    if (req.method === 'GET') {
      return res.status(200).json({ 
        success: true, 
        count: patients.length,
        data: patients 
      });
    }

    if (req.method === 'POST') {
      const newPatient = req.body;
      newPatient.id = String(patients.length + 1);
      newPatient.patient_id = `P${String(patients.length + 1).padStart(6, '0')}`;
      newPatient.created_at = new Date().toISOString();
      
      patients.push(newPatient);
      fs.writeFileSync(filePath, JSON.stringify(patients, null, 2));
      
      return res.status(201).json({ 
        success: true, 
        data: newPatient 
      });
    }

    return res.status(405).json({ error: 'Method not allowed' });

  } catch (error) {
    console.error('API error:', error);
    return res.status(500).json({ 
      error: 'Server error', 
      message: error.message
    });
  }
}
