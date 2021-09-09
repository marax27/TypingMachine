using FluentAssertions;
using TypingMachine.Tests.Utilities;
using Xunit;

namespace TypingMachine.Tests.Domain.TypeIdentifierTests
{
    public class WhenReadingFullName
    {
        [Fact]
        public void GivenZeroParameters_ReturnExpectedResult()
        {
            var entity = "HelloController".AsSimpleId();

            entity.GetFullName().Should().Be("HelloController");
        }

        [Fact]
        public void GivenOneParameter_ReturnExpectedResult()
        {
            var entity = "IQueryHandler".AsGenericId("TQuery");

            entity.GetFullName().Should().Be("IQueryHandler<TQuery>");
        }

        [Fact]
        public void GivenTwoParameters_ReturnExpectedResult()
        {
            var entity = "IFunctor".AsGenericId("TIn", "TOut");

            entity.GetFullName().Should().Be("IFunctor<TIn, TOut>");
        }
    }
}
