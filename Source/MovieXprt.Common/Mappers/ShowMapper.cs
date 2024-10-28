using MovieXprt.Common.Contracts.TvMaze;
using MovieXprt.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain = MovieXprt.Common.Models;
using TvMaze = MovieXprt.Common.Contracts.TvMaze;

namespace MovieXprt.Common.Mappers
{
    internal class ShowMapper : IMapper<TvMaze::Show, Domain::Show>
    {
        public Domain.Show Map(TvMaze.Show source)
        {
            return new Domain::Show
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
