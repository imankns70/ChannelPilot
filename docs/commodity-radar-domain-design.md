# CommodityRadar Domain Design

## 1. Purpose

CommodityRadar is a future module inside ChannelPilot for commodity and economic market intelligence. It is intended to track:

- Commodity and product prices.
- Immutable raw source observations.
- Normalized, comparable price records.
- Daily summaries and market signals.
- Purchasing-power indicators.
- Watchlists and alerts.
- Shop-owner price-review tools.
- Economic posts prepared for Telegram publishing.

This is a design document only. No CommodityRadar Domain, persistence, ingestion, API, Worker, or UI implementation exists yet.

## 2. Design Principles

- Store raw observations separately from normalized records.
- Store monetary prices explicitly in IRR; toman is a display-only conversion.
- Treat product variants as first-class concepts.
- Compare only compatible units and variants.
- Never cross-convert mass and volume without reviewed and documented product-specific density.
- Give every source explicit confidence and reliability metadata.
- Cap the confidence of single-source market data.
- Scrapers and adapters emit raw observations only.
- Keep normalization, confidence scoring, and analysis as separate stages.
- Keep CommodityRadar isolated from Football, Trend, and Novel domain logic.

## 3. Proposed Domain Folder

Expected future folder:

`backend/src/ChannelPilot.Domain/CommodityRadar/`

Expected future files:

- `CommodityItem.cs`
- `CommodityVariant.cs`
- `CommoditySource.cs`
- `RawPriceObservation.cs`
- `NormalizedPriceRecord.cs`
- `DailyPriceSummary.cs`
- `MarketSignal.cs`
- `PurchasingPowerSnapshot.cs`
- `CommodityWatchlistItem.cs`
- `CommodityAlertRule.cs`
- `CommodityAlertEvent.cs`
- `ShopProfile.cs`
- `ShopItem.cs`
- `PriceReviewSignal.cs`

These files must not be created until this design and the selected MVP slice are approved.

## 4. Core Entities

### CommodityItem

**Purpose:** Represents a canonical tracked concept such as USDT, 18k gold, chicken, rice, or milk. It is not a source-specific listing.

**Important fields:** `Id`, `Code`, `Name`, `PersianName`, `Category`, `DefaultUnit`, `IsActive`, `SortOrder`, `CreatedAt`, `UpdatedAt`.

**Important rules:** `Code` is stable and unique. The item is the canonical comparison concept; source naming differences must not create new items automatically.

**Relationships:** Has many `CommodityVariant` records and may be referenced by normalized records, summaries, signals, purchasing-power snapshots, watchlists, and shop items.

### CommodityVariant

**Purpose:** Represents one specifically comparable form of an item, such as Iranian rice per kilogram, a 30-count egg tray, or one-liter milk.

**Important fields:** `Id`, `CommodityItemId`, `Code`, `Name`, `PersianName`, `Unit`, `PackageSize`, `PackageUnit`, nullable `BrandName`, nullable `QualityGrade`, `IsDefault`, `IsActive`, `CreatedAt`, `UpdatedAt`.

**Important rules:** A variant is required whenever package size, unit, brand, or quality changes comparison meaning. `Code` is stable and unique. Only compatible variants may be compared.

**Relationships:** Belongs to one `CommodityItem`; may be referenced by normalized records, summaries, signals, purchasing-power snapshots, watchlists, and shop items.

### CommoditySource

**Purpose:** Represents an independently managed price-data source.

**Important fields:** `Id`, `Name`, nullable `BaseUrl`, `SourceKind`, `Status`, `ReliabilityScore`, `ConfidenceCap`, `RequiresReview`, nullable `LastFetchedAt`, `FailureCount`, `KillSwitchEnabled`, `CreatedAt`, `UpdatedAt`.

**Important rules:** Every source is independently disable-able and has health/failure tracking. Playwright is allowed only when reviewed direct HTTP extraction is insufficient. Sources fail closed when quality, health, or legality is uncertain.

**Relationships:** Has many `RawPriceObservation` and `NormalizedPriceRecord` records.

### RawPriceObservation

**Purpose:** Stores immutable, source-specific price data exactly as observed.

**Important fields:** `Id`, `CommoditySourceId`, `ObservedAt`, `CollectedAt`, `SourceItemName`, nullable `SourceVariantName`, `RawPrice`, `RawCurrency`, `RawUnit`, nullable `RawPayload`, nullable `SourceUrl`, nullable `ContentHash`, `IsProcessed`, nullable `ProcessingError`, `CreatedAt`.

**Important rules:** Do not mutate raw business data after ingestion; only processing state may change. Raw data is not inherently comparable. Preserve payload and source URL when available for traceability. Duplicate detection should consider source, observation time, item name, price, and hash.

**Relationships:** Belongs to one `CommoditySource`; may produce zero or one `NormalizedPriceRecord` initially.

### NormalizedPriceRecord

**Purpose:** Represents a comparable price derived from one raw observation.

**Important fields:** `Id`, `RawPriceObservationId`, `CommodityItemId`, `CommodityVariantId`, `CommoditySourceId`, `ObservedAt`, `PriceInIrr`, `Unit`, `PricePerCanonicalUnit`, `NormalizationConfidence`, `SourceReliabilityScore`, `FinalConfidenceScore`, nullable `NormalizationNotes`, `CreatedAt`.

**Important rules:** Store IRR explicitly and retain the link to raw evidence. Never normalize incompatible units. Every record carries normalization, source, and final confidence. Initially, one raw observation produces zero or one normalized record.

**Relationships:** Belongs to a raw observation, canonical item, variant, and source; contributes to daily summaries and analysis.

### DailyPriceSummary

**Purpose:** Stores a daily aggregate for one canonical item and variant.

**Important fields:** `Id`, `CommodityItemId`, `CommodityVariantId`, `Date`, `MinPriceInIrr`, `MaxPriceInIrr`, `AveragePriceInIrr`, nullable `MedianPriceInIrr`, `ObservationCount`, `SourceCount`, `ConfidenceScore`, `CreatedAt`, `UpdatedAt`.

**Important rules:** Calculate summaries from normalized records. Do not manually edit them except through an explicit admin repair process. Retain observation and source counts so consumers can judge trustworthiness.

**Relationships:** Belongs to one item and variant.

### MarketSignal

**Purpose:** Represents an explainable market event, such as a sharp increase, drop, widened source spread, new high, stale data, or low confidence.

**Important fields:** `Id`, `CommodityItemId`, nullable `CommodityVariantId`, `SignalType`, `Severity`, `Title`, `Description`, `DetectedAt`, nullable `RelatedDate`, `ConfidenceScore`, `IsPublishable`, `CreatedAt`.

**Important rules:** A signal must be explainable and evidence-backed. It may feed Telegram post generation, but sensitive or low-confidence signals must not auto-publish.

**Relationships:** Belongs to one item and optionally one variant; may later be transformed into ChannelPilot publishing content.

### PurchasingPowerSnapshot

**Purpose:** Expresses how much of a commodity can be purchased for a reference amount on a given date.

**Important fields:** `Id`, `Date`, `ReferenceAmountInIrr`, `CommodityItemId`, `CommodityVariantId`, `Quantity`, `PriceInIrr`, nullable `ChangePercent`, `CreatedAt`.

**Important rules:** Use for understandable economic explanation, not financial advice. Calculations must identify their price evidence and units.

**Relationships:** Belongs to one item and variant.

### CommodityWatchlistItem

**Purpose:** Represents a commodity or variant watched by a future user.

**Important fields:** `Id`, nullable `UserId`, nullable `TelegramUserId`, `CommodityItemId`, nullable `CommodityVariantId`, `IsActive`, `CreatedAt`, `UpdatedAt`.

**Important rules:** User identity is intentionally unresolved. Do not introduce authentication or a user system as part of the first CommodityRadar slice.

**Relationships:** Belongs to an item and optionally a variant; has many alert rules.

### CommodityAlertRule

**Purpose:** Defines an alert condition for a watchlist item.

**Important fields:** `Id`, `CommodityWatchlistItemId`, `AlertType`, `ThresholdValue`, `CooldownMinutes`, `IsActive`, `CreatedAt`, `UpdatedAt`.

**Important rules:** Threshold meaning must match the selected alert type. Cooldown and deduplication are mandatory before delivery is implemented.

**Relationships:** Belongs to one watchlist item; has many alert events.

### CommodityAlertEvent

**Purpose:** Records an evaluated alert and its delivery outcome.

**Important fields:** `Id`, `CommodityAlertRuleId`, `TriggeredAt`, `TriggerValue`, `Message`, `DeliveryStatus`, nullable `DeliveredAt`, `CreatedAt`.

**Important rules:** Preserve suppressed, failed, and delivered outcomes for auditability. Telegram delivery is not part of the initial implementation.

**Relationships:** Belongs to one alert rule.

### ShopProfile

**Purpose:** Represents settings for a shop-owner workspace.

**Important fields:** `Id`, `Name`, nullable `TelegramUserId`, `DefaultMarginPercent`, `IsActive`, `CreatedAt`, `UpdatedAt`.

**Important rules:** It is an advisory workspace, not an accounting or guaranteed pricing system.

**Relationships:** Has many `ShopItem` records.

### ShopItem

**Purpose:** Connects a shop's purchase and selling prices to a canonical commodity variant.

**Important fields:** `Id`, `ShopProfileId`, `CommodityItemId`, `CommodityVariantId`, `PurchasePriceInIrr`, nullable `SellingPriceInIrr`, `TargetMarginPercent`, `CreatedAt`, `UpdatedAt`.

**Important rules:** Prices use IRR. The selected variant must match what the shop actually sells.

**Relationships:** Belongs to a shop profile, item, and variant; has many price-review signals.

### PriceReviewSignal

**Purpose:** Suggests that a shop item may need a human price review.

**Important fields:** `Id`, `ShopItemId`, `SignalType`, `CurrentMarketAverageInIrr`, nullable `CurrentSellingPriceInIrr`, nullable `SuggestedSellingPriceInIrr`, `Reason`, `CreatedAt`.

**Important rules:** This is a review suggestion, not guaranteed business advice. Include evidence quality and a plain-language reason.

**Relationships:** Belongs to one `ShopItem`.

## 5. Enums and Value Concepts

Recommended future enums:

- `CommodityCategory`: `Currency`, `Crypto`, `Gold`, `Coin`, `Protein`, `Dairy`, `Grain`, `Grocery`, `Other`.
- `CommodityUnit`: `Each`, `Gram`, `Kilogram`, `Liter`, `Package`, `Tray`, `Coin`, `Usd`, `Usdt`.
- `CommoditySourceKind`: `Api`, `DirectHttp`, `Html`, `Playwright`, `Manual`.
- `CommoditySourceStatus`: `Active`, `Paused`, `Disabled`, `Failing`, `UnderReview`.
- `MarketSignalType`: `PriceIncrease`, `PriceDecrease`, `HighVolatility`, `SourceSpread`, `NewHigh`, `NewLow`, `DataStale`, `LowConfidence`.
- `MarketSignalSeverity`: `Low`, `Medium`, `High`, `Critical`.
- `AlertType`: `PriceAbove`, `PriceBelow`, `PercentIncrease`, `PercentDecrease`, `DataStale`.
- `AlertDeliveryStatus`: `Pending`, `Delivered`, `Failed`, `Suppressed`.
- `PriceReviewSignalType`: `BelowMarket`, `AboveMarket`, `MarginTooLow`, `MarketMoved`, `DataLowConfidence`.

Potential value concepts to evaluate during implementation include an IRR money value, normalized quantity/unit pair, confidence score constrained to 0–100, and source observation identity. They should remain framework-independent and must not hide unit conversions.

## 6. Relationship Overview

- `CommodityItem` has many `CommodityVariant` records.
- `CommoditySource` has many `RawPriceObservation` records.
- `RawPriceObservation` may produce one `NormalizedPriceRecord`.
- `NormalizedPriceRecord` belongs to an item, variant, and source.
- `DailyPriceSummary` belongs to an item and variant.
- `MarketSignal` belongs to an item and optionally a variant.
- `CommodityWatchlistItem` belongs to an item and optionally a variant.
- `CommodityAlertRule` belongs to a watchlist item.
- `CommodityAlertEvent` belongs to an alert rule.
- `ShopProfile` has many `ShopItem` records.
- `ShopItem` belongs to an item and variant.
- `PriceReviewSignal` belongs to a shop item.

## 7. Initial MVP Slice Recommendation

The smallest safe first implementation is **MVP Slice 1**:

- `CommodityItem`
- `CommodityVariant`
- `CommoditySource`
- `RawPriceObservation`
- `NormalizedPriceRecord`

Do not begin with watchlists, alerts, shop-owner mode, purchasing power, Telegram post generation, or a Mini App. The first slice must prove raw-data traceability, canonical mapping, unit compatibility, normalization, and confidence handling before downstream features depend on it.

## 8. Initial Catalog to Review Later

Candidate catalog preserved from the earlier planning handoff:

- USD free market.
- USDT.
- 18k gold.
- Emami coin.
- Chicken.
- Egg.
- Iranian rice.
- Foreign rice.
- Cooking oil.
- Milk.
- Cheese.
- Yogurt.
- Sugar.
- Tea.
- Tomato paste.
- Pasta.

Do not seed this catalog yet. Review canonical units, package variants, and reliable sources before implementation. Start with a very small vertical slice, likely USDT first, only after source review.

## 9. Persistence Notes

ChannelPilot already uses EF Core with SQL Server. CommodityRadar tables should be introduced through a future migration after Domain approval. Use Fluent API configurations in Infrastructure and no EF attributes in Domain.

Plan indexes for:

- Unique `CommodityItem.Code`.
- Unique `CommodityVariant.Code`.
- Raw observation source, observation time, and hash.
- Normalized record item, variant, and observation time.
- Unique daily summary item, variant, and date.

Use restrictive deletion where raw evidence, normalized records, or history could otherwise be lost.

## 10. Integration with ChannelPilot Publishing

CommodityRadar should eventually detect `MarketSignal` records and transform selected signals into ChannelPilot `PublishQueueItem` records.

- Never publish directly to Telegram.
- Use the central ChannelPilot publishing queue.
- Apply approval, channel limits, scheduling, duplicate, and retry policies.
- Do not auto-publish low-confidence or sensitive signals.

## 11. Explicit Non-Goals for First Implementation

- No scraping or Playwright.
- No Nobitex integration.
- No watchlists or alerts.
- No shop-owner mode.
- No Telegram posts.
- No Mini App or frontend.
- No AI predictions.
- No financial advice.

## 12. Recommended Next Steps

1. Review and approve the CommodityRadar domain design.
2. Implement CommodityRadar MVP Slice 1 Domain entities.
3. Add EF configurations for MVP Slice 1.
4. Create a migration for CommodityRadar MVP Slice 1.
5. Seed only a minimal reviewed catalog if explicitly approved.
6. Implement the raw-observation repository.
7. Implement a normalization skeleton.
8. Add the first source adapter only after source-level review.
