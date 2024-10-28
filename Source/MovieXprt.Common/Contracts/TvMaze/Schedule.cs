using System.Text.Json.Serialization;

namespace MovieXprt.Common.Contracts.TvMaze
{
    public sealed record Schedule
    {
        [JsonPropertyName("_embedded")]
        public required Embedded Embeded { get; set; }

        public sealed record Embedded
        {
            [JsonPropertyName("show")]
            public required Show Show { get; init; }
        }
    }
}
