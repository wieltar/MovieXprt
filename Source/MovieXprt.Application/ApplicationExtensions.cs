using Microsoft.Extensions.DependencyInjection;
using MovieXprt.Application.UseCases;

namespace MovieXprt.Application
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection ConfigureUserCases(this IServiceCollection services)
        {
            return services.AddScoped<IGetScheduleUsecase, GetScheduleUseCase>();
        }
    }
}
