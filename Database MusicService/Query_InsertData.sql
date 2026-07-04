INSERT INTO Artists (Name, Country, CreatedAt)
VALUES
('PHARAOH', 'Россия', '2012-01-01'),
('Boulevard Depo', 'Россия', '2013-01-01'),
('Mnogoznaal', 'Россия', '2014-01-01'),
('SALUKI', 'Россия', '2016-01-01'),
('MAYOT', 'Россия', '2018-01-01');

INSERT INTO Albums (ArtistId, Title, ReleaseDate)
SELECT Id, 'PHUNERAL', '2018-08-24'
FROM Artists
WHERE Name = 'PHARAOH';

INSERT INTO Albums (ArtistId, Title, ReleaseDate)
SELECT Id, 'OLD BLOOD', '2020-05-15'
FROM Artists
WHERE Name = 'Boulevard Depo';

INSERT INTO Albums (ArtistId, Title, ReleaseDate)
SELECT Id, 'Круг Ветров', '2019-11-15'
FROM Artists
WHERE Name = 'Mnogoznaal';

INSERT INTO Albums (ArtistId, Title, ReleaseDate)
SELECT Id, 'WILD EAST', '2022-04-22'
FROM Artists
WHERE Name = 'SALUKI';

INSERT INTO Albums (ArtistId, Title, ReleaseDate)
SELECT Id, 'Ghetto Garden', '2021-04-16'
FROM Artists
WHERE Name = 'MAYOT';

INSERT INTO Tracks (AlbumId, Title, Duration, Genre, IsExplicit)
SELECT Id, 'Louis Vuitton Kiss', 3.47, 'Hip-Hop', 1
FROM Albums
WHERE Title = 'PHUNERAL';

INSERT INTO Tracks (AlbumId, Title, Duration, Genre, IsExplicit)
SELECT Id, 'Дико, например', 3.12, 'Hip-Hop', 1
FROM Albums
WHERE Title = 'PHUNERAL';

INSERT INTO Tracks (AlbumId, Title, Duration, Genre, IsExplicit)
SELECT Id, 'Angry Toys', 2.58, 'Hip-Hop', 1
FROM Albums
WHERE Title = 'OLD BLOOD';

INSERT INTO Tracks (AlbumId, Title, Duration, Genre, IsExplicit)
SELECT Id, 'Круг Ветров', 4.21, 'Hip-Hop', 0
FROM Albums
WHERE Title = 'Круг Ветров';

INSERT INTO Tracks (AlbumId, Title, Duration, Genre, IsExplicit)
SELECT Id, 'Voron', 3.30, 'Hip-Hop', 0
FROM Albums
WHERE Title = 'WILD EAST';

INSERT INTO Tracks (AlbumId, Title, Duration, Genre, IsExplicit)
SELECT Id, 'Море', 2.56, 'Hip-Hop', 1
FROM Albums
WHERE Title = 'Ghetto Garden';

INSERT INTO Users (Username, Email, BirthDate, IsPremium)
VALUES
('alisa', 'alisa@mail.ru', '2005-03-17', 1),
('danik', 'danik@mail.ru', '2003-08-12', 0),
('semyon', 'semyon@mail.ru', '2004-11-01', 1);

INSERT INTO Playlists (UserId, Name, CreatedAt)
SELECT Id, 'Любимое', GETDATE()
FROM Users
WHERE Username='alisa';

INSERT INTO Playlists (UserId, Name, CreatedAt)
SELECT Id, 'В машину', GETDATE()
FROM Users
WHERE Username='danik';

INSERT INTO Playlists (UserId, Name, CreatedAt)
SELECT Id, 'Чилл', GETDATE()
FROM Users
WHERE Username='semyon';