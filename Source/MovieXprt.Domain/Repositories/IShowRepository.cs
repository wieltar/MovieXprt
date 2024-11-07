namespace MovieXprt.Domain.Repositories;

using MovieXprt.Domain.Models;

public interface IShowRepository
{
    Task<int> getHighestShowId();
    Task AddShows(IEnumerable<Show> showsOnPage, CancellationToken ct);
    public Task<IEnumerable<Show>> GetShows(int page, int pageSize, CancellationToken ct);
}