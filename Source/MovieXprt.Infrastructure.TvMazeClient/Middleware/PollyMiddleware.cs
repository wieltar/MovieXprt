using Polly;

namespace MovieXprt.Infrastructure.TvMazeClient.Middleware
{
    public class PollyHandler(IAsyncPolicy<HttpResponseMessage> policy) : DelegatingHandler
    {
        private readonly IAsyncPolicy<HttpResponseMessage> _policy = policy;

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return _policy.ExecuteAsync(ct => base.SendAsync(request, ct), cancellationToken);
        }
    }
}
