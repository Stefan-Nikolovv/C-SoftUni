SELECT * 
FROM Clients

SELECT c.Id, c.[Name], CONCAT(a.StreetName, + ' ', 
A.StreetNumber, + ', ', a.City, + ', ', a.PostCode, + ', ', cnt.[Name] ) AS Address
FROM Clients AS c 
LEFT JOIN ProductsClients AS pc ON
c.Id = pc.ClientId
LEFT JOIN Products AS p ON
pc.ProductId =  p.CategoryId
JOIN Addresses AS a ON
c.AddressId = a.Id
JOIN Countries AS cnt ON
a.CountryId = cnt.Id
WHERE ProductId IS NULL
ORDER BY c.[Name] 