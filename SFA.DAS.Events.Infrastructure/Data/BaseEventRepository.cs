using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;

namespace SFA.DAS.Events.Infrastructure.Data
{
    public abstract class BaseEventRepository
    {
        private readonly string _connectionString;

        protected BaseEventRepository(string databaseConnectionString)
        {
            _connectionString = databaseConnectionString;
        }

        private async Task<T> WithConnection<T>(Func<IDbConnection, Task<T>> getData)
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

        protected async Task<IEnumerable<T>> GetByDateRange<T>(string tableName, DateTime @from, DateTime to)
        {
            return await WithConnection(async c =>
            {
                var parameters = new DynamicParameters();
                parameters.Add("@from", from);
                parameters.Add("@to", to);

                var results = await c.QueryAsync<T>($"SELECT * FROM [dbo].[{tableName}] WHERE CreatedOn >=  @from AND CreatedOn < @to;", parameters);

                return results;
            });
        }

        protected async Task Create<T>(string tableName, T @event)
        {
            await WithConnection(async c =>
                await c.ExecuteAsync($"INSERT INTO [dbo].[{tableName}](Event, EventType, Data, CreatedOn) VALUES (@event, @eventType, @data, @createdOn);", @event));
        }
    }
}
