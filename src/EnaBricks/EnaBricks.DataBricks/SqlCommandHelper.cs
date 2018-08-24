namespace EnaBricks.DataBricks
{
    using System;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Threading.Tasks;

    public class SqlCommandHelper
    {
        private readonly string _connectionStringName;

        #region Constructors

        public SqlCommandHelper(string connectionStringName)
        {
            if (string.IsNullOrWhiteSpace(connectionStringName))
            {
                throw new ArgumentException("message", nameof(connectionStringName));
            }

            _connectionStringName = connectionStringName;
        }

        #endregion Constructors

        #region Protocol

        public async Task<int> ExecuteNonQueryAsync(string cmdText, bool isStoreProcedure) => await ExecuteNonQueryAsync(cmdText, isStoreProcedure, Array.Empty<SqlParameter>());

        public async Task<int> ExecuteNonQueryAsync(string cmdText, bool isStoreProcedure, SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(_connectionStringName))
            {
                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    if (isStoreProcedure)
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                    }
                    if (parameters != null && parameters.Any())
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    await connection.OpenAsync();
                    return await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<object> ExecuteScalarAsync(string cmdText, bool isStoreProcedure) => await ExecuteScalarAsync(cmdText, isStoreProcedure, Array.Empty<SqlParameter>());

        public async Task<object> ExecuteScalarAsync(string cmdText, bool isStoreProcedure, SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(_connectionStringName))
            {
                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    if (isStoreProcedure)
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                    }
                    if (parameters != null && parameters.Any())
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    await connection.OpenAsync();
                    return await command.ExecuteScalarAsync();
                }
            }
        }

        #endregion Protocol
    }
}
