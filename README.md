# MediCore EMR - Dr. Prakash's Internal Medicine System

Modern, fast, and HIPAA/GDPR-compliant Electronic Medical Records system designed specifically for Dr. Prakash's internal medicine practice.

## 🚀 Features

- **Patient Management** - Quick registration, search, and history
- **OPD Case Sheet** - Fast data entry with pre-loaded complaints and diagnoses
- **E-Prescribing** - One-click medicine selection with customization
- **Billing & Payments** - Integrated billing with multiple payment modes
- **Follow-up Management** - Automated reminders and tracking
- **iPad Optimized** - Touch-friendly interface for iPad use
- **WhatsApp Integration** - Send prescriptions and reminders
- **Offline Support** - PWA with offline capabilities

## 📦 Project Structure

```
medicore-emr/
├── backend/              # ASP.NET Core 8.0 Web API
│   ├── Controllers/
│   ├── Models/
│   ├── Services/
│   └── Data/
├── frontend/             # React PWA
│   ├── src/
│   │   ├── components/
│   │   ├── services/
│   │   └── styles/
│   └── public/
└── prototype/            # HTML Prototype (No setup needed)
    └── medicore-prototype.html
```

## 🎯 Quick Start

### Option 1: HTML Prototype (Instant Demo)
1. Open `prototype/medicore-prototype.html` in Chrome
2. Press F12 → Toggle Device Toolbar (Ctrl+Shift+M)
3. Select "iPad Pro" and rotate to landscape
4. Start testing!

### Option 2: Full Stack Application

#### Backend Setup
```bash
cd backend
dotnet restore
dotnet ef database update
dotnet run
```

#### Frontend Setup
```bash
cd frontend
npm install
npm run dev
```

## 🛠️ Technology Stack

**Backend:**
- ASP.NET Core 8.0
- Entity Framework Core
- PostgreSQL
- JWT Authentication

**Frontend:**
- React 18
- Vite
- PWA Support
- Axios

## 📱 iPad Testing

Use Chrome DevTools to simulate iPad:
1. Open Chrome DevTools (F12)
2. Toggle Device Toolbar (Ctrl+Shift+M)
3. Select iPad Pro (1366 x 1024)
4. Rotate to Landscape mode

## 🔒 Security & Compliance

- HIPAA compliant architecture
- GDPR data protection
- AES-256 encryption
- Role-based access control
- Audit logging

## 📄 License

Proprietary - Dr. Prakash's Clinic

## 👨‍💻 Development

Built for Dr. Prakash's clinic to replace slow legacy EMR systems with a fast, modern, iPad-optimized solution.
