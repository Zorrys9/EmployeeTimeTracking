using EmployeeTimeTracking.Common.Models;
using EmployeeTimeTracking.Common.ViewModels;
using EmployeeTimeTracking.Data.Repository;
using EmployeeTimeTracking.Services.Services;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeTimeTracking._Services.Services.Implementations
{
    /// <summary>
    /// Сервис по работе с отчетами сотрудников
    /// </summary>
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;
        private readonly ISummaryReportRepository _summaryReportRepository;
        private readonly ISearchReportsRepository _searchReportsRepository;
        private readonly ILogger _logger;
        public ReportService(IReportRepository reportRepository, ISummaryReportRepository summaryReportRepository,
            ISearchReportsRepository searchReportsRepository, ILogger logger)
        {
            _reportRepository = reportRepository;
            _summaryReportRepository = summaryReportRepository;
            _searchReportsRepository = searchReportsRepository;
            _logger = logger;
        }

        public async Task<Guid?> DeleteAsync(Guid id)
        {
            return await _reportRepository.DeleteAsync(id);
        }

        public IEnumerable<ReportModel> GetAll()
        {
            return _reportRepository.GetAll();
        }

        public ReportModel GetById(Guid id)
        {
            return _reportRepository.GetById(id);
        }

        public async Task<Guid?> InsertAsync(ReportModel model)
        {
            _logger.Information("A new report has been created");
            return await _reportRepository.InsertAsync(model);
        }

        public async Task<Guid?> UpdateAsync(ReportModel model)
        {
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

        public IEnumerable<EmployeeReportViewModel> SearchReports(SearchReportsViewModel model)
        {
            if(model.LastDate == DateTime.MinValue)
            {
                model.LastDate = DateTime.Now;
            }
            return _searchReportsRepository.SearchReports(model);
        }
    }
}
