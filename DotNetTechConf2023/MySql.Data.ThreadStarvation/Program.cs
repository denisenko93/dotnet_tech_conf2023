using System.Diagnostics;
using MySqlConnector;

Console.WriteLine("Press any key to start application");
Console.ReadKey();
Console.WriteLine("Application started");

var sw = Stopwatch.StartNew();

await Parallel.ForAsync(1, 1000, new ParallelOptions{MaxDegreeOfParallelism = 100}, async (_, _) =>
{
    await DoWorkAsync();
});

sw.Stop();
Console.WriteLine(sw.ElapsedMilliseconds);

Console.ReadKey();

async Task<object?> DoWorkAsync()
{
    using MySqlConnection connection = new MySqlConnection("server=localhost;port=3306;database=test;uid=root;password=root;ConnectionTimeout=60;");

    await connection.OpenAsync();
    using var command = connection.CreateCommand();

    command.CommandText = "DO SLEEP(1); SELECT 1;";

    var result = await command.ExecuteScalarAsync();

    return result;
}