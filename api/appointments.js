import fs from 'fs';
import path from 'path';

export default async function handler(req, res) {
  res.setHeader('Access-Control-Allow-Origin', '*');
  res.setHeader('Access-Control-Allow-Methods', 'GET, POST, PUT, DELETE, OPTIONS');
  res.setHeader('Access-Control-Allow-Headers', 'Content-Type');

  if (req.method === 'OPTIONS') {
    return res.status(200).end();
  }

  try {
    const filePath = path.join(process.cwd(), 'data', 'appointments.json');
    const fileData = fs.readFileSync(filePath, 'utf8');
    const appointments = JSON.parse(fileData);

    if (req.method === 'GET') {
      const { date } = req.query;
      let filtered = appointments;
      
      if (date) {
        filtered = appointments.filter(a => a.appointment_date === date);
      }
      
      return res.status(200).json({ 
        success: true, 
        count: filtered.length,
        data: filtered 
      });
    }

    if (req.method === 'POST') {
      const newAppointment = req.body;
      newAppointment.id = String(appointments.length + 1);
      newAppointment.created_at = new Date().toISOString();
      
      appointments.push(newAppointment);
      fs.writeFileSync(filePath, JSON.stringify(appointments, null, 2));
      
      return res.status(201).json({ 
        success: true, 
        data: newAppointment 
      });
    }

    return res.status(405).json({ error: 'Method not allowed' });

  } catch (error) {
    return res.status(500).json({ 
      error: 'Server error', 
      message: error.message
    });
  }
}
