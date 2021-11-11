using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace EvaluationSystem.Persistence
{
    public class DbConfigConnection
    {
        private readonly string _configurationString;

        public DbConfigConnection(IConfiguration configuration)
        {
            _configurationString = configuration.GetConnectionString("EvaluationSystemDBConnection");
        }

        public IDbConnection Connection => new SqlConnection(_configurationString);
    }
}
