SELECT a.Title AS Album, COUNT(t.Id) AS TrackCount
FROM Albums a
LEFT JOIN Tracks t ON t.AlbumId = a.Id
GROUP BY a.Title;

SELECT
    t.Title AS Track,
    a.Title AS Album,
    ar.Name AS Artist
FROM Tracks t
INNER JOIN Albums a ON t.AlbumId = a.Id
INNER JOIN Artists ar ON a.ArtistId = ar.Id;

SELECT
    ar.Name,
    a.Title
FROM Artists ar
LEFT JOIN Albums a ON a.ArtistId = ar.Id;

SELECT
    ar.Name,
    a.Title
FROM Artists ar
RIGHT JOIN Albums a ON a.ArtistId = ar.Id;
