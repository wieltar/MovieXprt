namespace MovieXprt.Application.Mappers;

using MovieXprt.Application.Gateways.TvMaze.Models;
using Domain = MovieXprt.Domain;
internal static class ShowMapper
{
    public static Domain::Models.Show MapToDomain(this Show source)
    {
        return new Domain::Models.Show
        {
            Id = source.Id,
            Name = source.Name,
            Premiered = source.Premiered,
            Language = source.Language,
            Summary = source.Summary,
            Genres = source.Genres
        };
    }

    public static Domain::Models.Show MapToDomainShow(this Schedule source)
    {
        return source.Embeded.Show.MapToDomain();
    }
}
