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

        protected BaseRepository(string databaseConnectionString)
        {
            _connectionString = databaseConnectionString;
        }

        protected async Task<T> WithConnection<T>(Func<IDbConnection, Task<T>> getData)
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
                throw new Exception($"{GetType().FullName}.WithConnection() experienced a SQL timeout", ex);
            }
            catch (SqlException ex)
            {
                throw new Exception($"{GetType().FullName}.WithConnection() experienced a SQL exception (not a timeout)", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"{GetType().FullName}.WithConnection() experienced an exception (not a SQL Exception)", ex);
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
