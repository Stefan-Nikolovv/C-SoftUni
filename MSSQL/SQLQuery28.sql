

CREATE FUNCTION udf_ProductWithClients(@name NVARCHAR(35)) 
RETURNS int
AS
BEGIN 
DECLARE @result INT
SELECT	@result =  COUNT(*) 
FROM Products AS p
JOIN ProductsClients AS pc ON
pc.ProductId = p.Id
JOIN Clients AS c ON
c.Id = pc.ClientId
WHERE p.[Name] LIKE 'DAF FILTER HU12103X'
IF(@result IS NULL)
SET @result = 0
RETURN @result;
END;

SELECT dbo.udf_ProductWithClients('DAF FILTER HU12103X')