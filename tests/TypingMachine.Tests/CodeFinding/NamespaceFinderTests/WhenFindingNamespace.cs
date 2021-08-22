using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TypingMachine.CodeParsing.Finders;
using Xunit;

namespace TypingMachine.Tests.CodeFinding.NamespaceFinderTests
{
    public class WhenFindingNamespace
    {
        [Theory]
        [ClassData(typeof(TestContexts))]
        public void GivenSampleSourceCode_ReturnExpectedNamespace(IFindingNamespaceTestContext context)
        {
            var givenClassNode = CSharpSyntaxTree.ParseText(context.GivenSource)
                .GetRoot()
                .DescendantNodes().OfType<ClassDeclarationSyntax>()
                .Single();
            var sut = new NamespaceFinder();

            var actualResult = sut.FindFor(givenClassNode);

            actualResult.Should().Be(context.ExpectedResult);
        }

        private class TestContexts : IEnumerable<object[]>
        {
            private readonly IReadOnlyCollection<object[]> _data = new List<object[]>
            {
                new object[] {new SampleContext()},
                new object[] {new TwoNamespacesSideBySideContext()},
                new object[] {new NodeOutsideNamespaceContext()},
                new object[] {new SampleNestedContext()},
            };

            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
