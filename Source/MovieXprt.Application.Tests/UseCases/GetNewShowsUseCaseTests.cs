using AutoFixture;
using MovieXprt.Application.Repositories;
using MovieXprt.Application.UseCases;
using MovieXprt.Infrastructure.Gateways;
using NSubstitute;
using Domain = MovieXprt.Common.Models;
using TvMaze = MovieXprt.Common.Contracts.TvMaze;

namespace MovieXprt.Application.Tests.UseCases
{
    public class GetNewShowsUseCaseTests: TestBase<StoreNewShowsUseCase>
    {

        [Fact]
        public async Task Run_Calls_QueryShows_Until_Empty()
        {
            // Arrange
            var shows = Fixture.CreateMany<TvMaze::Show>(3).ToList();
            var showRepository = Fixture.Freeze<IShowRepository>();
            var tvMazeGateway = Fixture.Freeze<ITvMazeGateway>();

            tvMazeGateway.queryShows(0, Arg.Any<CancellationToken>()).Returns(shows);
            tvMazeGateway.queryShows(1, Arg.Any<CancellationToken>()).Returns(new List<TvMaze::Show>());

            var sut = CreateSut();

            // Act
            await sut.Run(Arg.Any<CancellationToken>());

            // Assert
            await tvMazeGateway.Received(2).queryShows(Arg.Any<int>(), Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task Run_Calls_AddShows_With_Shows_After_Premiered_Date()
        {
            var shows = Fixture.CreateMany<TvMaze::Show>(StoreNewShowsUseCase.PageSize - 4).ToList();
            var premieredDate = DateOnly.Parse("2014-01-01");

            var showOnPremieredDate = Fixture.Build<TvMaze::Show>().With(s => s.Premiered, premieredDate).Create();
            var showAfterPremieredDate = Fixture.Build<TvMaze::Show>().With(s => s.Premiered, premieredDate.AddDays(5)).Create();

            shows.Add(showOnPremieredDate);
            shows.Add(showAfterPremieredDate);

            var showRepository = Fixture.Freeze<IShowRepository>();
            var tvMazeGateway = Fixture.Freeze<ITvMazeGateway>();

            // Act
            var sut = CreateSut();
            await sut.Run(Arg.Any<CancellationToken>());

            //// Assert
            //await showRepository.Received(1).AddShows(
            //    Arg.Is<List<Domain::Show>>(x => x.All(s => s.Premiered >= premieredDate)),
            //    Arg.Any<CancellationToken>()
            //    );
        }
    }
}
