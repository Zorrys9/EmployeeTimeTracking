using AutoMapper;
using EmployeeTimeTracking.Common.Models;
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
        public async Task<Guid> DeleteAsync(Guid reportId)
        {
            var sqlQuery = $"DELETE FROM \"EmployeeReports\" WHERE \"ReportId\" = '{reportId}'  returning \"ReportId\"";
            var result = await ExecuteAsync(sqlQuery);
            return result;
        }

        public IEnumerable<EmployeeReportModel> GetByEmployee(Guid employeeId)
        {
            var sqlQuery = $"SELECT * FROM \"EmployeeReports\" WHERE \"EmployeeId\" = '{employeeId}'";
            var result = GetList(sqlQuery);
            return _mapper.Map<IEnumerable<EmployeeReportModel>>(result);
        }

        public Guid GetByReport(Guid reportId)
        {
            var sqlQuery = $"SELECT \"EmployeeId\" FROM \"EmployeeReports\" WHERE \"ReportId\" = '{reportId}'";
            var result = (Guid)GetColumn(sqlQuery);
            return result;
        }

        public async Task<Guid> InsertAsync(EmployeeReportModel model)
        {
            var sqlQuery = $"INSERT INTO \"EmployeeReports\" (\"ReportId\", \"EmployeeId\") VALUES ('{model.ReportId}', '{model.EmployeeId}')  returning \"ReportId\"";
            var result = await ExecuteAsync(sqlQuery);
            return result;
        }
    }
}
