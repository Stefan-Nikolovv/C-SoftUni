SELECT TOP(7) inv.Number, inv.Amount, c.[Name] AS Client
FROM Invoices AS inv
JOIN Clients AS c ON
inv.ClientId = c.Id
WHERE (inv.IssueDate < '2023-01-01'
AND inv.Currency = 'EUR') OR (
 inv.Amount > 500.00
AND LEFT(c.NumberVAT, 2) = 'DE')
ORDER BY inv.Number, inv.Currency DESC

SELECT *
FROM Invoices AS inv
JOIN Clients AS c ON
inv.ClientId = c.Id
WHERE IssueDate < '2023-01-01'
AND Currency = 'EUR'
AND Amount > 500.00