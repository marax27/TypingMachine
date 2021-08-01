using FluentAssertions;
using TypingMachine.Entities;
using Xunit;

namespace TypingMachine.Tests.NamespaceEntityTests
{
    public class WhenComparingNamespaceEntities
    {
        private static string[] GivenSampleSections => new[] {"ProjectName", "Application", "Services"};
        private static string[] GivenOtherSections => new[] {"ProjectName", "Infrastructure", "Database"};
        private static string[] GivenMoreDetailedSections => new[] {"ProjectName", "Application", "Services", "Hello"};


        [Fact]
        public void GivenIdenticalSections_TheyAreEqual()
        {
            var firstEntity = NamespaceEntity.Create(GivenSampleSections);
            var otherEntity = NamespaceEntity.Create(GivenSampleSections);

            (firstEntity == otherEntity)
                .Should().BeTrue();
        }

        [Fact]
        public void GivenIdenticalSections_TheyAreNotUnequal()
        {
            var firstEntity = NamespaceEntity.Create(GivenSampleSections);
            var otherEntity = NamespaceEntity.Create(GivenSampleSections);

            (firstEntity != otherEntity)
                .Should().BeFalse();
        }

        [Fact]
        public void GivenDifferentSections_TheyAreNotEqual()
        {
            var firstEntity = NamespaceEntity.Create(GivenSampleSections);
            var otherEntity = NamespaceEntity.Create(GivenOtherSections);

            (firstEntity == otherEntity)
                .Should().BeFalse();
        }

        [Fact]
        public void GivenDifferentSections_TheyAreUnequal()
        {
            var firstEntity = NamespaceEntity.Create(GivenSampleSections);
            var otherEntity = NamespaceEntity.Create(GivenOtherSections);

            (firstEntity != otherEntity)
                .Should().BeTrue();
        }

        [Fact]
        public void GivenPartiallyMatchingSections_TheyAreNotEqual()
        {
            var firstEntity = NamespaceEntity.Create(GivenSampleSections);
            var otherEntity = NamespaceEntity.Create(GivenMoreDetailedSections);

            (firstEntity == otherEntity)
                .Should().BeFalse();
        }

        [Fact]
        public void GivenPartiallyMatchingSections_TheyAreUnequal()
        {
            var firstEntity = NamespaceEntity.Create(GivenSampleSections);
            var otherEntity = NamespaceEntity.Create(GivenMoreDetailedSections);

            (firstEntity != otherEntity)
                .Should().BeTrue();
        }

        [Fact]
        public void GivenEntityComparedWithItself_TheyAreEqual()
        {
            var firstEntity = NamespaceEntity.Create(GivenSampleSections);
            var otherEntity = firstEntity;

            (firstEntity == otherEntity)
                .Should().BeTrue();
            firstEntity.Equals(otherEntity)
                .Should().BeTrue();
        }

        [Fact]
        public void GivenEntityAndNull_TheyAreNotEqual()
        {
            var entity = NamespaceEntity.Create(GivenSampleSections);

            (entity == null)
                .Should().BeFalse();
            entity.Equals(null)
                .Should().BeFalse();
        }

        [Fact]
        public void GivenSampleSectionsAndNoNamespace_TheyAreNotEqual()
        {
            var entity = NamespaceEntity.Create(GivenSampleSections);

            (entity == NamespaceEntity.NoNamespace)
                .Should().BeFalse();
        }

        [Fact]
        public void GivenSampleSectionsAndNoNamespace_TheyAreUnequal()
        {
            var entity = NamespaceEntity.Create(GivenSampleSections);

            (entity != NamespaceEntity.NoNamespace)
                .Should().BeTrue();
        }

        [Fact]
        public void Given2InstancesOfNoNamespace_TheyAreEqual()
        {
            var firstEntity = NamespaceEntity.NoNamespace;
            var otherEntity = NamespaceEntity.NoNamespace;

            (firstEntity == otherEntity)
                .Should().BeTrue();
        }
    }
}