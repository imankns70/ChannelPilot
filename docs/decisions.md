# Architecture decisions

## 0001 — Create a clean solution

**Decision:** Create a new clean solution instead of rewriting the old football project directly.

**Reason:** ChannelPilot is no longer only football-related. It combines football news, trend news, and serial novel publishing, each with different publishing behavior.

The old project remains a reference for Telegram publishing, RSS/HTML reading, deduplication ideas, and scheduling experience. It is not modified by this project setup.

## 0002 — Use SQL Server for persistence

**Decision:** Use SQL Server for both development and production persistence.

**Reason:** The project owner already has SQL Server experience, using one provider keeps environments aligned, and SQL Server supports ChannelPilot's queue, history, retry, configuration, reporting, and operational-query needs. EF Core and concrete persistence artifacts will be added in a later phase.

## 0003 — Limit initial seed data to managed channels

**Decision:** Initial seed data will contain only the Football, Trend News, and Novel managed channels.

**Reason:** These records are the minimum required for consistent setup and testing. Content, sources, novels, queue items, publishing history, users, and real Telegram identifiers should not be fabricated or committed as seed data.

## 0004 — Absorb PriceRadar conceptually as CommodityRadar

**Decision:** The previous PriceRadar idea will be absorbed conceptually as CommodityRadar, a future commodity and economic market-intelligence module inside ChannelPilot.

**Reason:** PriceRadar contains mostly planning and scaffold material, while ChannelPilot already provides the shared platform boundaries it would need. CommodityRadar keeps the product idea without creating another disconnected system and remains isolated from Football, Trend, and Novel behavior.

## 0005 — Start CommodityRadar with the smallest data-quality slice

**Decision:** CommodityRadar implementation will begin with item, variant, source, raw observation, and normalized record concepts only.

**Reason:** Traceability, unit compatibility, normalization, and confidence must be proven before summaries, signals, alerts, shop-owner tools, publishing, or UI features depend on the data.
