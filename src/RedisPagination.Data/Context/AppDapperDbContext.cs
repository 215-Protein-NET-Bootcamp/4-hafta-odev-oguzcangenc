using Microsoft.Extensions.Options;
using Npgsql;
using RedisPagination.Core.Data;
using System.Data;

namespace RedisPagination.Data
{
    public class AppDapperDbContext
    {
        public readonly IOptions<DatabaseConfig> _config;
        private readonly string connectionString;

        public AppDapperDbContext(IOptions<DatabaseConfig> config)
        {
            _config = config;
            connectionString = _config.Value.ConnectionString;
        }
        public IDbConnection CreateConnection()
        {
            return new NpgsqlConnection(connectionString);
        }
    }
}
