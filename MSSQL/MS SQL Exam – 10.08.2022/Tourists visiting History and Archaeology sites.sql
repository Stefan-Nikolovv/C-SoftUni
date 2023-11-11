SELECT SUBSTRING(t.[Name],CHARINDEX(' ',t.[Name]),LEN(t.[Name])) as LastName, t.Nationality, t.Age, t.PhoneNumber
FROM Tourists AS t
JOIN SitesTourists AS st ON
t.Id = st.TouristId
JOIN Sites AS s ON
s.Id = st.SiteId
JOIN Categories AS c ON
s.CategoryId = c.Id
WHERE c.[Name] LIKE 'History and archaeology'
GROUP BY SUBSTRING(t.[Name],CHARINDEX(' ',t.[Name]),LEN(t.[Name])), t.Nationality, t.Age, t.PhoneNumber
ORDER BY LastName



