using EmployeeTimeTracking.Common.Models;
using EmployeeTimeTracking.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTimeTracking._Services.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Guid?> InsertAsync(EmployeeModel model)
        {
            if(model == null)
            {
                return null;
            }
            return await _employeeRepository.InsertAsync(model);
        }

        public async Task<Guid?> UpdateAsync(EmployeeModel model)
        {
            if (model == null)
            {
                return null;
            }
            return await _employeeRepository.UpdateAsync(model);
        }

        public async Task<Guid?> DeleteAsync(Guid id)
        {
            return await _employeeRepository.DeleteAsync(id);
        }

        public IEnumerable<EmployeeModel> GetAll()
        {
            return _employeeRepository.GetAll();
        }
    }
}
