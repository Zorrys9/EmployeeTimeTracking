using AutoMapper;
using EmployeeTimeTracking.Common.Models;
using EmployeeTimeTracking.Common.ViewModels;
using EmployeeTimeTracking.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeTimeTracking.Data.Repository.Implementations
{
    public class ReportRepository : BaseRepository<ReportEntityModel>, IReportRepository
    {
        private readonly IMapper _mapper;
        public ReportRepository(string connectionString, IMapper mapper)
            : base(connectionString)
        {
            _mapper = mapper;
        }

        public async Task<ReportModel> DeleteAsync(Guid id)
        {
            var sqlQuery = $"DELETE FROM \"Report\" WHERE \"Id\" = '{id}'  returning *";
            var result = await GetEnemyAsync(sqlQuery);
            return _mapper.Map<ReportModel>(result);
        }

        public async Task<IEnumerable<ReportModel>> GetAllForPage(PageInfoViewModel pageInfo)
        {
            var sqlQuery = $"SELECT * FROM \"Report\" LIMIT {pageInfo.PageSize} OFFSET {(pageInfo.CurrentPage - 1) * pageInfo.PageSize}";
            var result = await GetListAsync(sqlQuery);
            return _mapper.Map<IEnumerable<ReportModel>>(result);
        }

        public async Task<IEnumerable<ReportModel>> GetAll()
        {
            var sqlQuery = $"SELECT * FROM \"Report\"";
            var result = await GetListAsync(sqlQuery);
            return _mapper.Map<IEnumerable<ReportModel>>(result);
        }
        public async Task<int> Count()
        {
            var sqlQuery = $"SELECT COUNT(*) FROM \"Report\"";
            var result = await GetColumnAsync(sqlQuery);
            return Convert.ToInt32(result);
        }

        public async Task<ReportModel> GetById(Guid id)
        {
            var sqlQuery = $"SELECT * FROM \"Report\" WHERE \"Id\" = '{id}'";
            var result = await GetEnemyAsync(sqlQuery);
            return _mapper.Map<ReportModel>(result);
        }

        public async Task<ReportModel> InsertAsync(ReportModel model)
        {
            var sqlQuery = $"INSERT INTO \"Report\" (\"Id\",  \"Date\", \"NumberOfHour\", \"Recycling\", \"ReasonForRecycling\", \"DescriptionWork\") VALUES ('{Guid.NewGuid()}','{model.Date.ToShortDateString()}','{model.NumberOfHour}','{model.Recycling}','{model.ReasonForRecycling}','{model.DescriptionWork}')  returning *";
            var result = await ExecuteAsync(sqlQuery);
            return _mapper.Map<ReportModel>(result);
        }

        public async Task<ReportModel> UpdateAsync(ReportModel model)
        {
            var sqlQuery = $"UPDATE \"Report\" SET  \"Date\"='{model.Date.ToShortDateString()}',\"NumberOfHour\"='{model.NumberOfHour}',\"Recycling\"='{model.Recycling}',\"ReasonForRecycling\"='{model.ReasonForRecycling}',\"DescriptionWork\"='{model.DescriptionWork}' WHERE \"Id\" = '{model.Id}'  returning *";
            var result = await ExecuteAsync(sqlQuery);
            return _mapper.Map<ReportModel>(result);
        }

    }
}
