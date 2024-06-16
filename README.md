# Enterprise Web-App Template - Nuxt3 + .NET8 + PostgreSQL

Development package based on Vue3 Nuxt, .NET8 and PostgreSQL. Modern & Performance Oriented.
[Nuxt3 documentation](https://nuxt.com/docs/getting-started/introduction) to learn more.
[.NET8 documentation](https://learn.microsoft.com/en-us/dotnet/fundamentals/) to learn more.

> **Note:**
> This package was made by Avalonia for personal use. It includes pages for legal and privacy policies.

# Setup

Install tools. [.NET8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) is required for this project.

```bash
dotnet tool install --global dotnet-ef
```

Run migrations on database, for this the database (Docker) needs to run.

```bash
cd ./server
dotnet ef database update
```

## Development Server

Start the development server on client `http://localhost:3000`, api `http://localhost:8080`, db `http://localhost:5432`.

```bash
docker-compose up --build
```

## Production

Build the application for production:

```bash
docker-compose -f docker-compose.prod.yml up -d
```

# Frontend Package - Nuxt3

This package was made in consideration for most optimal production performance. It also uses powerful, crispy and clean UI packages.
The package includes:
- NuxtJs Mailer - For sending out E-mails through NuxtJs server.
- NuxtJs Content - For loading formatted markdown files, great for policies and blogs.
- Language framework [i18n](https://developer.mozilla.org/en-US/docs/Mozilla/Add-ons/WebExtensions/API/i18n) - Uses [@nuxtjs/i18n](https://i18n-legacy.nuxtjs.org/basic-usage/), vue-i18n-routing for browser and [language detection](https://v8.i18n.nuxtjs.org/guide/browser-language-detection).
- PostCSS - Base post-processed CSS language.
- UI Components: [ShadCn-Vue](https://www.shadcn-vue.com/docs/components/accordion.html) - Visit the link to install more components.
- Tailwind - Theme engine, a powerful custom configuration is included.
- Global Theme Variable - A theme variable class that is hooked onto default layout as a class.
- Radix Icons - Crispy and modern 15x15 vector icons.
- SF UI Text - Apple's free-to-use font. Crispy base font-family fitting all other installed packages.
- Header - A beautiful and easily expandable Header element inspired by [OpenAI](https://openai.com/) Web-App state 11.06.2024.

# Backend Package - .NET8
This package was made in consideration for optimal maintainability, production performance and productivity. It uses powerful tools supported by MSFT.
The package includes:
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=netcore-cli) ORM - Full ORM maintained by MSFT, utilizes memory for optimal performance.

## Development

Create new migrations based on Application Db Context.

```bash
cd ./server
dotnet ef migrations add Initial
```