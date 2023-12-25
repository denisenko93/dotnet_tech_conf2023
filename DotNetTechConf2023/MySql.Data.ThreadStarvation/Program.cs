using System.Diagnostics;
using System.Diagnostics.Tracing;
using Microsoft.Diagnostics.NETCore.Client;
using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Parsers;
using MySql.Data.MySqlClient;

Console.WriteLine("Press any key to start application");
Console.ReadKey();
Console.WriteLine("Application started");

//ThreadPool.SetMinThreads(250, 250);

Task.Run(() => PrintRuntimeGCEvents(Process.GetCurrentProcess().Id));

var sw = Stopwatch.StartNew();

await Parallel.ForAsync(1, 1000, new ParallelOptions{MaxDegreeOfParallelism = 150}, async (_, _) =>
{
    await DoWorkAsync();
});

sw.Stop();
Console.WriteLine(sw.ElapsedMilliseconds);

async Task<object?> DoWorkAsync()
{
    using MySqlConnection connection = new MySqlConnection("server=localhost;port=3306;database=test;uid=root;password=root;ConnectionTimeout=60;");

    await connection.OpenAsync();
    using var command = connection.CreateCommand();

    command.CommandText = "DO SLEEP(1); SELECT 1;";

    var result = await command.ExecuteScalarAsync(CancellationToken.None);

    return result;
}

void PrintRuntimeGCEvents(int processId)
{
    var providers = new List<EventPipeProvider>()
    {
        new EventPipeProvider("Microsoft-Windows-DotNETRuntime",
            EventLevel.Informational, (long)ClrTraceEventParser.Keywords.Threading)
    };

    var client = new DiagnosticsClient(processId);
    using (EventPipeSession session = client.StartEventPipeSession(providers, false))
    {
        var source = new EventPipeEventSource(session.EventStream);

        source.Clr.All += (TraceEvent obj) =>
        {
            if (obj.EventName == "AppDomainResourceManagement/ThreadCreated")
            {
                Console.WriteLine(obj.ToString());
            }
        };

        try
        {
            source.Process();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error encountered while processing events");
            Console.WriteLine(e.ToString());
        }
    }
}