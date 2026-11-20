# ChannelPilot workers

The Worker host defines four periodic background boundaries:

- **FootballNewsWorker** represents event-based football collection and runs every five minutes by default.
- **TrendNewsWorker** represents quality-gated trend collection and runs every ten minutes by default.
- **NovelPublishingWorker** checks every minute for a fixed-time nightly novel episode that may be due.
- **PublishQueueWorker** checks every minute for queued, approved posts that may be ready to send.

Each worker can be enabled or disabled and configured with an interval in the `Workers` configuration section. All current jobs only log that their placeholder ran. They do not collect content, query durable storage, evaluate schedules, or publish to Telegram.
