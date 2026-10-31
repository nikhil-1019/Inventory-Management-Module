using Microsoft.Data.Sqlite;

namespace InventoryManagement.Data;

public static class Database
{
    public static string ConnectionString { get; private set; } = "Data Source=inventory.db";

    public static void Initialize(IServiceProvider services)
    {
        var configuration = services.GetRequiredService<IConfiguration>();
        ConnectionString = configuration.GetConnectionString("InventoryDB")
            ?? "Data Source=inventory.db";

        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Products (
                ProductId INTEGER PRIMARY KEY AUTOINCREMENT,
                ProductName TEXT NOT NULL,
                Quantity INTEGER NOT NULL,
                Price NUMERIC NOT NULL
            );";
        command.ExecuteNonQuery();
    }

    public static SqliteConnection GetConnection() => new(ConnectionString);
}
