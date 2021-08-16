using AutoMapper;
using EmployeeTimeTracking.Common.Models;
using EmployeeTimeTracking.Common.ViewModels;
using EmployeeTimeTracking.Services.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeTimeTracking._Logic.Logics.Implementations
{
    public class TrackingLogic : ITrackingLogic
    {
        private readonly IReportService _reportService;
        private readonly IEmployeeReportService _employeeReportService;
        private readonly IMapper _mapper;
        public TrackingLogic(IReportService reportService, IEmployeeReportService employeeReportService, IMapper mapper)
        {
            _reportService = reportService;
            _employeeReportService = employeeReportService;
            _mapper = mapper;
        }
        public async Task<bool> ReportDeleteAsync(Guid reportId)
        {
            var resultDelete = await _employeeReportService.DeleteAsync(reportId);
            var result = await _reportService.DeleteAsync(resultDelete);
            
            if(result == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> ReportInsertAsync(ReportViewModel model)
        {
            var reportModel = _mapper.Map<ReportModel>(model);
            var reportId = _reportService.InsertAsync(reportModel);
            var employeeReportModel = _mapper.Map<EmployeeReportModel>(model);
            employeeReportModel.ReportId = (Guid)await reportId;

            var result = await _employeeReportService.InsertAsync(employeeReportModel);
            if (string.IsNullOrEmpty(result.ToString()))
            {
                return false;
            }
            return true;
        }

        public IEnumerable<EmployeeReportViewModel> GetAllReports()
        {
            throw new();
        }

        public IEnumerable<EmployeeReportViewModel> GetReportsForEmployee(Guid employeeId)
        {
            throw new();
        }
    }
}
