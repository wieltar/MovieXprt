namespace MovieXprt.Domain.UseCases;
using MovieXprt.Domain.Models;

public interface IGetScheduleUsecase
{
    public Task<ICollection<Show>> Run(DateOnly airDate, string? countryCode, CancellationToken ct);
}
