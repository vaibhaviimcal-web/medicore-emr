# MediCore EMR - Complete Setup Guide

This guide will help you set up and run the MediCore EMR system.

## 📋 Table of Contents

1. [Quick Start (HTML Prototype)](#quick-start-html-prototype)
2. [Full Stack Setup](#full-stack-setup)
3. [Testing on iPad Simulator](#testing-on-ipad-simulator)
4. [Deployment](#deployment)

---

## 🚀 Quick Start (HTML Prototype)

**Fastest way to see the system - Takes 2 minutes!**

### Step 1: Download
```bash
git clone https://github.com/vaibhaviimcal-web/medicore-emr.git
cd medicore-emr/prototype
```

### Step 2: Open
- Double-click `medicore-prototype.html`
- OR open in Chrome directly

### Step 3: Test on iPad Simulator
1. Press `F12` in Chrome
2. Click Toggle Device Toolbar (`Ctrl+Shift+M`)
3. Select "iPad Pro" (1366 x 1024)
4. Rotate to Landscape
5. Start testing!

**That's it! No installation, no setup!**

---

## 🏗️ Full Stack Setup

### Prerequisites

**Backend:**
- .NET 8.0 SDK - [Download](https://dotnet.microsoft.com/download)
- PostgreSQL 14+ - [Download](https://www.postgresql.org/download/)
- Visual Studio 2022 or VS Code

**Frontend:**
- Node.js 18+ - [Download](https://nodejs.org/)
- npm (comes with Node.js)

---

### Backend Setup

#### 1. Navigate to backend folder
```bash
cd backend
```

#### 2. Restore NuGet packages
```bash
dotnet restore
```

#### 3. Update database connection
Edit `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=medicore;Username=postgres;Password=YOUR_PASSWORD"
  }
}
```

#### 4. Create database
```bash
# Create database in PostgreSQL
psql -U postgres
CREATE DATABASE medicore;
\q
```

#### 5. Run migrations
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

#### 6. Run the API
```bash
dotnet run
```

✅ **Backend running at:** `https://localhost:5001`
✅ **Swagger UI:** `https://localhost:5001/swagger`

---

### Frontend Setup

#### 1. Navigate to frontend folder
```bash
cd frontend
```

#### 2. Install dependencies
```bash
npm install
```

#### 3. Create environment file
```bash
cp .env.example .env
```

Edit `.env`:
```
VITE_API_URL=http://localhost:5000/api/v1
```

#### 4. Run development server
```bash
npm run dev
```

✅ **Frontend running at:** `http://localhost:3000`

---

## 📱 Testing on iPad Simulator

### Method 1: Chrome DevTools (Recommended)

1. **Open Chrome** and navigate to `http://localhost:3000`
2. **Open DevTools:** Press `F12` or `Ctrl+Shift+I`
3. **Toggle Device Toolbar:** Click icon or press `Ctrl+Shift+M`
4. **Select Device:** Choose "iPad Pro" from dropdown
5. **Rotate:** Click rotate icon for landscape mode
6. **Test:** Use like a real iPad!

### Method 2: Firefox Responsive Design

1. Press `Ctrl+Shift+M`
2. Enter dimensions: 1366 x 1024
3. Test features

### Supported iPad Sizes

| Device | Portrait | Landscape |
|--------|----------|-----------|
| iPad Mini | 744 x 1133 | 1133 x 744 |
| iPad Air | 820 x 1180 | 1180 x 820 |
| iPad Pro 11" | 834 x 1194 | 1194 x 834 |
| iPad Pro 12.9" | 1024 x 1366 | 1366 x 1024 |

---

## 🚀 Deployment

### Backend Deployment (Railway/Heroku/Azure)

#### Railway (Recommended - Free tier available)

1. **Create account:** [railway.app](https://railway.app)
2. **Create new project**
3. **Add PostgreSQL database**
4. **Deploy backend:**
```bash
# Install Railway CLI
npm install -g @railway/cli

# Login
railway login

# Link project
railway link

# Deploy
railway up
```

5. **Update connection string** with Railway database URL

#### Azure App Service

1. Create App Service in Azure Portal
2. Create Azure Database for PostgreSQL
3. Deploy using Visual Studio or Azure CLI
4. Update connection strings in Azure Portal

---

### Frontend Deployment (Vercel/Netlify)

#### Vercel (Recommended - Free tier)

1. **Install Vercel CLI:**
```bash
npm install -g vercel
```

2. **Deploy:**
```bash
cd frontend
vercel
```

3. **Follow prompts** and your app will be live!

#### Netlify

1. **Build the app:**
```bash
npm run build
```

2. **Deploy:**
```bash
npm install -g netlify-cli
netlify deploy --prod --dir=dist
```

---

## 🔧 Troubleshooting

### Backend Issues

**Problem:** `dotnet ef` command not found
```bash
# Solution: Install EF Core tools
dotnet tool install --global dotnet-ef
```

**Problem:** Database connection failed
- Check PostgreSQL is running
- Verify connection string
- Check firewall settings

### Frontend Issues

**Problem:** API calls failing
- Check backend is running
- Verify VITE_API_URL in `.env`
- Check CORS settings in backend

**Problem:** npm install fails
- Clear npm cache: `npm cache clean --force`
- Delete `node_modules` and `package-lock.json`
- Run `npm install` again

---

## 📞 Support

For issues or questions:
1. Check the README files in each folder
2. Review the troubleshooting section
3. Contact the development team

---

## 🎯 Next Steps

1. ✅ Test the HTML prototype
2. ✅ Set up full stack locally
3. ✅ Test on iPad simulator
4. ✅ Get feedback from Dr. Prakash
5. ✅ Deploy to production

---

## 📄 License

Proprietary - Dr. Prakash's Clinic
