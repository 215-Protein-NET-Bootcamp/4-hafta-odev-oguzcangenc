using AutoMapper;
using RedisPagination.Core;
using RedisPagination.Core.Extensions;
using RedisPagination.Data;
using RedisPagination.Entities;

namespace RedisPagination.Business
{
    public class EmployeeService : GenericService<EmployeeDto, Employee>, IEmployeeService
    {
        private readonly IRelatePaginationUri relatePaginationUri;
        private readonly IEmployeeRepository employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper, IUnitOfWork unitOfWork,IRelatePaginationUri relatePaginationUri) : base(employeeRepository, mapper, unitOfWork)
        {
            this.relatePaginationUri = relatePaginationUri;
            this.employeeRepository = employeeRepository;

        }

        public async Task<IDataResult<IEnumerable<EmployeeDto>>> GetPaginationAsync(PaginationFilter paginationFilter, EmployeeDto filterResource,string route)
        {
            var paginationPerson = await employeeRepository.GetPaginationAsync(paginationFilter, filterResource);
            
            // Mapping
            var tempResource = Mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(paginationPerson.records);

            var resource = new PaginatedResult<IEnumerable<EmployeeDto>>(tempResource);

            ////// Using extension-method for pagination
            resource.CreatePaginationResponse(paginationFilter, paginationPerson.total, relatePaginationUri,route);

            return resource;
        }
    }
}