namespace MovieXprt.Application;

using Microsoft.Extensions.DependencyInjection;
using MovieXprt.Application.UseCases;
using MovieXprt.Domain.UseCases;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureApplication(this IServiceCollection services)
    {
        return services
            .AddScoped<IStoreNewShowsUseCase, StoreNewShowsUseCase>()
            .AddScoped<IGetScheduleUsecase, GetScheduleUseCase>();
    }
}
