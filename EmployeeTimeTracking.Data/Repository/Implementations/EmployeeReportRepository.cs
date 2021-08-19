using AutoMapper;
using EmployeeTimeTracking.Common.Models;
using EmployeeTimeTracking.Common.ViewModels;
using EmployeeTimeTracking.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTimeTracking.Data.Repository.Implementations
{
    public class EmployeeReportRepository : BaseRepository<EmployeeReportEntityModel>, IEmployeeReportRepository
    {
        private readonly IMapper _mapper;
        public EmployeeReportRepository(string connectionString, IMapper mapper)
            :base(connectionString)
        {
            _mapper = mapper;
        }
        public async Task<EmployeeReportModel> DeleteAsync(Guid reportId)
        {
            var sqlQuery = $"DELETE FROM \"EmployeeReports\" WHERE \"ReportId\" = '{reportId}'  returning *";
            var result = await GetEnemyAsync(sqlQuery);
            return _mapper.Map<EmployeeReportModel>(result);
        }

        public async Task<IEnumerable<EmployeeReportModel>> GetByEmployee(Guid employeeId)
        {
            var sqlQuery = $"SELECT * FROM \"EmployeeReports\" WHERE \"EmployeeId\" = '{employeeId}'";
            var result = await GetListAsync(sqlQuery);
            return _mapper.Map<IEnumerable<EmployeeReportModel>>(result);
        }
        public async Task<IEnumerable<EmployeeReportModel>> GetByEmployeeForPage(Guid employeeId, PageInfoViewModel pageInfo)
        {
            var sqlQuery = $"SELECT * FROM \"EmployeeReports\" WHERE \"EmployeeId\" = '{employeeId}' LIMIT {pageInfo.PageSize} OFFSET {(pageInfo.CurrentPage - 1) * pageInfo.PageSize}";
            var result = await GetListAsync(sqlQuery);
            return _mapper.Map<IEnumerable<EmployeeReportModel>>(result);
        }
        public async Task<int> CountByEmployee(Guid employeeId)
        {
            var sqlQuery = $"SELECT COUNT(*) FROM \"EmployeeReports\" WHERE \"EmployeeId\" = '{employeeId}'";
            var result = await GetColumnAsync(sqlQuery);
            return Convert.ToInt32(result);
        }

        public async Task<Guid> GetByReport(Guid reportId)
        {
            var sqlQuery = $"SELECT \"EmployeeId\" FROM \"EmployeeReports\" WHERE \"ReportId\" = '{reportId}'";
            var result =  (Guid) await GetColumnAsync(sqlQuery);
            return result;
        }

        public async Task<EmployeeReportModel> InsertAsync(EmployeeReportModel model)
        {
            var sqlQuery = $"INSERT INTO \"EmployeeReports\" (\"ReportId\", \"EmployeeId\") VALUES ('{model.ReportId}', '{model.EmployeeId}')  returning *";
            var result = await GetEnemyAsync(sqlQuery);
            return _mapper.Map<EmployeeReportModel>(result);
        }
    }
}
