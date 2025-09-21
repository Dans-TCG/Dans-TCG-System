# Dans TCG System – Runbook

This document provides operational runbooks for maintenance, monitoring, and recovery.

---

## 🔍 Monitoring
- Use **Azure Application Insights** for performance & error logging
- Alerts configured for:
  - API errors > 1% of requests
  - DB connection failures
  - High latency (>2s avg response time)

---

## 💾 Backups
- **Postgres**: Automated daily backups via Azure Database for PostgreSQL
- **CosmosDB**: Automatic backups configured in Azure
- **Blob Storage**: Lifecycle policies for archive/retention

Restore procedure:
1. Identify backup timestamp in Azure portal
2. Restore DB to new instance
3. Update connection strings in App Service (staging)
4. Validate before promoting to production

---

## 🚨 Incident Response
1. **Critical outage (API down)**  
   - Check Azure App Service logs  
   - Roll back to last stable deployment (swap staging → prod)  
   - Notify stakeholders  

2. **Database outage**  
   - Failover to standby (if configured) or restore from latest backup  
   - Re-run CI/CD pipeline to reconnect  

3. **Blob Storage corruption**  
   - Restore from archived snapshots  

---

## 🔑 Security
- Rotate API keys (eBay, AusPost) every 90 days
- Rotate Azure AD secrets regularly
- Review role assignments quarterly (least privilege principle)

---

## 🔄 Deployment
- PR merged into `develop` → auto deploy to **staging**
- Manual approval required for **staging → production**
- Rollback = re-deploy last successful build from GitHub Actions artifacts

---

## 📝 Maintenance Tasks
- Weekly: review logs & error alerts
- Monthly: validate BAS report exports against accountant expectations
- Quarterly: rotate secrets, review user access
