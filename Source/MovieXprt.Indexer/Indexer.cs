using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieXprt.Indexer
{
    public class Indexer : IHostedService, IHostedLifecycleService
    {
        public Indexer() { }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task StartedAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task StartingAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task StoppedAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task StoppingAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
