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
    const filePath = path.join(process.cwd(), 'data', 'inventory.json');
    const fileData = fs.readFileSync(filePath, 'utf8');
    const inventory = JSON.parse(fileData);

    if (req.method === 'GET') {
      return res.status(200).json({ 
        success: true, 
        count: inventory.length,
        data: inventory 
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
