# Initial Seed Strategy

## Implementation status

Initial channel seeding is implemented in `ChannelPilot.Infrastructure/Persistence/Seeding/DatabaseSeeder.cs`. It runs automatically from API startup in the Development environment only. It is not run automatically by Worker or in Production.

## Goal

ChannelPilot needs minimal initial data so developers can configure and test the system consistently without introducing sample content or production identifiers.

## Initial seed scope

The first seed will create only these three managed Telegram channels:

- Football Channel.
- Trend News Channel.
- Novel Channel.

It will not create content items, news analysis, novels, episodes, publishing queue items, published posts, or real content sources.

## Seeded channels

### Football Channel

| Setting | Value |
|---|---|
| Type | `Football` |
| Status | `Active` |
| RequiresApproval | `false` initially |
| MinScoreToPublish | 70 |
| MaxPostsPerDay | 40 |
| MinMinutesBetweenPosts | 10 |
| FixedPublishTime | `null` |
| TelegramChatId | Non-production placeholder only |
| Username | Placeholder only |

### Trend News Channel

| Setting | Value |
|---|---|
| Type | `Trend` |
| Status | `Active` |
| RequiresApproval | `true` |
| MinScoreToPublish | 85 |
| MaxPostsPerDay | 15 |
| MinMinutesBetweenPosts | 30 |
| FixedPublishTime | `null` |
| TelegramChatId | Non-production placeholder only |
| Username | Placeholder only |

### Novel Channel

| Setting | Value |
|---|---|
| Type | `Novel` |
| Status | `Active` |
| RequiresApproval | `false` for already approved and scheduled episodes |
| MinScoreToPublish | 0; news scoring is not relevant |
| MaxPostsPerDay | 1 |
| MinMinutesBetweenPosts | 1440, or an equivalent one-per-night policy |
| FixedPublishTime | 22:30 |
| TelegramChatId | Non-production placeholder only |
| Username | Placeholder only |

## Idempotency

Seed logic must be idempotent:

- Running it repeatedly must not duplicate channels.
- Match channels by `Type` or a stable default `Name`.
- Do not destructively overwrite an existing channel.
- Fill only missing default values when doing so is demonstrably safe.

## Environment safety

- Never commit real Telegram chat IDs.
- Real channel usernames can be configured later.
- Sensitive values belong in user-secrets, environment variables, deployment secrets, or a future admin UI.
- Seed placeholders must remain non-production values until Telegram integration is added.

## Future code location

Seed code lives under:

`backend/src/ChannelPilot.Infrastructure/Persistence/Seeding/`

Current and optional files:

- `DatabaseSeeder.cs` (implemented)
- `SeedOptions.cs`, if configuration options are needed later

## When seeding should run

API startup runs the seeder automatically in Development. Automatic production seeding is disabled and should remain avoided unless explicitly configured and designed to be safe.

## Not included yet

- No migration changes.
- No real Telegram IDs.
- No content sources.
- No sample news.
- No sample novels.
- No admin users or authentication.

The current implementation uses distinct negative numeric chat-ID placeholders because the Domain model correctly represents Telegram chat IDs as `long`. These values are non-routable placeholders, not real Telegram identifiers.
