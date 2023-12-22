SELECT c.[Name] AS Client, MAX(p.Price) AS Price, c.NumberVAT AS 'VAT Number'
FROM Clients AS c
 JOIN ProductsClients AS pc ON
c.Id = pc.ClientId
 JOIN Products AS p ON
pc.ProductId = p.CategoryId

WHERE RIGHT(c.[Name] , 2) != 'KG'
GROUP BY c.[Name], Price, c.NumberVAT
ORDER BY Price DESC

SELECT c.[Name] AS Client, MAX(p.Price) AS Price, c.NumberVAT AS 'VAT Number'
FROM Clients c
 JOIN ProductsClients AS pc ON
 pc.ClientId = c.Id
 JOIN Products AS p ON
pc.ProductId = p.Id
WHERE c.[Name] NOT LIKE '%KG' 
GROUP BY c.[Name], c.NumberVAT
ORDER BY MAX(p.Price) DESC