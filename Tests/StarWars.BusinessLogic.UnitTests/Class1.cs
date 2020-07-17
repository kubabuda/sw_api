using FluentAssertions;
using NUnit.Framework;

namespace StarWars.BusinessLogic.UnitTests
{
    [TestFixture]
    public class BusinessLogicUnitTests
    {
        [Test]
        public void Test_shouldPass()
        {
            (2 + 2).Should().Be(4);
        }
    }
}
