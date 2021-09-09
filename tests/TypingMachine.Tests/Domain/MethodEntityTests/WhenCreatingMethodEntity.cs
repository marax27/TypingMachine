using FluentAssertions;
using System;
using System.Collections.Generic;
using TypingMachine.Domain;
using TypingMachine.Domain.Builders;
using TypingMachine.Tests.Utilities;
using Xunit;

namespace TypingMachine.Tests.Domain.MethodEntityTests
{
    public class WhenCreatingMethodEntity
    {
        private Identifier GivenSampleType => "int".AsSimpleId();
        private Identifier GivenOtherType => "IService".AsSimpleId();

        [Fact]
        public void GivenValidParameters_CreateEntity()
        {
            Action act = () =>
            {
                var entity = new MethodBuilder()
                    .WithArgumentTypes(new[] {GivenSampleType})
                    .Build("Foo".AsSimpleId(), GivenSampleType);
            };

            act.Should().NotThrow();
        }

        [Fact]
        public void GivenValidParameters_ContainExpectedValues()
        {
            var expectedArgumentTypes = new List<Identifier> {GivenOtherType, GivenSampleType};

            var entity = new MethodBuilder()
                .WithArgumentTypes(new[] {GivenOtherType, GivenSampleType})
                .Build("Foo".AsSimpleId(), GivenSampleType);

            entity.Identifier.Name.Should().Be("Foo");
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
                .Which.ParamName.Should().Be("identifier");
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
                    .Build(givenName.AsSimpleId(), GivenSampleType);
            };

            var thrownException = act.Should().Throw<ArgumentException>().Which;

            thrownException.ParamName.Should().Be("name");
            thrownException.Message.Should().Contain("String is empty or whitespace-only.");
        }

        [Fact]
        public void GivenNullReturnType_ThrowExpectedException()
        {
            Action act = () =>
            {
                var entity = new MethodBuilder()
                    .Build("GetValue".AsSimpleId(), null);
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
                    .Build("GetValue".AsSimpleId(), GivenSampleType);
            };

            act.Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("argumentTypes");
        }
    }
}
