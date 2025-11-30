# 🚀 Sprint 1: MVP Foundation (Days 1-12)

## Goal: Deliver working EMR for daily OPD use

---

## ✅ **COMPLETED TASKS**

### Day 1: Planning & Setup
- [x] Create development plan
- [x] Design database schema
- [x] Update README
- [x] Create project structure
- [x] Setup GitHub repository
- [x] Deploy prototype to Vercel

---

## 📋 **PENDING TASKS**

### Day 2-3: Backend Foundation
- [ ] Setup ASP.NET Core project structure
- [ ] Configure PostgreSQL connection
- [ ] Implement authentication (JWT)
- [ ] Create base entities and repositories
- [ ] Setup dependency injection
- [ ] Configure CORS
- [ ] Add Swagger documentation
- [ ] Deploy backend to Railway

**Deliverables:**
- Working API with authentication
- Database deployed on Railway
- Swagger docs accessible

---

### Day 4-5: Patient Management API
- [ ] Create Patient entity and DTOs
- [ ] Implement Patient CRUD endpoints
  - POST /api/patients (Create)
  - GET /api/patients (List with pagination)
  - GET /api/patients/{id} (Get by ID)
  - PUT /api/patients/{id} (Update)
  - DELETE /api/patients/{id} (Soft delete)
  - GET /api/patients/search (Search by name/phone)
- [ ] Add validation
- [ ] Write unit tests
- [ ] Test with Postman

**Deliverables:**
- Patient API fully functional
- 90%+ test coverage

---

### Day 6-7: Visit & Prescription API
- [ ] Create Visit entity and DTOs
- [ ] Implement Visit CRUD endpoints
- [ ] Create Prescription entity and DTOs
- [ ] Implement Prescription endpoints
- [ ] Create PrescriptionItem entity
- [ ] Link visits with prescriptions
- [ ] Add medicine master data
- [ ] Write unit tests

**Deliverables:**
- Visit API functional
- Prescription API functional
- Medicine database seeded

---

### Day 8-9: Billing API
- [ ] Create Bill entity and DTOs
- [ ] Implement Bill CRUD endpoints
- [ ] Create BillItem entity
- [ ] Implement Payment endpoints
- [ ] Add calculation logic (tax, discount)
- [ ] Generate bill numbers
- [ ] Write unit tests

**Deliverables:**
- Billing API functional
- Payment tracking working

---

### Day 10-12: Frontend Foundation
- [ ] Setup Next.js project
- [ ] Configure Tailwind CSS
- [ ] Setup shadcn/ui components
- [ ] Create authentication pages (Login/Register)
- [ ] Implement JWT token management
- [ ] Create main layout with sidebar
- [ ] Setup routing
- [ ] Configure API client (Axios)
- [ ] Add loading states
- [ ] Add error handling

**Deliverables:**
- Login/Register working
- Main layout complete
- API integration ready

---

## 🎯 **Sprint 1 Success Criteria**

By end of Day 12:
✅ Backend API deployed and accessible
✅ Database schema implemented
✅ Authentication working
✅ Patient, Visit, Prescription, Billing APIs functional
✅ Frontend foundation ready
✅ All APIs tested and documented

---

## 📊 **Progress Tracking**

### Overall Progress: 10%
- [x] Planning (100%)
- [x] Database Design (100%)
- [ ] Backend API (0%)
- [ ] Frontend Foundation (0%)

### Daily Standup Format:
**Yesterday:** What was completed
**Today:** What will be done
**Blockers:** Any issues

---

## 🚀 **Next Sprint Preview**

### Sprint 2 (Days 13-18): Core UI Components
- Patient registration form
- Patient list and search
- Visit/Case sheet form
- Prescription generator
- Billing form
- Dashboard

---

## 📝 **Notes**

### Priority Order:
1. **Authentication** - Must work first
2. **Patient Management** - Core functionality
3. **Visit & Prescription** - Main workflow
4. **Billing** - Revenue tracking
5. **Frontend** - User interface

### Technical Decisions:
- **Database:** PostgreSQL on Railway
- **Backend:** ASP.NET Core on Railway
- **Frontend:** Next.js on Vercel
- **Authentication:** JWT tokens
- **API Style:** RESTful

### Code Quality Standards:
- All APIs must have unit tests
- Code coverage > 80%
- Follow Clean Architecture
- Use async/await
- Proper error handling
- Logging for all operations

---

## 🔗 **Resources**

### Documentation:
- [ASP.NET Core Docs](https://docs.microsoft.com/aspnet/core)
- [Next.js Docs](https://nextjs.org/docs)
- [PostgreSQL Docs](https://www.postgresql.org/docs/)
- [Tailwind CSS Docs](https://tailwindcss.com/docs)

### Tools:
- [Postman](https://www.postman.com/) - API testing
- [DBeaver](https://dbeaver.io/) - Database management
- [VS Code](https://code.visualstudio.com/) - Code editor
- [GitHub](https://github.com/) - Version control

---

## 🎉 **Let's Build!**

**Start Date:** December 1, 2025
**Target Completion:** December 12, 2025
**Daily Commitment:** 8-10 hours

**Remember:** Ship fast, iterate quickly, get feedback early!

🚀 **Speed is key in the AI era!**
