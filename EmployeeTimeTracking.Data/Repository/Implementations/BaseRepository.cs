using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTimeTracking.Data.Repository.Implementations
{
    public class BaseRepository<T> :IBaseRepository<T> where T: class, new()
    {
        private readonly string _connectionString;

        public BaseRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        

        private IDbConnection CreateConnection()
        {
            var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            return connection;
        }

        protected IEnumerable<T> GetList(string sql)
        {
            using(var conn = CreateConnection())
            {
                var result = conn.Query<T>(sql);

                return result;
            }
        }

        protected T GetEnemy(string sql)
        {
            using(var conn= CreateConnection())
            {
                var result = conn.QueryFirstOrDefault<T>(sql);

                return result;
            }
        }
        
        protected object GetColumn(string sql)
        {
            using(var conn = CreateConnection())
            {
                var result = conn.ExecuteScalar(sql);

                return result;
            }
        }

        protected async Task<Guid> ExecuteAsync(string sql)
        {
            using(var conn = CreateConnection())
            {
                var result = await conn.ExecuteScalarAsync(sql);
                return (Guid)result;
            }
        }
    }
}
