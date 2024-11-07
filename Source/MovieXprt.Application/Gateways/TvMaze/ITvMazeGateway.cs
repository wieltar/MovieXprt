using MovieXprt.Application.Gateways.TvMaze.Models;

namespace MovieXprt.Application.Gateways.TvMaze;

public interface ITvMazeGateway
{
    public Task<ICollection<Schedule>> GetSchedule(DateOnly from, string? countrycode, CancellationToken ct);
    public Task<ICollection<Show>> queryShows(int page, CancellationToken ct);
}
