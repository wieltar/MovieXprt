namespace MovieXprt.Application.Gateways.TvMaze.Models;
using System.Text.Json.Serialization;

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
