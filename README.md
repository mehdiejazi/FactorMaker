# FactorMaker

فکتورمیکر یک نمونه اوپن‌سورس برای مدیریت فروشگاه و صدور فاکتور است که با `ASP.NET Core 3.1 Web API` در بک‌اند و `Angular 17` در فرانت ساخته شده است. دامنه اصلی پروژه روی این قابلیت‌ها متمرکز است:

- مدیریت فروشگاه
- مدیریت دسته‌بندی و کالا
- مدیریت مشتری
- صدور، ویرایش و بستن فاکتور
- مدیریت کاربران و نقش‌ها
- داشبورد فروش بر پایه داده واقعی فاکتور

## Tech Stack

- Backend: `ASP.NET Core 3.1`
- Frontend: `Angular 17`
- Database: `SQL Server`
- Auth: `JWT`
- Architecture: `Repository + UnitOfWork`

## Prerequisites

- `.NET SDK` (هدف پروژه `netcoreapp3.1` است)
- `Node.js 18+`
- `SQL Server` در دسترس لوکال

## Project Structure

- `FactorMaker/`: وب API
- `factor-app/`: پنل Angular
- `factor-app/scripts/seed-store-data-utf8.ps1`: اسکریپت داده واقعی‌نما برای دمو و تست

## Backend Setup

1. فایل [FactorMaker/appsettings.json](/D:/Project/FactorMaker/FactorMaker/appsettings.json:1) را بررسی کنید.
2. در صورت نیاز `ConnectionStrings:FactorMakerConnectionString` را با SQL Server خودتان تنظیم کنید.
3. API را اجرا کنید:

```powershell
Set-Location D:\Project\FactorMaker\FactorMaker
dotnet run
```

API در محیط توسعه روی هر دو آدرس بالا می‌آید:

- `http://localhost:5004`
- `https://localhost:5005`

نکته: دیتابیس در startup با `Database.Migrate()` ساخته یا به‌روز می‌شود. اگر نقش پیش‌فرضی وجود نداشته باشد، نقش `StoreOwner` ساخته می‌شود تا ثبت‌نام عمومی بتواند فروشگاه بسازد.

## Frontend Setup

```powershell
Set-Location D:\Project\FactorMaker\factor-app
npm install
npx ng serve
```

فرانت به‌صورت پیش‌فرض از آدرس HTTP همین API استفاده می‌کند:

- `http://localhost:5004`

فرانت در محیط توسعه روی این آدرس بالا می‌آید:

- `http://localhost:4500`

## Default Login

کاربر اولیه از طریق seed دیتابیس پروژه ساخته می‌شود:

- `UserName: Programmer`
- `Password: prog@123`

## Seed Demo Data

بعد از بالا آمدن API، برای ساخت داده واقعی‌نما از این اسکریپت استفاده کنید:

```powershell
Set-Location D:\Project\FactorMaker\factor-app\scripts
powershell -ExecutionPolicy Bypass -File .\seed-store-data-utf8.ps1
```

اسکریپت به‌صورت پیش‌فرض به `http://localhost:5004` وصل می‌شود. برای آدرس دیگر:

```powershell
powershell -ExecutionPolicy Bypass -File .\seed-store-data-utf8.ps1 -BaseUrl "https://localhost:5005"
```

این اسکریپت این داده‌ها را برای فروشگاه دمو می‌سازد یا تکمیل می‌کند:

- فروشگاه نمونه با صفحه عمومی، شعار، پالت رنگ و لینک ارتباطی
- دسته‌بندی‌ها
- کالاهای قابل نمایش در ویترین، با توضیح کوتاه
- مشتری‌ها
- فاکتورهای بسته‌شده با آیتم‌های واقعی‌نما
- چند مطلب وبلاگ و اسلاید منتشرشده

فروشگاه دمو با نام انگلیسی `factormaker-demo` ساخته می‌شود. صفحه عمومی آن:

- `http://localhost:4500/s/factormaker-demo`

## Suggested Demo Flow

1. API را اجرا کنید.
2. فرانت را اجرا کنید.
3. با `Programmer / prog@123` وارد شوید.
4. اسکریپت seed را اجرا کنید.
5. فروشگاه `فروشگاه نمونه فکتور میکر` را انتخاب کنید.
6. صفحات `داشبورد`, `محصولات`, `مشتریان`, `فاکتورها` و `فروشگاه` را بررسی کنید.
7. صفحه عمومی فروشگاه را از دکمه `صفحه عمومی` یا مسیر `/s/factormaker-demo` باز کنید. فهرست فروشگاه‌های فعال هم در `/stores/active` است.

## Build Checks

```powershell
Set-Location D:\Project\FactorMaker
dotnet build FactorMaker.sln

Set-Location D:\Project\FactorMaker\factor-app
npx ng build
```

## Notes

- تمرکز این نسخه روی هسته محصول، داشبورد و ویترین عمومی فروشگاه است.
- ثبت‌نام از صفحه خانه حساب و فروشگاه عمومی می‌سازد. مسیرهای Gmail و اینستاگرام ورود OAuth نیستند؛ همان فرم ثبت‌نام را با ایمیل یا آدرس پیج پر می‌کنند و آدرس اینستاگرام به‌عنوان لینک اجتماعی فروشگاه ذخیره می‌شود.
- وبلاگ و اسلایدها در پنل هستند و اسکریپت seed چند مطلب نمونه برای ویترین می‌سازد، اما مسیر اصلی دمو همچنان فروش، فاکتور و صفحه فروشگاه است.
- اگر برای انتشار عمومی اسکرین‌شات می‌خواهید، بعد از seed از صفحات `Dashboard`, `Products`, `Customers`, `Factors`, `Stores` و صفحه عمومی فروشگاه استفاده کنید.
