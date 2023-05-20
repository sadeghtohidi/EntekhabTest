using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entekhab.Domain.Common;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using AutoMapper;

namespace AdminSalary.DataLayer.Repositories.Dapper
{
    public class DapperRepositoryTEntity<TEntity> : IDapperRepositoryTEntity<TEntity> where TEntity : class, IEntity
    {
        private readonly string connectionString;
        private readonly IMapper _mapper;
        public DapperRepositoryTEntity(IConfiguration configuration, IMapper mapper)
        {
            connectionString = configuration.GetConnectionString("SqlServer"); 
            _mapper = mapper;
        }

        public void Dispose()
        {
            
        }

        public async Task<TEntity> GetAsync(string command,object parameters)
        {
            var sql = "SELECT * from " + typeof(TEntity).Name  + command;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var data =await connection.QuerySingleOrDefaultAsync(sql, parameters);
                var result = _mapper.Map<TEntity>(data);
                return result;
            }
        }
        public async Task<List<TEntity>> GetRangeAsync(string command,object parameters)
        {
            var sql = "SELECT * from " + typeof(TEntity).Name  + command;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var data = await connection.QueryAsync(sql, parameters);
                var result = _mapper.Map<List<TEntity>>(data);
                return result;
            }
        }
    }
}
