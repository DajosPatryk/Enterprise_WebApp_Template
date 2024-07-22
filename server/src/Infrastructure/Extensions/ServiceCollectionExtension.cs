namespace server.Infrastructure.Extensions;

using dotenv.net;
using Microsoft.EntityFrameworkCore;
using Data;
using FluentValidation;
using server.Application.Behaviors;
using server.Application.Services;
using server.Infrastructure.Filters;
using server.Application.Commands.Users;
using server.Application.Queries.Users;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        // Loads local .env file if env vars don't exist
        if (Environment.GetEnvironmentVariable("DB_CONNECTION") == null) LoadLocalDotEnvFile();
        
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(
                Environment.GetEnvironmentVariable("DB_CONNECTION") ?? 
                ConstructDbConnectionString()
            )
        );
        services.AddCors(options =>
        {
            options.AddPolicy("AllowLocalOrigins", builder =>
            {
                builder.WithOrigins("http://client:3000")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                builder.WithOrigins("http://localhost:3000")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
            options.AddPolicy("AllowProductionOrigins", builder =>
            {
                builder.WithOrigins("http://client:3000")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                builder.WithOrigins("http://localhost:3000")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                builder.WithOrigins("http://localhost")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                builder.WithOrigins("https://client:3000")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                builder.WithOrigins("https://localhost:3000")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                builder.WithOrigins("https://localhost")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                builder.WithOrigins($"http://{Environment.GetEnvironmentVariable("BASE_URL")}")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                builder.WithOrigins($"https://{Environment.GetEnvironmentVariable("BASE_URL")}")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });
        services.AddControllers(options =>
        {
            options.Filters.Add<ExceptionHandlingFilter>();
        });
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        
        services.AddMediatrServices();
        services.LinkInterfaces();
        services.AddAuthorization();

        return services;
    }
    
    public static IServiceCollection AddMediatrServices(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblyContaining(typeof(Program));

            config.AddOpenBehavior(typeof(ValidatorBehavior<,>));
            config.AddOpenBehavior(typeof(TransactionBehavior<,>));
        });

        // Validators
        services.AddSingleton<IValidator<AddUser>, AddUserValidator>();
        services.AddSingleton<IValidator<UpdateUser>, UpdateUserValidator>();
        services.AddSingleton<IValidator<GetUserBySub>, GetUserBySubValidator>();
        return services;
    }
    
    public static IServiceCollection LinkInterfaces(this IServiceCollection services)
    {
        return services;
    }
    
    public static IServiceCollection AddAuthorization(this IServiceCollection services)
    {
        return services;
    }

    private static void LoadLocalDotEnvFile()
    {
        DotEnv.Load(options: new DotEnvOptions(envFilePaths: new[] { "../.env" }, ignoreExceptions: false));
    }
    
    private static string ConstructDbConnectionString()
    {
        return $"Host={Environment.GetEnvironmentVariable("POSTGRES_HOST")};" +
               $"Port={Environment.GetEnvironmentVariable("POSTGRES_PORT")};" +
               $"Username={Environment.GetEnvironmentVariable("POSTGRES_USER")};" +
               $"Password={Environment.GetEnvironmentVariable("POSTGRES_PASSWORD")};" +
               $"Database={Environment.GetEnvironmentVariable("POSTGRES_DB")};";
    }
}
