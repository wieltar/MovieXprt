using AutoFixture;
using Flurl.Http;
using Flurl.Http.Configuration;
using MovieXprt.Infrastructure.Gateways;
using NSubstitute;

namespace MovieXprt.Infrastructure.Tests.Gateways
{
    public class TvMazeGatewayTests : TestBase<TvMazeGateway>
    {
        [Fact]
        public void GetSchedules_Calls_Api_With_ISO_Date_String()
        {

            var clientCache = Fixture.Create<IFlurlClientCache>();
            var flurlClient = Fixture.Create<IFlurlClient>();

            clientCache.Get("TvMazeClient").Returns(flurlClient);

            // Arrange
            var sut = CreateSut();
            var airDate = new DateOnly(2021, 10, 10);
            var countryCode = "US";
            var expectedUrl = $"http://api.tvmaze.com/schedule?country={countryCode}&date={airDate:yyyy-MM-dd}";

            // Act
            var result = sut.GetSchedule(airDate, null, CancellationToken.None);

            // Assert
            flurlClient.Received().Request("web", "shows")
                .SetQueryParam("date", airDate.ToString("yyyy-MM-dd"));
        }
    }
}
