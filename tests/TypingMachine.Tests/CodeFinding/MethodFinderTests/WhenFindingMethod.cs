using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TypingMachine.CodeFinders;
using Xunit;

namespace TypingMachine.Tests.CodeFinding.MethodFinderTests
{
    public class WhenFindingMethod
    {
        [Theory]
        [ClassData(typeof(TestContexts))]
        public void GivenSampleSource_ReturnExpectedField(IFindingMethodTestContext context)
        {
            var givenMethodNode = CSharpSyntaxTree.ParseText(context.GivenSource)
                .GetRoot()
                .DescendantNodes().OfType<MethodDeclarationSyntax>()
                .Single();
            var sut = new MethodFinder();

            var actualResult = sut.FindFor(givenMethodNode);

            actualResult.Should().BeEquivalentTo(context.ExpectedResult);
        }

        private class TestContexts : IEnumerable<object[]>
        {
            private readonly IReadOnlyCollection<object[]> _data = new List<object[]>
            {
                new object[] {new SimpleMethodContext()},
                new object[] {new MultipleArgumentsContext()},
            };

            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
