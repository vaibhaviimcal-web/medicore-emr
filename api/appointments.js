import clientPromise from '../lib/mongodb';

export default async function handler(req, res) {
  res.setHeader('Access-Control-Allow-Origin', '*');
  res.setHeader('Access-Control-Allow-Methods', 'GET, POST, PUT, DELETE, OPTIONS');
  res.setHeader('Access-Control-Allow-Headers', 'Content-Type');

  if (req.method === 'OPTIONS') {
    return res.status(200).end();
  }

  try {
    const client = await clientPromise;
    const db = client.db('medicore-emr');
    const collection = db.collection('appointments');

    if (req.method === 'GET') {
      const { date } = req.query;
      const query = date ? { appointment_date: date } : {};
      
      const appointments = await collection.find(query).sort({ appointment_date: 1, appointment_time: 1 }).toArray();
      
      return res.status(200).json({ 
        success: true, 
        count: appointments.length,
        data: appointments 
      });
    }

    if (req.method === 'POST') {
      const newAppointment = req.body;
      newAppointment.created_at = new Date().toISOString();
      
      const result = await collection.insertOne(newAppointment);
      newAppointment._id = result.insertedId;
      
      return res.status(201).json({ 
        success: true,
        message: 'Appointment scheduled successfully!',
        data: newAppointment 
      });
    }

    if (req.method === 'PUT') {
      const { id, ...updateData } = req.body;
      
      const result = await collection.updateOne(
        { _id: id },
        { $set: updateData }
      );
      
      return res.status(200).json({ 
        success: true, 
        message: 'Appointment updated successfully',
        modified: result.modifiedCount
      });
    }

    if (req.method === 'DELETE') {
      const { id } = req.query;
      
      const result = await collection.deleteOne({ _id: id });
      
      return res.status(200).json({ 
        success: true, 
        message: 'Appointment deleted successfully',
        deleted: result.deletedCount
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
