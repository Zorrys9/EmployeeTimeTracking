using EmployeeTimeTracking.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTimeTracking.Data.Repository
{
    public interface ISummaryReportRepository
    {
        Task<SummaryReportEntityModel> Get(Guid id);
        Task<IEnumerable<SummaryReportEntityModel>> GetAll();
    }
}
