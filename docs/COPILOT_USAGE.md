# Copilot Usage Guide – Dans TCG System

## Goals
Consistent, secure, and efficient use of GitHub Copilot Enterprise across backend (.NET), frontend (React/TS), and infrastructure (Azure) tasks.

## Prompt Patterns
### Backend (.NET)
Use explicit intent + constraints:
```
Generate a C# service class `CardPriceService` that fetches a price snapshot from Cosmos (Mongo API) given cardId. It should:
- Accept an interface `ICardPriceProvider`
- Return null gracefully if not found
- Log at Information when cache miss
```

### Frontend (React + TypeScript)
```
Refactor this component to:
- Split data fetching to a custom hook useCardDetails(cardId)
- Add error + loading states
- Keep existing prop contract
```

### Tests
```
Create xUnit tests for InventoryAdjuster.AdjustQuantity covering:
- Increase
- Decrease
- Negative result (should throw)
```

### Azure Infra / Ops
```
Draft a Bicep module for an Azure Storage account with soft delete and lifecycle rules for cold storage after 30 days.
```

## Good Practices
1. Always review generated code for secrets or incorrect license headers.
2. Prefer small iterative prompts over one huge request.
3. Include domain context (MTG card, inventory, order pipeline) to improve relevance.
4. Ask for edge cases explicitly.
5. For security-sensitive code (auth, crypto), request “explain each line” after generation.

## Avoid
1. Pasting production secrets or connection strings.
2. Accepting code without reading for data validation / error handling.
3. Relying on Copilot for regulatory (tax/BAS) logic without manual verification.

## Enterprise Features
- Code referencing: confirm sensitive repos classification if added later.
- Chat: Use “/tests” or “/fix” commands (when available) to accelerate common workflows.
- Knowledge base (future): index `/docs/` folder for internal onboarding Q&A.

## Metrics (Optional Future)
- Track acceptance rate: # suggestions accepted / total shown.
- Maintain a weekly “Copilot wins” log to refine prompting patterns.

---
Update this guide as the codebase and team evolve.