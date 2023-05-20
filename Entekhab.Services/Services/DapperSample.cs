using Dapper;
using Entekhab.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entekhab.Services.Services
{
    public class DapperSample
    {
        private readonly IDatabaseConnectionFactory _connectionFactory;
        public DapperSample(IDatabaseConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }


        //public async Task<List<Work>> dappersample()
        //{
        //    List<Work> karlst=new List<Work>();
        //    using var conn = await _connectionFactory.CreateConnectionAsync();
        //    karlst = conn.Query<Work>($"SELECT * from Works").ToList();

        //    return karlst;
        //}
    }
}
