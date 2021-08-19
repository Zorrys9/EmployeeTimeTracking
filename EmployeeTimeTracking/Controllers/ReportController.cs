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
using Newtonsoft;
using Newtonsoft.Json;

namespace EmployeeTimeTracking.Controllers
{
    [Route("Reports")]
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

        [HttpPost("")]
        public async Task<IActionResult> Insert([FromForm] ReportViewModel model)
        {
            if(model == null)
            {
                return StatusCode(500);
            }
            var result = await _trackingLogic.ReportInsertAsync(model);

            if (!result)
            {
                return StatusCode(500);
            }
            return Ok();
        }

        [HttpPut("")]
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

        [HttpDelete("")]
        public async Task<IActionResult> Delete([Required] Guid id)
        {
            var result = await _trackingLogic.ReportDeleteAsync(id);

            if (!result)
            {
                return StatusCode(500);
            }
            return Ok();
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 4)
        {
            PageInfoViewModel pageInfo = new PageInfoViewModel(pageNumber, 0, pageSize);

            var result = await _trackingLogic.GetAllReportsInPage(pageInfo);
            result.PageInfo = pageInfo;

            if (!result.List.Any() || result.PageInfo.CountItems == 0)
            {
                return StatusCode(500);
            }

            return Content(JsonConvert.SerializeObject(result), "application/json");
        }
        
        [HttpPost("Search")]
        public async Task<PaginationViewModel<EmployeeReportViewModel>> SearchReports([FromForm]SearchReportsViewModel model, int pageNumber = 1, int pageSize = 4)
        {
            if (model == null)
            {
                return null;
                 //return await GetAll(pageNumber, pageSize);
            }
            PageInfoViewModel pageInfo = new PageInfoViewModel(pageNumber, 0, pageSize);
            var result = await _trackingLogic.SearchReports(model, pageInfo);
            if (!result.List.Any() || result.PageInfo.CountItems == 0)
            {
                return null;
               // return StatusCode(500);
            }

            //            return Content(JsonConvert.SerializeObject(result), "application/json");
            return result;
        }

        [HttpGet("ReportById")]
        public async Task<IActionResult> GetReportsById([Required] Guid id, int pageNumber = 1, int pageSize = 4)
        {
            PageInfoViewModel pageInfo = new PageInfoViewModel(pageNumber, 0, pageSize);

            var result = await _trackingLogic.GetReportsByEmployeeForPage(id, pageInfo);

            if (!result.List.Any() || result.PageInfo.CountItems == 0)
            {
                return StatusCode(500);
            }

          
            return Content(JsonConvert.SerializeObject(result), "application/json");
        }
    }
}
