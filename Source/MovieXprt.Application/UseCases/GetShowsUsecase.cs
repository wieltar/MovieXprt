using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieXprt.Common.Models;
using MovieXprt.Infrastructure.Gateways;

namespace MovieXprt.Application.UseCases
{
    public interface IGetScheduleUsecase
    {
        public ICollection<Show> Run(DateOnly airDate, string? countryCode);
    }

    public class GetScheduleUseCase(IShowsGateway showsGateway) : IGetScheduleUsecase
    {
        private readonly IShowsGateway _showsGateway = showsGateway ?? throw new ArgumentNullException(nameof(showsGateway));
        public ICollection<Show> Run(DateOnly airDate, string? countryCode)
        {
            _showsGateway.getSchedule(airDate, countryCode);
        }
    }
}
