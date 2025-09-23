# Contributing to Dans TCG System

Thank you for considering contributing to the Dans TCG System project!  
This document outlines the guidelines and workflows for contributing to ensure smooth collaboration.

---

## 📌 Development Workflow

### Branching Strategy
We use **GitHub Flow** with a `develop` branch for active work:
- `main` → stable production-ready releases  
- `develop` → ongoing integration branch  
- `feature/*` → individual features or fixes  
- `hotfix/*` → urgent fixes applied to `main`  

### Workflow
1. Fork the repository (if external) or create a branch off `develop`.  
2. Name branches clearly:  
   - `feature/inventory-sync`  
   - `bugfix/order-status-update`  
3. Open a Pull Request (PR) into `develop`.  
4. Ensure all checks/tests pass before requesting review.  
5. At least one reviewer approval is required before merging.  

---

## 🧪 Testing
- **Unit Tests** → Required for all business logic (inventory, GST, orders).  
- **Integration Tests** → Required for external APIs (eBay, AusPost).  
- **E2E Tests** → Validate key flows (e.g., add card → sell → generate invoice → record transaction).  

Run all tests locally before submitting a PR.  

---

## ⚙️ Coding Standards
### Backend (.NET / C#)
- Follow standard **.NET C# conventions**.  
- Use dependency injection where possible.  
- Expose APIs with Swagger/OpenAPI documentation.  

### Frontend (React / TypeScript)
- Use **TypeScript** for type safety.  
- Follow ESLint + Prettier rules.  
- Use functional components and hooks.  

---

## 📂 Commit Messages
Use clear, structured commit messages:  
- `feat: add inventory sync with eBay`  
- `fix: correct GST calculation on invoices`  
- `docs: update README with setup instructions`  

---


## 🚀 CI/CD Pipeline
- Every PR triggers **GitHub Actions** for build + test.  
- Code must pass linting, unit tests, and integration tests.  
- **All PRs must pass all tests and lint checks before merge.**
- Deployment to **production** is automatic from `main` (App Service for backend, SWA for frontend).  

---

## 📝 Onboarding
See `ONBOARDING.md` for a step-by-step checklist for new contributors.

---

## 📝 Documentation
- Update **README.md** for architecture changes.  
- Add API contract updates to Swagger/OpenAPI.  
- Update **runbooks** for operational changes (e.g., DB migration steps).  

---

## 📌 Code Review Checklist
Before merging a PR, check:  
- [ ] Tests written and passing  
- [ ] Code follows style guidelines  
- [ ] No secrets committed (check `.gitignore`)  
- [ ] Documentation updated  
- [ ] Branch is up to date with `develop`  

---

Thank you for contributing to Dans TCG System! 🚀
