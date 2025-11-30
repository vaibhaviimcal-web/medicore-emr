# 🚀 MediCore EMR - Deployment Guide

Complete guide to deploy backend API to Railway and frontend to Vercel.

---

## 📋 **Prerequisites**

- Railway account (https://railway.app)
- Vercel account (https://vercel.com)
- GitHub repository access
- .NET 8.0 SDK (for local testing)
- Node.js 18+ (for frontend)

---

## 🗄️ **Step 1: Deploy Database to Railway (10 minutes)**

### **1.1 Create Railway Project**
```bash
# Install Railway CLI
npm install -g @railway/cli

# Login
railway login

# Create new project
railway init
```

### **1.2 Add PostgreSQL Database**
1. Go to https://railway.app/dashboard
2. Click "New Project"
3. Select "Provision PostgreSQL"
4. Wait for database to be created

### **1.3 Get Connection String**
1. Click on PostgreSQL service
2. Go to "Connect" tab
3. Copy "Postgres Connection URL"
4. Format: `postgresql://user:password@host:port/database`

### **1.4 Run Database Schema**
```bash
# Connect to Railway database
psql <your_railway_connection_string>

# Run schema
\i database/schema.sql

# Verify tables created
\dt

# Exit
\q
```

**Expected Output:**
```
List of relations
 Schema |        Name         | Type  |  Owner
--------+---------------------+-------+---------
 public | users               | table | postgres
 public | patients            | table | postgres
 public | visits              | table | postgres
 public | prescriptions       | table | postgres
 public | bills               | table | postgres
 public | appointments        | table | postgres
 public | medicines           | table | postgres
(12 rows)
```

---

## 🔧 **Step 2: Deploy Backend API to Railway (15 minutes)**

### **2.1 Prepare Backend for Deployment**

Update `backend/src/MediCore.API/appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "${{DATABASE_URL}}"
  },
  "JWT": {
    "Secret": "${{JWT_SECRET}}",
    "Issuer": "MediCore",
    "Audience": "MediCore",
    "ExpiryMinutes": 1440
  },
  "Cors": {
    "AllowedOrigins": [
      "https://medicore-emr.vercel.app",
      "http://localhost:3000"
    ]
  }
}
```

### **2.2 Create Railway Service**
```bash
cd backend

# Link to Railway project
railway link

# Add environment variables
railway variables set DATABASE_URL=<your_railway_postgres_url>
railway variables set JWT_SECRET=<generate_random_32_char_string>

# Deploy
railway up
```

### **2.3 Configure Railway Service**
1. Go to Railway dashboard
2. Click on your service
3. Go to "Settings"
4. Set:
   - **Start Command:** `dotnet run --project src/MediCore.API/MediCore.API.csproj`
   - **Root Directory:** `backend`
   - **Build Command:** `dotnet publish src/MediCore.API/MediCore.API.csproj -c Release -o out`

### **2.4 Get API URL**
1. Go to "Settings" tab
2. Click "Generate Domain"
3. Copy the URL (e.g., `https://medicore-api.up.railway.app`)

### **2.5 Test API**
```bash
# Health check
curl https://your-api-url.railway.app/health

# Expected response:
# {"status":"healthy","timestamp":"2025-12-01T..."}

# Test Swagger
# Open: https://your-api-url.railway.app/swagger
```

---

## 🎨 **Step 3: Deploy Frontend to Vercel (10 minutes)**

### **3.1 Create Frontend Project** (if not done)
```bash
cd frontend

# Create Next.js app
npx create-next-app@latest . --typescript --tailwind --app --src-dir

# Install dependencies
npm install axios react-query zustand
```

### **3.2 Configure Environment Variables**

Create `frontend/.env.local`:
```env
NEXT_PUBLIC_API_URL=https://your-api-url.railway.app
NEXT_PUBLIC_APP_NAME=MediCore EMR
```

### **3.3 Deploy to Vercel**
```bash
cd frontend

# Install Vercel CLI
npm install -g vercel

# Login
vercel login

# Deploy
vercel --prod
```

### **3.4 Configure Vercel**
1. Go to https://vercel.com/dashboard
2. Select your project
3. Go to "Settings" → "Environment Variables"
4. Add:
   - `NEXT_PUBLIC_API_URL` = `https://your-api-url.railway.app`

### **3.5 Redeploy**
```bash
vercel --prod
```

---

## ✅ **Step 4: Verify Deployment**

### **4.1 Test Backend**
```bash
# Health check
curl https://your-api-url.railway.app/health

# Register user
curl -X POST https://your-api-url.railway.app/api/auth/register \
  -H "Content-Type: application/json" \
  -d '{
    "email": "test@example.com",
    "password": "Test@123",
    "fullName": "Test User",
    "role": "doctor"
  }'

# Login
curl -X POST https://your-api-url.railway.app/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{
    "email": "test@example.com",
    "password": "Test@123"
  }'
```

### **4.2 Test Frontend**
1. Open: `https://your-app.vercel.app`
2. Verify it loads
3. Test API connection

---

## 🔒 **Step 5: Security Configuration**

### **5.1 Update CORS**
In `backend/src/MediCore.API/appsettings.json`:
```json
{
  "Cors": {
    "AllowedOrigins": [
      "https://medicore-emr.vercel.app",
      "https://your-custom-domain.com"
    ]
  }
}
```

### **5.2 Generate Strong JWT Secret**
```bash
# Generate random 32-character string
openssl rand -base64 32

# Update in Railway
railway variables set JWT_SECRET=<generated_secret>
```

### **5.3 Enable HTTPS Only**
Railway automatically provides HTTPS. Ensure:
- `UseHttpsRedirection()` is enabled in Program.cs ✅
- All API calls use HTTPS ✅

---

## 📊 **Step 6: Monitoring & Logs**

### **6.1 View Railway Logs**
```bash
# Real-time logs
railway logs

# Or view in dashboard
# https://railway.app/dashboard → Your Service → Logs
```

### **6.2 View Vercel Logs**
```bash
# Real-time logs
vercel logs

# Or view in dashboard
# https://vercel.com/dashboard → Your Project → Logs
```

---

## 🐛 **Troubleshooting**

### **Database Connection Error**
```bash
# Test connection
psql <your_railway_connection_string>

# Check environment variable
railway variables

# Restart service
railway restart
```

### **API Not Responding**
```bash
# Check logs
railway logs

# Check service status
railway status

# Redeploy
railway up
```

### **CORS Error**
1. Check `appsettings.json` has correct frontend URL
2. Verify `UseCors("AllowFrontend")` in Program.cs
3. Redeploy backend

### **JWT Error**
1. Verify JWT_SECRET is set in Railway
2. Check token expiry time
3. Ensure secret is at least 32 characters

---

## 🔄 **Continuous Deployment**

### **Backend (Railway)**
Railway automatically deploys on git push:
```bash
git add .
git commit -m "Update backend"
git push origin main
# Railway auto-deploys
```

### **Frontend (Vercel)**
Vercel automatically deploys on git push:
```bash
git add .
git commit -m "Update frontend"
git push origin main
# Vercel auto-deploys
```

---

## 📝 **Environment Variables Checklist**

### **Railway (Backend)**
- [x] `DATABASE_URL` - PostgreSQL connection string
- [x] `JWT_SECRET` - 32+ character random string
- [x] `ASPNETCORE_ENVIRONMENT` - Production

### **Vercel (Frontend)**
- [x] `NEXT_PUBLIC_API_URL` - Railway API URL
- [x] `NEXT_PUBLIC_APP_NAME` - MediCore EMR

---

## 🎉 **Deployment Complete!**

Your MediCore EMR is now live:
- **Backend API:** https://your-api.railway.app
- **Swagger Docs:** https://your-api.railway.app/swagger
- **Frontend:** https://your-app.vercel.app

---

## 📞 **Support**

If you encounter issues:
1. Check logs (Railway/Vercel)
2. Verify environment variables
3. Test database connection
4. Review CORS configuration

---

**Next Steps:**
1. Test authentication flow
2. Create first patient
3. Generate prescription
4. Share with Dr. Prakash!

🚀 **Happy Deploying!**
