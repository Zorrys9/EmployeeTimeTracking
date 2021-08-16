using EmployeeTimeTracking.Common.Models;
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

        public ReportService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
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
    }
}
