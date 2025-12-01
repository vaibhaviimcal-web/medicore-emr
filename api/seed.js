import clientPromise from '../lib/mongodb';
import fs from 'fs';
import path from 'path';

export default async function handler(req, res) {
  res.setHeader('Access-Control-Allow-Origin', '*');
  
  if (req.method !== 'POST') {
    return res.status(405).json({ error: 'Method not allowed' });
  }

  try {
    const client = await clientPromise;
    const db = client.db('medicore-emr');

    // Read seed data from JSON files
    const patientsData = JSON.parse(fs.readFileSync(path.join(process.cwd(), 'data', 'patients.json'), 'utf8'));
    const appointmentsData = JSON.parse(fs.readFileSync(path.join(process.cwd(), 'data', 'appointments.json'), 'utf8'));
    const inventoryData = JSON.parse(fs.readFileSync(path.join(process.cwd(), 'data', 'inventory.json'), 'utf8'));

    // Check if data already exists
    const patientsCount = await db.collection('patients').countDocuments();
    
    if (patientsCount > 0) {
      return res.status(200).json({ 
        success: true,
        message: 'Database already seeded',
        counts: {
          patients: patientsCount,
          appointments: await db.collection('appointments').countDocuments(),
          inventory: await db.collection('inventory').countDocuments()
        }
      });
    }

    // Seed the database
    await db.collection('patients').insertMany(patientsData);
    await db.collection('appointments').insertMany(appointmentsData);
    await db.collection('inventory').insertMany(inventoryData);

    return res.status(200).json({ 
      success: true,
      message: 'Database seeded successfully!',
      counts: {
        patients: patientsData.length,
        appointments: appointmentsData.length,
        inventory: inventoryData.length
      }
    });

  } catch (error) {
    console.error('Seed error:', error);
    return res.status(500).json({ 
      error: 'Seed failed', 
      message: error.message
    });
  }
}
