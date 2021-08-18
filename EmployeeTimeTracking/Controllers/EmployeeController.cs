using EmployeeTimeTracking._Logic.Logics;
using EmployeeTimeTracking._Services.Services;
using EmployeeTimeTracking.Common.Models;
using EmployeeTimeTracking.Common.ViewModels;
using Microsoft.AspNetCore.Http;
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
        private readonly IAccountLogic _accountLogic;
        public EmployeeController(IEmployeeService employeeService, IAccountLogic accountLogic)
        {
            _employeeService = employeeService;
            _accountLogic = accountLogic;
        }

        [HttpPost("[action]")]
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

        [HttpPut("[action]")]
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

        [HttpDelete("[action]")]
        public async Task<IActionResult> Delete([FromForm]Guid id)
        {
            var result = await _accountLogic.DeleteEmployee(id);

            if (!result)
            {
                return StatusCode(500);
            }

            return Ok();
        }

        [HttpPost("[action]")]
        public PaginationViewModel<EmployeeModel> GetAll(int pageNumber = 1, int pageSize = 4)
        {
            var result = _employeeService.GetAll();
            if (!result.Any())
            {
                return null;
            }

            PageInfoViewModel pageInfo = new PageInfoViewModel(pageNumber, result.Count(), pageSize);
            PaginationViewModel<EmployeeModel> pagination = new PaginationViewModel<EmployeeModel>();

            pagination.List = pagination.Pagination(result, pageNumber, pageSize);
            pagination.PageInfo = pageInfo;

            return pagination;
        }

        [HttpPost("[action]")]
        public EmployeeViewModel GetEmployeeInfo([FromForm]Guid id)
        {
            var result = _accountLogic.GetEmployeeInfo(id);

            return result;
        }
    }
}
