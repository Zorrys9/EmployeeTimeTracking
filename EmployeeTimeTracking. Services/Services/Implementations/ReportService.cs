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

        public async Task<ReportModel> DeleteAsync(Guid id)
        {
            return await _reportRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ReportModel>> GetAllForPage(PageInfoViewModel pageInfo)
        {
            return await _reportRepository.GetAllForPage(pageInfo);
        }

        public async Task<IEnumerable<ReportModel>> GetAll()
        {
            return await _reportRepository.GetAll();
        }

        public async Task<ReportModel> GetById(Guid id)
        {
            return await _reportRepository.GetById(id);
        }

        public async Task<ReportModel> InsertAsync(ReportModel model)
        {
            _logger.Information("A new report has been created");
            return await _reportRepository.InsertAsync(model);
        }

        public async Task<ReportModel> UpdateAsync(ReportModel model)
        {
            return await _reportRepository.UpdateAsync(model);
        }

        public async Task<SummaryReportViewModel> SummaryReportById(Guid id)
        {
            return await _summaryReportRepository.GetById(id);
        }

        public async Task<IEnumerable<SummaryReportViewModel>> SummaryReports()
        {
            return await _summaryReportRepository.GetAll();
        }

        public async Task<ICollection<EmployeeReportViewModel>> SearchReports(SearchReportsViewModel model, PageInfoViewModel pageInfo)
        {
            if(model.LastDate == DateTime.MinValue)
            {
                model.LastDate = DateTime.Now;
            }
            return await _searchReportsRepository.SearchReports(model, pageInfo);
        }

        public async Task<int> CountAll()
        {
            return await _reportRepository.Count();
        }

        public async Task<int> CountFound(SearchReportsViewModel model)
        {
            if (model.LastDate == DateTime.MinValue)
            {
                model.LastDate = DateTime.Now;
            }
            return await _searchReportsRepository.Count(model);
        }
    }
}
