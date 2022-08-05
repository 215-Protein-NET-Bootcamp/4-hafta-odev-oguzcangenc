using Microsoft.AspNetCore.Mvc;
using RedisPagination.Business;
using RedisPagination.Core;
using RedisPagination.Entities;
using Serilog;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        //[HttpGet]
        //public async Task<IActionResult> Get()
        //{
        //    var result = await employeeService.GetAllAsync();
        //    if (result.Success)
        //    {
        //        return Ok(result);
        //    }
        //    return BadRequest(result.Message);
        //}

        [HttpGet]
        public async Task<IActionResult> GetPaginationAsync([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            Log.Information($"Get pagination person.");

            var route = Request.Path.Value;

            PaginationFilter pagintation = new PaginationFilter(pageNumber, pageSize);
            var result = await employeeService.GetPaginationAsync(pagintation,new EmployeeDto(), route);

            if (!result.Success)
                return BadRequest(result);



            return Ok(result);
        }

        //[HttpPost("pagination")]
        //public async Task<IActionResult> GetPaginationWithFilterAsync([FromQuery] int page, [FromQuery] int pageSize, [FromBody] PersonDto filterResource)
        //{
        //    Log.Information($"{User.Identity?.Name}: get pagination person.");
        //    filterResource.CreatedBy = (User.Identity as ClaimsIdentity).FindFirst("AccountId").Value;
        //    filterResource.StaffId = (User.Identity as ClaimsIdentity).FindFirst("AccountId").Value;

        //    QueryResource pagintation = new QueryResource(page, pageSize);

        //    var result = await personService.GetPaginationAsync(pagintation, filterResource);

        //    if (!result.Success)
        //        return BadRequest(result);

        //    if (result.Response is null)
        //        return NoContent();

        //    return Ok(result);
        //}
    }
}
