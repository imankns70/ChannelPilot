# Scoring strategy

## Status

This is an initial editorial scoring model, not a final mathematical specification. Signals, weights, normalization, and thresholds will evolve as real content is reviewed. Scores should support editorial judgment rather than manufacture certainty.

## General model

Conceptually, `FinalScore` combines:

- Content quality.
- Match with the target channel.
- Freshness.
- Source reliability.
- Attractiveness and presentation potential.
- Safety and risk penalties.
- Duplicate penalty.

Hard exclusions—such as a confirmed duplicate or an unsafe unsupported claim—may reject an item regardless of its numeric score.

## Starting weights

### Football

| Signal | Weight |
|---|---:|
| Importance | 30% |
| Attractiveness | 30% |
| Freshness | 20% |
| Reliability | 10% |
| Media/source signals | 10% |

Duplicate, age, weak-source, and low-news-value penalties are applied after the positive signal score.

### Trend

| Signal | Weight or adjustment |
|---|---:|
| Public impact | 30% |
| Reliability | 25% |
| Freshness | 15% |
| Viral potential | 20% |
| Safety/risk penalty | −10% to −40% depending on sensitivity |

Channel match and broad relevance remain gating considerations. Highly sensitive or unsupported items can be rejected even when other signals are strong.

### Novel

Novel episodes are not ranked like news. They use editorial readiness, correct sequence, approval state, and schedule eligibility instead of a trend score.

## Score bands

| Score | Meaning |
|---|---|
| 0–39 | Reject |
| 40–59 | Low value |
| 60–69 | Manual review only |
| 70–84 | Publishable for Football |
| 85–100 | Strong candidate; eligible for Trend review |

A score never bypasses approval, safety, duplicate, rate-limit, or scheduling rules.
