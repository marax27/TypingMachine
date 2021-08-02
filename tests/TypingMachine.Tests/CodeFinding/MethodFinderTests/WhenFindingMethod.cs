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
        public void GivenSampleSource_MethodHasExpectedName(IFindingMethodTestContext context)
        {
            var givenMethodNode = GetMethodNode(context);
            var sut = new MethodFinder();

            var actualResult = sut.FindFor(givenMethodNode);

            actualResult.Name.Should().Be(context.ExpectedMethodName);
        }

        [Theory]
        [ClassData(typeof(TestContexts))]
        public void GivenSampleSource_MethodHasExpectedReturnType(IFindingMethodTestContext context)
        {
            var givenMethodNode = GetMethodNode(context);
            var sut = new MethodFinder();

            var actualResult = sut.FindFor(givenMethodNode);

            actualResult.ReturnType.Should().Be(context.ExpectedReturnType);
        }

        [Theory]
        [ClassData(typeof(TestContexts))]
        public void GivenSampleSource_MethodHasExpectedArgumentTypes(IFindingMethodTestContext context)
        {
            var givenMethodNode = GetMethodNode(context);
            var sut = new MethodFinder();

            var actualResult = sut.FindFor(givenMethodNode);

            actualResult.ArgumentTypes.Should().BeEquivalentTo(context.ExpectedArgumentTypes);
        }

        private MethodDeclarationSyntax GetMethodNode(IFindingMethodTestContext context)
            => CSharpSyntaxTree.ParseText(context.GivenSource)
                .GetRoot()
                .DescendantNodes().OfType<MethodDeclarationSyntax>()
                .Single();

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
