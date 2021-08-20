using AutoMapper;
using EmployeeTimeTracking.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeTimeTracking.Data.Repository.Implementations
{
    public class ReportRepository : BaseRepository<ReportEntityModel>, IReportRepository
    {
        public ReportRepository(string connectionString)
            : base(connectionString)  {     }

        public async Task<ReportEntityModel> DeleteAsync(Guid id)
        {
            var sqlQuery = $"DELETE FROM \"Report\" WHERE \"Id\" = '{id}'  returning *";
            var result = await GetEnemyAsync(sqlQuery);
            return result;
        }

        public async Task<IEnumerable<ReportEntityModel>> GetAllForPage(int pageSize, int currentPage)
        {
            var sqlQuery = $"SELECT * FROM \"Report\" LIMIT {pageSize} OFFSET {(currentPage - 1) * pageSize}";
            var result = await GetListAsync(sqlQuery);
            return result;
        }

        public async Task<IEnumerable<ReportEntityModel>> GetAll()
        {
            var sqlQuery = $"SELECT * FROM \"Report\"";
            var result = await GetListAsync(sqlQuery);
            return result;
        }
        public async Task<int> Count()
        {
            var sqlQuery = $"SELECT COUNT(*) FROM \"Report\"";
            var result = await GetColumnAsync(sqlQuery);
            return Convert.ToInt32(result);
        }

        public async Task<ReportEntityModel> Get(Guid id)
        {
            var sqlQuery = $"SELECT * FROM \"Report\" WHERE \"Id\" = '{id}'";
            var result = await GetEnemyAsync(sqlQuery);
            return result;
        }

        public async Task<ReportEntityModel> InsertAsync(ReportEntityModel model)
        {
            var sqlQuery = $"INSERT INTO \"Report\" (\"Id\",  \"Date\", \"NumberOfHour\", \"Recycling\", \"ReasonForRecycling\", \"DescriptionWork\") VALUES ('{Guid.NewGuid()}','{model.Date.ToShortDateString()}','{model.NumberOfHour}','{model.Recycling}','{model.ReasonForRecycling}','{model.DescriptionWork}')  returning *";
            var result = await GetEnemyAsync(sqlQuery);
            return result;
        }

        public async Task<ReportEntityModel> UpdateAsync(ReportEntityModel model)
        {
            var sqlQuery = $"UPDATE \"Report\" SET  \"Date\"='{model.Date.ToShortDateString()}',\"NumberOfHour\"='{model.NumberOfHour}',\"Recycling\"='{model.Recycling}',\"ReasonForRecycling\"='{model.ReasonForRecycling}',\"DescriptionWork\"='{model.DescriptionWork}' WHERE \"Id\" = '{model.Id}'  returning *";
            var result = await GetEnemyAsync(sqlQuery);
            return result;
        }

    }
}
