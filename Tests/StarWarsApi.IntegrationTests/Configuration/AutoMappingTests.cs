using AutoMapper;
using NUnit.Framework;
using StarWars.Api.Configuration;

namespace StarWarsApi.IntegrationTests.Configuration
{
    [TestFixture]
    public class AutoMappingTests
    {
        [Test]
        public void Configuration_ShouldBeValid()
        {
            // arrange
            var configuration = new MapperConfiguration(cfg => {
                cfg.AddProfile<AutoMapping>();
            });

            // Act+assert
            configuration.AssertConfigurationIsValid();
        }
    }
}
