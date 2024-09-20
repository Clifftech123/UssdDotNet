using Microsoft.EntityFrameworkCore;
using ussdDotNet.Contracts;
using ussdDotNet.Models;

namespace ussdDotNet.DBContext
{
    public class UssdDBAppContext : DbContext
    {
        private readonly AppSettings _appSettings;

        public UssdDBAppContext(DbContextOptions<UssdDBAppContext> options, AppSettings appSettings) : base(options)
        {
            _appSettings = appSettings;
        }

        public DbSet<UssdSession> UssdSessions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = _appSettings.USSDConnection;
                optionsBuilder.UseSqlServer(connectionString,
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 10,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null);
                    });
            }
        }
    }
}
