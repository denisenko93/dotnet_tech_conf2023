using Apps72.Dev.Data.DbMocker;
using Dapper;

var connection = new MockDbConnection();

connection.Mocks.When(x => true).ReturnsTable(MockTable.Empty());

Console.ReadKey();

for (int i = 0; i < 100000; i++)
{
    await connection.QueryAsync(new CommandDefinition($"SELECT {i}", flags: CommandFlags.NoCache));
    await connection.QueryAsync($"SELECT {i}");
}

Console.ReadKey();