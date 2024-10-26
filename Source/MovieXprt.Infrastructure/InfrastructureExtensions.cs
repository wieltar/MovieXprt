using Flurl.Http;
using Flurl.Http.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MovieXprt.Common.Options;
using MovieXprt.Infrastructure.Gateways;
using MovieXprt.Infrastructure.Middleware;
using Polly;
using System.Net;

namespace MovieXprt.Infrastructure
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection ConfigureTvMazeApi(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<TvMazeOptions>(config.GetSection(TvMazeOptions.TvMaze));

            services.AddSingleton(sp =>
            {
                var tvMaze = sp.GetRequiredService<IOptions<TvMazeOptions>>().Value;

                var tooManyRequestsPolicy = Policy
                    .Handle<HttpRequestException>()
                    .OrResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.TooManyRequests)
                    .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(5));

                return new FlurlClientCache()
                    .Add("TvMazeClient", tvMaze.BaseUrl, flurlBuilder =>
                    {
                        flurlBuilder.WithHeader("User-Agent", "MovieXprt-corp-client");
                        flurlBuilder.AddMiddleware(() => new PollyHandler(tooManyRequestsPolicy));
                    });
            });

            return services.AddSingleton<ITvMazeGateway, TvMazeGateway>();
        }
    }
}
