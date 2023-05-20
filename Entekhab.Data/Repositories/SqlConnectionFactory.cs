using Microsoft.Data.SqlClient;
using Entekhab.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entekhab.Data.Repositories
{
    public class SqlConnectionFactory : IDatabaseConnectionFactory
    {
        private readonly string _ConnectionString;

        public SqlConnectionFactory(string ConnectionString) => _ConnectionString = ConnectionString ??
            throw new ArgumentNullException(nameof(ConnectionString));

        public async Task<IDbConnection> CreateConnectionAsync()
        {
            var sqlConnection = new SqlConnection(_ConnectionString);
            await sqlConnection.OpenAsync();
            return sqlConnection;
        }
    }
}
