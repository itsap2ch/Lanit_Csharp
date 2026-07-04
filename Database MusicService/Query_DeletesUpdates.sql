UPDATE Users
SET IsPremium = 1
WHERE Username = 'danik';

UPDATE Tracks
SET Title = 'Friendly Fire'
WHERE Title = 'Angry Toys';

DELETE FROM Tracks
WHERE Title = 'Море';
