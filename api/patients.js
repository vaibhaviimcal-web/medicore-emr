import fs from 'fs';
import path from 'path';

// In-memory storage for new patients (session-based)
let newPatients = [];

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
    // Read base patients data from JSON file
    const filePath = path.join(process.cwd(), 'data', 'patients.json');
    const fileData = fs.readFileSync(filePath, 'utf8');
    const basePatients = JSON.parse(fileData);
    
    // Combine base patients with new patients
    const allPatients = [...basePatients, ...newPatients];

    if (req.method === 'GET') {
      return res.status(200).json({ 
        success: true, 
        count: allPatients.length,
        data: allPatients 
      });
    }

    if (req.method === 'POST') {
      const newPatient = req.body;
      newPatient.id = String(allPatients.length + 1);
      newPatient.patient_id = `P${String(allPatients.length + 1).padStart(6, '0')}`;
      newPatient.created_at = new Date().toISOString();
      
      // Add to in-memory storage
      newPatients.push(newPatient);
      
      return res.status(201).json({ 
        success: true, 
        message: 'Patient added successfully (session-based storage)',
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
