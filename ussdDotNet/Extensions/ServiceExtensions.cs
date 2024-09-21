using Microsoft.EntityFrameworkCore;
using ussdDotNet.Contracts;

namespace ussdDotNet.Extensions
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// Configures the application settings by reading values from the configuration.
        /// </summary>
        /// <param name="services">The service collection to add the settings to.</param>
        /// <param name="configuration">The configuration to read the settings from.</param>
        /// <exception cref="InvalidOperationException">Thrown when a required configuration value is not found.</exception>
        public static void ConfigureAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettings = new AppSettings
            {
                USSDConnection = configuration.GetConnectionString("USSDConnection") ?? throw new InvalidOperationException("Connection string 'USSDConnection' not found."),
                DisableAppMsg = configuration.GetSection("AppSettings").GetValue<string>("DisableAppMsg") ?? throw new InvalidOperationException("AppSettings.DisableAppMsg not found."),
                DebugMsg = configuration.GetSection("AppSettings").GetValue<string>("DebugMsg") ?? throw new InvalidOperationException("AppSettings.DebugMsg not found."),
                ShortCode = configuration.GetSection("AppSettings").GetValue<string>("ShortCode") ?? throw new InvalidOperationException("AppSettings.ShortCode not found.")
            };

            services.AddSingleton(appSettings);
        }




    }
}
