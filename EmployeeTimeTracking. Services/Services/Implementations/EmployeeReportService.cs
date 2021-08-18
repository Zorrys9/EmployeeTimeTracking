using EmployeeTimeTracking.Common.Models;
using EmployeeTimeTracking.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTimeTracking.Services.Services.Implementations
{
    /// <summary>
    /// Сервис по работе с сотрудниками и их отчетами
    /// </summary>
    public class EmployeeReportService : IEmployeeReportService
    {
        private readonly IEmployeeReportRepository _employeeReportRepository;

        public EmployeeReportService(IEmployeeReportRepository employeeReportRepository)
        {
            _employeeReportRepository = employeeReportRepository;
        }

        public async Task<Guid> DeleteAsync(Guid reportId)
        {
            return await _employeeReportRepository.DeleteAsync(reportId);
        }

        public IEnumerable<EmployeeReportModel> GetByEmployee(Guid employeeId)
        {
            return _employeeReportRepository.GetByEmployee(employeeId);
        }

        public Guid GetByReport(Guid reportId)
        {
            return _employeeReportRepository.GetByReport(reportId);
        }

        public async Task<Guid?> InsertAsync(EmployeeReportModel model)
        {
            return await _employeeReportRepository.InsertAsync(model);
        }
    }
}
