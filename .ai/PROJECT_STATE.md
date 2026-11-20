# Project state

- **Completed phase:** Real EF-backed Content Source Management API.
- **Current goal:** Review and approve CommodityRadar MVP Slice 1 before implementation.
- **Implemented:** Domain and Application foundations, API and Worker skeletons, SQL Server `AppDbContext`, Fluent API configurations, EF repositories, Infrastructure DI registration, and the applied `InitialCreate` migration.
- **Persistence decision:** SQL Server for development and production using EF Core.
- **Database update:** `InitialCreate` succeeded against the current `Server=.` SQL Server development target using user-secrets; all expected initial tables were verified. LocalDB remains only the earlier verification environment.
- **Seed implementation:** Development API startup creates only missing channel types, uses non-routable placeholders, and does not overwrite existing channels.
- **Management APIs:** Channel and Content Source management now use EF Core repositories and SQL Server.
- **CommodityRadar:** Integration decision and Domain design are documented. MVP Slice 1 proposes item, variant, source, raw observation, and normalized record only; no implementation exists yet.
- **Not implemented yet:** Production seeding, real Telegram identifiers, production database configuration, authentication, Telegram integration, AI integration, scraping, real job pipelines, real publishing orchestration, and frontend.
