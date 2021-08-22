using FluentAssertions;
using TypingMachine.Domain;
using Xunit;

namespace TypingMachine.Tests.Domain.NamespaceIdentifierTests
{
    public class WhenComparingNamespaceIdentifiers
    {
        private static string[] GivenSampleSections => new[] {"ProjectName", "Application", "Services"};
        private static string[] GivenOtherSections => new[] {"ProjectName", "Infrastructure", "Database"};
        private static string[] GivenMoreDetailedSections => new[] {"ProjectName", "Application", "Services", "Hello"};


        [Fact]
        public void GivenIdenticalSections_TheyAreEqual()
        {
            var firstEntity = NamespaceIdentifier.Create(GivenSampleSections);
            var otherEntity = NamespaceIdentifier.Create(GivenSampleSections);

            (firstEntity == otherEntity)
                .Should().BeTrue();
        }

        [Fact]
        public void GivenIdenticalSections_TheyAreNotUnequal()
        {
            var firstEntity = NamespaceIdentifier.Create(GivenSampleSections);
            var otherEntity = NamespaceIdentifier.Create(GivenSampleSections);

            (firstEntity != otherEntity)
                .Should().BeFalse();
        }

        [Fact]
        public void GivenDifferentSections_TheyAreNotEqual()
        {
            var firstEntity = NamespaceIdentifier.Create(GivenSampleSections);
            var otherEntity = NamespaceIdentifier.Create(GivenOtherSections);

            (firstEntity == otherEntity)
                .Should().BeFalse();
        }

        [Fact]
        public void GivenDifferentSections_TheyAreUnequal()
        {
            var firstEntity = NamespaceIdentifier.Create(GivenSampleSections);
            var otherEntity = NamespaceIdentifier.Create(GivenOtherSections);

            (firstEntity != otherEntity)
                .Should().BeTrue();
        }

        [Fact]
        public void GivenPartiallyMatchingSections_TheyAreNotEqual()
        {
            var firstEntity = NamespaceIdentifier.Create(GivenSampleSections);
            var otherEntity = NamespaceIdentifier.Create(GivenMoreDetailedSections);

            (firstEntity == otherEntity)
                .Should().BeFalse();
        }

        [Fact]
        public void GivenPartiallyMatchingSections_TheyAreUnequal()
        {
            var firstEntity = NamespaceIdentifier.Create(GivenSampleSections);
            var otherEntity = NamespaceIdentifier.Create(GivenMoreDetailedSections);

            (firstEntity != otherEntity)
                .Should().BeTrue();
        }

        [Fact]
        public void GivenEntityComparedWithItself_TheyAreEqual()
        {
            var firstEntity = NamespaceIdentifier.Create(GivenSampleSections);
            var otherEntity = firstEntity;

            (firstEntity == otherEntity)
                .Should().BeTrue();
            firstEntity.Equals(otherEntity)
                .Should().BeTrue();
        }

        [Fact]
        public void GivenEntityAndNull_TheyAreNotEqual()
        {
            var entity = NamespaceIdentifier.Create(GivenSampleSections);

            (entity == null)
                .Should().BeFalse();
            entity?.Equals(null)
                .Should().BeFalse();
        }

        [Fact]
        public void GivenSampleSectionsAndNoNamespace_TheyAreNotEqual()
        {
            var entity = NamespaceIdentifier.Create(GivenSampleSections);

            (entity == NamespaceIdentifier.NoNamespace)
                .Should().BeFalse();
        }

        [Fact]
        public void GivenSampleSectionsAndNoNamespace_TheyAreUnequal()
        {
            var entity = NamespaceIdentifier.Create(GivenSampleSections);

            (entity != NamespaceIdentifier.NoNamespace)
                .Should().BeTrue();
        }

        [Fact]
        public void Given2InstancesOfNoNamespace_TheyAreEqual()
        {
            var firstEntity = NamespaceIdentifier.NoNamespace;
            var otherEntity = NamespaceIdentifier.NoNamespace;

            (firstEntity == otherEntity)
                .Should().BeTrue();
        }
    }
}