namespace MovieXprt.Infrastructure.DataStore;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MovieXprt.Domain.Repositories;
using MovieXprt.Infrastructure.DataStore.Gateways;
using MovieXprt.Infrastructure.DataStore.Options;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureDataStore(this IServiceCollection services, IConfiguration config) =>
        services
            .Configure<ConnectionStrings>(config.GetRequiredSection("ConnectionStrings"))
            .AddScoped<IShowRepository, DapperShowRepository>();
}

