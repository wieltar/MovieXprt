namespace MovieXprt.Application.UseCases;

using MovieXprt.Application.Mappers;
using MovieXprt.Domain.UseCases;
using MovieXprt.Domain.Repositories;
using MovieXprt.Application.Gateways.TvMaze;

public class StoreNewShowsUseCase(
        ITvMazeGateway tvMazeGateway,
        IShowRepository showRepository
    ) : IStoreNewShowsUseCase
{
    public const int PageSize = 250;

    private readonly ITvMazeGateway _tvMazeGateway = tvMazeGateway;
    private readonly IShowRepository _showRepository = showRepository;

    public async Task Run(CancellationToken ct)
    {
        var showIndex = await _showRepository.getHighestShowId();
        var currentPage =  showIndex / PageSize;

        var interestingShows = new List<Domain.Models.Show>();
        var retrieveShows = true;

        do
        {
            var showsOnPage = await _tvMazeGateway.queryShows(currentPage, ct);

            retrieveShows = showsOnPage.Count > 0 || !ct.IsCancellationRequested;

            interestingShows.AddRange(showsOnPage.Select(x => x.MapToDomain()).Where(InteresedInShow).ToList());
            await _showRepository.AddShows(interestingShows, ct);

            currentPage++;

        } while (retrieveShows);
    }

    private static bool InteresedInShow(Domain.Models.Show show) => show.Premiered >= DateOnly.Parse("2014-01-01");
}
