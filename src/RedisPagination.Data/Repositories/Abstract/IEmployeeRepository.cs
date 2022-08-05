using RedisPagination.Core;
using RedisPagination.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisPagination.Data
{
    public interface IEmployeeRepository:IGenericRepository<Employee>
    {
        Task<(IEnumerable<Employee> records, int total)> GetPaginationAsync(PaginationFilter paginationFilter, EmployeeDto filterResource);
        Task<int> TotalRecordAsync();
    }
}
