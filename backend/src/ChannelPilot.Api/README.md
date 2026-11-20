# ChannelPilot API contracts

This project currently defines the future admin-panel HTTP surface only.

Available placeholder areas:

- `api/channels` for channel configuration and status.
- `api/content` for collected content, analysis, and rewriting requests.
- `api/football/news` for football-specific content views.
- `api/trends/news` for trend review workflows.
- `api/novels` for novels, imports, episodes, and publishing plans.
- `api/publishing` for queue approval, immediate publishing requests, and history.

Responses are deterministic placeholders and request echo shapes. Novel import uses the local paragraph-based episode splitter. No persistence, authentication, external AI, source collection, or Telegram publishing is performed.
