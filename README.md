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

- `.NET SDK`
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

نکته: دیتابیس در startup با `EnsureCreated()` ساخته می‌شود.

## Frontend Setup

```powershell
Set-Location D:\Project\FactorMaker\factor-app
npm install
npx ng serve
```

فرانت به‌صورت پیش‌فرض از این آدرس API استفاده می‌کند:

- `https://localhost:5001`

## Default Login

کاربر اولیه از طریق seed دیتابیس پروژه ساخته می‌شود:

- `UserName: Programmer`
- `Password: prog123`

## Seed Demo Data

بعد از بالا آمدن API، برای ساخت داده واقعی‌نما از این اسکریپت استفاده کنید:

```powershell
Set-Location D:\Project\FactorMaker\factor-app\scripts
powershell -ExecutionPolicy Bypass -File .\seed-store-data-utf8.ps1
```

این اسکریپت این داده‌ها را برای فروشگاه دمو می‌سازد یا تکمیل می‌کند:

- فروشگاه نمونه
- دسته‌بندی‌ها
- کالاها
- مشتری‌ها
- فاکتورهای بسته‌شده با آیتم‌های واقعی‌نما

## Suggested Demo Flow

1. API را اجرا کنید.
2. فرانت را اجرا کنید.
3. با `Programmer / prog123` وارد شوید.
4. اسکریپت seed را اجرا کنید.
5. فروشگاه `فروشگاه نمونه فکتور میکر` را انتخاب کنید.
6. صفحات `داشبورد`, `محصولات`, `مشتریان`, `فاکتورها`, `فروشگاه` را بررسی کنید.

## Build Checks

```powershell
Set-Location D:\Project\FactorMaker
dotnet build FactorMaker.sln

Set-Location D:\Project\FactorMaker\factor-app
npx ng build
```

## Notes

- تمرکز این نسخه روی هسته محصول و داشبورد است.
- بخش‌های محتوایی مثل وبلاگ و اسلایدها جزو مسیر اصلی دمو این نسخه نیستند.
- اگر برای انتشار عمومی اسکرین‌شات می‌خواهید، بعد از seed گرفتن از صفحات `Dashboard`, `Products`, `Customers`, `Factors` و `Stores` استفاده کنید.
