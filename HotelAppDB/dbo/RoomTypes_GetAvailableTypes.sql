CREATE PROCEDURE [dbo].[RoomTypes_GetAvailableTypes]
	@startDate date,
	@endDate date
AS
Begin
	set nocount on;
	with AvailableRooms (Id) as 
        (
            select Id from dbo.Rooms
            EXCEPT
            Select RoomId from dbo.Bookings Bo
		Where Not((Bo.StartDate < Bo.EndDate) and ( (Bo.EndDate < @startDate) or (Bo.StartDate > @endDate )))
        )
        select Distinct Rt.Id, Rt.Title, Rt.[Description], Rt.Price from AvailableRooms A 
                        left join dbo.Rooms R on A.Id = R.Id 
                        left join dbo.RoomTypes RT on RT.Id = R.RoomTypeId;

End
