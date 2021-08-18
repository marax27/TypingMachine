using System;
using System.Collections.Generic;
using FluentAssertions;
using TypingMachine.Builders;
using TypingMachine.Entities;
using TypingMachine.Tests.Utilities;
using Xunit;

namespace TypingMachine.Tests.Entities.MethodEntityTests
{
    public class WhenCreatingMethodEntity
    {
        private TypeIdentifier GivenSampleType => "int".AsSimpleTypeId();
        private TypeIdentifier GivenOtherType => "IService".AsSimpleTypeId();

        [Fact]
        public void GivenValidParameters_CreateEntity()
        {
            Action act = () =>
            {
                var entity = new MethodBuilder()
                    .WithArgumentTypes(new[] {GivenSampleType})
                    .Build("Foo", GivenSampleType);
            };

            act.Should().NotThrow();
        }

        [Fact]
        public void GivenValidParameters_ContainExpectedValues()
        {
            var expectedArgumentTypes = new List<TypeIdentifier> {GivenOtherType, GivenSampleType};

            var entity = new MethodBuilder()
                .WithArgumentTypes(new[] {GivenOtherType, GivenSampleType})
                .Build("Foo", GivenSampleType);

            entity.Name.Should().Be("Foo");
            entity.ReturnType.Should().Be(GivenSampleType);
            entity.ArgumentTypes
                .Should().BeEquivalentTo(expectedArgumentTypes, options => options.WithStrictOrdering());
        }

        [Fact]
        public void GivenNullName_ThrowExpectedException()
        {
            Action act = () =>
            {
                var entity = new MethodBuilder()
                    .Build(null, GivenSampleType);
            };

            act.Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("name");
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("\t\t    \t")]
        public void GivenEmptyOrWhitespaceName_ThrowExpectedException(string givenName)
        {
            Action act = () =>
            {
                var entity = new MethodBuilder()
                    .Build(givenName, GivenSampleType);
            };

            var thrownException = act.Should().Throw<ArgumentOutOfRangeException>().Which;

            thrownException.ParamName.Should().Be("name");
            thrownException.Message.Should().Contain("Method name is empty or whitespace-only.");
        }

        [Fact]
        public void GivenNullReturnType_ThrowExpectedException()
        {
            Action act = () =>
            {
                var entity = new MethodBuilder()
                    .Build("GetValue", null);
            };

            act.Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("returnType");
        }

        [Fact]
        public void GivenNullArgumentTypes_ThrowExpectedException()
        {
            Action act = () =>
            {
                var entity = new MethodBuilder()
                    .WithArgumentTypes(null)
                    .Build("GetValue", GivenSampleType);
            };

            act.Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("argumentTypes");
        }
    }
}
