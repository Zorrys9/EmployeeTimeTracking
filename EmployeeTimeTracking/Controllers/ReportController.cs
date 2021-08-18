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

        [HttpPut("[action]")]
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
        public PaginationViewModel<EmployeeReportViewModel> GetAll(int pageNumber = 1, int pageSize = 4)
        {
            var result = _trackingLogic.GetAllReports();
            if (!result.Any())
            {
                return null;
            }

            PageInfoViewModel pageInfo = new PageInfoViewModel(pageNumber, result.Count(), pageSize);
            PaginationViewModel<EmployeeReportViewModel> pagination = new PaginationViewModel<EmployeeReportViewModel>();

            pagination.List = pagination.Pagination(result, pageNumber, pageSize);
            pagination.PageInfo = pageInfo;

            return pagination;
        }

        [HttpPost("[action]")]
        public PaginationViewModel<EmployeeReportViewModel> SearchReports([FromForm]SearchReportsViewModel model, int pageNumber = 1, int pageSize = 4)
        {
            if (model == null)
            {
                return GetAll(pageNumber, pageSize);
            }
            var result = _trackingLogic.SearchReports(model);
            if (!result.Any())
            {
                return null;
            }

            PageInfoViewModel pageInfo = new PageInfoViewModel(pageNumber, result.Count(), pageSize);
            PaginationViewModel<EmployeeReportViewModel> pagination = new PaginationViewModel<EmployeeReportViewModel>();

            pagination.List = pagination.Pagination(result, pageNumber, pageSize);
            pagination.PageInfo = pageInfo;

            return pagination;
        }

        [HttpGet("[action]")]
        public PaginationViewModel<EmployeeReportViewModel> GetReportById([Required] Guid id, int pageNumber = 1, int pageSize = 4)
        {
            var result = _trackingLogic.GetReportsForEmployee(id);

            if (!result.Any())
            {
                return null;
            }

            PageInfoViewModel pageInfo = new PageInfoViewModel(pageNumber, result.Count(), pageSize);
            PaginationViewModel<EmployeeReportViewModel> pagination = new PaginationViewModel<EmployeeReportViewModel>();

            pagination.List = pagination.Pagination(result, pageNumber, pageSize);
            pagination.PageInfo = pageInfo;
            
            return pagination;
        }
    }
}
