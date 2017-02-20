using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using SFA.DAS.Events.Domain.Entities;
using SFA.DAS.Events.Domain.Repositories;

namespace SFA.DAS.Events.Infrastructure.Data
{
    public class GenericEventRepository : BaseRepository, IGenericEventRepository
    {
        protected override string TableName => "GenericEvents";

        public GenericEventRepository(string databaseConnectionString) : base(databaseConnectionString)
        {
        }

        public async Task Create(GenericEvent @event)
        {
            await WithConnection(async c =>
                await c.ExecuteAsync($"INSERT INTO [dbo].[{TableName}](Event, Type, Payload, CreatedOn) " +
                                     $"VALUES (@event, @type, @payload, @createdOn);", @event));
        }

        public async Task<IEnumerable<GenericEvent>> GetByDateRange(IEnumerable<string> eventTypes, DateTime fromDate, DateTime toDate, int pageSize, int pageNumber)
        {
            var offset = pageSize * (pageNumber - 1);

            var parameters = new EventTypeTableParam(eventTypes);

            parameters.Add("@fromDate", DbType.DateTime, fromDate);
            parameters.Add("@toDate", DbType.DateTime, toDate);
            parameters.Add("@pageSize", DbType.Int32, pageSize);
            parameters.Add("@offset", DbType.Int32, offset);

            var result = await WithConnection(async c =>
                await c.QueryAsync<GenericEvent>(
                    sql: "[dbo].[GetGenericEventsByDateRange]",
                    param: parameters,
                    commandType: CommandType.StoredProcedure));

            return result;
        }

       

        public async Task<IEnumerable<GenericEvent>> GetSinceEvent(IEnumerable<string> eventTypes, long fromEventId, int pageSize, int pageNumber)
        {
            var offset = pageSize * (pageNumber - 1);

            var parameters = new EventTypeTableParam(eventTypes);

            parameters.Add("@@fromEventId", DbType.Int64, fromEventId);
            parameters.Add("@pageSize", DbType.Int32, pageSize);
            parameters.Add("@offset", DbType.Int32, offset);

            var result = await WithConnection(async c =>
                await c.QueryAsync<GenericEvent>(
                    sql: "[dbo].[[GetGenericEventsSinceEvent]",
                    param: parameters,
                    commandType: CommandType.StoredProcedure));

            return result;
        }
    }
}
