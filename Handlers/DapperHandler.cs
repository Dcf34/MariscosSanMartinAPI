using Dapper;
using MariscosSanMartinAPI;
using MariscosSanMartinAPI.Models;
using MySqlConnector;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace MariscosSanMartinAPI.Handlers
{
    public class DapperHandler
    {
        private static readonly string envConfig = JsonConfiguration.ObtenerAmbiente();

        private static string GetConnectionDB(string keyDB = "MARISCOS")
        {
            return JsonConfiguration.AppSetting[$"ConnectionDB:{keyDB}:{envConfig}"] ?? "";
        }

        public static List<T> GetFromProcedure<T>(string spName, DynamicParameters? spParams = null, CommandType commandType = CommandType.StoredProcedure)
        {
            using var connection = new MySqlConnection(GetConnectionDB());

            if (connection.State != ConnectionState.Open)
                connection.Open();

            var result = connection.Query<T>(spName, spParams, commandType: commandType).ToList();

            connection.Close();

            return result;
        }

        public static List<T> GetFromQuery<T>(string query, DynamicParameters spParams, CommandType commandType = CommandType.Text)
        {
            using var connection = new MySqlConnection(GetConnectionDB());

            if (connection.State != ConnectionState.Open)
                connection.Open();

            var result = connection.Query<T>(query, spParams, commandType: commandType).ToList();

            connection.Close();

            return result;
        }

        public static T GetFromQuerySingle<T>(string spName, DynamicParameters spParams, CommandType commandType = CommandType.StoredProcedure)
        {
            using var connection = new MySqlConnection(GetConnectionDB());

            if (connection.State != ConnectionState.Open)
                connection.Open();

            var result = connection.QuerySingle<T>(spName, spParams, commandType: commandType);

            connection.Close();

            return result;
        }

        public static Ejecucion SetFromProcedure(string spName, DynamicParameters spParams, CommandType commandType = CommandType.StoredProcedure)
        {
            using var connection = new MySqlConnection(GetConnectionDB());

            if (connection.State != ConnectionState.Open)
                connection.Open();

            var result = connection.Query<Ejecucion>(spName, spParams, commandType: commandType).Single();

            connection.Close();

            return result;
        }

        public static dynamic ExecuteProcedure(string spName, DynamicParameters spParams)
        {
            using var connection = new MySqlConnection(GetConnectionDB());

            if (connection.State != ConnectionState.Open)
                connection.Open();

            var result = connection.Query(spName, spParams, commandType: CommandType.StoredProcedure);

            connection.Close();

            return result;
        }


        public static int GetFromSequence(string sequenceName)
        {
            using var connection = new MySqlConnection(GetConnectionDB());

            if (connection.State != ConnectionState.Open)
                connection.Open();

            List<int> result = connection.Query<int>($"SELECT NEXT VALUE FOR {sequenceName} AS Id", commandType: CommandType.Text).ToList();

            connection.Close();

            return result != null && result.Count > 0 ? result[0] : -1;
        }

        public static string GetValuesFromInputParameters(DynamicParameters spParams)
        {
            string parameters = "";
            var templates = spParams.GetType().GetField("templates", BindingFlags.NonPublic | BindingFlags.Instance);

            if (templates != null)
            {
                if (templates.GetValue(spParams) is List<object> list)
                {
                    parameters = list[0].ToString() ?? "";

                    if (!string.IsNullOrEmpty(parameters))
                    {
                        parameters = parameters.TrimStart('{').TrimEnd('}').Trim();
                    }
                }
            }

            return parameters;
        }
    }
}
