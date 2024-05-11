
using Microsoft.AspNetCore.Authentication.JwtBearer;
using chatbot.entities.Config;

namespace chatbot.api.Startup
{
    public class AuthenticationSetup
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            SetUpJwtBearerAuth(services, configuration);
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(15);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = false;
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                options.Cookie.MaxAge = TimeSpan.FromMinutes(60 * 24 * 30);
            });
        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
        }

        private static void SetUpJwtBearerAuth(IServiceCollection services, IConfiguration configuration)
        {
            SecurityConfig security = new SecurityConfig();
            JwtOptions jsonWebTokenConfig = new JwtOptions();

            configuration.GetSection("SecurityConfig").Bind(security);
            configuration.GetSection("JsonWebTokenConfig").Bind(jsonWebTokenConfig);

            // If you don't want the cookie to be automatically authenticated and assigned to
            // HttpContext.User, remove the CookieAuthenticationDefaults.AuthenticationScheme
            // parameter passed to AddAuthentication.
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();

            services.ConfigureOptions<JwtOptionsSetup>();
            services.ConfigureOptions<JwtBearerOptionsSetup>();
        }
    }
}
