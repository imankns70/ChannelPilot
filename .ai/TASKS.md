# Next tasks

1. [x] Define initial domain entities.
2. [x] Design initial Application abstractions and use-case skeletons.
3. [x] Design Infrastructure placeholders and dependency-injection boundaries.
4. [x] Define API contract and controller skeletons.
5. [x] Define Worker job skeletons for football, trend, novel, and publishing queue flows.
6. [x] Define channel behavior rules and scoring strategy documentation.
7. [x] Design the persistence strategy and choose SQL Server for development and production.
8. [x] Add EF Core SQL Server persistence infrastructure, `DbContext`, entity configurations, and repositories.
9. [x] Create the first EF Core migration and verify database creation.
10. [x] Document the minimal channel seed/admin setup strategy.
11. [x] Implement idempotent initial database seeding for the three managed Telegram channels.
12. [x] Implement channel management API using EF repositories instead of placeholder responses.
13. [x] Implement Content Source Management API using EF repositories.
14. [x] Create the CommodityRadar domain design document.
15. Review and approve CommodityRadar MVP Slice 1 before implementation.
16. Define the initial football content sources seed strategy. (covered by «انتقال فوتبال فوری» F2.6)
17. Refine publishing queue behavior. (covered by F6)
18. Refine novel episode scheduling behavior.
19. Add the Telegram publisher later. (covered by F6)
20. Add authentication later. (covered by F8)
21. Add the admin frontend later. (covered by F9; Razor Pages in the API host)

---

# انتقال «فوتبال فوری» به ChannelPilot

«فوتبال فوری» اولین کانال عملیاتی ChannelPilot است و منطق آن از پروژهٔ آرشیوی `C:\Projects\TelegramAggregator` منتقل می‌شود. پروژهٔ قدیمی فقط مرجع است و تغییر نمی‌کند. ChannelPilot با صف و تاریخچهٔ خالی شروع می‌کند و داده‌ای از PostgreSQL قدیمی وارد نمی‌شود. ترتیب مراحل از پایین‌ترین لایه تا جابه‌جایی نهایی است و هر مرحله به مرحلهٔ قبلی وابسته است. طبق `AGENTS.md`، پیاده‌سازی هر مرحله بعد از تأیید طراحی آن شروع می‌شود.

## F0. طراحی و تأیید

- [x] F0.1 سند `docs/football-fori-migration.md` را از روی این برنامه بنویس: تغییرات دامنه، وضعیت‌های خبر و صف، نگاشت امتیاز ۰–۲۰۰ به ۰–۱۰۰، دلایل رد، قواعد زمان کاری و نقشهٔ نگاشت کلاس‌های قدیمی به لایه‌های جدید.
- [ ] F0.2 سند طراحی را برای تأیید ارائه کن و تا تأیید، کد ننویس.
- [ ] F0.3 تصمیم‌ها را در `docs/decisions.md` ثبت کن: شروع با دادهٔ خالی، Razor Pages در میزبان API، پکیج‌های تأییدشده (AngleSharp، System.ServiceModel.Syndication، Microsoft.Playwright) و روش اتصال به تلگرام (پکیج Telegram.Bot یا HttpClient مستقیم).
- [x] F0.4 نمونهٔ واقعی خروجی RSS و HTML هر چهار منبع را در `backend/tests/.../Fixtures/Football` ذخیره کن تا آزمون‌ها به شبکه وابسته نباشند.

## F1. دامنه (`ChannelPilot.Domain`، بدون کد زیرساخت)

- [ ] F1.1 در `ContentSource`، نوع آداپتور را مشخص کن (`Rss`، `Football360`) و فیلدهای لازم برای فعال/غیرفعال‌سازی و ثبت آخرین خطا را اضافه کن.
- [ ] F1.2 به `ContentItem` فیلد `NormalizedUrl` و زمان انتشار معتبر را اضافه کن؛ `OriginalBody` متن کامل خبر را نگه دارد.
- [ ] F1.3 enum دلیل رد فوتبال را تعریف کن: ورزش دیگر، فوتبال بانوان، ویدئو، کلیدواژهٔ حذفی (ترکیب رسمی، پخش زنده و…)، بدون تصویر، متن ناقص، تکراری، قبل از زمان جابه‌جایی، زیر آستانهٔ امتیاز، منقضی.
- [ ] F1.4 به `FootballNewsItem` امتیاز خام، امتیاز نرمال‌شده، جزئیات امتیاز و دلیل رد را اضافه کن.
- [ ] F1.5 `PublishStatus` را با وضعیت‌های `Reserved` و `UnknownOutcome` کامل کن و قواعد مجاز تغییر وضعیت را در دامنه تعریف کن.
- [ ] F1.6 به `PublishQueueItem` فیلدهای رزرو (`ReservedUntil`، `ReservationToken`)، `NextAttemptAt`، `LastError` و نسخهٔ همزمانی را اضافه کن.
- [ ] F1.7 `PublishedPost` فقط با `TelegramMessageId` معتبر ساخته شود و برای هر آیتم صف حداکثر یک رکورد داشته باشد.
- [ ] F1.8 به `TelegramChannel` یا `PublishingPolicy` کلید فعال‌بودن ارسال، زمان جابه‌جایی و فوتر کپشن را اضافه کن. پیش‌فرض فوتبال: آستانهٔ ۷۰، فاصلهٔ ۱۰ دقیقه، سقف ۴۰ پست در روز، تأیید دستی غیرفعال، ارسال خاموش.
- [ ] F1.9 آزمون واحد قواعد تغییر وضعیت صف و ساخت `PublishedPost`.

## F2. پایگاه داده (SQL Server و EF Core)

- [ ] F2.1 پیکربندی EF فیلدهای جدید را بنویس و `rowversion` را برای `PublishQueueItem` اضافه کن.
- [ ] F2.2 ایندکس‌ها: `NormalizedUrl` یکتا برای هر منبع، وضعیت و زمان صف، `PublishedPost.PublishQueueItemId` یکتا، و زمان انتشار هر کانال.
- [ ] F2.3 migration جدید را بساز و روی پایگاه توسعه اعمال کن.
- [ ] F2.4 متدهای مخزن محتوا: جست‌وجو با URL نرمال، و خبرهای ۸ ساعت اخیر برای مقایسهٔ شباهت.
- [ ] F2.5 متدهای مخزن صف: رزرو اتمیک یک آیتم واجد شرایط (`UPDLOCK, READPAST`)، شمارش پست‌های روز به وقت تهران، آخرین زمان انتشار کانال، و یافتن رزروهای منقضی‌شده.
- [ ] F2.6 seed منابع فوتبال (ورزش سه، طرفداری، خبرورزشی، فوتبال ۳۶۰) و سیاست پیش‌فرض کانال فوتبال؛ idempotent و با ارسال خاموش.
- [ ] F2.7 آزمون یکپارچه با SQL Server آزمایشی برای رزرو همزمان، یکتایی URL و شمارش روزانه.

## F3. منطق خالص فوتبال (`ChannelPilot.Application`)

- [ ] F3.1 `UrlNormalizer` (حذف query، اسلش انتهایی و حروف بزرگ) را منتقل کن.
- [ ] F3.2 پاک‌سازی متن فارسی را منتقل کن: `TextCleaner` و پاک‌کننده‌های مخصوص هر سایت (طرفداری، ورزش سه، خبرورزشی، فوتبال ۳۶۰)، شامل حذف نام منبع و کاراکترهای مخفی.
- [ ] F3.3 `TextSimilarity` را منتقل کن. بهبود: فقط خبر جدیدتر تکراری شود، خبرهای داخل یک دسته هم با هم مقایسه شوند، و مقایسه پیش از ذخیره انجام شود تا هر دو نسخهٔ خبر رد نشوند.
- [ ] F3.4 فیلتر کلیدواژه‌ها (ورزش دیگر، بانوان، ویدئو و کلیدواژه‌های حذفی) را با تنظیمات قابل تغییر منتقل کن.
- [ ] F3.5 `FootballScoringOptions` را از روی `footbalScoring.json` بساز. وزن‌های `Conferences` و `IranianPlayer` که در نسخهٔ قدیمی نادیده گرفته می‌شدند در محاسبه لحاظ شوند.
- [ ] F3.6 `FootballScoreCalculator` را منتقل کن، خروجی ۰–۱۰۰ و جزئیات امتیاز را برگردان، و امتیاز را بعد از دریافت متن کامل حساب کن.
- [ ] F3.7 بررسی شرایط ارسال: تصویر، ویدئو، متن ناقص و انتشار پس از زمان جابه‌جایی؛ خروجی آن واجد شرایط بودن یا دلیل رد است.
- [ ] F3.8 `FootballCaptionService` را از `CaptionMakerExtension` منتقل کن: HTML امن، سقف ۱۰۲۴ کاراکتر، برش در پایان جمله، و فوتر از تنظیمات کانال.
- [ ] F3.9 آزمون واحد با fixtureها برای پاک‌سازی، شباهت، فیلترها، امتیاز و کپشن.

## F4. آداپتورهای منبع (`ChannelPilot.Infrastructure`)

- [ ] F4.1 پکیج‌های تأییدشده را فقط به پروژهٔ Infrastructure اضافه کن: AngleSharp، System.ServiceModel.Syndication و Microsoft.Playwright.
- [ ] F4.2 `HttpClient` نام‌دار با timeout، User-Agent و غیرفعال‌سازی cache را تنظیم کن.
- [ ] F4.3 `RssContentSourceReader` برای ورزش سه، طرفداری و خبرورزشی: ۱۰ خبر آخر هر منبع و تبدیل به `CollectedContentDto`.
- [ ] F4.4 `ArticlePageEnricher` با AngleSharp: متن کامل، تصویر اصلی و تشخیص ویدئو را با یک بار دریافت صفحه استخراج کند. نسخهٔ قدیمی هر خبر را سه بار جداگانه باز می‌کرد.
- [ ] F4.5 `PlaywrightBrowserProvider`: یک مرورگر Chromium مشترک (singleton)، ساخت context برای هر کار و بستن درست هنگام توقف سرویس.
- [ ] F4.6 `Football360SourceReader` با Playwright: خواندن صفحهٔ اصلی و صفحهٔ هر خبر، عنوان، زیرعنوان، متن، تصویر از `__NEXT_DATA__`، و حذف خبرهای لایو، زنده و فانتزی.
- [ ] F4.7 dispatcher منبع بر اساس نوع آداپتور: خطای یک منبع ثبت شود (`FailureCount`، `LastFetchedAt`، آخرین خطا) و جمع‌آوری از بقیه ادامه پیدا کند. منبع غیرفعال خوانده نشود.
- [ ] F4.8 مستند نصب مرورگرهای Playwright برای توسعه و سرور را بنویس.
- [ ] F4.9 آزمون آداپتورها با fixtureهای ذخیره‌شده، شامل تغییر ساختار صفحه و شکست یک منبع.

## F5. چرخهٔ پردازش فوتبال

- [ ] F5.1 `FootballNewsProcessingService`: جمع‌آوری ← پاک‌سازی ← حذف URL تکراری ← فیلتر کلیدواژه ← تکمیل صفحه ← شباهت ← امتیاز ← بررسی شرایط ← ذخیرهٔ `ContentItem` و `FootballNewsItem` با وضعیت و دلیل.
- [ ] F5.2 خبر واجد شرایط با کپشن ساخته‌شده به صف مشترک کانال فوتبال سپرده شود.
- [ ] F5.3 `CollectFootballNewsJob` و `FootballNewsWorker` را وصل کن: هر ۲۰ دقیقه، از ۸ صبح تا ۱ بامداد به وقت تهران؛ هر دو مقدار قابل تنظیم باشند.
- [ ] F5.4 کار انقضا: ساعت ۳ بامداد به وقت تهران، آیتم‌های مانده در صف فوتبال منقضی شوند.
- [ ] F5.5 لاگ ساخت‌یافتهٔ هر دور جمع‌آوری: تعداد دریافت، رد به تفکیک دلیل، صف‌شده و خطای منابع.
- [ ] F5.6 آزمون چرخهٔ کامل با منابع fake و پایگاه آزمایشی.

## F6. انتشار در تلگرام

- [ ] F6.1 `PublishResultDto` را با نوع نتیجه (`Success`، `Retryable`، `RateLimited`، `Terminal`، `Unknown`) و `RetryAfter` کامل کن.
- [ ] F6.2 تنظیمات امن: توکن ربات و شناسهٔ کانال از user-secrets یا متغیر محیطی خوانده شوند و در مخزن ذخیره نشوند.
- [ ] F6.3 `TelegramBotApiPublisher` پشت `ITelegramPublisher`: ارسال `sendPhoto` با HTML، خواندن `message_id`، رعایت `retry_after` در پاسخ 429، و ثبت timeout بعد از ارسال به‌صورت `Unknown`.
- [ ] F6.4 `PublishingGate`: فعال‌بودن کانال و کلید ارسال، تأیید در صورت نیاز، زمان‌بندی، حداقل فاصله، سقف روزانه به وقت تهران، تکراری‌نبودن در کانال و ساعت کاری (۸ صبح تا ۳ بامداد).
- [ ] F6.5 `PublishQueuedPostsJob`: رزرو اتمیک ← بررسی دروازه ← ارسال ← در صورت موفقیت، ثبت `PublishedPost` و تغییر وضعیت در یک تراکنش.
- [ ] F6.6 بازیابی رزروهای منقضی: آیتمی که حین ارسال رها شده به `UnknownOutcome` برود و دوباره ارسال نشود.
- [ ] F6.7 سیاست تلاش مجدد: فقط خطاهای قابل تکرار، با سقف تعداد و `NextAttemptAt`؛ خطای نهایی مستقیم به `Failed` برود.
- [ ] F6.8 ارسال دستی (`PublishNow`) از همین دروازه و رزرو عبور کند.
- [ ] F6.9 حالت ارسال خاموش: کل چرخه اجرا شود ولی پیامی ارسال نشود.
- [ ] F6.10 آزمون: اجرای موازی دو worker، سقف روزانه، فاصلهٔ پست‌ها، تأیید دستی، 429 و نتیجهٔ نامشخص.

## F7. API

- [ ] F7.1 `FootballController` به داده‌های واقعی وصل شود: فهرست خبرها با امتیاز، جزئیات امتیاز و دلیل رد.
- [ ] F7.2 `PublishingController`: صف، تأیید، لغو، ارسال فوری و تعیین تکلیف آیتم‌های `UnknownOutcome` (ارسال‌شده با شناسهٔ پیام یا ارسال‌نشده).
- [ ] F7.3 `ContentSourcesController`: فعال/غیرفعال‌کردن منبع فوتبال و نمایش وضعیت آخرین واکشی.
- [ ] F7.4 endpoint تنظیمات کانال فوتبال: آستانه، فاصله، سقف روزانه و کلید ارسال.

## F8. احراز هویت و امنیت

- [ ] F8.1 ورود مدیر با cookie authentication خود ASP.NET Core؛ اطلاعات مدیر از تنظیمات امن خوانده شود.
- [ ] F8.2 همهٔ endpointها و صفحه‌های مدیریتی نیاز به ورود داشته باشند.
- [ ] F8.3 محافظت CSRF روی فرم‌ها و درخواست‌های تغییردهنده فعال باشد.

## F9. پنل فارسی (Razor Pages در میزبان API)

- [ ] F9.1 Razor Pages را راه‌اندازی کن و layout راست‌چین با فونت فارسی بساز.
- [ ] F9.2 صفحهٔ ورود.
- [ ] F9.3 داشبورد: فهرست کانال‌ها، وضعیت ماژول‌ها و آمار امروز (دریافت، رد، صف، ارسال). ترند و رمان فقط وضعیت نمایش دهند.
- [ ] F9.4 مدیریت منابع فوتبال: فعال/غیرفعال، آخرین واکشی و خطا.
- [ ] F9.5 خبرهای فوتبال: فهرست، پیش‌نمایش کپشن و تصویر، جزئیات امتیاز و دلیل رد.
- [ ] F9.6 صف: ترتیب، تأیید، لغو و ارسال فوری.
- [ ] F9.7 بررسی موارد `UnknownOutcome` و خطاهای ارسال.
- [ ] F9.8 تاریخچهٔ انتشار با شناسهٔ پیام تلگرام.
- [ ] F9.9 تنظیمات کانال فوتبال: آستانه، فاصله، سقف روزانه و کلید ارسال.

## F10. آزمون و ارزیابی

- [ ] F10.1 کل آزمون‌های واحد و یکپارچه را اجرا کن و موارد F1–F6 را پوشش بده.
- [ ] F10.2 جمع‌آوری را با ارسال خاموش اجرا کن و صف را روی نمونهٔ واقعی بازبینی کن.
- [ ] F10.3 آستانهٔ ۷۰ را با نمونهٔ واقعی ارزیابی و در صورت نیاز از پنل تنظیم کن.
- [ ] F10.4 با کانال و پایگاه آزمایشی جداگانه، چرخهٔ کامل را تا دریافت شناسهٔ واقعی پیام و نمایش آن در تاریخچهٔ پنل تأیید کن.

## F11. جابه‌جایی و بازگشت

- [ ] F11.1 نسخهٔ پشتیبان SQL Server تولید و دسترسی ربات به کانال اصلی را بررسی کن.
- [ ] F11.2 توکن ربات و رمزهای پایگاه دادهٔ پروژهٔ قدیمی در مخزن Git آن ذخیره شده‌اند؛ پیش از استفاده در ChannelPilot توکن تازه بگیر.
- [ ] F11.3 وضعیت سرویس مستقر قدیمی روی Liara را مستقل از آرشیو بررسی کن و ارسال آن را متوقف کن.
- [ ] F11.4 زمان جابه‌جایی را ثبت کن و ارسال ChannelPilot را برای کانال اصلی فعال کن.
- [ ] F11.5 اولین ارسال واقعی را با تاریخچهٔ پنل تطبیق بده.
- [ ] F11.6 روال بازگشت: ارسال ChannelPilot را متوقف کن، پیام‌های ارسال‌شده و موارد `UnknownOutcome` را با کانال تطبیق بده، سپس ارسال سامانهٔ قدیمی را از سر بگیر.
- [ ] F11.7 `PROJECT_STATE.md`، READMEهای زیرساخت و سند انتقال را به‌روز کن.
