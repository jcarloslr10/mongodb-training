using Microsoft.Extensions.Configuration;

namespace MongoDB.Training.Helpers
{
    public class ConfigurationHelper
    {
        public static IConfigurationRoot GetConfiguration()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true)
                .Build();
        }
    }
}
