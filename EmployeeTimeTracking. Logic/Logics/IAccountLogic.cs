using EmployeeTimeTracking.Logic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTimeTracking._Logic.Logics
{
    public interface IAccountLogic
    {
        Task<bool> CreateEmployee(EmployeeViewModel model);
        Task<bool> UpdateEmployee(EmployeeViewModel model);
        Task<bool> DeleteEmployee(Guid id);
        EmployeeViewModel GetEmployee(Guid id);

    }
}
