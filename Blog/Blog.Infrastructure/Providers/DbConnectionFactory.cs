using Blog.Application.Providers.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Blog.Infrastructure.Providers
{
    public class DbConnectionFactory(string connectionString) : IDbConnectionFactory
    {
        private readonly string _connectionString = connectionString;

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
