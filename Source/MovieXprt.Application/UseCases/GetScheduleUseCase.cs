
using MovieXprt.Infrastructure.Gateways;
using MovieXprt.Common.Contracts.TvMaze;

using Domain = MovieXprt.Common.Models;

namespace MovieXprt.Application.UseCases
{
    public interface IGetScheduleUsecase
    {
        public Task<ICollection<Domain::Show>> Run(DateOnly airDate, string? countryCode, CancellationToken ct);
    }

    public class GetScheduleUseCase(ITvMazeGateway showsGateway) : IGetScheduleUsecase
    {
        private readonly ITvMazeGateway _showsGateway = showsGateway ?? throw new ArgumentNullException(nameof(showsGateway));
        public async Task<ICollection<Domain::Show>> Run(DateOnly airDate, string? countryCode, CancellationToken ct)
        {
            var shows = await _showsGateway.GetSchedule(airDate, countryCode, ct);

            return shows.Select(this.MapToDomain)
                .GroupBy(x => x.Id).Select(x => x.First()).ToList();
        }

        private Domain::Show MapToDomain(Schedule schedule)
        {
            var show = schedule.Embeded.Show;

            return new Domain::Show
            {
                Id = show.Id,
                Name = show.Name,
                Premiered = show.Premiered,
                Language = show.Language,
                Summary = show.Summary,
                Genres = show.Genres
            };
        }
    }
}
