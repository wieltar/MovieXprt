namespace MovieXprt.Domain.UseCases;
public interface IStoreNewShowsUseCase
{
    public Task Run(CancellationToken ct);
}

