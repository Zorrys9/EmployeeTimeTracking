using EmployeeTimeTracking._Logic.Logics;
using EmployeeTimeTracking.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace EmployeeTimeTracking.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : Controller
    {
        private readonly ITrackingLogic _trackingLogic;
        private readonly IFileService _fileService;
        private readonly ILogger _logger;
        public FileController(ITrackingLogic trackingLogic, IFileService fileService, ILogger logger)
        {
            _trackingLogic = trackingLogic;
            _fileService = fileService;
            _logger = logger;
        }

        [HttpGet("[action]")]
        public IActionResult DetailReportForAllInJson()
        {
            var result = _trackingLogic.DetailReportForAllInJson();
            _logger.Information("Uploading detailed reports on employees to JSON...");
            return result;
        }

        [HttpGet("[action]")]
        public IActionResult DetailReportForAllInXml()
        {
            var result = _trackingLogic.DetailReportForAllInXml();
            _logger.Information("Uploading detailed reports on employees to XML...");
            return result;
        }

        [HttpGet("[action]")]
        public IActionResult DetailReportForEmployeeInJson([Required] Guid id)
        {
            var result = _trackingLogic.DetailReportForEmployeeInJson(id);
            _logger.Information("Uploading detailed reports for one employee to JSON...");
            return result;
        }

        [HttpGet("[action]")]
        public IActionResult DetailReportForEmployeeInXml([Required] Guid id)
        {
            var result = _trackingLogic.DetailReportForEmployeeInXml(id);
            _logger.Information("Uploading detailed reports for one employee to XML...");
            return result;
        }

        [HttpGet("[action]")]
        public IActionResult SummaryReportForEmployeeInJson([Required] Guid id)
        {
            var result = _trackingLogic.SummaryReportForEmployeeInJson(id);
            _logger.Information("Uploading summary reports for one employee to JSON...");
            return result;
        }

        [HttpGet("[action]")]
        public IActionResult SummaryReportForEmployeeInXml([Required] Guid id)
        {
            var result = _trackingLogic.SummaryReportForEmployeeInXml(id);
            _logger.Information("Uploading summary reports for one employee to XML...");
            return result;
        }

        [HttpGet("[action]")]
        public IActionResult SummaryReportForAllJson()
        {
            var result = _trackingLogic.SummaryReportsInJson();
            _logger.Information("Uploading summary reports on employees to JSON...");
            return result;
        }

        [HttpGet("[action]")]
        public IActionResult SummaryReportForAllInXml()
        {
            var result = _trackingLogic.SummaryReportsInXml();
            _logger.Information("Uploading summary reports on employees to XML...");
            return result;
        }

        [HttpGet("[action]")]
        public IActionResult DownloadTemplate([Required] Guid employeeId)
        {
            HttpContext.Response.ContentType = "application/vnd.ms-excel";
            _logger.Information("Downloading a report template...");
            return _fileService.DownloadTemplateReport(employeeId);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SetReportsFromXls([FromForm] IFormFile file)
        {
            if(file == null)
            {
                return StatusCode(500);
            }
            var result = await _trackingLogic.SetReportsFromXls(file);
            if (!result)
            {
                return StatusCode(500);
            }
            return StatusCode(201);
        }
    }
}
