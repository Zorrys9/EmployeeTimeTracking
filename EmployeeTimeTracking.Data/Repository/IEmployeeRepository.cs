using EmployeeTimeTracking.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeTimeTracking.Data.Repository
{
    public interface IEmployeeRepository
    {
        Task<EmployeeEntityModel> InsertAsync(EmployeeEntityModel model);

        Task<EmployeeEntityModel> UpdateAsync(EmployeeEntityModel model);

        Task<EmployeeEntityModel> DeleteAsync(Guid id);

        Task<EmployeeEntityModel> Get(Guid employeeId);

        Task<IEnumerable<EmployeeEntityModel>> GetAll();

    }
}
