# 🚀 Quick Start Development Guide

## Get Started in 15 Minutes!

---

## 📋 **Prerequisites**

Install these tools:
- [Node.js 18+](https://nodejs.org/)
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL 15+](https://www.postgresql.org/download/)
- [VS Code](https://code.visualstudio.com/)
- [Git](https://git-scm.com/)

---

## ⚡ **Step 1: Clone Repository (1 min)**

```bash
git clone https://github.com/vaibhaviimcal-web/medicore-emr.git
cd medicore-emr
```

---

## 🗄️ **Step 2: Setup Database (3 min)**

### **Option A: Local PostgreSQL**
```bash
# Create database
createdb medicore_emr

# Run schema
psql -d medicore_emr -f database/schema.sql
```

### **Option B: Railway (Recommended)**
1. Go to [Railway.app](https://railway.app)
2. Create new project
3. Add PostgreSQL
4. Copy connection string
5. Run schema using Railway CLI or web console

---

## 🔧 **Step 3: Setup Backend (5 min)**

```bash
cd backend

# Create new ASP.NET Core Web API project
dotnet new webapi -n MediCore.API
cd MediCore.API

# Add required packages
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add package Swashbuckle.AspNetCore
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection

# Create appsettings.json
cat > appsettings.json << 'EOF'
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=medicore_emr;Username=postgres;Password=your_password"
  },
  "JWT": {
    "Secret": "YourSuperSecretKeyThatIsAtLeast32CharactersLong!",
    "Issuer": "MediCore",
    "Audience": "MediCore",
    "ExpiryMinutes": 1440
  },
  "Cors": {
    "AllowedOrigins": ["http://localhost:3000", "https://medicore-emr.vercel.app"]
  }
}
EOF

# Run backend
dotnet run
```

Backend will run on: `http://localhost:5000`
Swagger docs: `http://localhost:5000/swagger`

---

## 🎨 **Step 4: Setup Frontend (5 min)**

```bash
cd frontend

# Create Next.js app with TypeScript and Tailwind
npx create-next-app@latest . --typescript --tailwind --app --src-dir --import-alias "@/*"

# Install additional dependencies
npm install axios react-query zustand react-hook-form zod @hookform/resolvers
npm install lucide-react date-fns
npm install -D @types/node

# Create .env.local
cat > .env.local << 'EOF'
NEXT_PUBLIC_API_URL=http://localhost:5000
NEXT_PUBLIC_APP_NAME=MediCore EMR
EOF

# Run frontend
npm run dev
```

Frontend will run on: `http://localhost:3000`

---

## 🧪 **Step 5: Verify Setup (1 min)**

### **Test Backend:**
```bash
curl http://localhost:5000/api/health
```

Should return: `{"status": "healthy"}`

### **Test Frontend:**
Open browser: `http://localhost:3000`

Should see Next.js welcome page.

---

## 📝 **Next Steps**

### **Day 2-3: Build Authentication**

1. **Backend:**
   - Create User entity
   - Implement JWT authentication
   - Add login/register endpoints

2. **Frontend:**
   - Create login page
   - Create register page
   - Implement token management

### **Day 4-5: Build Patient Management**

1. **Backend:**
   - Create Patient entity
   - Implement CRUD endpoints
   - Add search functionality

2. **Frontend:**
   - Create patient list page
   - Create patient registration form
   - Create patient detail page

---

## 🛠️ **Development Workflow**

### **Daily Routine:**
```bash
# Morning: Pull latest changes
git pull origin main

# Work on feature
git checkout -b feature/patient-management

# Commit frequently
git add .
git commit -m "Add patient registration form"

# Push to GitHub
git push origin feature/patient-management

# Create Pull Request on GitHub
```

### **Testing:**
```bash
# Backend tests
cd backend
dotnet test

# Frontend tests
cd frontend
npm test
```

### **Deployment:**
```bash
# Frontend (Vercel)
cd frontend
vercel --prod

# Backend (Railway)
cd backend
railway up
```

---

## 📚 **Useful Commands**

### **Backend:**
```bash
# Create new migration
dotnet ef migrations add InitialCreate

# Update database
dotnet ef database update

# Run with watch (auto-reload)
dotnet watch run

# Build for production
dotnet publish -c Release
```

### **Frontend:**
```bash
# Development mode
npm run dev

# Build for production
npm run build

# Start production server
npm start

# Lint code
npm run lint
```

### **Database:**
```bash
# Connect to database
psql -d medicore_emr

# List tables
\dt

# Describe table
\d patients

# Run query
SELECT * FROM patients LIMIT 10;

# Backup database
pg_dump medicore_emr > backup.sql

# Restore database
psql medicore_emr < backup.sql
```

---

## 🐛 **Troubleshooting**

### **Backend won't start:**
```bash
# Check .NET version
dotnet --version

# Clean and rebuild
dotnet clean
dotnet build

# Check port availability
netstat -an | grep 5000
```

### **Frontend won't start:**
```bash
# Clear node_modules
rm -rf node_modules package-lock.json
npm install

# Check Node version
node --version

# Check port availability
lsof -i :3000
```

### **Database connection error:**
```bash
# Check PostgreSQL is running
pg_isready

# Check connection string
psql "Host=localhost;Database=medicore_emr;Username=postgres;Password=your_password"

# Restart PostgreSQL
# macOS: brew services restart postgresql
# Linux: sudo systemctl restart postgresql
# Windows: net stop postgresql-x64-15 && net start postgresql-x64-15
```

---

## 📖 **Resources**

### **Documentation:**
- [ASP.NET Core](https://docs.microsoft.com/aspnet/core)
- [Next.js](https://nextjs.org/docs)
- [PostgreSQL](https://www.postgresql.org/docs/)
- [Tailwind CSS](https://tailwindcss.com/docs)

### **Tutorials:**
- [Clean Architecture in ASP.NET Core](https://www.youtube.com/watch?v=dK4Yb6-LxAk)
- [Next.js Full Course](https://www.youtube.com/watch?v=843nec-IvW0)
- [PostgreSQL Tutorial](https://www.postgresqltutorial.com/)

### **Tools:**
- [Postman](https://www.postman.com/) - API testing
- [DBeaver](https://dbeaver.io/) - Database GUI
- [Insomnia](https://insomnia.rest/) - API client
- [TablePlus](https://tableplus.com/) - Database GUI

---

## 🎯 **Development Checklist**

### **Before Starting:**
- [ ] All prerequisites installed
- [ ] Repository cloned
- [ ] Database created
- [ ] Backend running
- [ ] Frontend running
- [ ] Swagger accessible
- [ ] Git configured

### **Daily:**
- [ ] Pull latest changes
- [ ] Create feature branch
- [ ] Write code
- [ ] Write tests
- [ ] Commit changes
- [ ] Push to GitHub
- [ ] Create PR

### **Before Deployment:**
- [ ] All tests passing
- [ ] Code reviewed
- [ ] Documentation updated
- [ ] Environment variables set
- [ ] Database migrated
- [ ] Performance tested

---

## 🚀 **Ready to Build!**

You're all set! Start with Sprint 1 tasks:
1. Authentication
2. Patient Management
3. Visit & Prescription
4. Billing

**Remember:** Ship fast, iterate quickly, get feedback early!

---

## 💬 **Need Help?**

- **Email:** vaibhav.iimcal@gmail.com
- **GitHub Issues:** [Create Issue](https://github.com/vaibhaviimcal-web/medicore-emr/issues)

---

**Let's build something amazing! 🎉**
