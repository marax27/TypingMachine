﻿using System;
using FluentAssertions;
using TypingMachine.Entities;
using Xunit;

namespace TypingMachine.Tests.Entities.UsingEntityTests
{
    public class WhenCreatingUsingEntity
    {
        private NamespaceEntity SampleNamespace
            => NamespaceEntity.Create(new[] {"Application", "ApiGateway", "Controllers"});

        [Fact]
        public void GivenSampleNamespace_ContainExpectedNamespace()
        {
            var givenNamespace = SampleNamespace;
            var expectedNamespace = SampleNamespace;

            var entity = UsingEntity.Create(givenNamespace);

            entity.UsedNamespace.Should().Be(expectedNamespace);
        }

        [Fact]
        public void GivenNoNamespace_ThrowExpectedException()
        {
            var givenNamespace = NamespaceEntity.NoNamespace;

            Action act = () =>
            {
                var entity = UsingEntity.Create(givenNamespace);
            };

            var thrownException = act.Should().Throw<ArgumentException>().Which;

            thrownException.ParamName.Should().Be("usedNamespace");
            thrownException.Message.Should().Contain("Using directive must involve a concrete namespace.");
        }

        [Fact]
        public void GivenNullNamespace_ThrowExpectedException()
        {
            Action act = () =>
            {
                var entity = UsingEntity.Create(null);
            };

            var thrownException = act.Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("usedNamespace");
        }
    }
}