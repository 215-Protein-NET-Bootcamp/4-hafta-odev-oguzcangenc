using Microsoft.EntityFrameworkCore;
using RedisPagination.Entities;

namespace RedisPagination.Data
{
    public class AppEfDbContext : DbContext
    {
        public AppEfDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
    }
}
