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
        protected override string TableName => "AgreementEvents";

        public AgreementEventRepository(string databaseConnectionString) : base(databaseConnectionString) {}

        public async Task Create(AgreementEvent @event)
        {
            await WithConnection(async c =>
                await c.ExecuteAsync($"INSERT INTO [dbo].[{TableName}](Event, CreatedOn, ProviderId, ContractType) " +
                                     $"VALUES (@event, @createdOn, @providerId, @contractType);", @event));
        }

        public async Task<IEnumerable<AgreementEvent>> GetByRange(DateTime fromDate, DateTime toDate, int pageSize, int pageNumber, long fromEventId)
        {
            return await GetByRange<AgreementEvent>(fromDate, toDate, pageSize, pageNumber, fromEventId);
        }
    }
}
