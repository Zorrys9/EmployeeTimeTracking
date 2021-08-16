using AutoMapper;
using EmployeeTimeTracking.Common.Models;
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

        public async Task<Guid> DeleteAsync(Guid id)
        {
            var sqlQuery = $"DELETE FROM \"Report\" WHERE \"Id\" = '{id}'  returning \"Id\"";
            var result = await ExecuteAsync(sqlQuery);
            return result;
        }

        public IEnumerable<ReportModel> GetAll()
        {
            var sqlQuery = $"SELECT * FROM \"Report\" ORDER BY \"Name\"";
            var result = GetList(sqlQuery);
            return _mapper.Map<IEnumerable<ReportModel>>(result);
        }

        public ReportModel GetReportById(Guid id)
        {
            var sqlQuery = $"SELECT * FROM \"Report\" WHERE \"Id\" = '{id}'";
            var result = GetEnemy(sqlQuery);
            return _mapper.Map<ReportModel>(result);
        }

        public async Task<Guid> InsertAsync(ReportModel model)
        {
            var sqlQuery = $"INSERT INTO \"Report\" (\"Id\", \"Name\", \"Date\", \"NumberOfHour\", \"Recycling\", \"ReasonForRecycling\", \"DescriptionWork\") VALUES ('{Guid.NewGuid()}', '{model.Name}','{model.Date.ToShortDateString()}','{model.NumberOfHour}','{model.Recycling}','{model.ReasonForRecycling}','{model.DescriptionWork}')  returning \"Id\"";
            var result = await ExecuteAsync(sqlQuery);
            return result;
        }

        public async Task<Guid> UpdateAsync(ReportModel model)
        {
            var sqlQuery = $"UPDATE \"Report\" SET \"Name\" = '{model.Name}', \"Date\"='{model.Date.ToShortDateString()}',\"NumberOfHour\"='{model.NumberOfHour}',\"Recycling\"='{model.Recycling}',\"ReasonForRecycling\"='{model.ReasonForRecycling}',\"DescriptionWork\"='{model.DescriptionWork}' WHERE \"Id\" = '{model.Id}'  returning \"Id\"";
            var result = await ExecuteAsync(sqlQuery);
            return result;
        }
    }
}
