using FluentAssertions;
using NUnit.Framework;

namespace StarWars.BusinessLogic.UnitTests.Services
{
    [TestFixture]
    public class CharactersServiceTests
    {
        [Test]
        public void GetCharacters_ShouldReturnNthPage_GivenPageNr()
        {

        }

        [Test]
        public void Test_shouldPass()
        {
            (2 + 2).Should().Be(4);
        }
    }
}
