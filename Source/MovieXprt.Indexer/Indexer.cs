using Microsoft.Extensions.Hosting;
using MovieXprt.Domain.UseCases;

namespace MovieXprt.Indexer
{
    public class Indexer(
            IStoreNewShowsUseCase storeNewShowsUseCase,
            IHostApplicationLifetime applicationLifetime
        ) : IHostedService
    {
        private readonly IStoreNewShowsUseCase _storeNewShowsUseCase = storeNewShowsUseCase ?? throw new ArgumentNullException(nameof(applicationLifetime));

        private bool pleaseStop;
        private Task BackgroundTask;
        

        public Task StartAsync(CancellationToken ct)
        {
            Console.WriteLine("Starting service");

            BackgroundTask = Task.Run(async () =>
            {
                while (!pleaseStop)
                {
                   await _storeNewShowsUseCase.Run(ct);
                }
                Console.WriteLine("Background task gracefully stopped");
            }, ct);

            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Stopping service");

            pleaseStop = true;
            await BackgroundTask;

            Console.WriteLine("Service stopped");
        }
    }
}
