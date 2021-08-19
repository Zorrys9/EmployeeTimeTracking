using EmployeeTimeTracking.Common.Models;
using EmployeeTimeTracking.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTimeTracking.Data.Repository
{
    public interface IReportRepository
    {
        Task<ReportModel> InsertAsync(ReportModel model);
        Task<ReportModel> UpdateAsync(ReportModel model);
        Task<ReportModel> DeleteAsync(Guid id);
        Task<IEnumerable<ReportModel>> GetAllForPage(PageInfoViewModel pageInfo);
        Task<IEnumerable<ReportModel>> GetAll();
        Task<int> Count();
        Task<ReportModel> GetById(Guid id);
    }
}
