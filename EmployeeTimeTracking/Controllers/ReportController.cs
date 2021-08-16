using EmployeeTimeTracking._Logic.Logics;
using EmployeeTimeTracking.Common.Models;
using EmployeeTimeTracking.Common.ViewModels;
using EmployeeTimeTracking.Services.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeTimeTracking.Controllers
{
    [Route("[controller]")]
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

        [HttpPost("[action]")]
        public async Task<IActionResult> Insert([FromForm] ReportViewModel model)
        {
            var result = await _trackingLogic.ReportInsertAsync(model);

            if (!result)
            {
                return StatusCode(500);
            }
            return Ok();
        }
        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromForm] ReportModel model)
        {
            var result = await _reportService.UpdateAsync(model);

            if (result == null)
            {
                return StatusCode(500);
            }
            return Ok();
        }
        [HttpDelete("[action]")]
        public async Task<IActionResult> Delete([FromForm] Guid id)
        {
            var result = await _trackingLogic.ReportDeleteAsync(id);

            if (!result)
            {
                return StatusCode(500);
            }
            return Ok();
        }
        [HttpPost("[action]")]
        public IEnumerable<ReportModel> GetAll()
        {
            var result = _reportService.GetAll();

            if(result.Count() == 0)
            {
                return null;
            }
            return result;
        }
        [HttpPost("[action]")]
        public ReportModel GetReporyById([FromForm]Guid id)
        {
            var result = _reportService.GetReportById(id);
            return result;
        }
    }
}
