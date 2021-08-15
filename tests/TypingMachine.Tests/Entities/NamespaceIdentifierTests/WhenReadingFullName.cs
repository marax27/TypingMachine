using System.Collections.Generic;
using FluentAssertions;
using TypingMachine.Entities;
using Xunit;

namespace TypingMachine.Tests.Entities.NamespaceIdentifierTests
{
    public class WhenReadingFullName
    {
        [Fact]
        public void Given1Section_ReturnExpectedName()
        {
            var sut = NamespaceIdentifier.Create(new List<string> {"ProjectName"});

            var actualValue = sut.GetFullName();

            actualValue.Should().Be("ProjectName");
        }

        [Fact]
        public void Given2Sections_ReturnExpectedName()
        {
            var sut = NamespaceIdentifier.Create(new List<string> {"ProjectName", "Infrastructure"});

            var actualValue = sut.GetFullName();

            actualValue.Should().Be("ProjectName.Infrastructure");
        }

        [Fact]
        public void Given3Sections_ReturnExpectedName()
        {
            var sut = NamespaceIdentifier.Create(new List<string> {"TypingMachine", "Application", "Features"});

            var actualValue = sut.GetFullName();

            actualValue.Should().Be("TypingMachine.Application.Features");
        }

        [Fact]
        public void GivenNoNamespace_ReturnExpectedName()
        {
            var sut = NamespaceIdentifier.NoNamespace;

            var actualValue = sut.GetFullName();

            actualValue.Should().Be("<root>");
        }
    }
}