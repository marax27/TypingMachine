﻿using System;
using System.Collections.Generic;
using FluentAssertions;
using TypingMachine.Entities;
using Xunit;

namespace TypingMachine.Tests.Entities.MethodEntityTests
{
    public class WhenCreatingMethodEntity
    {
        private TypeIdentifier GivenSampleType => TypeIdentifier.Create("int", new List<TypeIdentifier>());
        private TypeIdentifier GivenOtherType => TypeIdentifier.Create("IService", new List<TypeIdentifier>());

        [Fact]
        public void GivenValidParameters_CreateEntity()
        {
            Action act = () =>
            {
                var entity = MethodEntity.Create("Foo", GivenSampleType, new List<TypeIdentifier> { GivenSampleType });
            };

            act.Should().NotThrow();
        }

        [Fact]
        public void GivenValidParameters_ContainExpectedValues()
        {
            var entity = MethodEntity.Create(
                "Foo",
                GivenSampleType,
                new List<TypeIdentifier> {GivenOtherType, GivenSampleType}
            );

            entity.Name.Should().Be("Foo");
            entity.ReturnType.Should().Be(GivenSampleType);
            entity.ArgumentTypes.Should().BeEquivalentTo(GivenOtherType, GivenSampleType);
        }

        [Fact]
        public void GivenNullName_ThrowExpectedException()
        {
            Action act = () =>
            {
                var entity = MethodEntity.Create(null, GivenSampleType, new List<TypeIdentifier>());
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
                var entity = MethodEntity.Create(givenName, GivenSampleType, new List<TypeIdentifier>());
            };

            act.Should().Throw<ArgumentOutOfRangeException>()
                .Which.ParamName.Should().Be("name");
        }

        [Fact]
        public void GivenNullReturnType_ThrowExpectedException()
        {
            Action act = () =>
            {
                var entity = MethodEntity.Create("GetValue", null, new List<TypeIdentifier>());
            };

            act.Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("returnType");
        }

        [Fact]
        public void GivenNullArgumentTypes_ThrowExpectedException()
        {
            Action act = () =>
            {
                var entity = MethodEntity.Create("GetValue", GivenSampleType, null);
            };

            act.Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("argumentTypes");
        }
    }
}
