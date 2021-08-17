using EmployeeTimeTracking.Common.Models;
using EmployeeTimeTracking.Common.ViewModels;
using EmployeeTimeTracking.Data.Repository;
using EmployeeTimeTracking.Services.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeTimeTracking._Services.Services.Implementations
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;
        private readonly ISummaryReportRepository _summaryReportRepository;

        public ReportService(IReportRepository reportRepository, ISummaryReportRepository summaryReportRepository)
        {
            _reportRepository = reportRepository;
            _summaryReportRepository = summaryReportRepository;
        }

        public async Task<Guid?> DeleteAsync(Guid id)
        {
            return await _reportRepository.DeleteAsync(id);
        }

        public IEnumerable<ReportModel> GetAll()
        {
            return _reportRepository.GetAll();
        }

        public ReportModel GetReportById(Guid id)
        {
            return _reportRepository.GetReportById(id);
        }

        public async Task<Guid?> InsertAsync(ReportModel model)
        {
            if(model == null)
            {
                return null;
            }
            return await _reportRepository.InsertAsync(model);
        }

        public async Task<Guid?> UpdateAsync(ReportModel model)
        {
            if (model == null)
            {
                return null;
            }
            return await _reportRepository.UpdateAsync(model);
        }

        public SummaryReportViewModel SummaryReportById(Guid id)
        {
            return _summaryReportRepository.GetSummaryReportById(id);
        }

        public IEnumerable<SummaryReportViewModel> SummaryReports()
        {
            return _summaryReportRepository.GetSummaryReports();
        }
    }
}
