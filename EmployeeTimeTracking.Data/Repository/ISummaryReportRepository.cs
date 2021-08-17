using EmployeeTimeTracking.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTimeTracking.Data.Repository
{
    public interface ISummaryReportRepository
    {
        SummaryReportViewModel GetSummaryReportById(Guid id);
        IEnumerable<SummaryReportViewModel> GetSummaryReports();
    }
}
