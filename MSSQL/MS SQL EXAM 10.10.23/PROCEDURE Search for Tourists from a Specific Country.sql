CREATE PROCEDURE usp_SearchByCountry
    @country NVARCHAR(50)
AS
BEGIN
   SELECT t.[Name], t.PhoneNumber, t.Email, COUNT(*) AS CountOfBookings
   FROM Countries AS c
   JOIN Tourists AS t ON
   t.CountryId = c.Id
   JOIN Bookings AS b ON 
   b.TouristId = t.Id
   JOIN Rooms AS r ON
   r.Id = b.RoomId
   WHERE c.[Name] = @country
   GROUP BY 
        t.[Name], 
        t.PhoneNumber, 
        t.Email
		 ORDER BY 
        t.Name ASC, 
        CountOfBookings DESC;
END;

EXEC usp_SearchByCountry 'Greece'