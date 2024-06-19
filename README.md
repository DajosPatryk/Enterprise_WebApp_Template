# Enterprise Web-App Template - Nuxt3 + .NET8 + PostgreSQL

Development package based on Vue3 Nuxt, .NET8 and PostgreSQL. Modern & Performance Oriented.
[Nuxt3 documentation](https://nuxt.com/docs/getting-started/introduction) to learn more.
[.NET8 documentation](https://learn.microsoft.com/en-us/dotnet/fundamentals/) to learn more.

> **Note:**
> This package was made by Avalonia for personal use. It includes pages for legal and privacy policies.

# Setup

Bun is required to manage this projects client.
- [Windows Download](https://bun.sh/docs/installation#windows)
- [Linux/MacOS Download](https://bun.sh/docs/installation#macos-and-linux)

.NET8 is required to manage this projects server.
- [Windows Download](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Linux/MacOS Download](https://learn.microsoft.com/en-us/dotnet/core/install/linux)

Install Entity Framework Core tool.

```bash
dotnet tool install --global dotnet-ef
```

## Development Server

Start the development server on Client `http://localhost:3000`, API `http://localhost:8080`, DB `http://localhost:5432`.

```bash
docker-compose up --build
```

Run migrations on database, for this the database (Docker) needs to run.

```bash
cd ./server
dotnet ef database update
```

## Production

Build the application for production:

```bash
docker-compose -f docker-compose.prod.yml up --build
```

The application is accessible on `http://localhost:80` and `http://localhost:443`.
API is accessible at `http://localhost:80/api` and `http://localhost:443/api`.

> **Note:**
> Production uses Nginx reverse-proxy method to send '/api' routes to API. 
> In order to lock API away from the world, remove `location /api/` configuration from `nginx/nginx.conf`.
> 
> ```
> location /api/ {
>           rewrite ^/api(/.*)$ $1 break;
>           proxy_pass http://api;
>           proxy_http_version 1.1;
>           proxy_set_header Upgrade $http_upgrade;
>           proxy_set_header Connection 'upgrade';
>           proxy_set_header Host $host;
>           proxy_cache_bypass $http_upgrade;
>           limit_req zone=req_limit burst=20 nodelay;
>           proxy_cache req_cache;
>       }
> ```

# Frontend Package - Nuxt3


This package was made in consideration for most optimal production performance. It also uses powerful, crispy and clean UI packages.
> **Note:**
> This package does not use Typescript, instead it opts for Javascript with types set by JSDoc comments. This is for best productivity and build speeds.

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
- PostgreSQL - Feature-rich SQL database.
- Mediator - Mediator design pattern aims to reduce dependencies between objects by restricting direct communication, creating a way for them to collaborate only through the mediator object.
- [Fluent Results](https://github.com/altmann/FluentResults) - A lightweight Result object implementation, made to compile and return responses.
- [Fluent Validation](https://github.com/FluentValidation/FluentValidation) - Library for building strongly-typed validation rules.

## Development

Create initial migration based on Application Db Context.

```bash
cd ./server
dotnet ef migrations add Initial
```

Create new migrations based on Application Db Context.

```bash
cd ./server
dotnet ef migrations add Migration
```

Run migrations on database, for this the database (Docker) needs to run.

```bash
cd ./server
dotnet ef database update
```