SELECT c.[Name] AS Clients, FLOOR(AVG(p.Price)) AS 'Average Price'
FROM Clients AS c
JOIN ProductsClients AS pc ON
c.Id = pc.ClientId
JOIN Products AS p ON
pc.ProductId = p.Id
JOIN Vendors AS v ON
v.Id = p.VendorId
WHERE v.NumberVAT LIKE '%FR%'
GROUP BY c.[Name]
ORDER BY FLOOR(AVG(p.Price)) ASC, c.[Name] DESC