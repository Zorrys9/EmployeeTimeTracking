using EmployeeTimeTracking.Common.Models;
using EmployeeTimeTracking.Common.ViewModels;
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

        public async Task<EmployeeReportModel> DeleteAsync(Guid reportId)
        {
            return await _employeeReportRepository.DeleteAsync(reportId);
        }

        public async Task<IEnumerable<EmployeeReportModel>> GetByEmployee(Guid employeeId)
        {
            return await _employeeReportRepository.GetByEmployee(employeeId);
        }
        public async Task<IEnumerable<EmployeeReportModel>> GetByEmployeeForPage(Guid employeeId, PageInfoViewModel pageInfo)
        {
            return await _employeeReportRepository.GetByEmployeeForPage(employeeId, pageInfo);
        }
        public async Task<int> CountByEmployee(Guid employeeId)
        {
            return await _employeeReportRepository.CountByEmployee(employeeId);
        }

        public async Task<Guid> GetByReport(Guid reportId)
        {
            return await _employeeReportRepository.GetByReport(reportId);
        }

        public async Task<EmployeeReportModel> InsertAsync(EmployeeReportModel model)
        {
            return await _employeeReportRepository.InsertAsync(model);
        }
    }
}
