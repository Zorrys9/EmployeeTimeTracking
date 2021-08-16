using EmployeeTimeTracking.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTimeTracking.Data.Repository
{
    public interface IReportRepository
    {
        Task<Guid> InsertAsync(ReportModel model);
        Task<Guid> UpdateAsync(ReportModel model);
        Task<Guid> DeleteAsync(Guid id);
        IEnumerable<ReportModel> GetAll();
        ReportModel GetReportById(Guid id);
    }
}
