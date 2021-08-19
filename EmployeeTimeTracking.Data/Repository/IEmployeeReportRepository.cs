using EmployeeTimeTracking.Common.Models;
using EmployeeTimeTracking.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTimeTracking.Data.Repository
{
    public interface IEmployeeReportRepository
    {
        Task<EmployeeReportModel> InsertAsync(EmployeeReportModel model);
        Task<EmployeeReportModel> DeleteAsync(Guid reportId);
        Task<IEnumerable<EmployeeReportModel>> GetByEmployee(Guid employeeId);
        Task<IEnumerable<EmployeeReportModel>> GetByEmployeeForPage(Guid employeeId, PageInfoViewModel pageInfo);
        Task<int> CountByEmployee(Guid employeeId);
        Task<Guid> GetByReport(Guid reportId);
    }
}
