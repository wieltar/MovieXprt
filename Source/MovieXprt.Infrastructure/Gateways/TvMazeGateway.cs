namespace MovieXprt.Infrastructure.Gateways;

using Flurl;
using Flurl.Http;
using Flurl.Http.Configuration;
using MovieXprt.Common.Models;

public interface ITvMazeGateway
{
    public Task<ICollection<Show>> GetSchedule(DateOnly from, string? countrycode);
}

public class TvMazeGateway(
    IFlurlClientCache flurlClientCache
    ) : ITvMazeGateway
{
    private readonly IFlurlClient _flurlClient = flurlClientCache.Get("TvMazeClient") ?? throw new ArgumentNullException(nameof(flurlClientCache));

    public async Task<ICollection<Show>> GetSchedule(DateOnly from, string? countrycode)
    {
        return await _flurlClient.Request("web", "shows")
            .AppendPathSegment("schedule")
            .SetQueryParam("country", countrycode, NullValueHandling.Remove)
            .SetQueryParam("date", from.ToString("yyyy-MM-dd"))
            .GetJsonAsync<ICollection<Show>>();
    }
}
