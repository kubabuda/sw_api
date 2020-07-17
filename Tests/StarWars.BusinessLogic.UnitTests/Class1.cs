using FluentAssertions;
using NUnit.Framework;
using System;

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
