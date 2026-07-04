using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MusicServiceConsole.Data;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

string connectionString =
    configuration.GetConnectionString("DefaultConnection")!;

Console.WriteLine("""
Выберите способ работы:

1 - ADO.NET
2 - Entity Framework
""");

string? mode = Console.ReadLine();

IRepository repository;

if (mode == "1")
{
    repository = new AdoRepository(connectionString);
}
else
{
    var options = new DbContextOptionsBuilder<MusicContext>()
        .UseSqlServer(connectionString)
        .Options;

    repository = new EfRepository(new MusicContext(options));
}

while (true)
{
    Console.WriteLine("""
        1 - Добавить исполнителя
        2 - Показать исполнителей
        3 - Изменить исполнителя
        4 - Удалить исполнителя
        0 - Выход
        """);

    switch (Console.ReadLine())
    {
        case "1":
            repository.CreateArtist();
            break;

        case "2":
            repository.ReadArtists();
            break;

        case "3":
            repository.UpdateArtist();
            break;

        case "4":
            repository.DeleteArtist();
            break;

        case "0":
            return;
    }

    Console.WriteLine();
}


