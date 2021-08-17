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
    public class EmployeeRepository:BaseRepository<EmployeeEntityModel>, IEmployeeRepository
    {
        private readonly IMapper _mapper;
        public EmployeeRepository(string connectionString, IMapper mapper)
            : base(connectionString)
        {
            _mapper = mapper;
        }


        public async Task<Guid> InsertAsync(EmployeeModel model)
        {
            var sqlQuery = $"INSERT INTO \"Employee\" (\"Id\", \"FirstName\", \"SecondName\", \"LastName\", \"DateOfBirth\", \"Position\", \"Avatar\") VALUES ('{model.Id}', '{model.FirstName}', '{model.SecondName}','{model.LastName}', '{model.DateOfBirth.ToShortDateString()}', '{model.Position}', '{model.Avatar}') returning \"Id\"";
            var result = await ExecuteAsync(sqlQuery);
            return result;
        }

        public async Task<Guid> UpdateAsync(EmployeeModel model)
        {
            var sqlQuery = $"UPDATE \"Employee\" SET \"FirstName\"='{model.FirstName}', \"SecondName\"='{model.SecondName}', \"LastName\"='{model.LastName}', \"DateOfBirth\"='{model.DateOfBirth.ToShortDateString()}', \"Position\"='{model.Position}', \"Avatar\"='{model.Avatar}' WHERE \"Id\" = '{model.Id}' returning \"Id\"";
            var result = await ExecuteAsync(sqlQuery);
            return result;
        }

        public async Task<Guid> DeleteAsync(Guid id)
        {
            var sqlQuery = $"DELETE FROM \"Employee\" WHERE \"Id\" = '{id}'  returning \"Id\"";
            var result = await ExecuteAsync(sqlQuery);
            return result;
        }

        public IEnumerable<EmployeeModel> GetAll()
        {
            var sqlQuery = "SELECT * FROM \"Employee\" ORDER BY \"FirstName\"";
            var result = GetList(sqlQuery);

            return _mapper.Map<IEnumerable<EmployeeModel>>(result);
        }

        public EmployeeModel GetEmployee(Guid employeeId)
        {
            var sqlQuery = $"SELECT * FROM \"Employee\" WHERE \"Id\" = '{employeeId}'";
            var result = GetEnemy(sqlQuery);

            return _mapper.Map<EmployeeModel>(result);
        }
    }
}
