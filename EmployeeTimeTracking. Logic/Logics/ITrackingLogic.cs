using EmployeeTimeTracking.Common.ViewModels;
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
        Task<bool> ReportInsertAsync(ReportViewModel model);
        Task<bool> ReportDeleteAsync(Guid reportId);
        Task<PaginationViewModel<EmployeeReportViewModel>> GetReportsByEmployeeForPage(Guid employeeId, PageInfoViewModel pageInfo);
        Task<ICollection<EmployeeReportViewModel>> GetReportsByEmployee(Guid employeeId);
        Task<PaginationViewModel<EmployeeReportViewModel>> GetAllReportsInPage(PageInfoViewModel pageInfo);
        Task<FileContentResult> DetailReportForAllInJson();
        Task<FileContentResult> DetailReportForAllInXml();
        Task<FileContentResult> DetailReportForEmployeeInJson(Guid employeeId);
        Task<FileContentResult> DetailReportForEmployeeInXml(Guid employeeId);
        Task<FileContentResult> SummaryReportsInJson();
        Task<FileContentResult> SummaryReportsInXml();
        Task<FileContentResult> SummaryReportForEmployeeInJson(Guid employeeId);
        Task<FileContentResult> SummaryReportForEmployeeInXml(Guid employeeId);
        Task<bool> SetReportsFromXls(IFormFile file);
        Task<PaginationViewModel<EmployeeReportViewModel>> SearchReports(SearchReportsViewModel model, PageInfoViewModel pageInfo);
    }
}
