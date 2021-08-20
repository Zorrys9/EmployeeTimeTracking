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
    [Route("Reports/File")]
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

        [HttpGet("Json/DetailReports")]
        public async Task<IActionResult> DetailReportForAllInJson()
        {
            var result = await _trackingLogic.DetailReportsJson();
            _logger.Information("Uploading detailed reports on employees to JSON...");
            return result;
        }

        [HttpGet("Xml/DetailReports")]
        public async Task<IActionResult> DetailReportForAllInXml()
        {
            var result = await _trackingLogic.DetailReportsXml();
            _logger.Information("Uploading detailed reports on employees to XML...");
            return result;
        }

        [HttpGet("Json/DetailReports/{id}")]
        public async Task<IActionResult> DetailReportForEmployeeInJson([FromRoute] Guid id)
        {
            var result = await _trackingLogic.DetailReportsEmployeeJson(id);
            _logger.Information("Uploading detailed reports for one employee to JSON...");
            return result;
        }

        [HttpGet("Xml/DetailReports/{id}")]
        public async Task<IActionResult> DetailReportForEmployeeInXml([FromRoute] Guid id)
        {
            var result = await _trackingLogic.DetailReportsEmployeeXml(id);
            _logger.Information("Uploading detailed reports for one employee to XML...");
            return result;
        }

        [HttpGet("Json/SummaryReports/{id}")]
        public async Task<IActionResult> SummaryReportForEmployeeInJson([FromRoute] Guid id)
        {
            var result = await _trackingLogic.SummaryReportsEmployeeJson(id);
            _logger.Information("Uploading summary reports for one employee to JSON...");
            return result;
        }

        [HttpGet("Xml/SummaryReports/{id}")]
        public async Task<IActionResult> SummaryReportForEmployeeInXml([FromRoute] Guid id)
        {
            var result = await _trackingLogic.SummaryReportsEmployeeXml(id);
            _logger.Information("Uploading summary reports for one employee to XML...");
            return result;
        }

        [HttpGet("Json/SummaryReports")]
        public async Task<IActionResult> SummaryReportForAllJson()
        {
            var result = await _trackingLogic.SummaryReportsJson();
            _logger.Information("Uploading summary reports on employees to JSON...");
            return result;
        }

        [HttpGet("Xml/SummaryReports")]
        public async Task<IActionResult> SummaryReportForAllInXml()
        {
            var result = await _trackingLogic.SummaryReportsXml();
            _logger.Information("Uploading summary reports on employees to XML...");
            return result;
        }

        [HttpGet("Template/{employeeId}")]
        public IActionResult DownloadTemplate([FromRoute] Guid employeeId)
        {
            HttpContext.Response.ContentType = "application/vnd.ms-excel";
            _logger.Information("Downloading a report template...");
            return _fileService.GetTemplateReport(employeeId);
        }

        [HttpPost("Save")]
        public async Task<IActionResult> SetReportsFromXls([FromForm] IFormFile file)
        {
            if(file == null)
            {
                return StatusCode(500);
            }
            var result = await _trackingLogic.SetReports(file);
            if (!result)
            {
                return StatusCode(500);
            }
            return StatusCode(201);
        }
    }
}
