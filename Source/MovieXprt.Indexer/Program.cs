using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MovieXprt.Indexer;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Indexer>();

IHost host = builder.Build();
host.Run();
