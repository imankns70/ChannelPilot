# Football channel rules

## Publishing model

The Football channel is **event-based**. A relevant football event triggers collection and evaluation; the channel is not filled to satisfy a schedule or quota.

## Publish candidates

Prefer:

- Breaking football news.
- Transfers and meaningful transfer developments.
- Injuries and suspensions.
- Important match results.
- Controversial or consequential interviews.
- Club crises.
- National-team news.
- Important Iranian and European football news.
- News involving major teams or players.

Avoid or strongly down-rank:

- Routine training reports.
- Photo reports with no news value.
- Generic congratulations.
- Administrative announcements.
- Weak rumors without a credible source.
- Repetitive match previews.
- Low-value ceremony or newsroom items.

## Editorial style

Posts should be short, attractive, Persian-language, and easy to scan in Telegram. Lead with a strong first line and use emojis sparingly. Do not exaggerate beyond what the source supports, and include source attribution when it adds credibility or context.

## Initial scoring signals

Positive signals:

- Transfer relevance.
- Breaking-news status.
- A major team or player.
- Injury or suspension impact.
- Controversy or crisis.
- National-team relevance.
- Confirmation by multiple sources.
- Freshness.
- A useful, relevant image.

Negative signals:

- Duplicate or substantially repeated content.
- Old news.
- A low-value category.
- A weak source.
- No clear news value.
- A pure gallery or video report without a meaningful event.

## Initial channel policy

| Setting | Initial value |
|---|---:|
| `MinScoreToPublish` | 70 |
| `RequiresApproval` | `false` initially; may be `true` during testing |
| `MinMinutesBetweenPosts` | 10 |
| `MaxPostsPerDay` | 40 |

These are starting values and should be reviewed against real editorial outcomes before automation is trusted.
