namespace server;

using Microsoft.EntityFrameworkCore;
using server.Data;
using dotenv.net;

public class Startup
{
    private IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(
                Environment.GetEnvironmentVariable("DB_CONNECTION") ?? 
                ConstructDbConnectionString()
            )
        );
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    private string ConstructDbConnectionString()
    {
        DotEnv.Load(options: new DotEnvOptions(envFilePaths: new[] { "../.env" }, ignoreExceptions: false));
        return $"Host={Environment.GetEnvironmentVariable("POSTGRES_HOST")};" +
               $"Port={Environment.GetEnvironmentVariable("POSTGRES_PORT")};" +
               $"Username={Environment.GetEnvironmentVariable("POSTGRES_USER")};" +
               $"Password={Environment.GetEnvironmentVariable("POSTGRES_PASSWORD")};" +
               $"Database={Environment.GetEnvironmentVariable("POSTGRES_DB")};";
    }
}