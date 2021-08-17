using EmployeeTimeTracking.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTimeTracking.Data.Repository
{
    public interface IEmployeeReportRepository
    {
        Task<Guid> InsertAsync(EmployeeReportModel model);
        Task<Guid> DeleteAsync(Guid reportId);
        IEnumerable<EmployeeReportModel> GetByEmployee(Guid employeeId);
        Guid GetByReport(Guid reportId);
    }
}
