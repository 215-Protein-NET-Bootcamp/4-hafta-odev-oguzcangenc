using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using RedisPagination.Business;
using RedisPagination.Core;
using RedisPagination.Entities;

namespace RedisPagination.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }
        [HttpGet]
        public async Task<IActionResult> GetPaginationAsync([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {

            var route = Request.Path.Value;
            PaginationFilter pagintation = new PaginationFilter(pageNumber, pageSize);
            var result = await employeeService.GetPaginationAsync(pagintation, new EmployeeDto(), route);

            if (result.Success)
            {
                return Ok(result);

            }
            return BadRequest(result);

        }
    }
}
