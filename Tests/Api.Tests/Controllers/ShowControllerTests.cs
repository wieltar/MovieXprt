using Api.Controllers;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using MovieXprt.Application.UseCases;
using MovieXprt.Common.Models;
using NSubstitute;

namespace Api.Tests.Controllers
{
    public class ShowControllerTests : TestBase<ShowController>
    {
        [Fact]
        public async Task Get_Returns_Array_Of_Shows()
        {
            // Arrange
            var usecase = Fixture.Freeze<IGetScheduleUsecase>();
            var expected = Fixture.CreateMany<Show>().ToList();

            var date = Fixture.Create<DateOnly>();
            var countryCode = Fixture.Create<string>();

            usecase.Run(Arg.Any<DateOnly>(), Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns(expected);

            // Act
            var sut = CreateSut();

            var result = await sut.Get(date, countryCode, CancellationToken.None);

            // Assert
            result.Value.Should().BeEquivalentTo(expected);
            result.Result.Should().BeOfType<OkObjectResult>();

            await usecase.Received().Run(date, countryCode, Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task Get_Returns_Ok_On_Empty_Result()
        {
            // Arrange
            var usecase = Fixture.Freeze<IGetScheduleUsecase>();

            var date = Fixture.Create<DateOnly>();
            var countryCode = Fixture.Create<string>();

            usecase.Run(Arg.Any<DateOnly>(), Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns([]);

            // Act
            var sut = CreateSut();

            var result = await sut.Get(date, countryCode, CancellationToken.None);

            // Assert
            result.Value.Should().BeEmpty();
            result.Result.Should().BeOfType<OkObjectResult>();

            await usecase.Received().Run(date, countryCode, Arg.Any<CancellationToken>());
        }

        [Fact]
        public void Delete_Returns_NoContent()
        {
            // Arrange
            var sut = CreateSut();

            // Act
           var result = sut.Delete(1);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }
    }
}
