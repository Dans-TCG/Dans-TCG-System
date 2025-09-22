# Onboarding Checklist – Dans TCG System

Follow these steps to get started as a new contributor:

---

## 1. Prerequisites
- Install [Git](https://git-scm.com/)
- Install [.NET 7 SDK](https://dotnet.microsoft.com/download)
- Install [Node.js (LTS)](https://nodejs.org/) + npm or yarn
- Install [PostgreSQL](https://www.postgresql.org/) (local or Azure)
- (Optional) Install [Docker](https://www.docker.com/) for local DBs
- (Optional) Install [Azure CLI](https://learn.microsoft.com/en-us/cli/azure/install-azure-cli)

## 2. Clone the Repository
```bash
git clone https://github.com/your-org/dans-tcg-system.git
cd dans-tcg-system
```

## 3. Set Up Environment Variables
- Copy the example from `SETUP.md` and create a `.env` file in the root directory
- Fill in all required secrets (see `SETUP.md`)

## 4. Provision Azure Resources (if needed)
- See `SETUP.md` for required Azure resources and naming conventions

## 5. Install Dependencies
- Backend:
  ```bash
  cd backend
  dotnet restore
  ```
- Frontend:
  ```bash
  cd frontend
  npm install
  ```

## 6. Run the Application
- Backend:
  ```bash
  cd backend
  dotnet run
  ```
- Frontend:
  ```bash
  cd frontend
  npm start
  ```

## 7. Run Tests
- Backend:
  ```bash
  cd backend
  dotnet test
  ```
- Frontend:
  ```bash
  cd frontend
  npm test
  ```

## 8. Explore API Docs
- Visit `http://localhost:5000/swagger` for Swagger/OpenAPI documentation

## 9. Review Project Structure & Docs
- See `PROJECT_STRUCTURE.md` and `/docs/` for architecture, data models, and workflows

## 10. (Optional) Seed Test Data
- See `/backend/tests/` or `SETUP.md` for test data seeding scripts or conventions

---

You are now ready to contribute! See `CONTRIBUTING.md` for workflow and code style guidelines.
