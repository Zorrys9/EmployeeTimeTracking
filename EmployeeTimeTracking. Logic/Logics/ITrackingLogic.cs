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
        IEnumerable<EmployeeReportViewModel> GetReportsForEmployee(Guid employeeId);
        IEnumerable<EmployeeReportViewModel> GetAllReports();
        FileContentResult DetailReportForAllInJson();
        FileContentResult DetailReportForAllInXml();
        FileContentResult DetailReportForEmployeeInJson(Guid employeeId);
        FileContentResult DetailReportForEmployeeInXml(Guid employeeId);
        FileContentResult SummaryReportsInJson();
        FileContentResult SummaryReportsInXml();
        FileContentResult SummaryReportForEmployeeInJson(Guid employeeId);
        FileContentResult SummaryReportForEmployeeInXml(Guid employeeId);
        Task<bool> SetReportsFromXls(IFormFile file);
    }
}
