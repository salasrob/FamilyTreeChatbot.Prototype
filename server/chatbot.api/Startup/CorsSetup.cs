
namespace chatbot.api.Startup
{
    public class CorsSetup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllCors", builder =>
                {
                    builder.AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials()
                           .SetIsOriginAllowedToAllowWildcardSubdomains()
                           .SetIsOriginAllowed(delegate (string requestingOrigin)
                           {
                               return true;
                           }).Build();
                });
            });
        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("AllowAllCors");
        }
    }
}
