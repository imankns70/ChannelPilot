# Architecture

ChannelPilot uses a layered .NET backend:

- **ChannelPilot.Domain** contains core entities, enums, and domain concepts. It has no project dependencies and must not contain database, Telegram, HTTP, scraping, AI, or other infrastructure code.
- **ChannelPilot.Application** contains use cases, application services, interfaces, DTOs, and orchestration. It depends only on Domain.
- **ChannelPilot.Infrastructure** implements external concerns such as persistence, Telegram, source readers, AI access, logging, and the system clock. It depends on Application and Domain.
- **ChannelPilot.Worker** hosts future background collection, scheduling, and queue-processing jobs. It depends on Application and Infrastructure.
- **ChannelPilot.Api** hosts future administrative HTTP endpoints. It depends on Application and Infrastructure.
- **ChannelPilot.UnitTests** is the initial unit test project for Domain and Application behavior.

## Backend and frontend separation

The backend and frontend are separated so publishing workflows and integrations can evolve independently from their administrative interface. The `frontend` directory is intentionally reserved for a future admin panel; no frontend framework is installed yet.

## Current domain model

The initial framework-independent Domain model has been added. It covers managed Telegram channels, general and channel-specific content, novels and episodes, publishing plans, the publishing queue, and published-post records. Application behavior and infrastructure implementations remain separate and are not implemented yet.

## Current application layer

The initial Application layer defines framework-independent boundaries for persistence, content sources, Telegram publishing, AI analysis, and time. It also contains simple DTOs and use-case skeletons for channel matching, content processing, football and trend preparation, novel splitting and scheduling, and publishing flow checks. These types describe the intended boundaries only; external implementations and full orchestration are deferred to Infrastructure and later phases.

## Current infrastructure boundary

Infrastructure now provides temporary in-memory repositories, a system clock, and fake Telegram, source-reader, and AI adapters for local composition. `AddChannelPilotInfrastructure` is the single dependency-injection boundary used by both API and Worker. These placeholders make the dependency graph executable without adding databases, external SDKs, network integrations, or durable behavior.

## Current API boundary

The API now exposes controller and contract skeletons for the future admin panel: channels, content, football news, trend review, novels and episode import, publishing plans, and the publishing queue/history. Controllers return response contracts and deterministic placeholder data. Novel import reuses the Application episode splitter, but no endpoint performs durable persistence, real AI analysis, source collection, or Telegram publishing. Authentication is also deferred.

## Current worker boundary

The Worker host now registers four configurable `BackgroundService` skeletons for football collection, trend collection, nightly novel due checks, and publishing-queue processing. Each worker runs a logging-only job through a cancellation-aware delay loop and isolates job exceptions. The built-in host configuration controls enablement and intervals; no external scheduler, real collection pipeline, persistence, or publishing behavior is present.

## Product rules and scoring

The initial editorial rules are documented under [`docs/rules`](rules/). They define the event-based Football policy, quality-gated Trend policy, scheduled Novel policy, starting score bands and weights, and the publishing queue as the mandatory gate before Telegram. These documents are decision inputs for later Domain and Application refinement; they do not change runtime behavior yet.

## Persistence provider

SQL Server is the chosen persistence provider for both development and production. EF Core persistence is implemented within `ChannelPilot.Infrastructure`; migrations remain a later step. See [Persistence Strategy](persistence-strategy.md).

`AppDbContext`, Fluent API configurations, and SQL Server EF repositories now live under `ChannelPilot.Infrastructure/Persistence`. API and Worker provide configuration to the shared Infrastructure registration, and EF repositories are the default persistence implementations.

EF Core migrations are stored in the Infrastructure project under `ChannelPilot.Infrastructure/Migrations`. The first `InitialCreate` migration has been applied successfully to the development SQL Server LocalDB database.

LocalDB records the initial migration verification only. The current development database target is `Server=.` with SQL authentication, and its real connection string is supplied through user-secrets rather than committed configuration.

`InitialCreate` has also been applied successfully to the current `Server=.` development target, and the expected schema tables were verified there.

Idempotent seed logic lives under `ChannelPilot.Infrastructure/Persistence/Seeding`. API startup invokes it in Development only, and its scope is limited to the three managed channel records; see [Initial Seed Strategy](seed-strategy.md).

## Content source management

The Content Source Management API is EF-backed through a dedicated Application service and repository boundary. `api/content-sources` supports listing, lookup, creation, update, activation, and disabling of source configuration records. It does not collect content or implement RSS, HTML, or browser-based reading.

## Planned CommodityRadar module

[CommodityRadar](commodity-radar-integration.md) is planned as an independent future commodity and economic market-intelligence module inside ChannelPilot. It may reuse shared source management, workers, approvals, publishing queue, Telegram, API, and admin-panel infrastructure, but its price observations, normalization, market signals, and alerts must remain separate from Football, Trend, and Novel logic.

The [CommodityRadar Domain Design](commodity-radar-domain-design.md) now defines proposed entities and a data-quality-first MVP slice. The module remains unimplemented pending review and approval.
