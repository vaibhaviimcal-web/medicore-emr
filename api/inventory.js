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
    const collection = db.collection('inventory');

    if (req.method === 'GET') {
      const inventory = await collection.find({}).sort({ medicine_name: 1 }).toArray();
      
      return res.status(200).json({ 
        success: true, 
        count: inventory.length,
        data: inventory 
      });
    }

    if (req.method === 'POST') {
      const newItem = req.body;
      newItem.created_at = new Date().toISOString();
      
      const result = await collection.insertOne(newItem);
      newItem._id = result.insertedId;
      
      return res.status(201).json({ 
        success: true,
        message: 'Inventory item added successfully!',
        data: newItem 
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
        message: 'Inventory updated successfully',
        modified: result.modifiedCount
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
