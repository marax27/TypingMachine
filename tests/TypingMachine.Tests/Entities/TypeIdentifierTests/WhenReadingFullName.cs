using System.Collections.Generic;
using FluentAssertions;
using TypingMachine.Entities;
using TypingMachine.Tests.Utilities;
using Xunit;

namespace TypingMachine.Tests.Entities.TypeIdentifierTests
{
    public class WhenReadingFullName
    {
        [Fact]
        public void GivenZeroParameters_ReturnExpectedResult()
        {
            var entity = "HelloController".AsSimpleTypeId();

            entity.GetFullName().Should().Be("HelloController");
        }

        [Fact]
        public void GivenOneParameter_ReturnExpectedResult()
        {
            var entity = "IQueryHandler".AsGenericTypeId("TQuery");

            entity.GetFullName().Should().Be("IQueryHandler<TQuery>");
        }

        [Fact]
        public void GivenTwoParameters_ReturnExpectedResult()
        {
            var entity = "IFunctor".AsGenericTypeId("TIn", "TOut");

            entity.GetFullName().Should().Be("IFunctor<TIn, TOut>");
        }
    }
}
