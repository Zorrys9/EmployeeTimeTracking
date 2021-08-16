using EmployeeTimeTracking.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTimeTracking.Services.Services
{
    public interface IReportService
    {
        Task<Guid?> InsertAsync(ReportModel model);
        Task<Guid?> UpdateAsync(ReportModel model);
        Task<Guid?> DeleteAsync(Guid Id);
        IEnumerable<ReportModel> GetAll();
        ReportModel GetReportById(Guid id);

    }
}
