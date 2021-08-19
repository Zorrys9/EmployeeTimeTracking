using EmployeeTimeTracking.Common.Models;
using EmployeeTimeTracking.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeTimeTracking.Data.Repository
{
    public interface IEmployeeRepository
    {
        Task<EmployeeModel> InsertAsync(EmployeeModel model);

        Task<EmployeeModel> UpdateAsync(EmployeeModel model);

        Task<EmployeeModel> DeleteAsync(Guid id);

        Task<IEnumerable<EmployeeModel>> GetAll();

        Task<EmployeeModel> GetById(Guid employeeId);
    }
}
