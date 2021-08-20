using EmployeeTimeTracking._Logic.Logics;
using EmployeeTimeTracking.Common.CommonModels;
using EmployeeTimeTracking.Filters;
using EmployeeTimeTracking.Logic.ViewModels;
using EmployeeTimeTracking.Services.Models;
using EmployeeTimeTracking.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeTimeTracking.Controllers
{
    [Route("Reports")]
    [ApiController]
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;
        private readonly ITrackingLogic _trackingLogic;

        public ReportController(IReportService reportService, ITrackingLogic trackingLogic)
        {
            _reportService = reportService;
            _trackingLogic = trackingLogic;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Insert([FromForm] ReportViewModel model)
        {
            if (model == null)
            {
                return StatusCode(500);
            }
            var result = await _trackingLogic.InsertReportAsync(model);

            if (!result)
            {
                return StatusCode(500);
            }
            return Ok();
        }

        [HttpPut]
        [ValidateModel]
        public async Task<IActionResult> Update([FromForm] ReportModel model)
        {
            if (model == null)
            {
                return StatusCode(500);
            }
            var result = await _reportService.UpdateAsync(model);

            if (result == null)
            {
                return StatusCode(500);
            }
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([Required] Guid id)
        {
            var result = await _trackingLogic.DeleteReportAsync(id);

            if (!result)
            {
                return StatusCode(500);
            }
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 4)
        {
            PageInfoViewModel pageInfo = new PageInfoViewModel(pageNumber, 0, pageSize);

            var result = await _trackingLogic.GetReportsForPage(pageInfo);
            result.PageInfo = pageInfo;

            if (!result.List.Any() || result.PageInfo.CountItems == 0)
            {
                return StatusCode(500);
            }

            return Content(JsonConvert.SerializeObject(result), "application/json");
        }

        [HttpPost("Search")]
        [ValidateModel]
        public async Task<IActionResult> SearchReports([FromForm] SearchReportModel model, int pageNumber = 1, int pageSize = 4)
        {
            if (model == null)
            {
                return await GetAll(pageNumber, pageSize);
            }
            PageInfoViewModel pageInfo = new PageInfoViewModel(pageNumber, 0, pageSize);
            var result = await _trackingLogic.SearchReports(model, pageInfo);
            if (!result.List.Any() || result.PageInfo.CountItems == 0)
            {
                return StatusCode(500);
            }

            return Content(JsonConvert.SerializeObject(result), "application/json");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReportsById([FromRoute] Guid id, int pageNumber = 1, int pageSize = 4)
        {
            PageInfoViewModel pageInfo = new PageInfoViewModel(pageNumber, 0, pageSize);

            var result = await _trackingLogic.GetReportsEmployeeForPage(id, pageInfo);

            if (!result.List.Any() || result.PageInfo.CountItems == 0)
            {
                return StatusCode(500);
            }

            return Content(JsonConvert.SerializeObject(result), "application/json");
        }
    }
}
