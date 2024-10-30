using MovieXprt.Common.Models;

namespace MovieXprt.Application.Repositories
{
    public interface IShowRepository
    {
        void AddShows(IEnumerable<Show> showsOnPage, CancellationToken ct);
        int? getHighestShowId();
        public int getPage();
        
    }


    public class ShowRepository
    {
    }
}
