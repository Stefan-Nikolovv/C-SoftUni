SELECT TOP(10) h.[Name] AS HotelName, d.[Name] AS DestinationName, c.[Name] AS CountryName
FROM Hotels AS h
JOIN Destinations AS d ON
h.DestinationId = d.Id
JOIN Countries AS c ON
c.Id = d.CountryId
JOIN Bookings AS b ON
B.HotelId = h.Id
WHERE 
    b.ArrivalDate < '2023-12-31' 
    AND 
    h.Id % 2 <> 0
	ORDER BY c.[Name], b.ArrivalDate
