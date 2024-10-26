namespace MovieXprt.Infrastructure.Gateways;

using Flurl;
using Flurl.Http;
using Flurl.Http.Configuration;
using Microsoft.Extensions.Logging;
using MovieXprt.Common.Contracts;
public interface ITvMazeGateway
{
    public Task<ICollection<TvMazeShow>> GetSchedule(DateOnly from, string? countrycode, CancellationToken ct);
}

public class TvMazeGateway(
    IFlurlClientCache flurlClientCache,
    ILogger<TvMazeGateway> logger
    ) : ITvMazeGateway
{
    private readonly IFlurlClient _flurlClient = flurlClientCache.Get("TvMazeClient") ?? throw new ArgumentNullException(nameof(flurlClientCache));
    private readonly ILogger<TvMazeGateway> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<ICollection<TvMazeShow>> GetSchedule(DateOnly from, string? countrycode, CancellationToken ct)
    {
        try
        {
            var fromDate = from.ToString("yyyy-MM-dd");

            return await _flurlClient.Request("schedule", "web")
              .AllowHttpStatus(429, 200)
              .SetQueryParam("country", countrycode, NullValueHandling.Remove)
              .SetQueryParam("date", fromDate)
              .GetJsonAsync<ICollection<TvMazeShow>>(default, ct);
        }
        catch (FlurlHttpException e)
        {
            this._logger.LogError(e, "Error getting schedule from TvMaze");
            return [];
        }

    }
}
