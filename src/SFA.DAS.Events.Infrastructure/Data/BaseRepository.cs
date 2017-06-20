using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using SFA.DAS.Events.Domain.Entities;

namespace SFA.DAS.Events.Infrastructure.Data
{
    public abstract class BaseRepository
    {
        private readonly string _connectionString;
        protected abstract string TableName { get; }
        private static IList<int> _transientErrorNumbers = new List<int>
            {
                // https://docs.microsoft.com/en-us/azure/sql-database/sql-database-develop-error-messages
                // https://docs.microsoft.com/en-us/azure/sql-database/sql-database-connectivity-issues
                4060, 40197, 40501, 40613, 49918, 49919, 49920, 11001,
                -2, 20, 64, 233, 10053, 10054, 10060, 40143
            };

        protected BaseRepository(string databaseConnectionString)
        {
            _connectionString = databaseConnectionString;
        }

        protected async Task<T> WithConnection<T>(Func<SqlConnection, Task<T>> getData)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    return await getData(connection);
                }
            }
            catch (TimeoutException ex)
            {
                throw new Exception($"{GetType().FullName}.WithConnection() experienced a timeout", ex);
            }
            catch (SqlException ex)
            {
                if (ex.Number == -2) // SQL Server error number for connection timeout
                    throw new Exception($"{GetType().FullName}.WithConnection() experienced a SQL timeout", ex);

                throw new Exception($"{GetType().FullName}.WithConnection() experienced a SQL exception (error code {ex.Number})", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"{GetType().FullName}.WithConnection() experienced an exception (not a SQL Exception)", ex);
            }
        }

        protected async Task WithTransaction(Func<IDbConnection, IDbTransaction, Task> command)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (var trans = connection.BeginTransaction())
                    {
                        await command(connection, trans);
                        trans.Commit();
                    }
                }
            }
            catch (TimeoutException ex)
            {
                throw new Exception($"{GetType().FullName}.WithConnection() experienced a SQL timeout", ex);
            }
            catch (SqlException ex) when (_transientErrorNumbers.Contains(ex.Number))
            {
                throw new Exception($"{GetType().FullName}.WithConnection() experienced a transient SQL Exception. ErrorNumber {ex.Number}", ex);
            }
            catch (SqlException ex)
            {
                throw new Exception($"{GetType().FullName}.WithConnection() experienced a non-transient SQL exception (error code {ex.Number})", ex);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"{GetType().FullName}.WithConnection() experienced an exception (not a SQL Exception)", ex);
            }
        }

        protected async Task<IEnumerable<T>> GetByRange<T>(DateTime fromDate, DateTime toDate, int pageSize, int pageNumber, long fromEventId) where T : BaseEvent
        {
            var offset = pageSize*(pageNumber - 1);

            // query by ID or by date range, order appropriately
            if (fromEventId > 0)
            {
                return await WithConnection(async c =>
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@fromEventId", fromEventId);

                    var results = await c.QueryAsync<T>($"SELECT * FROM [dbo].[{TableName}] WHERE Id >=  @fromEventId ORDER BY Id OFFSET {offset} ROWS FETCH NEXT {pageSize} ROWS ONLY;", parameters);

                    return results;
                });
            }
            else
            {
                return await WithConnection(async c =>
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@fromDate", fromDate);
                    parameters.Add("@toDate", toDate);

                    var results = await c.QueryAsync<T>($"SELECT * FROM [dbo].[{TableName}] WHERE CreatedOn >=  @fromDate AND CreatedOn < @toDate ORDER BY CreatedOn OFFSET {offset} ROWS FETCH NEXT {pageSize} ROWS ONLY;", parameters);

                    return results;
                });
            }
        }
    }
}
