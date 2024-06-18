using FluentValidation;
using server.Application.Behaviors;
using server.Application.Commands.Users;
using server.Application.Queries.Users;
using server.Infrastructure.Filters;

namespace server.Infrastructure.Extensions;

using dotenv.net;
using Microsoft.EntityFrameworkCore;
using Data;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
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
                builder.WithOrigins("http://localhost:3000")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                builder.WithOrigins("http://client:3000")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });
        services.AddMediatrServices();
        services.AddControllers(options =>
        {
            options.Filters.Add<ExceptionHandlingFilter>();
        });
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

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

        services.AddSingleton<IValidator<AddUser>, AddUserValidator>();
        services.AddSingleton<IValidator<UpdateUser>, UpdateUserValidator>();
        services.AddSingleton<IValidator<GetUserBySub>, GetUserBySubValidator>();

        return services;
    }
    
    private static string ConstructDbConnectionString()
    {
        DotEnv.Load(options: new DotEnvOptions(envFilePaths: new[] { "../.env" }, ignoreExceptions: false));
        
        return $"Host={Environment.GetEnvironmentVariable("POSTGRES_HOST")};" +
               $"Port={Environment.GetEnvironmentVariable("POSTGRES_PORT")};" +
               $"Username={Environment.GetEnvironmentVariable("POSTGRES_USER")};" +
               $"Password={Environment.GetEnvironmentVariable("POSTGRES_PASSWORD")};" +
               $"Database={Environment.GetEnvironmentVariable("POSTGRES_DB")};";
    }
}