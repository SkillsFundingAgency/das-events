using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using SFA.DAS.Events.Domain.Entities;
using SFA.DAS.Events.Domain.Repositories;

namespace SFA.DAS.Events.Infrastructure.Data
{
    public class AgreementEventRepository : BaseRepository, IAgreementEventRepository
    {
        private const string TableName = "AgreementEvents";

        public AgreementEventRepository(string databaseConnectionString) : base(databaseConnectionString) {}

        public async Task Create(AgreementEvent @event)
        {
            await WithConnection(async c =>
                await c.ExecuteAsync($"INSERT INTO [dbo].[{TableName}](Event, CreatedOn, ProviderId, EmployerAccountId) VALUES (@event, @createdOn, @providerId, @employerAccountId);", @event));
        }

        public async Task<IEnumerable<AgreementEvent>> GetByDateRange(DateTime fromDate, DateTime toDate, int pageSize, int pageNumber, long fromEventId)
        {
            var offset = pageSize*(pageNumber - 1);

            // query by ID or by date range
            if (fromEventId > 0)
            {
                return await WithConnection(async c =>
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@fromEventId", fromEventId);

                    var results = await c.QueryAsync<AgreementEvent>($"SELECT * FROM [dbo].[{TableName}] WHERE Id >=  @fromEventId ORDER BY CreatedOn OFFSET {offset} ROWS FETCH NEXT {pageSize} ROWS ONLY;", parameters);

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

                    var results = await c.QueryAsync<AgreementEvent>($"SELECT * FROM [dbo].[{TableName}] WHERE CreatedOn >=  @fromDate AND CreatedOn < @toDate ORDER BY CreatedOn OFFSET {offset} ROWS FETCH NEXT {pageSize} ROWS ONLY;", parameters);

                    return results;
                });
            }
        }
    }
}
