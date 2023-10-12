CREATE FUNCTION udf_GetTouristsCountOnATouristSite (@Site VARCHAR(100))
RETURNS INT
AS
BEGIN
RETURN (SELECT COUNT(*) 
FROM SITES AS s 
JOIN SitesTourists AS st ON
s.Id = st.SiteId
JOIN Tourists AS t ON
st.TouristId = t.Id
WHERE s.[Name] = @Site)
END 