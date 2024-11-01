﻿using AutoFixture;
using FluentAssertions;
using MovieXprt.Application.UseCases;
using MovieXprt.Common.Contracts;
using NSubstitute;


using Domain = MovieXprt.Common.Models;
using TvMaze = MovieXprt.Common.Contracts;

namespace MovieXprt.Application.Tests.UseCases
{
    public class GetShowsUseCaseTests : TestBase<GetScheduleUseCase>
    {
        [Fact]
        public async Task GetShowsUseCase_WhenCalled_RemovesDuplicatesFromShows()
        {
            // Arrange
            var duplicate = Fixture.Create<TvMazeShow>();

            var uniqueCollection = Fixture.CreateMany<TvMazeShow>(4).ToList();
             uniqueCollection.Add(duplicate);

            var duplicateCollection = new List<TvMazeShow>(uniqueCollection);

            for (var i = 0; i < 5; i++)
            {
                duplicateCollection.Add(duplicate);
            }

            // Act
            var sut = CreateSut(); 

           var actual = await sut.Run(Arg.Any<DateOnly>(), null, Arg.Any<CancellationToken>());

            // Aseert
            actual.Should().BeEquivalentTo(uniqueCollection);
        }

        [Fact]
        public void GetShowsUseCase_WhenCalled_MapsTvMazeShowsCorrectly()
        {
            // Arrange
            var tvMazeShows = Fixture.CreateMany<TvMaze::TvMazeShow>(3).ToList();
            var expected = tvMazeShows.Select(x => new Domain::Show()
            {
                Id = x.Embeded.Show.Id,
                Name = x.Embeded.Show.Name,
                Premiered = x.Embeded.Show.Premiered,
                Language = x.Embeded.Show.Language,
                Genres = x.Embeded.Show.Genres,
                Summary = x.Embeded.Show.Summary
            }).ToList();

            // Act
            var sut = CreateSut();
            var actual = sut.Run(Arg.Any<DateOnly>(), null, Arg.Any<CancellationToken>());


            // Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
