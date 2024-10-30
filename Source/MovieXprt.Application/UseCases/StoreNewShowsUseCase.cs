namespace MovieXprt.Application.UseCases;

using MovieXprt.Application.Repositories;
using MovieXprt.Common.Mappers;
using MovieXprt.Infrastructure.Gateways;
using Domain = MovieXprt.Common.Models;
using TvMaze = MovieXprt.Common.Contracts.TvMaze;
public interface IStoreNewShowsUseCase
{
    public Task Run(CancellationToken ct);
}

public class StoreNewShowsUseCase(
        ITvMazeGateway tvMazeGateway,
        IShowRepository showRepository,
        IMapper<TvMaze::Show, Domain::Show> mapper
    ) : IStoreNewShowsUseCase
{
    public const int PageSize = 250;

    private readonly ITvMazeGateway _tvMazeGateway = tvMazeGateway;
    private readonly IShowRepository _showRepository = showRepository;
    private readonly IMapper<TvMaze.Show, Domain.Show> _mapper = mapper;

    public async Task Run(CancellationToken ct)
    {
        var showIndex = _showRepository.getHighestShowId() ?? 1;
        var currentPage =  showIndex / PageSize;

        var interestingShows = new List<Domain::Show>();
        var retrieveShows = true;

        do
        {
            var showsOnPage = await _tvMazeGateway.queryShows(currentPage, ct);

            retrieveShows = showsOnPage.Count > 0 || !ct.IsCancellationRequested;

            interestingShows.AddRange(showsOnPage.Select(_mapper.Map).Where(InteresedInShow).ToList());
            _showRepository.AddShows(interestingShows, ct);

            currentPage++;

        } while (retrieveShows);
    }

    private bool InteresedInShow(Domain::Show show)
    {
        return show.Premiered >= DateOnly.Parse("2014-01-01");
    }
}
