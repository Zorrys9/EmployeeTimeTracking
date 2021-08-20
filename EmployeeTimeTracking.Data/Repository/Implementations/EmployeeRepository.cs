using AutoMapper;
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
        public EmployeeRepository(string connectionString)
            : base(connectionString)  {   }

        public async Task<EmployeeEntityModel> InsertAsync(EmployeeEntityModel model)
        {
            var sqlQuery = $"INSERT INTO \"Employee\" (\"Id\", \"FirstName\", \"SecondName\", \"LastName\", \"DateOfBirth\", \"Position\") VALUES ('{model.Id}', '{model.FirstName}', '{model.SecondName}','{model.LastName}', '{model.DateOfBirth.ToShortDateString()}', '{model.Position}') returning *";
            var result = await GetEnemyAsync(sqlQuery);
            return result;
        }

        public async Task<EmployeeEntityModel> UpdateAsync(EmployeeEntityModel model)
        {
            var sqlQuery = $"UPDATE \"Employee\" SET \"FirstName\"='{model.FirstName}', \"SecondName\"='{model.SecondName}', \"LastName\"='{model.LastName}', \"DateOfBirth\"='{model.DateOfBirth.ToShortDateString()}', \"Position\"='{model.Position}' WHERE \"Id\" = '{model.Id}' returning *";
            var result = await GetEnemyAsync(sqlQuery);
            return result;
        }

        public async Task<EmployeeEntityModel> DeleteAsync(Guid id)
        {
            var sqlQuery = $"DELETE FROM \"Employee\" WHERE \"Id\" = '{id}'  returning *";
            var result = await GetEnemyAsync(sqlQuery);
            return result;
        }

        public async Task<IEnumerable<EmployeeEntityModel>> GetAll()
        {
            var sqlQuery = "SELECT * FROM \"Employee\" ORDER BY \"FirstName\"";
            var result = await GetListAsync(sqlQuery);
            return result;
        }

        public async Task<EmployeeEntityModel> Get(Guid employeeId)
        {
            var sqlQuery = $"SELECT * FROM \"Employee\" WHERE \"Id\" = '{employeeId}'";
            var result = await GetEnemyAsync(sqlQuery);

            return result;
        }
    }
}
