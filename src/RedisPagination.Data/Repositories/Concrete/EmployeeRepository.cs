using Microsoft.EntityFrameworkCore;
using RedisPagination.Core;
using RedisPagination.Core.Extensions;
using RedisPagination.Entities;

namespace RedisPagination.Data
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly AppEfDbContext dbContext;
        public EmployeeRepository(AppEfDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<(IEnumerable<Employee> records, int total)> GetPaginationAsync(PaginationFilter paginationFilter, EmployeeDto filterResource)
        {
            var queryable = ConditionFilter(filterResource);

            var total = await queryable.CountAsync();

            var records = await queryable.AsNoTracking()
                .AsSplitQuery()
                .OrderBy(x => x.Id)
                .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                .Take(paginationFilter.PageSize)
                .ToListAsync();

            return (records, total);
        }
        private IQueryable<Employee> ConditionFilter(EmployeeDto filterResource)
        {
            var queryable = dbContext.Employees.AsQueryable();
       
            if (filterResource != null)
            {
               
                if (!string.IsNullOrEmpty(filterResource.FirstName))
                {
                    string fullName = filterResource.FirstName.RemoveSpaceCharacter().ToLower();
                    queryable = queryable.Where(x => x.FirstName.Contains(fullName));
                }

                if (!string.IsNullOrEmpty(filterResource.LastName))
                {
                    string fullName = filterResource.LastName.RemoveSpaceCharacter().ToLower();
                    queryable = queryable.Where(x => x.LastName.Contains(fullName));
                }
            }

            return queryable;
        }
        public async Task<int> TotalRecordAsync()
        {
            return await dbContext.Employees.CountAsync();
        }
    }
}
