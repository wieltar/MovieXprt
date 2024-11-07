namespace MovieXprt.Infrastructure.DataStore.Gateways;
using MovieXprt.Domain.Models;
using MovieXprt.Domain.Repositories;

public class SqlKataShowRepository : IShowRepository
{
    public Task AddShows(IEnumerable<Show> showsOnPage, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<int> getHighestShowId()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Show>> GetShows(int page, int pageSize, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
