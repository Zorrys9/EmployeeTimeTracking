using EmployeeTimeTracking.Common.CommonModels;
using EmployeeTimeTracking.Logic.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmployeeTimeTracking._Logic.Logics
{
    public interface ITrackingLogic
    {
        Task<bool> InsertReportAsync(ReportViewModel model);
        Task<bool> DeleteReportAsync(Guid reportId);
        Task<PaginationViewModel<EmployeeReportViewModel>> GetReportsEmployeeForPage(Guid employeeId, PageInfoViewModel pageInfo);
        Task<ICollection<EmployeeReportViewModel>> GetReportsEmployee(Guid employeeId);
        Task<PaginationViewModel<EmployeeReportViewModel>> GetReportsForPage(PageInfoViewModel pageInfo);
        Task<FileContentResult> DetailReportsJson();
        Task<FileContentResult> DetailReportsXml();
        Task<FileContentResult> DetailReportsEmployeeJson(Guid employeeId);
        Task<FileContentResult> DetailReportsEmployeeXml(Guid employeeId);
        Task<FileContentResult> SummaryReportsJson();
        Task<FileContentResult> SummaryReportsXml();
        Task<FileContentResult> SummaryReportsEmployeeJson(Guid employeeId);
        Task<FileContentResult> SummaryReportsEmployeeXml(Guid employeeId);
        Task<bool> SetReports(IFormFile file);
        Task<PaginationViewModel<EmployeeReportViewModel>> SearchReports(SearchReportModel model, PageInfoViewModel pageInfo);
    }
}
