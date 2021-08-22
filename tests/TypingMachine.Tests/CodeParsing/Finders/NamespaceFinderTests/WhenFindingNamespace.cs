using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;
using TypingMachine.CodeParsing.Finders;
using TypingMachine.Tests.Utilities;
using Xunit;

namespace TypingMachine.Tests.CodeParsing.Finders.NamespaceFinderTests
{
    public class WhenFindingNamespace
    {
        [Theory]
        [ContextData(typeof(IFindingNamespaceTestContext))]
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
    }
}
