using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace upload
{
    public class FarmaDbContext : IDisposable, IAsyncDisposable
    {
        #region Properties
        private readonly OleDbConnection _connection;
        private const int MIN_SUCCESSFUL_UPDATE = 1;
        #endregion

        #region Constructors

        public static async Task<OleDbConnection> FarmaDbContextAsync(string connectionString, CancellationToken cancellationToken = default)
        {
            var connection = new OleDbConnection(connectionString);
            await connection.OpenAsync(cancellationToken).ConfigureAwait(false);
            return connection;
        }

        public FarmaDbContext(string connectionString)
        {
            if (connectionString == string.Empty) // TODO: This is just for testing purpose. Should be deleted.
            {
                connectionString = "Provider = VFPOLEDB.1; Data Source = E:\\ICENTRO\\DATA; Collating Sequence = general; ";
            }
            _connection = FarmaDbContextAsync(connectionString).Result;
        }
        #endregion

        #region FUNCTION
        public async Task<DataSet> Exec(string function, string database, Dictionary<string, int> parametros)
        {
            DataTable dataset = new DataTable();
            string fullconection = $"Provider = VFPOLEDB.1; Data Source = E:\\Share\\datatest\\DATA\\{database}.dbc;";
            using (var connection = new OleDbConnection(fullconection))
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = function;
                    command.CommandType = CommandType.StoredProcedure;
                    foreach (var items in parametros)
                    {
                        command.Parameters.Add(items.Key, items.Value);
                    }
                    var adapter = new OleDbDataAdapter(command);
                    adapter.Fill(dataset);
                    return new DataSet();
                }
            }
        }
        #endregion

        #region Select Method
        public async Task<T> Get<T>(string query) where T : class
        {
            var dataSet = new DataSet();
            await Task.Run(async () =>
            {
                using (var _adapter = new OleDbDataAdapter(query, _connection))
                {
                    await Task.FromResult(_adapter.Fill(dataSet));
                }
            });
            return dataSet.First<T>();
        }
        public async Task<DataSet> Get(string query)
        {
            var dataSet = new DataSet();
            await Task.Run(async () =>
            {
                using (var _adapter = new OleDbDataAdapter(query, _connection))
                {
                    await Task.FromResult(_adapter.Fill(dataSet));
                }
            });
            return dataSet;
        }
        public async Task<IEnumerable<T>> GetList<T>(string query) where T : class
        {
            try
            {
                var dataSet = new DataSet();
                using (var _adapter = new OleDbDataAdapter(query, _connection))
                {
                    await Task.FromResult(_adapter.Fill(dataSet));
                }
                return dataSet.ToList<T>();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Update Method
        public async Task<int> Update(string query)
        {
            int result = 0;
            try
            {
                using (var command = new OleDbCommand(query, _connection))
                {
                    result = await command.ExecuteNonQueryAsync();
                };

            }
            catch (OleDbException ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
        #endregion

        #region Insert Method
        public async Task Add(string query)
        {
            await Task.Run(async () =>
            {
                using (var command = new OleDbCommand(query, _connection))
                {
                    command.SkipValidateNullOnSave();
                    var update = await command.ExecuteNonQueryAsync();
                    if (update >= MIN_SUCCESSFUL_UPDATE)
                    {
                        return;
                    }
                }
            });
        }
        #endregion

        #region Deleted Method
        public async Task Delete(string sql)
        {
            await Task.Run(async () =>
            {
                using (var command = new OleDbCommand(sql, _connection))
                {
                    if (await command.ExecuteNonQueryAsync() > 0)
                    {
                        return;
                    }
                }
            });
        }
        #endregion

        #region Dinamic Insert
        //TODO: Dinamic Insert
        public OleDbDataAdapter DataInsert(string tabla, string[] columns)
        {
            string commandSql = $"INSERT INTO {tabla} (";
            string values = "VALUES(";
            using (var command = new OleDbCommand(commandSql + values, _connection))
            {
                for (int increment = 0; increment < columns.Length; increment++)
                {
                    commandSql += columns[increment] + ",";
                    values += "?,";
                    command.Parameters.Add(columns[increment], OleDbType.Char, 5, columns[increment]);
                }
                //command = new OleDbCommand($"INSERT INTO {tabla} (CustomerID, CompanyName) VALUES (?, ?)", _conn);
                commandSql = commandSql.TrimEnd(',');
                commandSql += ')';
                values = values.TrimEnd(',');
                values += ')';
                commandSql += $" {values}";

                string query = "INSERT INTO inv_diferenciaconteo VALUES(1,9055,2,0,4,5,CTOD('09/08/20'),1,'10:14 AM',0,553.87,'PAQUETE','037000862093',0,0)";
                command.CommandText = query;

                command.ExecuteNonQuery();
                _connection.Close();
            }
            return null;//_adapter;
        }
        #endregion

        #region Dispose
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        public async ValueTask DisposeAsync()
        {
            await DisposeAsyncCore();
            Dispose(disposing: false);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _connection?.Dispose();
            }
        }
        protected virtual async ValueTask DisposeAsyncCore()
        {
            await Task.Run(() =>
            {
                _connection.Dispose();
            });
        }
        #endregion
    }
}
