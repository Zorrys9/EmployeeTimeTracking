using EmployeeTimeTracking.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTimeTracking.Services.Services
{
    public interface IEmployeeReportService
    {

        Task<Guid> InsertAsync(EmployeeReportModel model);
        Task<Guid> DeleteAsync(Guid reportId);
        IEnumerable<EmployeeReportModel> GetByEmployee(Guid employeeId);
        IEnumerable<EmployeeReportModel> GetByReport(Guid reportId);



    }
}
