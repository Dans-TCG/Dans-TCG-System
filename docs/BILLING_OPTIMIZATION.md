# Billing & Usage Optimization – Dans TCG System

This document maps your current GitHub Enterprise budgets to practical actions and guard‑rails, and lists ways to leverage Enterprise & Copilot features efficiently.

## Current Budgets (Monthly)
- Git LFS: $10
- GitHub Advanced Security (GHAS): $0 (not purchasing usage)
- Copilot: $100
- Actions: $25
- Codespaces: $50
- Packages: $15

> Adjust numbers here if budgets change; automation can later alert when projected spend > 80%.

## 1. GitHub Actions ($25)
Typical .NET + React build (no tests) on ubuntu-latest: ~2–4 minutes/job.

Recommendations:
1. Collapse duplicate backend publish steps: reuse build outputs rather than re‑publishing in multiple workflows.
2. Enable job-level concurrency to prevent overlapping deploys:
   ```yaml
   concurrency:
     group: backend-deploy
     cancel-in-progress: true
   ```
3. Cache dependencies (already caching NuGet; add Node cache in frontend CI):
   ```yaml
   - uses: actions/cache@v4
     with:
       path: ~/.npm
       key: ${{ runner.os }}-npm-${{ hashFiles('frontend/package-lock.json') }}
       restore-keys: ${{ runner.os }}-npm-
   ```
4. Restrict triggers using `paths:` (already applied) to avoid full pipeline on doc‑only changes.
5. Set artifact retention short (e.g., 7 days) via repo settings to reduce storage costs.
6. Pin action SHAs or use `@vX` (already using major pins) for reproducibility/security.

## 2. Codespaces ($50)
Assuming single developer currently; risk of idle spend is highest cost driver.

Recommendations:
1. Set org policy: auto-stop after 30 minutes idle; retention 24h.
2. Prefer default 2-core machine unless a heavy migration or bulk import requires more.
3. Use prebuilds only if startup time becomes a pain (prebuilds also consume minutes).
4. Add ‘postCreateCommand’ to warm caches (NuGet, npm) locally rather than extending active dev time.
5. Consider devcontainer feature `ghcr.io/devcontainers/features/docker-in-docker` only when needed (saves resources otherwise).

## 3. Copilot Enterprise ($100)
Leverage org features:
1. **Code Referencing**: Ensure private code referencing is enabled; classify sensitive repos if any future finance/tax modules require restricted suggestions.
2. **Policies**: Create org policy to disallow public suggestion matching (optional) if IP hygiene becomes important.
3. **Knowledge Bases** (when available): Index `/docs/` for retrieval to speed internal Q&A.
4. Add short doc in `/docs/COPILOT_USAGE.md` summarizing best prompts for .NET + React + Azure.

## 4. Git LFS ($10)
Currently no large binary assets committed. Preempt overspend:
1. Add `.gitattributes` template but do not track until you actually add images/scans.
2. If card image originals are large, store originals in Blob Storage; commit only thumbnails or metadata.
3. Configure branch protection to block >20MB file pushes with a lightweight pre-receive (if using GitHub Enterprise Cloud, use repo rulesets with file size patterns).

## 5. Packages ($15)
If you later publish internal NuGet/npm packages:
1. Set retention policy (e.g., keep last 5 versions) to control storage.
2. Use scoped package names (`@danstcg/<pkg>` for npm).

## 6. GHAS ($0 Budget)
Even without paid GHAS usage, free security features still help:
1. Enable Dependabot alerts & updates (security + version bumps).
2. Enable Secret Scanning (standard) & Push Protection (blocks accidental credential commits).
3. Optional: Actions workflow for `dotnet list package --vulnerable` in nightly schedule.

If you later allocate GHAS budget, first enable CodeQL with a language matrix for C# & JavaScript.

## 7. Governance & Enterprise Features
1. **Org Rulesets**: Define a ruleset requiring status checks (backend + frontend CI) on `main`.
2. **Branch Protection**: Require linear history, signed commits optionally (cost neutral, integrity boost).
3. **Audit Logs**: Periodically export for retention (S3 or Azure Blob) if compliance needed.
4. **SAML/SSO (if configured)**: Ensure enforcement so personal tokens align with enterprise access controls.

## 8. Observability Tie‑In
Add an Actions step to push deployment metadata (commit, build number) to Application Insights via custom event. This helps correlate performance regressions to deployments.

## 9. Cost Monitoring Enhancements
Create optional workflow `.github/workflows/budget-check.yml`:
1. Nightly scheduled run hits GitHub API for usage metrics.
2. Compares projected usage against thresholds (80% / 100%).
3. Opens/updates an issue if threshold exceeded.

## 10. Suggested Next Small Improvements
- Add Node package cache in frontend build workflow.
- Add concurrency groups to deploy workflow.
- Add Dependabot config file (`.github/dependabot.yml`).
- Add secret scanning & push protection verification checklist to `RUNBOOK.md`.

---
*Update this file whenever budgets or policies change to keep operational practice aligned.*