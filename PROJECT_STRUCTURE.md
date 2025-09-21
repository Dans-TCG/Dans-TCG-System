# Dans TCG System – Project Structure

This document describes the recommended repository structure.

---

## Root Layout
```
dans-tcg-system/
│
├── backend/                # ASP.NET Core backend
│   ├── src/                # Application source code
│   ├── tests/              # Unit/integration tests
│   ├── DansTCG.API/        # API layer
│   ├── DansTCG.Core/       # Core business logic
│   └── DansTCG.Infrastructure/ # DB, external integrations
│
├── frontend/               # React frontend
│   ├── src/                # Components, pages, services
│   ├── public/             # Static assets
│   ├── tests/              # Unit/E2E tests
│   └── package.json
│
├── docs/                   # Documentation
│   ├── README.md
│   ├── CONTRIBUTING.md
│   ├── SETUP.md
│   ├── ROADMAP.md
│   ├── API_REFERENCE.md
│   ├── DATABASE_SCHEMA.md
│   ├── PROJECT_STRUCTURE.md
│   └── RUNBOOK.md
│
├── .github/
│   └── workflows/          # GitHub Actions CI/CD pipelines
│
├── docker-compose.yml      # Optional local dev setup
├── .gitignore
└── LICENSE
```

---

## Guidelines
- **Backend**: layered architecture (API → Core → Infrastructure)
- **Frontend**: feature-based folder structure (components grouped by feature)
- **Docs**: all project documentation lives in `/docs/`
- **CI/CD**: workflows under `.github/workflows/`
