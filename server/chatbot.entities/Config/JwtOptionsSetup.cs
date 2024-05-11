
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace chatbot.entities.Config
{
    public class JwtOptionsSetup : IConfigureOptions<JwtOptions>
    {
        private readonly IConfiguration _configuration;
        private const string sectionName = "JsonWebTokenConfig";
        public JwtOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(JwtOptions options)
        {
            _configuration.GetSection(sectionName);
        }
    }
}
