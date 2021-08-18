using EmployeeTimeTracking.Common.Models;
using EmployeeTimeTracking.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeTimeTracking.Data.Repository
{
    public interface IEmployeeRepository : IBaseRepository<EmployeeEntityModel>
    {

        Task<Guid> InsertAsync(EmployeeModel model);

        Task<Guid> UpdateAsync(EmployeeModel model);

        Task<Guid> DeleteAsync(Guid id);

        IEnumerable<EmployeeModel> GetAll();

        EmployeeModel Get(Guid employeeId);
    }
}
