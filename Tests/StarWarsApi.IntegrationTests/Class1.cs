using FluentAssertions;
using System;
using Xunit;

namespace StarWarsApi.IntegrationTests
{
    
    public class Class1
    {
        [Fact]
        public void Test_shouldPass()
        {
            (2 + 2).Should().Be(4);
        }


        [Fact]
        public void Test_shouldFail()
        {
            (2 + 2).Should().Be(5);
        }
    }
}
