CREATE FUNCTION udf_RoomsWithTourists
(
    @name VARCHAR(20)
)


RETURNS INT
AS
BEGIN
    DECLARE @TotalTourists INT;
    SET @TotalTourists = 0;

	SELECT @TotalTourists = SUM(B.AdultsCount + B.ChildrenCount)
	FROM Bookings AS b
	JOIN Tourists AS t ON
	T.Id = b.TouristId
	JOIN Rooms AS r ON
	b.RoomId = r.Id
	WHERE r.Type = @name
    
    RETURN @TotalTourists;
END;

SELECT dbo.udf_RoomsWithTourists('Double Room')