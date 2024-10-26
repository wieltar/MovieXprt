using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieXprt.Infrastructure.Gateways
{
    public interface ITvMazeGateway
    {
        public ICollection<TVMazeSchedule> GetSchedule(DateOnly from, string? countrycode);
    }

    public class TvMazeGateway : ITvMazeGateway
    {
    }
}
