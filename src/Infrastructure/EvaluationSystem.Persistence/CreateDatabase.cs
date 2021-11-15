using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace EvaluationSystem.Persistence.Migrations
{
    public static class CreateDatabase
    {
        public static void EnsureDatabase(IConfiguration configuration)
        {
            var masterConnectionString = configuration.GetConnectionString("MasterDBConnection");
            var evaluationConnectionString = new SqlConnectionStringBuilder(configuration.GetConnectionString("EvaluationSystemDBConnection"));

            var name = evaluationConnectionString.InitialCatalog;

            var parameters = new DynamicParameters();
            parameters.Add("name", name);
            using var connection = new SqlConnection(masterConnectionString);
            var records = connection.Query("SELECT * FROM sys.databases WHERE name = @name",
                 parameters);
            if (!records.Any())
            {
                connection.Execute($"CREATE DATABASE {name}");
            }
        }
    }
}
