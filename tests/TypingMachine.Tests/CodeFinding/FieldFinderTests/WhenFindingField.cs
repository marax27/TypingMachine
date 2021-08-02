using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TypingMachine.CodeFinders;
using Xunit;

namespace TypingMachine.Tests.CodeFinding.FieldFinderTests
{
    public class WhenFindingField
    {
        [Theory]
        [ClassData(typeof(TestContexts))]
        public void GivenSampleSource_ReturnExpectedField(IFindingFieldTestContext context)
        {
            var givenNodes = CSharpSyntaxTree.ParseText(context.GivenSource)
                .GetRoot()
                .DescendantNodes().OfType<ClassDeclarationSyntax>()
                .Single()
                .ChildNodes().ToList();
            var sut = new FieldFinder();

            var actualResult = sut.FindFor(givenNodes).ToList();

            actualResult.Should().BeEquivalentTo(context.ExpectedResult, options => options.WithStrictOrdering());
        }

        private class TestContexts : IEnumerable<object[]>
        {
            private readonly IReadOnlyCollection<object[]> _data = new List<object[]>
            {
                new object[] {new SampleFieldContext()},
                new object[] {new MultipleFieldsContext() },
                new object[] {new MultipleFieldsInOneLineContext() },
            };

            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
