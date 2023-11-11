SELECT l.Province AS Province, l.Municipality, l.[Name] AS Location, COUNT(s.Name) AS CountOfSites
FROM Locations AS l
JOIN Sites AS s ON
l.Id = s.LocationId
WHERE Province =  'Sofia'
GROUP BY l.[Name], l.Province, l.Municipality
ORDER BY CountOfSites DESC, l.Name


