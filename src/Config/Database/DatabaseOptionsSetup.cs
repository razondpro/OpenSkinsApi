namespace OpenSkinsApi.Config.Database
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;

    public class DatabaseOptionsSetup : IConfigureOptions<DatabaseOptions>
    {
        private readonly IConfiguration _configuration;

        public DatabaseOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(DatabaseOptions options)
        {
            options.MaxRetryCount = int.Parse(_configuration["MYSQL_MAX_RETRY_COUNT"] ?? "3");
            options.CommandTimeout = int.Parse(_configuration["MYSQL_COMMAND_TIMEOUT"] ?? "30");
            options.EnableDetailedErrors = bool.Parse(_configuration["MYSQL_ENABLE_DETAILED_ERRORS"] ?? "false");
            options.EnableSensitiveDataLogging = bool.Parse(_configuration["MYSQL_ENABLE_SENSITIVE_DATA_LOGGING"] ?? "false");

            var host = _configuration["MYSQL_HOST"] ?? "localhost";
            var port = _configuration["MYSQL_PORT"] ?? "3306";
            var database = _configuration["MYSQL_DATABASE"] ?? "skins";
            var username = _configuration["MYSQL_USERNAME"] ?? "root";
            var password = _configuration["MYSQL_PASSWORD"] ?? "root_password";

            options.ConnectionString = $"Server={host};Port={port};Database={database};Uid={username};Pwd={password};";
        }
    }
}