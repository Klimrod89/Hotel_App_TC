CREATE PROCEDURE [dbo].[RoomTypes_GetAvailableTypes]
	@startDate date,
	@endDate date
AS
Begin
	set nocount on;
	WITH AvailableRooms AS (
		SELECT Id FROM dbo.Rooms Ro
		EXCEPT
		SELECT RoomId FROM dbo.Bookings Bo
		Where Bo.StartDate = @startDate and Bo.EndDate = @endDate)
	SELECT DISTINCT RT.Id, RT.Title, RT.[Description], RT.Price
	FROM AvailableRooms AR
	LEFT JOIN dbo.Rooms R ON AR.Id = R.Id
	LEFT JOIN dbo.RoomTypes RT ON R.RoomTypeId = RT.Id;

End
