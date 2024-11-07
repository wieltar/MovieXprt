using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MovieXprt.Indexer;
using MovieXprt.Application;
using MovieXprt.Infrastructure.DataStore;
using MovieXprt.Infrastructure.TvMazeClient;

var host = new HostBuilder()
    .ConfigureServices((hostContext, services) =>
    {
        services
            .ConfigureDataStore(hostContext.Configuration)
            .ConfigureTvMazeApi(hostContext.Configuration)
            .ConfigureApplication()
            .AddHostedService<Indexer>();
    })
    .UseConsoleLifetime()
    .Build();

await host.RunAsync();
