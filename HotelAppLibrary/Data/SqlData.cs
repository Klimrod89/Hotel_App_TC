using HotelAppLibrary.DataAccess;
using HotelAppLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace HotelAppLibrary.Data;
internal class SqlData
{
    private readonly ISqlDataAccess db;

    public SqlData(ISqlDataAccess db)
    {
        this.db = db;
    }
    public List<RoomTypeModel> GetAvailableRoomTypes(DateOnly startDate, DateOnly endDate)
    {
        return db.LoadData<RoomTypeModel, dynamic>("dbo.RoomTypes_GetAvailableTypes", new {startDate, endDate}, true);
    }


}
