using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RedisPagination.Core;
using RedisPagination.Core.Extensions;
using RedisPagination.Data;
using RedisPagination.Entities;
using System.Text;

namespace RedisPagination.Business
{
    public class EmployeeService : GenericService<EmployeeDto, Employee>, IEmployeeService
    {
        private readonly IRelatePaginationUri relatePaginationUri;
        private readonly IEmployeeRepository employeeRepository;
        private readonly IDistributedCache distributedCache;
        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper, IUnitOfWork unitOfWork, IRelatePaginationUri relatePaginationUri, IDistributedCache distributedCache) : base(employeeRepository, mapper, unitOfWork)
        {
            this.relatePaginationUri = relatePaginationUri;
            this.employeeRepository = employeeRepository;
            this.distributedCache = distributedCache;
        }

        public async Task<IDataResult<IEnumerable<EmployeeDto>>> GetPaginationAsync(PaginationFilter paginationFilter, EmployeeDto filterResource, string route)
        {
            var cacheKey = $"{route}-{paginationFilter.PageNumber}-{paginationFilter.PageSize}";
            string json;
            PaginationEntityResponse<EmployeeDto> employees;
            var employeesFromCache = await distributedCache.GetAsync(cacheKey);
            if (employeesFromCache != null)
            {
                json = Encoding.UTF8.GetString(employeesFromCache);
                employees = JsonConvert.DeserializeObject<PaginationEntityResponse<EmployeeDto>>(json);
                return new PaginatedResult<IEnumerable<EmployeeDto>>(employees.Data, employees.PageNumber, employees.PageSize)
                {
                    PreviousPage = employees.PreviousPage,
                    NextPage = employees.NextPage,
                    LastPage = employees.LastPage,
                    FirstPage = employees.FirstPage,
                    TotalPages = employees.TotalPages,
                    TotalRecords = employees.TotalRecords
                };
            }
            else
            {
                var paginationPerson = await employeeRepository.GetPaginationAsync(paginationFilter, filterResource);
                PaginatedResult<IEnumerable<EmployeeDto>> resource = GeneratePagination(paginationFilter, route, paginationPerson);
                json = JsonConvert.SerializeObject(resource);
                employeesFromCache = Encoding.UTF8.GetBytes(json);
                var options = new DistributedCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromDays(1))
                        .SetAbsoluteExpiration(DateTime.Now.AddMonths(1));
                await distributedCache.SetAsync(cacheKey, employeesFromCache, options);
                return resource;
            }
        }
        private PaginatedResult<IEnumerable<EmployeeDto>> GeneratePagination(PaginationFilter paginationFilter, string route, (IEnumerable<Employee> records, int total) paginationPerson)
        {
            var tempResource = Mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(paginationPerson.records);
            var resource = new PaginatedResult<IEnumerable<EmployeeDto>>(tempResource);
            resource.CreatePaginationResponse(paginationFilter, paginationPerson.total, relatePaginationUri, route);
            return resource;
        }
    }
}