using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;
using TypingMachine.CodeParsing.Finders;
using TypingMachine.Tests.Utilities;
using Xunit;

namespace TypingMachine.Tests.CodeParsing.Finders.TypeFinderTests
{
    public class WhenFindingType
    {
        [Theory]
        [ContextData(typeof(IFindingTypeTestContext))]
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
    }
}
