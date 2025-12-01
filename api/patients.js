import clientPromise from '../lib/mongodb';

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
    const client = await clientPromise;
    const db = client.db('medicore-emr');
    const collection = db.collection('patients');

    if (req.method === 'GET') {
      const patients = await collection.find({}).sort({ created_at: -1 }).toArray();
      
      return res.status(200).json({ 
        success: true, 
        count: patients.length,
        data: patients 
      });
    }

    if (req.method === 'POST') {
      const newPatient = req.body;
      
      // Generate patient ID
      const count = await collection.countDocuments();
      newPatient.patient_id = `P${String(count + 1).padStart(6, '0')}`;
      newPatient.created_at = new Date().toISOString();
      
      // Insert into MongoDB
      const result = await collection.insertOne(newPatient);
      newPatient._id = result.insertedId;
      
      return res.status(201).json({ 
        success: true, 
        message: 'Patient added successfully to permanent storage!',
        data: newPatient 
      });
    }

    if (req.method === 'PUT') {
      const { id, ...updateData } = req.body;
      
      const result = await collection.updateOne(
        { patient_id: id },
        { $set: updateData }
      );
      
      return res.status(200).json({ 
        success: true, 
        message: 'Patient updated successfully',
        modified: result.modifiedCount
      });
    }

    if (req.method === 'DELETE') {
      const { id } = req.query;
      
      const result = await collection.deleteOne({ patient_id: id });
      
      return res.status(200).json({ 
        success: true, 
        message: 'Patient deleted successfully',
        deleted: result.deletedCount
      });
    }

    return res.status(405).json({ error: 'Method not allowed' });

  } catch (error) {
    console.error('API error:', error);
    return res.status(500).json({ 
      error: 'Server error', 
      message: error.message,
      stack: process.env.NODE_ENV === 'development' ? error.stack : undefined
    });
  }
}
