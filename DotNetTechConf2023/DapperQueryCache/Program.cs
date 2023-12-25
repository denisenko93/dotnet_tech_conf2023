using Dapper;
using MySqlConnector;

Console.ReadKey();

using MySqlConnection connection = new MySqlConnection("server=localhost;port=3306;database=test;uid=root;password=root;");

for (int i = 0; i < 100000; i++)
{
    await connection.QueryFirstAsync(new CommandDefinition($"SELECT {i}"));
    await connection.QueryFirstAsync(new CommandDefinition($"SELECT {i}"));
}

Console.ReadKey();