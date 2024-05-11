
namespace chatbot.entities.Config
{
    public class AppSettings
    {
        public string SqlDatabaseConnectionString { get; set; }
        public JwtOptions JsonWebTokenSecret { get; set; }
    }
}
