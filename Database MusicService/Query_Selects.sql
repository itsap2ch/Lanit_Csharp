SELECT Title, Duration
FROM Tracks
ORDER BY Duration DESC;

SELECT Title, ReleaseDate
FROM Albums
ORDER BY ReleaseDate ASC;

SELECT t.Title, t.Duration
FROM Tracks t
JOIN Albums a ON t.AlbumId = a.Id
JOIN Artists ar ON a.ArtistId = ar.Id
WHERE ar.Name = 'PHARAOH';