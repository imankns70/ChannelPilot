# CommodityRadar Integration Decision

## 1. Decision

The previous PriceRadar product idea will be integrated conceptually into ChannelPilot as a planned module named **CommodityRadar**.

CommodityRadar is the selected name. `PriceRadar` must not be used as the technical module name inside ChannelPilot; it refers only to the earlier planning context.

The proposed entities, invariants, relationships, and first implementation slice are described in [CommodityRadar Domain Design](commodity-radar-domain-design.md).

## 2. Reason

ChannelPilot is becoming the central platform for multiple Telegram channels and content engines. The earlier PriceRadar project was primarily documentation and scaffold work, so there is little implemented software to migrate directly. Incorporating its product ideas into ChannelPilot avoids operating disconnected systems and permits reuse of shared platform capabilities:

- Telegram channel management.
- Publishing queue.
- Scheduling.
- Background workers.
- Admin API.
- Future frontend and admin panel.
- Source management.
- Approval flows.

CommodityRadar must remain an independent module. Its market-data concepts and behavior must not be mixed into Football, Trend, or Novel logic.

## 3. Product Meaning

CommodityRadar is a commodity and economic market-intelligence module. It may eventually provide:

- Iranian market price tracking.
- A product and commodity catalog.
- Raw price observations.
- Price normalization.
- Daily price summaries.
- Price-movement detection.
- Purchasing-power indicators.
- Watchlists and alerts.
- Shop-owner price-review tools.
- Economic and commodity Telegram posts.
- A future Mini App or dashboard.

## 4. Relation to Existing ChannelPilot Channels

ChannelPilot's current and planned channel/module behaviors are distinct:

1. **Football Channel:** event-based football news publishing.
2. **Trend News Channel:** quality-gated trending news publishing.
3. **Novel Channel:** schedule-based nightly episode publishing.
4. **CommodityRadar:** a future commodity/economic module and possible channel driven by market data.

CommodityRadar will collect price observations, normalize and summarize market data, and generate alerts, signals, and economic posts. This data pipeline differs fundamentally from news selection and serial publishing.

## 5. Mapping from Old PriceRadar Concept

| Old PriceRadar concept | New ChannelPilot / CommodityRadar concept |
|---|---|
| PriceRadar product | CommodityRadar module |
| PriceRadar Mini App | Future CommodityRadar Mini App or admin/user dashboard |
| PriceRadar Bot notifications | ChannelPilot Telegram publishing and notification infrastructure |
| Price sources | CommodityRadar price sources |
| Raw price ticks | CommodityRadar raw price observations |
| Normalized price ticks | CommodityRadar normalized price records |
| Daily summaries | CommodityRadar daily price summaries |
| Market signals | CommodityRadar market signals |
| Watchlist and alerts | CommodityRadar watchlist and alert module |
| Shop-owner mode | CommodityRadar shop-owner tools |
| Nobitex first source | Future CommodityRadar source adapter; not implemented |
| Essential-goods scraping | Future controlled CommodityRadar source ingestion; not implemented |

## 6. MVP Scope to Preserve

The earlier PriceRadar MVP catalog should be retained as candidate future CommodityRadar scope:

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

This catalog is not implemented or seeded. It must be reviewed again before implementation for usefulness, source availability, units, variants, legality, and maintenance cost.

## 7. Future Technical Module Boundaries

Expected future folders:

- `backend/src/ChannelPilot.Domain/CommodityRadar/`
- `backend/src/ChannelPilot.Application/CommodityRadar/`
- `backend/src/ChannelPilot.Infrastructure/CommodityRadar/`
- `backend/src/ChannelPilot.Api/Controllers/CommodityRadar/`
- `backend/src/ChannelPilot.Worker/Jobs/`
- `backend/src/ChannelPilot.Worker/Workers/`

Expected future Domain concepts:

- `CommodityItem`
- `CommodityVariant`
- `CommoditySource`
- `RawPriceObservation`
- `NormalizedPriceRecord`
- `DailyPriceSummary`
- `MarketSignal`
- `PurchasingPowerSnapshot`
- `WatchlistItem`
- `AlertRule`
- `AlertEvent`
- `ShopProfile`
- `ShopItem`
- `PriceReviewSignal`

These folders and types are design targets only and must not be created until the CommodityRadar domain design is approved.

## 8. Source Strategy to Preserve

- Prefer APIs and direct HTTP.
- Use Playwright only when a reviewed public page is JavaScript-rendered and HTTP extraction is insufficient.
- Run Playwright only in background workers.
- Source adapters and scrapers emit raw observations only.
- Keep normalization and confidence scoring separate from ingestion.
- Give every source independent limits, health state, and a kill switch.
- Fail closed when source quality or legality is uncertain.
- Do not enable a source without source-level review.

## 9. Data Rules to Preserve

- Store prices explicitly in IRR.
- Treat toman as display-only conversion.
- Keep raw observations separate from normalized records.
- Treat product variants as first-class concepts.
- Do not compare incompatible units.
- Do not convert between mass and volume without reviewed, product-specific density.
- Make source confidence explicit.
- Limit confidence for single-source market data.

## 10. What Will Not Be Migrated Now

- No PriceRadar source code.
- No CommodityRadar domain entities.
- No database tables or migrations.
- No source adapters.
- No Nobitex integration.
- No Playwright scraping.
- No Telegram alert delivery.
- No Mini App or frontend.
- No watchlist, alert, or shop-owner implementation.

## 11. Recommended Future Steps

1. Review this CommodityRadar integration decision.
2. Create the CommodityRadar domain design document.
3. Decide the CommodityRadar MVP slice.
4. Implement CommodityRadar Domain entities.
5. Add EF configurations and a CommodityRadar migration.
6. Add CommodityRadar source management.
7. Implement the first source adapter—likely Nobitex/USDT—only after source review.
8. Implement raw observation storage.
9. Implement the normalization pipeline.
10. Implement daily summaries.
11. Generate economic posts into the ChannelPilot publishing queue.
12. Add Telegram publishing only after queue rules are stable.
13. Add a future Mini App or admin UI later.
