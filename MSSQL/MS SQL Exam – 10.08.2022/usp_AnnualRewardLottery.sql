CREATE PROCEDURE usp_AnnualRewardLottery
@TouristName VARCHAR(50)
AS

BEGIN
IF( SELECT COUNT(*)
FROM Tourists AS t
JOIN SitesTourists AS st ON
t.Id = st.TouristId
JOIN Sites AS s ON
s.Id = st.SiteId
WHERE t.[Name] = @TouristName) >= 100
BEGIN 
UPDATE Tourists
			SET	Reward = 'Gold badge'
			WHERE Name = @TouristName
END
ELSE IF ( SELECT COUNT(*)
FROM Tourists AS t
JOIN SitesTourists AS st ON
t.Id = st.TouristId
JOIN Sites AS s ON
s.Id = st.SiteId
WHERE t.[Name] = @TouristName) >= 50
BEGIN 
UPDATE Tourists
			SET	Reward = 'Silver badge'
			WHERE Name = @TouristName
END
ELSE IF ( SELECT COUNT(*)
FROM Tourists AS t
JOIN SitesTourists AS st ON
t.Id = st.TouristId
JOIN Sites AS s ON
s.Id = st.SiteId
WHERE t.[Name] = @TouristName) >= 25
BEGIN 
UPDATE Tourists
			SET	Reward = 'Bronze badge'
			WHERE Name = @TouristName
END
SELECT Name, Reward FROM Tourists
WHERE Name = @TouristName
END


EXEC usp_AnnualRewardLottery 'Gerhild Lutgard'
GO

EXEC usp_AnnualRewardLottery 'Brus Brown'
GO

EXEC usp_AnnualRewardLottery 'Zac Walsh'
GO

EXEC usp_AnnualRewardLottery 'Teodor Petrov'
GO