using AutoMapper;
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
        public EmployeeReportRepository(string connectionString)
            :base(connectionString)  {   }

        public async Task<EmployeeReportEntityModel> DeleteAsync(Guid reportId)
        {
            var sqlQuery = $"DELETE FROM \"EmployeeReports\" WHERE \"ReportId\" = '{reportId}'  returning *";
            var result = await GetEnemyAsync(sqlQuery);
            return result;
        }

        public async Task<IEnumerable<EmployeeReportEntityModel>> GetByEmployee(Guid employeeId)
        {
            var sqlQuery = $"SELECT * FROM \"EmployeeReports\" WHERE \"EmployeeId\" = '{employeeId}'";
            var result = await GetListAsync(sqlQuery);
            return result;
        }
        public async Task<IEnumerable<EmployeeReportEntityModel>> GetByEmployeeForPage(Guid employeeId, int pageSize, int currentPage)
        {
            var sqlQuery = $"SELECT * FROM \"EmployeeReports\" WHERE \"EmployeeId\" = '{employeeId}' LIMIT {pageSize} OFFSET {(currentPage - 1) * pageSize}";
            var result = await GetListAsync(sqlQuery);
            return result;
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

        public async Task<EmployeeReportEntityModel> InsertAsync(EmployeeReportEntityModel model)
        {
            var sqlQuery = $"INSERT INTO \"EmployeeReports\" (\"ReportId\", \"EmployeeId\") VALUES ('{model.ReportId}', '{model.EmployeeId}')  returning *";
            var result = await GetEnemyAsync(sqlQuery);
            return result;
        }
    }
}
