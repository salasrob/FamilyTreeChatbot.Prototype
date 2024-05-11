
using chatbot.entities.Config;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "chatbot.api v1"));
}
else
{
    app.UseHsts(); // TODO: learn more about this
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

void ConfigConfiguration(WebApplicationBuilder builder)
{
    var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
    IConfigurationBuilder root = builder.Configuration.SetBasePath(builder.Environment.ContentRootPath);

    //the settings in the env settings will override the appsettings.json values, recursively at the key level.
    // where the key could be nested. this would allow very fine tuned control over the settings
    IConfigurationBuilder appSettings = root.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

    string jsonFileName = $"appsettings.{builder.Environment.EnvironmentName}.json";
    IConfigurationBuilder envSettings = appSettings
        .AddJsonFile(jsonFileName, optional: true, reloadOnChange: true);
}

void ConfigureLogger(IServiceCollection services, ILoggingBuilder logger)
{
    var loggerConfiguration = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration.GetSection("Serilog"))
    .CreateLogger();

    services.AddLogging(cfg => cfg.AddSerilog(loggerConfiguration));
}

void ConfigureAppSettings(IServiceCollection services)
{
    var config = builder.Configuration;
    services.Configure<AppSettings>(config.GetSection("AppSettings"));
    services.Configure<SecurityConfig>(config.GetSection("SecurityConfig"));
    services.Configure<JwtOptions>(config.GetSection("JsonWebTokenConfig"));
    //services.Configure<AzureEmailSettings>(config.GetSection("AzureEmailSettings"));
}

void ConfigureDataAccessLayer(IServiceCollection services)
{
    //services.AddSingleton<ITokenDataRepository, TokenDataRepository>();
    //services.AddSingleton<IUsersDataRepository, UsersDataRepository>();
    //services.AddSingleton<IEmailDataRepository, EmailDataRepository>();
}

void ConfigureBusinessServicesLayer(IServiceCollection services)
{
    //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    //services.AddSingleton<IAuthenticationBusinessService<int>, AuthenticationBusinessService>();
    //services.AddSingleton<ITokenBusinessService, TokenBusinessService>();
    //services.AddSingleton<IUsersBusinessService, UsersBusinessService>();
    //services.AddSingleton<IEmailerBusinessService, EmailerBusinessService>();
    //services.AddSingleton<ITokenProvider, TokenProvider>();
}
