using FluentAssertions;
using System;
using TypingMachine.Domain;
using TypingMachine.Domain.Builders;
using TypingMachine.Tests.Utilities;
using Xunit;

namespace TypingMachine.Tests.Domain.FieldEntityTests
{
    public class WhenCreatingFieldEntity
    {
        private Identifier GivenSampleType
            => "IService".AsSimpleTypeId();

        [Fact]
        public void GivenValidParameters_ContainExpectedName()
        {
            var entity = new FieldBuilder().Build("_service", GivenSampleType);

            entity.Name.Should().Be("_service");
        }

        [Fact]
        public void GivenValidParameters_ContainExpectedType()
        {
            var entity = new FieldBuilder().Build("_service", GivenSampleType);

            entity.Type.Should().Be(GivenSampleType);
        }

        [Theory]
        [InlineData("")]
        [InlineData("\t")]
        [InlineData("    \t ")]
        public void GivenEmptyOrWhitespaceName_ThrowExpectedException(string givenName)
        {
            Action act = () =>
            {
                var entity = new FieldBuilder().Build(givenName, GivenSampleType);
            };

            var thrownException = act.Should().Throw<ArgumentException>().Which;

            thrownException.ParamName.Should().Be("name");
            thrownException.Message.Should().Contain("String is empty or whitespace-only.");
        }

        [Fact]
        public void GivenNullName_ThrowExpectedException()
        {
            Action act = () =>
            {
                var entity = new FieldBuilder().Build(null, GivenSampleType);
            };

            act.Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("name");
        }

        [Fact]
        public void GivenNullType_ThrowExpectedException()
        {
            Action act = () =>
            {
                var entity = new FieldBuilder().Build("_service", null);
            };

            act.Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("type");
        }

        [Fact]
        public void GivenBothParametersAreNull_ThrowExpectedException()
        {
            Action act = () =>
            {
                var entity = new FieldBuilder().Build(null, null);
            };

            act.Should().Throw<ArgumentNullException>();
        }
    }
}
