using System.Collections.Generic;
using FluentAssertions;
using TypingMachine.Entities;
using Xunit;

namespace TypingMachine.Tests.Entities.TypeIdentifierTests
{
    public class WhenReadingFullName
    {
        [Fact]
        public void GivenZeroParameters_ReturnExpectedResult()
        {
            var entity = TypeIdentifier.Create("HelloController", new List<TypeIdentifier>());

            entity.GetFullName().Should().Be("HelloController");
        }

        [Fact]
        public void GivenOneParameter_ReturnExpectedResult()
        {
            var entity = TypeIdentifier.Create("IQueryHandler", new List<TypeIdentifier>
            {
                TypeIdentifier.Create("TQuery", new List<TypeIdentifier>())
            });

            entity.GetFullName().Should().Be("IQueryHandler<TQuery>");
        }

        [Fact]
        public void GivenTwoParameters_ReturnExpectedResult()
        {
            var entity = TypeIdentifier.Create("IFunctor", new List<TypeIdentifier>
            {
                TypeIdentifier.Create("TIn", new List<TypeIdentifier>()),
                TypeIdentifier.Create("TOut", new List<TypeIdentifier>()),
            });

            entity.GetFullName().Should().Be("IFunctor<TIn, TOut>");
        }
    }
}
