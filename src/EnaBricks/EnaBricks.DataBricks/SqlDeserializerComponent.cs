namespace EnaBricks.DataBricks
{
    using System;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml.Serialization;

    public class SqlDeserializerComponent<T> : IDisposable
        where T : class
    {
        #region Private Members

        private readonly string _connectionStringName;
        private readonly string _cmdText;
        private SqlParameter[] _parameters;
        private XmlSerializer _deserializer;

        #endregion Private Members

        #region Constructors

        public SqlDeserializerComponent(
            string connectionStringName,
            string cmdText,
            string rootName,
            SqlParameter[] parameters)
        {
            if (string.IsNullOrWhiteSpace(connectionStringName))
            {
                throw new ArgumentException("message", nameof(connectionStringName));
            }

            if (string.IsNullOrWhiteSpace(cmdText))
            {
                throw new ArgumentException("message", nameof(cmdText));
            }

            if (rootName == null)
            {
                throw new ArgumentNullException(nameof(rootName));
            }

            _connectionStringName = connectionStringName;
            _cmdText = cmdText;
            _parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
            _deserializer = new XmlSerializer(typeof(T),
                                                new XmlRootAttribute(rootName));
        }

        public SqlDeserializerComponent(
            string connectionStringName,
            string cmdText,
            SqlParameter[] parameters)
            : this(connectionStringName, cmdText, string.Empty, parameters)
        {
        }

        public SqlDeserializerComponent(string connectionStringName, string cmdText)
            : this(connectionStringName, cmdText, string.Empty, Array.Empty<SqlParameter>())
        {
        }

        #endregion Constructors

        #region Protocol

        public async Task<T> ExecuteStoreProcedureAndDeserialize()
        {
            return await ExecuteAndDeserializeInternal();
        }

        public async Task<T> ExecuteCmdTextAndDeserialize()
        {
            return await ExecuteAndDeserializeInternal(false);
        }

        #endregion Protocol

        #region Private Behavior

        private async Task<T> ExecuteAndDeserializeInternal(bool isStoreProcedure = true)
        {
            using (SqlConnection connection = new SqlConnection(_connectionStringName))
            {
                using (SqlCommand command = new SqlCommand(_cmdText, connection))
                {
                    if (isStoreProcedure)
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                    }
                    if (_parameters.Any())
                    {
                        command.Parameters.AddRange(_parameters);
                    }
                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            using (var xmlReader = reader.GetXmlReader(0))
                            {
                                return _deserializer.Deserialize(xmlReader) as T;
                            }
                        }
                        throw new InvalidOperationException();
                    }
                }
            }
        }

        #endregion Private Behavior

        #region IDisposable Support

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                _parameters = null;
                _deserializer = null;

                disposedValue = true;
            }
        }

        ~SqlDeserializerComponent()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable Support
    }
}
