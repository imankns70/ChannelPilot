# Publishing policy

## Global rules

- Every post belongs to one managed `TelegramChannel`.
- A content item may be suitable for more than one channel. The initial product operates one Football, one Trend, and one Novel channel.
- Do not publish duplicate content inside the same channel.
- Respect `MinMinutesBetweenPosts`.
- Respect `MaxPostsPerDay`.
- Respect `RequiresApproval`; approval must be recorded before an approval-gated item can proceed.
- The Novel channel must respect its fixed nightly time, episode order, and one-post-per-day limit.
- Trend posts require manual approval initially.

## Publishing queue

The publishing queue is the central gate before Telegram. Collection, analysis, rewriting, or scheduling may prepare an item, but none of those steps sends a Telegram message directly. A queue item must be eligible by status, approval, schedule, channel limits, and duplicate checks before a publisher may process it.

## Published history

`PublishedPost` records form the immutable history of sent messages. A sent record should not be rewritten to represent a later attempt; corrections or replacements should create explicit new records and preserve the original audit trail.

## Failures and retries

- Record a failure reason and retry count.
- Retry only retryable failures and only within configured limits.
- Respect rate-limit delays rather than retrying immediately.
- Do not retry missing content, missing approval, cancellation, or other terminal policy failures automatically.
- Move exhausted attempts to a failed state for review.

Exact retry counts and delay strategy remain future configuration decisions.
