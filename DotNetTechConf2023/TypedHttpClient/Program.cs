using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var hostBuilder = Host.CreateApplicationBuilder(args);

hostBuilder.Services.AddHttpClient<TypedClient1>(x => x.BaseAddress = new Uri("http://example.com"));
hostBuilder.Services.AddHttpClient<TypedClient2>(x => x.BaseAddress = new Uri("https://github.com"));

hostBuilder.Services.AddTransient<ITypedClient>(x => x.GetService<TypedClient1>());
hostBuilder.Services.AddTransient<ITypedClient>(x => x.GetService<TypedClient2>());

var host = hostBuilder.Build();

await host.StartAsync();

var clients = host.Services.GetServices<ITypedClient>();

foreach (ITypedClient typedClient in clients)
{
    Console.WriteLine($"Client: {typedClient.GetType()}. Base url: {typedClient.Client.BaseAddress}");
}

await host.StopAsync();

interface ITypedClient
{
    HttpClient Client { get; }
}

record TypedClient1(HttpClient Client): ITypedClient;
record TypedClient2(HttpClient Client): ITypedClient;