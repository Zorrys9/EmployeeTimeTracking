using EmployeeTimeTracking.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTimeTracking._Services.Services
{
    public interface IEmployeeService
    {
        Task<Guid?> InsertAsync(EmployeeModel model);
        Task<Guid?> UpdateAsync(EmployeeModel model);
        Task<Guid?> DeleteAsync(Guid id);
        IEnumerable<EmployeeModel> GetAll();
    }
}
