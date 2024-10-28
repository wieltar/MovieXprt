namespace MovieXprt.Infrastructure.Gateways;

using Flurl;
using Flurl.Http;
using Flurl.Http.Configuration;
using Microsoft.Extensions.Logging;
using MovieXprt.Common.Contracts.TvMaze;

public interface ITvMazeGateway
{
    public Task<ICollection<Schedule>> GetSchedule(DateOnly from, string? countrycode, CancellationToken ct);
    public Task<ICollection<Show>> queryShows(int page, CancellationToken ct);
}

public class TvMazeGateway(
    IFlurlClientCache flurlClientCache,
    ILogger<TvMazeGateway> logger
    ) : ITvMazeGateway
{
    private readonly IFlurlClient _flurlClient = flurlClientCache.Get("TvMazeClient") ?? throw new ArgumentNullException(nameof(flurlClientCache));
    private readonly ILogger<TvMazeGateway> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<ICollection<Schedule>> GetSchedule(DateOnly from, string? countrycode, CancellationToken ct)
    {
        try
        {
            var fromDate = from.ToString("yyyy-MM-dd");

            return await _flurlClient.Request("schedule", "web")
              .AllowHttpStatus(200)
              .SetQueryParam("country", countrycode, NullValueHandling.Remove)
              .SetQueryParam("date", fromDate)
              .GetJsonAsync<ICollection<Schedule>>(default, ct);
        }
        catch (FlurlHttpException e)
        {
            this._logger.LogError(e, "Error getting schedule from TvMaze");
            return [];
        }
    }

    public async Task<ICollection<Show>> queryShows(int page, CancellationToken ct)
    {
        try
        {
            var response = await _flurlClient.Request("shows")
               .AllowHttpStatus(200, 404)
               .SetQueryParam("page", page)
               .GetAsync(default, ct);

            return response.StatusCode == 200 ? await response.GetJsonAsync<ICollection<Show>>() : [];
        }
        catch (FlurlHttpException e)
        {
            this._logger.LogError(e, "Error getting shows from TvMaze");
            return [];
        }
    }
}
