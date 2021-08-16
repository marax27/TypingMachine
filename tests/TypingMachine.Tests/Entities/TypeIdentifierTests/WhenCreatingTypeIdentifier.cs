using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using TypingMachine.Entities;
using TypingMachine.Tests.Utilities;
using Xunit;

namespace TypingMachine.Tests.Entities.TypeIdentifierTests
{
    public class WhenCreatingTypeIdentifier
    {
        [Fact]
        public void GivenNullParameters_ThrowExpectedExceptions()
        {
            Action act = () =>
            {
                var entity = TypeIdentifier.Create("IService", null);
            };

            act.Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("parameters");
        }

        [Fact]
        public void GivenZeroParameters_ContainExpectedName()
        {
            var entity = "IService".AsSimpleTypeId();

            entity.Name.Should().Be("IService");
        }

        [Fact]
        public void GivenZeroParameters_ContainZeroParameters()
        {
            var entity = "IService".AsSimpleTypeId();

            entity.Parameters.Should().BeEquivalentTo(new List<TypeIdentifier>());
        }

        [Fact]
        public void GivenZeroParameters_ArityIsZero()
        {
            var entity = "IService".AsSimpleTypeId();

            entity.Arity.Should().Be(0);
        }

        [Fact]
        public void GivenOneParameter_ContainExpectedParameter()
        {
            var givenParameterName = "NestedType";

            var entity = "IService".AsGenericTypeId(givenParameterName);

            entity.Parameters.Should().HaveCount(1);
            entity.Parameters.Single().Name.Should().Be(givenParameterName);
        }

        [Fact]
        public void GivenOneParameter_ArityIsOne()
        {
            var givenParameterName = "NestedType";

            var entity = "IService".AsGenericTypeId(givenParameterName);

            entity.Arity.Should().Be(1);
        }

        [Fact]
        public void GivenNullName_ThrowExpectedException()
        {
            Action act = () =>
            {
                var entity = TypeIdentifier.Create(null, new List<TypeIdentifier>());
            };

            act.Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("name");
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("    \t")]
        public void GivenInvalidName_ThrowExpectedException(string givenName)
        {
            Action act = () =>
            {
                var entity = givenName.AsSimpleTypeId();
            };

            var thrownException = act.Should().Throw<ArgumentException>().Which;

            thrownException.ParamName
                .Should().Be("name");
            thrownException.Message
                .Should().Contain("String is empty or whitespace-only.");
        }

        [Theory]
        [InlineData("IEnumerable<double>")]
        [InlineData("SampleService<TIn, TOut>")]
        public void GivenGenericParametersInName_ThrowExpectedException(string givenName)
        {
            Action act = () =>
            {
                var entity = givenName.AsSimpleTypeId();
            };

            var thrownException = act.Should().Throw<ArgumentException>().Which;

            thrownException.ParamName
                .Should().Be("name");
            thrownException.Message
                .Should().Contain("Type name contains generic parameters. They must be passed separately, as parameters.");
        }
    }
}
