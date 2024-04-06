using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAppLibrary.DataAccess;
internal class SqliteDataAccess : ISqlDataAccess
{
    private readonly string? conn;
    public SqliteDataAccess(IConfiguration config, string connectionStringName)
    {
        conn = config.GetConnectionString(connectionStringName);
        if (conn is null) throw new ArgumentNullException($"Connection string '{connectionStringName}' not found.");
    }
    public List<T> LoadData<T, U>(string sqlStatement, U parameters, bool isStoredProcedure = false)
    {
        CommandType commandType = TypeOfCommand(isStoredProcedure);

        using SqlConnection sqlConnection = new(conn);
        return sqlConnection.Query<T>(sqlStatement, parameters, commandType:commandType).ToList();
    }

    public void SaveData<T>(string sqlStatement, T parameters, bool isStoredProcedure = false)
    {
        CommandType commandType = TypeOfCommand(isStoredProcedure);

        using SqlConnection sqlConnection = new(conn);
        sqlConnection.Execute(sqlStatement, parameters, commandType:commandType);
    }

    private CommandType TypeOfCommand(bool isSP)
    {
        return isSP ? CommandType.StoredProcedure : CommandType.Text;
    }
}
