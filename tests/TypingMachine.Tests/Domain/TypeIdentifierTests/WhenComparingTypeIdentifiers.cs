using FluentAssertions;
using System.Collections.Generic;
using TypingMachine.Domain;
using TypingMachine.Tests.Utilities;
using Xunit;

namespace TypingMachine.Tests.Domain.TypeIdentifierTests
{
    public class WhenComparingTypeIdentifiers
    {
        [Fact]
        public void GivenIdenticalNameAndZeroParameters_ReturnEqual()
        {
            var firstIdentifier = "Service".AsSimpleId();
            var otherIdentifier = "Service".AsSimpleId();

            firstIdentifier.Should().Be(otherIdentifier);
        }

        [Fact]
        public void GivenIdenticalNameAndSampleParameters_ReturnEqual()
        {
            var firstIdentifier = "Service".AsGenericId("T1", "T2");
            var otherIdentifier = "Service".AsGenericId("T1", "T2");

            firstIdentifier.Should().Be(otherIdentifier);
        }

        [Fact]
        public void GivenDifferentName_ReturnNotEqual()
        {
            var firstIdentifier = "IService".AsSimpleId();
            var otherIdentifier = "Service".AsSimpleId();

            firstIdentifier.Should().NotBe(otherIdentifier);
        }

        [Fact]
        public void GivenDifferentParameters_ReturnNotEqual()
        {
            var firstIdentifier = Identifier.Create("Service", new List<Identifier>
            {
                "IEnumerable".AsGenericId("int"),
                "T".AsSimpleId()
            });
            var otherIdentifier = Identifier.Create("Service", new List<Identifier>
            {
                "IEnumerable".AsSimpleId(),
                "T".AsSimpleId()
            });

            firstIdentifier.Should().NotBe(otherIdentifier);
        }

        [Fact]
        public void GivenEntityComparedWithItself_ReturnEqual()
        {
            var firstIdentifier = "Service".AsGenericId("T1", "T2");
            var otherIdentifier = firstIdentifier;

            (firstIdentifier == otherIdentifier)
                .Should().BeTrue();
        }

        [Fact]
        public void GivenEntityAndNull_ReturnEqual()
        {
            var firstIdentifier = "Service".AsGenericId("T1", "T2");

            (firstIdentifier == null)
                .Should().BeFalse();
        }
    }
}
