# Novel channel rules

## Publishing model

The Novel channel is **schedule-based**, not event-based. Its purpose is to release a serial story at a predictable nightly cadence.

## Scheduling rules

- Publish exactly one episode per night.
- Use a fixed publishing time; the initial default is **22:30**.
- Never dump multiple episodes in a single night.
- Split long novels across multiple episodes and nights.
- Preserve episode order without gaps or reordering.
- `NovelPublishingPlan` controls the current episode and schedule.

## Episode presentation

Each episode should include:

- Novel title.
- Episode number.
- Hook or opening, when available.
- Main episode content.
- Ending teaser, when available.
- `ادامه دارد...`
- A reminder such as `قسمت بعدی فردا شب منتشر می‌شود.`

## Episode length and splitting

- Preferred length: **3,500–7,500 Persian characters**.
- Do not cut in the middle of a sentence.
- Prefer a paragraph, scene transition, or suspense point as the boundary.
- The last episode may be shorter than the preferred range.

The current deterministic splitter is only a starting point; editorial review should correct awkward narrative boundaries before scheduling.

## Approval and automation

An imported novel must be reviewed before publishing begins. After the novel and its episode sequence are approved and scheduled, nightly publication may run automatically. Already approved episodes do not require approval again.

## Initial channel policy

| Setting | Initial value |
|---|---:|
| `FixedPublishTime` | 22:30 |
| `MaxPostsPerDay` | 1 |
| `RequiresApproval` | `false` for previously approved episodes |
| Episode progression | Controlled by `NovelPublishingPlan` |
