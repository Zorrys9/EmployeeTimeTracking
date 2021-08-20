using EmployeeTimeTracking.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTimeTracking.Data.Repository
{
    public interface IEmployeeReportRepository
    {
        Task<EmployeeReportEntityModel> InsertAsync(EmployeeReportEntityModel model);
        Task<EmployeeReportEntityModel> DeleteAsync(Guid reportId);
        Task<IEnumerable<EmployeeReportEntityModel>> GetByEmployee(Guid employeeId);
        Task<IEnumerable<EmployeeReportEntityModel>> GetByEmployeeForPage(Guid employeeId, int pageSize, int currentPage);
        Task<int> CountByEmployee(Guid employeeId);
        Task<Guid> GetByReport(Guid reportId);
    }
}
