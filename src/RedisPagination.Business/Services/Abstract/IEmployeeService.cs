using RedisPagination.Core;
using RedisPagination.Entities;


namespace RedisPagination.Business
{
    public interface IEmployeeService:IGenericService<EmployeeDto,Employee>
    {
        Task<IDataResult<IEnumerable<EmployeeDto>>> GetPaginationAsync(PaginationFilter paginationFilter, EmployeeDto filterResource,string route);

    }
}
