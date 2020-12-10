using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyForms.Data
{
    public class DataRepository : IDataRepository
    {
        private readonly string _connectionString;

        public DataRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:MySqlConnection"];
        }

        public IEnumerable<dynamic> GetFormsNumber()
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var result = connection.Query("SELECT COUNT(id) FROM form");
                return result;
            }
        }
    }
}
