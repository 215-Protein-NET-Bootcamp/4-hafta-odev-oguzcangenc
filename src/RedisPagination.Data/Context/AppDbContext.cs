using Microsoft.Extensions.Options;
using Npgsql;
using RedisPagination.Core.Data;
using System.Data;

namespace RedisPagination.Data
{
    public class DapperDbContext
    {
        public readonly IOptions<DatabaseConfig> _config;
        private readonly string connectionString;

        public DapperDbContext(IOptions<DatabaseConfig> config)
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
