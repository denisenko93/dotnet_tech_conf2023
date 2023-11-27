using System.Diagnostics;
using MySqlConnector;

Console.ReadKey();

var sw = Stopwatch.StartNew();

await Parallel.ForAsync(1, 1000, new ParallelOptions{MaxDegreeOfParallelism = 100}, async (_, _) =>
{
    using MySqlConnection connection = new MySqlConnection("server=localhost;port=3306;database=test;uid=root;password=root;");

    connection.Open();
    using var command = connection.CreateCommand();

    command.CommandText = "DO SLEEP(1);";

    await command.ExecuteNonQueryAsync();
});

sw.Stop();
Console.WriteLine(sw.ElapsedMilliseconds);

Console.ReadKey();