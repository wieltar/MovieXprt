namespace Api.Contracts
{
    public sealed record ShowContract
    {
        public int Id { get; init; }
        public required string Name { get; init; }
        public DateOnly? Premiered { get; init; }
        public required string Language { get; init; }
        public string Summary { get; init; } = string.Empty;
        public ICollection<string> Genres { get; init; } = [];
    }
}
