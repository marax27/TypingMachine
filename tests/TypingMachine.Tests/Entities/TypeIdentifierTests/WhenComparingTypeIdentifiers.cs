using System.Collections.Generic;
using FluentAssertions;
using TypingMachine.Domain;
using TypingMachine.Tests.Utilities;
using Xunit;

namespace TypingMachine.Tests.Entities.TypeIdentifierTests
{
    public class WhenComparingTypeIdentifiers
    {
        [Fact]
        public void GivenIdenticalNameAndZeroParameters_ReturnEqual()
        {
            var firstIdentifier = "Service".AsSimpleTypeId();
            var otherIdentifier = "Service".AsSimpleTypeId();

            firstIdentifier.Should().Be(otherIdentifier);
        }

        [Fact]
        public void GivenIdenticalNameAndSampleParameters_ReturnEqual()
        {
            var firstIdentifier = "Service".AsGenericTypeId("T1", "T2");
            var otherIdentifier = "Service".AsGenericTypeId("T1", "T2");

            firstIdentifier.Should().Be(otherIdentifier);
        }

        [Fact]
        public void GivenDifferentName_ReturnNotEqual()
        {
            var firstIdentifier = "IService".AsSimpleTypeId();
            var otherIdentifier = "Service".AsSimpleTypeId();

            firstIdentifier.Should().NotBe(otherIdentifier);
        }

        [Fact]
        public void GivenDifferentParameters_ReturnNotEqual()
        {
            var firstIdentifier = TypeIdentifier.Create("Service", new List<TypeIdentifier>
            {
                "IEnumerable".AsGenericTypeId("int"),
                "T".AsSimpleTypeId()
            });
            var otherIdentifier = TypeIdentifier.Create("Service", new List<TypeIdentifier>
            {
                "IEnumerable".AsSimpleTypeId(),
                "T".AsSimpleTypeId()
            });

            firstIdentifier.Should().NotBe(otherIdentifier);
        }

        [Fact]
        public void GivenEntityComparedWithItself_ReturnEqual()
        {
            var firstIdentifier = "Service".AsGenericTypeId("T1", "T2");
            var otherIdentifier = firstIdentifier;

            (firstIdentifier == otherIdentifier)
                .Should().BeTrue();
        }

        [Fact]
        public void GivenEntityAndNull_ReturnEqual()
        {
            var firstIdentifier = "Service".AsGenericTypeId("T1", "T2");

            (firstIdentifier == null)
                .Should().BeFalse();
        }
    }
}
