
namespace HotelAppLibrary.DataAccess;

internal interface ISqlDataAccess
{
    List<T> LoadData<T, U>(string sqlStatement,
                           U parameters,
                           bool isStoredProcedure = false);
    void SaveData<T>(string sqlStatement,
                     T parameters,
                     bool isStoredProcedure = false);
}