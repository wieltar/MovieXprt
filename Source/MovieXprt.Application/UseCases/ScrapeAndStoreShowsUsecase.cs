using MovieXprt.Common.Models;
using MovieXprt.Infrastructure.Gateways;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieXprt.Application.UseCases
{
    interface IScrapeAndStoreShowsUsecase
    {
        public bool Run(CancellationToken ct);
    }

    public class ScrapeAndStoreShowsUsecase(IShowsGateway showGateway) : IScrapeAndStoreShowsUsecase
    {
        private readonly IShowsGateway _showGateway = showGateway ?? throw new ArgumentNullException(nameof(showGateway));

        public bool Run(CancellationToken ct)
        {

            throw new NotImplementedException();
        }
    }
}
