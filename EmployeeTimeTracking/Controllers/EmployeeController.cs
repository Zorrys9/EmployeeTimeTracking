using EmployeeTimeTracking._Logic.Logics;
using EmployeeTimeTracking._Services.Services;
using EmployeeTimeTracking.Filters;
using EmployeeTimeTracking.Logic.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeTimeTracking.Controllers
{
    [ApiController]
    [Route("Employees")]
    public class EmployeeController : Controller
    {
        private readonly IAccountLogic _accountLogic;
        public EmployeeController(IAccountLogic accountLogic)
        {
            _accountLogic = accountLogic;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Insert([FromForm]EmployeeViewModel model)
        {
            if(model == null)
            {
                return StatusCode(500);
            }
            var result = await _accountLogic.CreateEmployee(model);

            if (!result)
            {
                return StatusCode(500);
            }

            return StatusCode(201);
        }

        [HttpPut]
        [ValidateModel]
        public async Task<IActionResult> Update([FromForm] EmployeeViewModel model)
        {
            if(model == null)
            {
                return StatusCode(500);
            }
            var result = await _accountLogic.UpdateEmployee(model);

            if (!result)
            {
                return StatusCode(500);
            }

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromForm]Guid id)
        {
            var result = await _accountLogic.DeleteEmployee(id);

            if (!result)
            {
                return StatusCode(500);
            }

            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployeeInfo([FromRoute]Guid id)
        {
            var result = _accountLogic.GetEmployee(id);
            return Content(JsonConvert.SerializeObject(result), "application/json");
        }
    }
}
