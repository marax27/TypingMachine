using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using TypingMachine.Entities;
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
            var entity = TypeIdentifier.Create("IService", new List<TypeIdentifier>());

            entity.Name.Should().Be("IService");
        }

        [Fact]
        public void GivenZeroParameters_ContainZeroParameters()
        {
            var entity = TypeIdentifier.Create("IService", new List<TypeIdentifier>());

            entity.Parameters.Should().BeEquivalentTo(new List<TypeIdentifier>());
        }

        [Fact]
        public void GivenOneParameters_ContainExpectedParameter()
        {
            var givenParameterName = "NestedType";

            var entity = TypeIdentifier.Create("IService", new List<TypeIdentifier>
            {
                TypeIdentifier.Create(givenParameterName, new List<TypeIdentifier>())
            });

            entity.Parameters.Should().HaveCount(1);
            entity.Parameters.Single().Name.Should().Be(givenParameterName);
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
                var entity = TypeIdentifier.Create(givenName, new List<TypeIdentifier>());
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
                var entity = TypeIdentifier.Create(givenName, new List<TypeIdentifier>());
            };

            var thrownException = act.Should().Throw<ArgumentException>().Which;

            thrownException.ParamName
                .Should().Be("name");
            thrownException.Message
                .Should().Contain("Type name contains generic parameters. They must be passed separately, as parameters.");
        }
    }
}
