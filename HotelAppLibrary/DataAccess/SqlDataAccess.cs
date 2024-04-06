using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAppLibrary.DataAccess;
internal class SqlDataAccess : ISqlDataAccess
{
    private readonly string? connectionString;
    public SqlDataAccess(IConfiguration config, string connectionStringName)
    {
        connectionString = config.GetConnectionString(connectionStringName);
        if (connectionString is null) throw new ArgumentNullException($"Connection string '{connectionStringName}' not found.");
    }

    public List<T> LoadData<T, U>(string sqlStatement,
                                  U parameters,
                                  bool isStoredProcedure = false)
    {
        CommandType commandType = TypeOfCommand(isStoredProcedure);

        using SqlConnection connection = new(connectionString);
        List<T> rows = connection.Query<T>(sqlStatement, parameters, commandType: commandType).ToList();
        return rows;
    }

    public void SaveData<T>(string sqlStatement,
                            T parameters,
                            bool isStoredProcedure = false)
    {
        CommandType commandType = TypeOfCommand(isStoredProcedure);

        using SqlConnection connection = new(connectionString);
        connection.Execute(sqlStatement, parameters, commandType: commandType);
    }

    private CommandType TypeOfCommand(bool isSP)
    {
        return isSP ? CommandType.StoredProcedure : CommandType.Text;
    }
}
