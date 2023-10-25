CREATE PROCEDURE usp_SearchByCountry
@country VARCHAR(20)
AS
SELECT v.[Name] AS Vendor, v.NumberVAT AS VAT,
CONCAT(a.StreetName, + ' ', a.StreetNumber) AS [Street Info],
CONCAT(a.City,+ ' ', a.PostCode) AS [City Info]
FROM Countries AS c
JOIN Addresses AS a ON
c.Id = a.CountryId
JOIN Vendors AS v ON
a.Id = v.AddressId
WHERE c.[Name] LIKE @country
ORDER BY v.[Name], a.City

EXEC dbo.usp_SearchByCountry 'FRANCE'