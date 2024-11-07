using AutoFixture;

namespace Api.Tests
{
    public class TestBase<T> where T : class
    {
        protected Fixture Fixture { get; init; } = new Fixture();

        protected virtual T CreateSut()
        {
            return Fixture.Create<T>();
        }
    }
}