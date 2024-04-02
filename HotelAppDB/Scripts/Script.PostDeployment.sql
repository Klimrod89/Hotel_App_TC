/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

If Not Exists (Select 1 from dbo.RoomTypes)
Begin
	Insert into dbo.RoomTypes (Title, Description, Price)
	Values ('King Size Bed', 'A room with a king-size bed and a window', 100.00),
		   ('Two Queen Size Beds', 'A room with two queen-size beds and a window', 115.00),
		   ('Executive Suite', 'Two rooms, each with a king-size bed and a window', 205.00)
End

If Not Exists (Select 1 from dbo.Rooms)
Begin
	Declare @roomId1 int;
	Declare @roomId2 int;
	Declare @roomId3 int;

	Select @roomId1 = Id from dbo.RoomTypes where Title = 'King Size Bed';
	Select @roomId2 = Id from dbo.RoomTypes where Title = 'Two Queen Size Beds';
	Select @roomId3 = Id from dbo.RoomTypes where Title = 'Executive Suite';

	Insert into dbo.Rooms (RoomNumber, RoomTypeId)
	Values ('101', @roomId1),
		('102', @roomId1),
		('103', @roomId1),
		('201', @roomId2),
		('202', @roomId2),
		('301', @roomId3);
End