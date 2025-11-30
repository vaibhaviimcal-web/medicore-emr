# MediCore EMR - Quick Start Commands

Copy-paste these commands to get started quickly!

---

## 🚀 Option 1: HTML Prototype (2 Minutes)

```bash
# Clone repository
git clone https://github.com/vaibhaviimcal-web/medicore-emr.git
cd medicore-emr/prototype

# Open medicore-prototype.html in Chrome
# Then press F12 → Ctrl+Shift+M → Select iPad Pro → Rotate to Landscape
```

**That's it! Start testing immediately!**

---

## 🏗️ Option 2: Full Stack Setup

### Backend Setup

```bash
# Navigate to backend
cd backend

# Restore packages
dotnet restore

# Update appsettings.json with your PostgreSQL credentials
# Then run migrations
dotnet ef migrations add InitialCreate
dotnet ef database update

# Run the API
dotnet run

# API will be at: https://localhost:5001
# Swagger UI: https://localhost:5001/swagger
```

### Frontend Setup (New Terminal)

```bash
# Navigate to frontend
cd frontend

# Install dependencies
npm install

# Create .env file
cp .env.example .env

# Run development server
npm run dev

# Frontend will be at: http://localhost:3000
```

---

## 🧪 Testing on iPad Simulator

### Chrome DevTools
```
1. Open http://localhost:3000 in Chrome
2. Press F12
3. Press Ctrl+Shift+M
4. Select "iPad Pro" (1366 x 1024)
5. Click rotate icon for landscape
6. Test!
```

---

## 📦 One-Command Setup (Coming Soon)

```bash
# This will set up everything automatically
./setup.sh
```

---

## 🚀 Deployment Commands

### Backend to Railway
```bash
npm install -g @railway/cli
railway login
railway link
railway up
```

### Frontend to Vercel
```bash
npm install -g vercel
cd frontend
vercel
```

---

## 🔧 Useful Commands

### Backend
```bash
# Add new migration
dotnet ef migrations add MigrationName

# Update database
dotnet ef database update

# Run tests
dotnet test

# Build for production
dotnet publish -c Release
```

### Frontend
```bash
# Install new package
npm install package-name

# Build for production
npm run build

# Preview production build
npm run preview

# Run tests
npm test
```

---

## 📊 Check Status

```bash
# Backend health check
curl https://localhost:5001/api/v1/masterdata/chief-complaints

# Frontend build
cd frontend && npm run build

# Check for updates
git pull origin main
```

---

## 🐛 Troubleshooting

### Backend not starting?
```bash
# Check .NET version
dotnet --version

# Should be 8.0 or higher
# If not, download from: https://dotnet.microsoft.com/download
```

### Frontend not starting?
```bash
# Check Node version
node --version

# Should be 18 or higher
# If not, download from: https://nodejs.org

# Clear cache and reinstall
rm -rf node_modules package-lock.json
npm install
```

### Database connection failed?
```bash
# Check PostgreSQL is running
psql -U postgres -c "SELECT version();"

# Create database if not exists
psql -U postgres -c "CREATE DATABASE medicore;"
```

---

## 📞 Need Help?

1. Check [SETUP_GUIDE.md](SETUP_GUIDE.md) for detailed instructions
2. Review [PROJECT_STATUS.md](PROJECT_STATUS.md) for current progress
3. Open an issue on GitHub
4. Contact the development team

---

## ✅ Quick Checklist

Before starting development:
- [ ] .NET 8.0 SDK installed
- [ ] Node.js 18+ installed
- [ ] PostgreSQL 14+ installed
- [ ] Git installed
- [ ] Chrome browser (for testing)
- [ ] Repository cloned
- [ ] Database created
- [ ] Environment files configured

---

**Happy Coding! 🎉**
