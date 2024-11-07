using Api.Contracts;
using MovieXprt.Domain.Models;

namespace Api.Mapper
{
    public static class ShowResponseMapper
    {
        public static ShowContract MapToContract(this Show source)
        {
            return new ShowContract
            {
                Id = source.Id,
                Name = source.Name,
                Premiered = source.Premiered,
                Language = source.Language,
                Summary = source.Summary,
                Genres = source.Genres
            };
        }
    }
}
