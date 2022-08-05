using RedisPagination.Entities;


namespace RedisPagination.Data
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AppEfDbContext dbContext) : base(dbContext)
        {
        }
    }
}
