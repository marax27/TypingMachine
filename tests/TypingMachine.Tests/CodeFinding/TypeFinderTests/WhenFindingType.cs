using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TypingMachine.CodeFinders;
using Xunit;

namespace TypingMachine.Tests.CodeFinding.TypeFinderTests
{
    public class WhenFindingType
    {
        [Theory]
        [ClassData(typeof(TestContexts))]
        public void GivenSampleSource_ReturnExpectedType(IFindingTypeTestContext context)
        {
            var givenNode = CSharpSyntaxTree.ParseText(context.GivenSource)
                .GetRoot()
                .DescendantNodes().OfType<TypeSyntax>()
                .First();
            var sut = new TypeFinder();

            var actualResult = sut.FindFor(givenNode);

            actualResult.Should().Be(context.ExpectedResult);
        }

        private class TestContexts : IEnumerable<object[]>
        {
            private readonly IReadOnlyCollection<object[]> _data = new List<object[]>
            {
                new object[] {new SimpleTypeContext()},
                new object[] {new PredefinedTypeContext()},
                new object[] {new GenericTypeWithSingleParameterContext()},
                new object[] {new GenericTypeWithNestedParametersContext()},
                new object[] {new NullableTypeContext()},
                new object[] {new OneDimensionalArrayTypeContext()},
                new object[] {new ThreeDimensionalArrayTypeContext()},
            };

            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
