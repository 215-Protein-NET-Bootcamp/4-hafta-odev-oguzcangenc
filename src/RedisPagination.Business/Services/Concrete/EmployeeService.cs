using AutoMapper;
using RedisPagination.Data;
using RedisPagination.Entities;

namespace RedisPagination.Business
{
    public class EmployeeService : GenericService<EmployeeDto, Employee>, IEmployeeService
    {
        public EmployeeService(IGenericRepository<Employee> genericRepository, IMapper mapper, IUnitOfWork unitOfWork) : base(genericRepository, mapper, unitOfWork)
        {

        }
    }
}