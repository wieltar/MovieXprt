
using Domain = MovieXprt.Domain.Models;
using MovieXprt.Domain.UseCases;
using MovieXprt.Application.Mappers;
using MovieXprt.Application.Gateways.TvMaze;

namespace MovieXprt.Application.UseCases
{
    public class GetScheduleUseCase(ITvMazeGateway showsGateway) : IGetScheduleUsecase
    {
        private readonly ITvMazeGateway _showsGateway = showsGateway ?? throw new ArgumentNullException(nameof(showsGateway));
        public async Task<ICollection<Domain::Show>> Run(DateOnly airDate, string? countryCode, CancellationToken ct)
        {
            var shows = await _showsGateway.GetSchedule(airDate, countryCode, ct);

            return shows.Select(x => x.MapToDomainShow())
                .GroupBy(x => x.Id).Select(x => x.First()).ToList();
        }
    }
}
