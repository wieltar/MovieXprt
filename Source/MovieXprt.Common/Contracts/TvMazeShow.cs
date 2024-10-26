using MovieXprt.Common.Models;


namespace MovieXprt.Common.Contracts
{
    public sealed record TvMazeShow
    {
        public int Score { get; init; }
        public Show Show { get; init; }
    }
}
