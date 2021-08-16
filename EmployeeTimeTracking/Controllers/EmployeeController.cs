using EmployeeTimeTracking._Services.Services;
using EmployeeTimeTracking.Common.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeTimeTracking.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Insert([FromForm]EmployeeModel model)
        {
            var result = await _employeeService.InsertAsync(model);

            if (result == null)
            {
                return StatusCode(500);
            }

            return Ok();
        }
        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromForm] EmployeeModel model)
        {
            var result = await _employeeService.UpdateAsync(model);

            if (result == null)
            {
                return StatusCode(500);
            }

            return Ok();
        }
        [HttpDelete("[action]")]
        public async Task<IActionResult> Delete([FromForm]Guid id)
        {
            var result = await _employeeService.DeleteAsync(id);

            if (result == null)
            {
                return StatusCode(500);
            }

            return Ok();
        }
        [HttpPost("[action]")]
        public IEnumerable<EmployeeModel> GetAll()
        {
            var result = _employeeService.GetAll();

            if(result.Count() == 0)
            {
                return null;
            }
            return result;
        }

    }
}
