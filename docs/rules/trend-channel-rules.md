# Trend channel rules

## Publishing model

The Trend channel is **quality-gated**. A story must clear a high relevance, reliability, and safety threshold; being popular by itself is not enough.

## Publish candidates

Prefer:

- High-interest trending news and genuinely viral topics.
- Technology news with broad appeal.
- Economic news that affects ordinary people.
- Important social news.
- Important incidents.
- Entertainment or celebrity news with strong public interest.
- Very important sports stories with broad trend value.

Avoid:

- Political flamebait.
- Unverified rumors.
- Sensitive claims without confirmation.
- Low-quality gossip.
- Repeated generic news.
- Highly local stories unless they are exceptionally viral or consequential.
- Medical, legal, or financial claims without reliable sourcing.

## Editorial style

Use a clear, neutral, source-aware voice. Summaries should be short and attractive for Telegram, but cautious: do not exaggerate, imply certainty that sources do not provide, or hide material uncertainty.

## Initial scoring signals

Positive signals:

- Confirmation by multiple independent sources.
- Large public impact.
- Viral potential.
- Freshness.
- Search or social trend signals when reliable signals become available later.
- High practical relevance.
- A reliable primary or authoritative source.

Negative signals:

- Topic sensitivity.
- A weak source.
- An unconfirmed claim.
- Excessively political or inflammatory framing.
- Old or repeated content.
- Limited broad relevance.

## Initial channel policy

| Setting | Initial value |
|---|---:|
| `MinScoreToPublish` | 85 |
| `RequiresApproval` | `true` |
| `MinMinutesBetweenPosts` | 30 |
| `MaxPostsPerDay` | 10–15 |

Manual approval is mandatory initially. The daily maximum should start conservatively within the 10–15 range and be tuned only after editorial review.
