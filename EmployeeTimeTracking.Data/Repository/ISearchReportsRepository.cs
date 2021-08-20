using EmployeeTimeTracking.Common.CommonModels;
using EmployeeTimeTracking.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTimeTracking.Data.Repository
{
    public interface ISearchReportsRepository
    {
        Task<ICollection<DetailReportEntityModel>> Search(SearchReportModel model, int pageSize, int currentPage);
        Task<int> Count(SearchReportModel model);
    }
}
