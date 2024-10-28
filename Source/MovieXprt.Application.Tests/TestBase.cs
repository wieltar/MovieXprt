using AutoFixture;
using AutoFixture.AutoNSubstitute;

namespace MovieXprt.Application.Tests
{
    public class TestBase<T> where T : class
    {
        protected Fixture Fixture { get; }

        public TestBase()
        {
            Fixture = new Fixture();
            Fixture.Customize(new AutoNSubstituteCustomization());
            // https://github.com/AutoFixture/AutoFixture/pull/1305
            Fixture.Customize<DateOnly>(composer => composer.FromFactory<DateTime>(DateOnly.FromDateTime));
        }

        protected virtual T CreateSut()
        {
            return Fixture.Create<T>();
        }
    }
}
