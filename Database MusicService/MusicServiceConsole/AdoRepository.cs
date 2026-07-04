using Microsoft.Data.SqlClient;

public class AdoRepository : IRepository
{
    private readonly string _connectionString;

    public AdoRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void ReadArtists()
    {
        using SqlConnection connection = new(_connectionString);

        connection.Open();

        string sql = "SELECT Id, Name, Country FROM Artists";

        using SqlCommand command = new(sql, connection);

        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"{reader["Id"]} | {reader["Name"]} | {reader["Country"]}");
        }
    }

    public void CreateArtist()
    {
        Console.Write("Название: ");
        string name = Console.ReadLine()!;

        Console.Write("Страна: ");
        string country = Console.ReadLine()!;

        using SqlConnection connection = new(_connectionString);

        connection.Open();

        string sql = """
            INSERT INTO Artists(Id, Name, Country, CreatedAt)
            VALUES(@id, @name, @country, @date)
            """;

        using SqlCommand command = new(sql, connection);

        command.Parameters.AddWithValue("@id", Guid.NewGuid());
        command.Parameters.AddWithValue("@name", name);
        command.Parameters.AddWithValue("@country", country);
        command.Parameters.AddWithValue("@date", DateTime.Now);

        command.ExecuteNonQuery();

        Console.WriteLine("Исполнитель добавлен.");
    }

    public void UpdateArtist()
    {
        Console.Write("Введите имя исполнителя: ");
        string oldName = Console.ReadLine()!;

        Console.Write("Новое имя: ");
        string newName = Console.ReadLine()!;

        using SqlConnection connection = new(_connectionString);

        connection.Open();

        string sql = """
            UPDATE Artists
            SET Name = @newName
            WHERE Name = @oldName
            """;

        using SqlCommand command = new(sql, connection);

        command.Parameters.AddWithValue("@oldName", oldName);
        command.Parameters.AddWithValue("@newName", newName);

        int rows = command.ExecuteNonQuery();

        Console.WriteLine($"Изменено записей: {rows}");
    }

    public void DeleteArtist()
    {
        Console.Write("Введите имя исполнителя: ");
        string name = Console.ReadLine()!;

        using SqlConnection connection = new(_connectionString);

        connection.Open();

        string sql = "DELETE FROM Artists WHERE Name = @name";

        using SqlCommand command = new(sql, connection);

        command.Parameters.AddWithValue("@name", name);

        int rows = command.ExecuteNonQuery();

        Console.WriteLine($"Удалено записей: {rows}");
    }
}