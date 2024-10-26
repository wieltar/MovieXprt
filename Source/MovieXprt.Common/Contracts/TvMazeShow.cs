using System.Text.Json.Serialization;

namespace MovieXprt.Common.Contracts
{
    public sealed record TvMazeShow
    {
        [JsonPropertyName("_embedded")]
        public required Embedded Embeded{ get; set; }

        public sealed record Embedded
        {
            [JsonPropertyName("show")]
            public required Show Show { get; init; }
        }

        public sealed record Show
        {
            public int Id { get; init; }
            public required string Name { get; init; }
            public DateOnly? Premiered { get; init; }
            public required string Language { get; init; }
            public string Summary { get; init; } = string.Empty;
            public ICollection<string> Genres { get; init; } = [];
        }
    }
}
