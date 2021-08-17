using EmployeeTimeTracking._Logic.Logics;
using EmployeeTimeTracking.Common.Models;
using EmployeeTimeTracking.Common.ViewModels;
using EmployeeTimeTracking.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmployeeTimeTracking.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;
        private readonly ITrackingLogic _trackingLogic;
        private readonly IFileService _fileService;

        public ReportController(IReportService reportService, ITrackingLogic trackingLogic, IFileService fileService)
        {
            _reportService = reportService;
            _trackingLogic = trackingLogic;
            _fileService = fileService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Insert([Required] ReportViewModel model)
        {
            var result = await _trackingLogic.ReportInsertAsync(model);

            if (!result)
            {
                return StatusCode(500);
            }
            return Ok();
        }
        [HttpPut("[action]")]
        public async Task<IActionResult> Update([Required] ReportModel model)
        {
            var result = await _reportService.UpdateAsync(model);

            if (result == null)
            {
                return StatusCode(500);
            }
            return Ok();
        }
        [HttpDelete("[action]")]
        public async Task<IActionResult> Delete([Required] Guid id)
        {
            var result = await _trackingLogic.ReportDeleteAsync(id);

            if (!result)
            {
                return StatusCode(500);
            }
            return Ok();
        }
        [HttpGet("[action]")]
        public IEnumerable<EmployeeReportViewModel> GetAll()
        {
            var result = _trackingLogic.GetAllReports();

            if(result.Count() == 0)
            {
                return null;
            }
            return result;
        }
        [HttpGet("[action]")]
        public IEnumerable<EmployeeReportViewModel> GetReportById([Required] Guid id)
        {
            var result = _trackingLogic.GetReportsForEmployee(id);
            if(result.Count() == 0)
            {
                return null;
            }
            return result;
        }
        [HttpGet("[action]")]
        public IActionResult DetailReportForAllInJson()
        {
            var result = _trackingLogic.DetailReportForAllInJson();

            return result;
        }
        [HttpGet("[action]")]
        public IActionResult DetailReportForAllInXml()
        {
            var result =_trackingLogic.DetailReportForAllInXml();

            return result;
        }
        [HttpGet("[action]")]
        public IActionResult DetailReportForEmployeeInJson([Required] Guid id)
        {
            var result = _trackingLogic.DetailReportForEmployeeInJson(id);

            return result;
        }
        [HttpGet("[action]")]
        public IActionResult DetailReportForEmployeeInXml([Required] Guid id)
        {
            var result = _trackingLogic.DetailReportForEmployeeInXml(id);

            return result;
        }
        [HttpGet("[action]")]
        public IActionResult SummaryReportForEmployeeInJson([Required] Guid id)
        {
            var result = _trackingLogic.SummaryReportForEmployeeInJson(id);

            return result;
        }
        [HttpGet("[action]")]
        public IActionResult SummaryReportForEmployeeInXml([Required] Guid id)
        {
            var result = _trackingLogic.SummaryReportForEmployeeInXml(id);

            return result;
        }
        [HttpGet("[action]")]
        public IActionResult SummaryReportForAllJson()
        {
            var result = _trackingLogic.SummaryReportsInJson();

            return result;
        }
        [HttpGet("[action]")]
        public IActionResult SummaryReportForAllInXml()
        {
            var result = _trackingLogic.SummaryReportsInXml();

            return result;
        }
        [HttpGet("[action]")]
        public IActionResult DownloadTemplate([Required]Guid employeeId)
        {
            HttpContext.Response.ContentType = "application/vnd.ms-excel";
            return _fileService.DownloadTemplateReport(employeeId);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> SetReportsFromXls([FromForm]IFormFile file)
        {
            var result = await _trackingLogic.SetReportsFromXls(file);
            if (!result)
            {
                return StatusCode(500);
            }
            return Ok();
        }
    }
}
