# Persistence Strategy

## Decision

ChannelPilot will use SQL Server for both development and production.

## Rationale

- The project owner is already experienced with SQL Server.
- Development and production will remain aligned.
- SQL Server is suitable for publishing queues, content history, logs, retry tracking, channel configuration, and admin reporting.
- Using it from the beginning avoids switching database providers later.
- It supports future reporting and operational queries well.

## ORM

EF Core SQL Server infrastructure has been added. The `AppDbContext`, Fluent API entity configurations, and EF repository implementations live in `ChannelPilot.Infrastructure`. The `InitialCreate` migration has been created and successfully applied to the development LocalDB database.

## Persistence structure

Persistence is implemented under:

`backend/src/ChannelPilot.Infrastructure/Persistence/`

Current and planned structure:

- `AppDbContext.cs`
- `Configurations/`
- `Repositories/`
- `Migrations/`

## Connection strings

- Development connection strings should be stored in `appsettings.Development.json` or user-secrets.
- Production connection strings should come from environment variables or deployment secrets.
- Real credentials must never be committed to the repository.
- The current development target is the default SQL Server instance (`Server=.`) using SQL authentication, with credentials stored in user-secrets.
- SQL Server LocalDB was used only to verify the first migration and initial schema creation before the current development target was configured.

## Initial database scope

The first SQL Server schema should cover:

- `TelegramChannels`
- `ContentSources`
- `ContentItems`
- `FootballNewsItems`
- `TrendNewsItems`
- `TrendSignals`
- `Novels`
- `NovelEpisodes`
- `NovelPublishingPlans`
- `PublishQueueItems`
- `PublishedPosts`

## Not included yet

- No SQL scripts.
- No production deployment configuration.

## Migration status

- First migration: `InitialCreate`.
- Initial database creation: previously verified against SQL Server LocalDB.
- Current development target: `Server=.` using SQL authentication through user-secrets; migration application and schema creation have been verified.
- Expected application tables: verified.
- Seed data: not added.

Idempotent database seeding is implemented for the three default managed channels and runs from API startup in Development only. See [Initial Seed Strategy](seed-strategy.md).
