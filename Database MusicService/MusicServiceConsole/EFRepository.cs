using Microsoft.EntityFrameworkCore;
using MusicServiceConsole.Data;

public class EfRepository : IRepository
{
    private readonly MusicContext _context;

    public EfRepository(MusicContext context)
    {
        _context = context;
    }

    public void ReadArtists()
    {
        foreach (var artist in _context.Artists)
        {
            Console.WriteLine($"{artist.Id} | {artist.Name} | {artist.Country}");
        }
    }

    public void CreateArtist()
    {
        Console.Write("Название: ");
        string name = Console.ReadLine()!;

        Console.Write("Страна: ");
        string country = Console.ReadLine()!;

        Artist artist = new()
        {
            Id = Guid.NewGuid(),
            Name = name,
            Country = country,
            CreatedAt = DateTime.Now
        };

        _context.Artists.Add(artist);
        _context.SaveChanges();

        Console.WriteLine("Исполнитель добавлен.");
    }

    public void UpdateArtist()
    {
        Console.Write("Введите имя исполнителя: ");
        string oldName = Console.ReadLine()!;

        Artist? artist = _context.Artists
            .FirstOrDefault(a => a.Name == oldName);

        if (artist == null)
        {
            Console.WriteLine("Исполнитель не найден.");
            return;
        }

        Console.Write("Новое имя: ");
        artist.Name = Console.ReadLine()!;

        _context.SaveChanges();

        Console.WriteLine("Исполнитель изменен.");
    }

    public void DeleteArtist()
    {
        Console.Write("Введите имя исполнителя: ");
        string name = Console.ReadLine()!;

        Artist? artist = _context.Artists
            .FirstOrDefault(a => a.Name == name);

        if (artist == null)
        {
            Console.WriteLine("Исполнитель не найден.");
            return;
        }

        _context.Artists.Remove(artist);

        _context.SaveChanges();

        Console.WriteLine("Исполнитель удален.");
    }
}