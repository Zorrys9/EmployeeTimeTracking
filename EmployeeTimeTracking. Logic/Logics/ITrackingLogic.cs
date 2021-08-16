using EmployeeTimeTracking.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeTimeTracking._Logic.Logics
{
    public interface ITrackingLogic
    {
        Task<bool> ReportInsertAsync(ReportViewModel model);
        Task<bool> ReportDeleteAsync(Guid reportId);
        IEnumerable<EmployeeReportViewModel> GetAllReports();
        IEnumerable<EmployeeReportViewModel> GetReportsForEmployee(Guid employeeId);

    }
}
