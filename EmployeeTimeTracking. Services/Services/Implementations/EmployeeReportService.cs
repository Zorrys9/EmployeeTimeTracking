using AutoMapper;
using EmployeeTimeTracking.Data.EntityModels;
using EmployeeTimeTracking.Data.Repository;
using EmployeeTimeTracking.Services.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeTimeTracking.Services.Services.Implementations
{
    /// <summary>
    /// Сервис по работе с сотрудниками и их отчетами
    /// </summary>
    public class EmployeeReportService : IEmployeeReportService
    {
        private readonly IEmployeeReportRepository _employeeReportRepository;
        private readonly IMapper _mapper;

        public EmployeeReportService(IEmployeeReportRepository employeeReportRepository, IMapper mapper)
        {
            _employeeReportRepository = employeeReportRepository;
            _mapper = mapper;
        }

        public async Task<EmployeeReportModel> DeleteAsync(Guid reportId)
        {
            var result = await _employeeReportRepository.DeleteAsync(reportId);
            return _mapper.Map<EmployeeReportModel>(result);
        }

        public async Task<IEnumerable<EmployeeReportModel>> GetByEmployee(Guid employeeId)
        {
            var result = await _employeeReportRepository.GetByEmployee(employeeId);
            return _mapper.Map<IEnumerable<EmployeeReportModel>>(result);
        }
        public async Task<IEnumerable<EmployeeReportModel>> GetByEmployeeForPage(Guid employeeId, int pageSize, int currentPage)
        {
            var result = await _employeeReportRepository.GetByEmployeeForPage(employeeId, pageSize, currentPage);
            return _mapper.Map<IEnumerable<EmployeeReportModel>>(result);
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
            var newModel = _mapper.Map<EmployeeReportEntityModel>(model);
            var result = await _employeeReportRepository.InsertAsync(newModel);
            return _mapper.Map<EmployeeReportModel>(result);
        }
    }
}
