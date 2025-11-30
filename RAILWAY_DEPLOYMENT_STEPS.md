# 🚀 Railway Deployment - Step by Step

**Follow these exact steps to deploy MediCore EMR to Railway**

---

## 📋 **What You'll Deploy:**
1. PostgreSQL Database
2. Backend API (.NET 8.0)

**Time Required:** 15-20 minutes

---

## 🎯 **Step 1: Create Railway Account (2 min)**

1. Go to https://railway.app
2. Click "Login" → "Login with GitHub"
3. Authorize Railway to access your GitHub
4. You'll get $5 free credit (enough for testing)

---

## 🗄️ **Step 2: Create PostgreSQL Database (3 min)**

### **2.1 Create New Project**
1. Click "New Project"
2. Select "Provision PostgreSQL"
3. Wait 30 seconds for database to provision

### **2.2 Get Database Connection String**
1. Click on the PostgreSQL service
2. Go to "Variables" tab
3. Find and copy `DATABASE_URL`
4. It looks like: `postgresql://postgres:password@host:5432/railway`

### **2.3 Run Database Schema**

**Option A: Using Railway CLI (Recommended)**
```bash
# Install Railway CLI
npm install -g @railway/cli

# Login
railway login

# Link to your project
railway link

# Connect to database
railway run psql $DATABASE_URL

# Once connected, run:
\i database/schema.sql

# Verify tables
\dt

# Exit
\q
```

**Option B: Using Web Console**
1. In Railway dashboard, click PostgreSQL service
2. Click "Data" tab
3. Click "Query"
4. Copy entire content from `database/schema.sql`
5. Paste and click "Run"

### **2.4 Verify Database**
You should see these tables:
- users
- patients
- visits
- prescriptions
- prescription_items
- bills
- bill_items
- payments
- appointments
- medicines
- lab_orders
- lab_order_items

---

## 🔧 **Step 3: Deploy Backend API (5 min)**

### **3.1 Add Backend Service**
1. In Railway dashboard, click "New"
2. Select "GitHub Repo"
3. Choose `vaibhaviimcal-web/medicore-emr`
4. Railway will detect it's a .NET project

### **3.2 Configure Service**
1. Click on the new service
2. Go to "Settings" tab
3. Set:
   - **Name:** `medicore-api`
   - **Root Directory:** `backend`
   - **Watch Paths:** `backend/**`

### **3.3 Add Environment Variables**
1. Go to "Variables" tab
2. Click "New Variable"
3. Add these variables:

```
ASPNETCORE_ENVIRONMENT=Production
ASPNETCORE_URLS=http://0.0.0.0:$PORT
```

4. Click "Add Reference" → Select PostgreSQL → `DATABASE_URL`
   - This automatically links your database

5. Add JWT Secret:
```
JWT_SECRET=YourSuperSecretKeyThatIsAtLeast32CharactersLongForProduction!
```

### **3.4 Generate Public Domain**
1. Go to "Settings" tab
2. Scroll to "Networking"
3. Click "Generate Domain"
4. Copy the URL (e.g., `https://medicore-api.up.railway.app`)

### **3.5 Deploy**
Railway will automatically deploy when you push to GitHub.

To trigger manual deployment:
1. Go to "Deployments" tab
2. Click "Deploy"

---

## ✅ **Step 4: Verify Deployment (3 min)**

### **4.1 Check Deployment Status**
1. Go to "Deployments" tab
2. Wait for "Success" status (2-3 minutes)
3. Check logs for any errors

### **4.2 Test Health Endpoint**
```bash
curl https://your-api-url.up.railway.app/health
```

**Expected Response:**
```json
{
  "status": "healthy",
  "timestamp": "2025-12-01T..."
}
```

### **4.3 Test Swagger Documentation**
Open in browser:
```
https://your-api-url.up.railway.app/swagger
```

You should see the Swagger UI with:
- Auth endpoints (Register, Login, UserExists)
- Health check endpoint

### **4.4 Test Registration**
```bash
curl -X POST https://your-api-url.up.railway.app/api/auth/register \
  -H "Content-Type: application/json" \
  -d '{
    "email": "vaibhav@medicore.com",
    "password": "Admin@123",
    "fullName": "Kumar Vaibhav",
    "role": "admin",
    "phone": "+919821919175"
  }'
```

**Expected Response:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "email": "vaibhav@medicore.com",
  "fullName": "Kumar Vaibhav",
  "role": "admin",
  "expiresAt": "2025-12-02T..."
}
```

### **4.5 Test Login**
```bash
curl -X POST https://your-api-url.up.railway.app/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{
    "email": "vaibhav@medicore.com",
    "password": "Admin@123"
  }'
```

---

## 🎉 **Step 5: Save Your URLs**

**Database URL:**
```
postgresql://postgres:...@...railway.app:5432/railway
```

**API URL:**
```
https://medicore-api.up.railway.app
```

**Swagger Docs:**
```
https://medicore-api.up.railway.app/swagger
```

---

## 🔍 **Troubleshooting**

### **Deployment Failed**
1. Check "Deployments" → "Logs"
2. Look for error messages
3. Common issues:
   - Missing environment variables
   - Database connection error
   - Build errors

### **Database Connection Error**
```bash
# Test database connection
railway run psql $DATABASE_URL

# If fails, check:
# 1. DATABASE_URL is set correctly
# 2. PostgreSQL service is running
# 3. Network connectivity
```

### **API Returns 500 Error**
1. Check logs: Railway Dashboard → Service → Logs
2. Verify environment variables are set
3. Check database connection string
4. Ensure JWT_SECRET is set

### **CORS Error**
1. Update `backend/src/MediCore.API/appsettings.json`
2. Add your frontend URL to AllowedOrigins
3. Commit and push to trigger redeploy

---

## 📊 **Monitor Your Deployment**

### **View Logs**
```bash
# Using CLI
railway logs

# Or in dashboard
# Railway → Your Service → Logs
```

### **Check Metrics**
1. Go to "Metrics" tab
2. Monitor:
   - CPU usage
   - Memory usage
   - Network traffic
   - Response times

### **View Database**
1. Click PostgreSQL service
2. Go to "Data" tab
3. Run queries to check data

---

## 🔄 **Continuous Deployment**

Railway automatically deploys when you push to GitHub:

```bash
# Make changes
git add .
git commit -m "Update API"
git push origin main

# Railway automatically:
# 1. Detects changes
# 2. Builds new image
# 3. Deploys to production
# 4. Zero downtime!
```

---

## 💰 **Cost Estimate**

**Free Tier:**
- $5 credit/month
- Enough for development/testing

**Estimated Monthly Cost:**
- PostgreSQL: ~$5/month
- Backend API: ~$5/month
- **Total: ~$10/month**

**For Production:**
- Consider upgrading to paid plan
- Better performance
- More resources

---

## 🎯 **Next Steps After Deployment**

1. ✅ Test all endpoints
2. ✅ Create admin user
3. ✅ Share API URL with frontend team
4. ✅ Update frontend environment variables
5. ✅ Test end-to-end flow
6. ✅ Demo to Dr. Prakash!

---

## 📞 **Need Help?**

**Railway Support:**
- Discord: https://discord.gg/railway
- Docs: https://docs.railway.app

**Project Issues:**
- GitHub: https://github.com/vaibhaviimcal-web/medicore-emr/issues

---

## ✅ **Deployment Checklist**

- [ ] Railway account created
- [ ] PostgreSQL database provisioned
- [ ] Database schema executed
- [ ] Backend service created
- [ ] Environment variables set
- [ ] Public domain generated
- [ ] Deployment successful
- [ ] Health check passing
- [ ] Swagger accessible
- [ ] Registration working
- [ ] Login working
- [ ] URLs saved

---

**🎉 Once all checked, your backend is LIVE!**

**API URL:** https://your-api.up.railway.app
**Swagger:** https://your-api.up.railway.app/swagger

**Share this URL with Dr. Prakash and start building the frontend!** 🚀
