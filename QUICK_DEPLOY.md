# ⚡ Quick Deploy to Railway - 5 Minutes

**Ultra-fast deployment guide for experienced developers**

---

## 🚀 **Quick Steps**

### **1. Setup Railway (1 min)**
```bash
npm install -g @railway/cli
railway login
```

### **2. Create Project & Database (1 min)**
```bash
# In Railway dashboard:
# New Project → Provision PostgreSQL
# Copy DATABASE_URL
```

### **3. Run Schema (1 min)**
```bash
railway link
railway run psql $DATABASE_URL < database/schema.sql
```

### **4. Deploy Backend (2 min)**
```bash
cd backend

# Set variables
railway variables set ASPNETCORE_ENVIRONMENT=Production
railway variables set JWT_SECRET=YourSuperSecretKeyThatIsAtLeast32CharactersLong!

# Deploy
railway up
```

### **5. Generate Domain & Test**
```bash
# In Railway dashboard:
# Service → Settings → Generate Domain

# Test
curl https://your-api.railway.app/health
curl https://your-api.railway.app/swagger
```

---

## 🎯 **Environment Variables**

```bash
ASPNETCORE_ENVIRONMENT=Production
ASPNETCORE_URLS=http://0.0.0.0:$PORT
DATABASE_URL=<auto-linked-from-postgres>
JWT_SECRET=<generate-32-char-string>
```

---

## ✅ **Verification**

```bash
# Health check
curl https://your-api.railway.app/health

# Register user
curl -X POST https://your-api.railway.app/api/auth/register \
  -H "Content-Type: application/json" \
  -d '{"email":"test@test.com","password":"Test@123","fullName":"Test User","role":"doctor"}'

# Login
curl -X POST https://your-api.railway.app/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"email":"test@test.com","password":"Test@123"}'
```

---

## 📊 **Monitor**

```bash
# View logs
railway logs

# Check status
railway status
```

---

## 🔄 **Auto-Deploy**

```bash
git push origin main
# Railway auto-deploys!
```

---

**Done! API is live at:** `https://your-api.railway.app` 🎉
