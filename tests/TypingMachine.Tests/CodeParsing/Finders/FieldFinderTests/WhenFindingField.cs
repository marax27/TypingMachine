using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;
using TypingMachine.CodeParsing.Finders;
using TypingMachine.Tests.Utilities;
using Xunit;

namespace TypingMachine.Tests.CodeParsing.Finders.FieldFinderTests
{
    public class WhenFindingField
    {
        [Theory]
        [ContextData(typeof(IFindingFieldTestContext))]
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
    }
}
