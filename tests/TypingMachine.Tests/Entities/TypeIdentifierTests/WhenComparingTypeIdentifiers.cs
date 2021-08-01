using System.Collections.Generic;
using FluentAssertions;
using TypingMachine.Entities;
using Xunit;

namespace TypingMachine.Tests.Entities.TypeIdentifierTests
{
    public class WhenComparingTypeIdentifiers
    {
        [Fact]
        public void GivenIdenticalNameAndZeroParameters_ReturnEqual()
        {
            var firstIdentifier = TypeIdentifier.Create("Service", new List<TypeIdentifier>());
            var otherIdentifier = TypeIdentifier.Create("Service", new List<TypeIdentifier>());

            firstIdentifier.Should().Be(otherIdentifier);
        }

        [Fact]
        public void GivenIdenticalNameAndSampleParameters_ReturnEqual()
        {
            var firstIdentifier = TypeIdentifier.Create("Service", new List<TypeIdentifier>
            {
                TypeIdentifier.Create("T1", new List<TypeIdentifier>()),
                TypeIdentifier.Create("T2", new List<TypeIdentifier>())
            });
            var otherIdentifier = TypeIdentifier.Create("Service", new List<TypeIdentifier>
            {
                TypeIdentifier.Create("T1", new List<TypeIdentifier>()),
                TypeIdentifier.Create("T2", new List<TypeIdentifier>())
            });

            firstIdentifier.Should().Be(otherIdentifier);
        }

        [Fact]
        public void GivenDifferentName_ReturnNotEqual()
        {
            var firstIdentifier = TypeIdentifier.Create("IService", new List<TypeIdentifier>());
            var otherIdentifier = TypeIdentifier.Create("Service", new List<TypeIdentifier>());

            firstIdentifier.Should().NotBe(otherIdentifier);
        }

        [Fact]
        public void GivenDifferentParameters_ReturnNotEqual()
        {
            var firstIdentifier = TypeIdentifier.Create("Service", new List<TypeIdentifier>
            {
                TypeIdentifier.Create("IEnumerable", new List<TypeIdentifier>
                {
                    TypeIdentifier.Create("int", new List<TypeIdentifier>())
                }),
                TypeIdentifier.Create("T", new List<TypeIdentifier>())
            });
            var otherIdentifier = TypeIdentifier.Create("Service", new List<TypeIdentifier>
            {
                TypeIdentifier.Create("IEnumerable", new List<TypeIdentifier>()),
                TypeIdentifier.Create("T", new List<TypeIdentifier>())
            });

            firstIdentifier.Should().NotBe(otherIdentifier);
        }

        [Fact]
        public void GivenEntityComparedWithItself_ReturnEqual()
        {
            var firstIdentifier = TypeIdentifier.Create("Service", new List<TypeIdentifier>
            {
                TypeIdentifier.Create("T1", new List<TypeIdentifier>()),
                TypeIdentifier.Create("T2", new List<TypeIdentifier>())
            });
            var otherIdentifier = firstIdentifier;

            (firstIdentifier == otherIdentifier)
                .Should().BeTrue();
        }

        [Fact]
        public void GivenEntityAndNull_ReturnEqual()
        {
            var firstIdentifier = TypeIdentifier.Create("Service", new List<TypeIdentifier>
            {
                TypeIdentifier.Create("T1", new List<TypeIdentifier>()),
                TypeIdentifier.Create("T2", new List<TypeIdentifier>())
            });

            (firstIdentifier == null)
                .Should().BeFalse();
        }
    }
}
