using EmployeeTimeTracking.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTimeTracking.Data.Repository
{
    public interface ISearchReportsRepository
    {
        Task<ICollection<EmployeeReportViewModel>> SearchReports(SearchReportsViewModel model, PageInfoViewModel pageInfo);
        Task<int> Count(SearchReportsViewModel model);
    }
}
