namespace MovieXprt.Application.Gateways.TvMaze.Models;
public sealed record Show
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public DateOnly Premiered { get; init; }
    public required string Language { get; init; }
    public string Summary { get; init; } = string.Empty;
    public ICollection<string> Genres { get; init; } = [];
}
