using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using SFA.DAS.Events.Domain.Entities;
using SFA.DAS.Events.Domain.Repositories;

namespace SFA.DAS.Events.Infrastructure.Data
{
    public class AccountEventRepository : BaseRepository, IAccountEventRepository
    {
        protected override string TableName => "AccountEvents";

        public AccountEventRepository(string databaseConnectionString) : base(databaseConnectionString) {}

        public async Task Create(AccountEvent @event)
        {
            await WithConnection(async c =>
                await c.ExecuteAsync($"INSERT INTO [dbo].[{TableName}](Event, CreatedOn, EmployerAccountId) " +
                                     $"VALUES (@event, @createdOn, @employerAccountId);", @event));
        }

        public async Task<IEnumerable<AccountEvent>> GetByRange(DateTime fromDate, DateTime toDate, int pageSize, int pageNumber, long fromEventId)
        {
            return await GetByRange<AccountEvent>(fromDate, toDate, pageSize, pageNumber, fromEventId);
        }
    }
}
