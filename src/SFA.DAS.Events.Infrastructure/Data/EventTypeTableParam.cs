using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace SFA.DAS.Events.Infrastructure.Data
{
    public class EventTypeTableParam : SqlMapper.IDynamicParameters
    {
        private readonly IEnumerable<string> _eventTypes;
        private readonly List<SqlParameter> _parameters;

        public EventTypeTableParam(IEnumerable<string> eventTypes)
        {
            _eventTypes = eventTypes;
            _parameters = new List<SqlParameter>();
        }

        public void AddParameters(IDbCommand command, SqlMapper.Identity identity)
        {
            var sqlCommand = (SqlCommand) command;
            sqlCommand.CommandType = CommandType.StoredProcedure;

            var eventTypesList = new List<Microsoft.SqlServer.Server.SqlDataRecord>();

            Microsoft.SqlServer.Server.SqlMetaData[] tvpDefinition =
            {
                new Microsoft.SqlServer.Server.SqlMetaData("Name", SqlDbType.VarChar)
            };

            foreach (var eventType in _eventTypes)
            {
                Microsoft.SqlServer.Server.SqlDataRecord rec =
                    new Microsoft.SqlServer.Server.SqlDataRecord(tvpDefinition);
                rec.SetString(0, eventType);
                eventTypesList.Add(rec);
            }

            var p = sqlCommand.Parameters.Add("@eventTypes", SqlDbType.Structured);
            p.Direction = ParameterDirection.Input;
            p.TypeName = "[dbo].[eventTypes]";
            p.Value = eventTypesList;
          
            sqlCommand.Parameters.AddRange(_parameters.ToArray());
        }

        public void Add(string name, DbType dbType, object value, ParameterDirection direction = ParameterDirection.Input)
        {
            var param = new SqlParameter(name, dbType)
            {
                Value = value,
                Direction = direction
            };

            _parameters.Add(param);
        }
    }
}
