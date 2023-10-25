UPDATE Invoices
SET DueDate = '2023-04-01'
WHERE DueDate = '2022-11-01'

UPDATE Addresses
SET  StreetName = 'Industriestr',
		StreetNumber = 79,
		PostCode = 2353,
		City = 'Guntramsdorf, Austria'
WHERE Id IN (13, 3, 9, 5)

SELECT * 
FROM Addresses
WHERE Id IN (13, 3, 9, 5)