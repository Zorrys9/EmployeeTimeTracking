using EmployeeTimeTracking.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTimeTracking.Data.Repository
{
    public interface IReportRepository
    {
        Task<IEnumerable<ReportEntityModel>> GetAllForPage(int pageSize, int currentPage);
        Task<ReportEntityModel> InsertAsync(ReportEntityModel model);
        Task<ReportEntityModel> UpdateAsync(ReportEntityModel model);
        Task<ReportEntityModel> DeleteAsync(Guid id);
        Task<IEnumerable<ReportEntityModel>> GetAll();
        Task<ReportEntityModel> Get(Guid id);
        Task<int> Count();
    }
}
