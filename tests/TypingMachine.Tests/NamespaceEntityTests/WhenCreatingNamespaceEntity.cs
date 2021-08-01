using System;
using System.Collections.Generic;
using FluentAssertions;
using TypingMachine.Entities;
using Xunit;

namespace TypingMachine.Tests.NamespaceEntityTests
{
    public class WhenCreatingNamespaceEntity
    {
        private string[] GivenSections => new[] {"ProjectName", "Application", "Services"};

        [Fact]
        public void GivenValidSections_CreateEntity()
        {
            Action act = () =>
            {
                var entity = NamespaceEntity.Create(GivenSections);
            };

            act.Should().NotThrow();
        }

        [Fact]
        public void GivenValidSections_ContainExpectedNumberOfSections()
        {
            var entity = NamespaceEntity.Create(GivenSections);

            entity.Sections.Should().HaveCount(3);
        }

        [Fact]
        public void GivenNullSections_ThrowExpectedException()
        {
            Action act = () =>
            {
                var entity = NamespaceEntity.Create(null!);
            };

            act.Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("sections");
        }

        [Fact]
        public void GivenZeroSections_ThrowExceptionWithExpectedParameterName()
        {
            Action act = () =>
            {
                var entity = NamespaceEntity.Create(new List<string>());
            };

            act.Should().Throw<ArgumentOutOfRangeException>()
                .Which.ParamName.Should().Be("sections");
        }

        [Fact]
        public void GivenZeroSections_ThrowExceptionWithExpectedMessage()
        {
            Action act = () =>
            {
                var entity = NamespaceEntity.Create(new List<string>());
            };

            act.Should().Throw<ArgumentOutOfRangeException>()
                .Which.Message.Should()
                .Contain("No sections provided. To represent root-level namespace, use NoNamespace.");
        }
    }
}