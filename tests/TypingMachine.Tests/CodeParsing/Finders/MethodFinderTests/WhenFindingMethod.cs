using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;
using TypingMachine.CodeParsing.Finders;
using TypingMachine.Tests.Utilities;
using Xunit;

namespace TypingMachine.Tests.CodeParsing.Finders.MethodFinderTests
{
    public class WhenFindingMethod
    {
        [Theory]
        [ContextData(typeof(IFindingMethodTestContext))]
        public void GivenSampleSource_MethodHasExpectedName(IFindingMethodTestContext context)
        {
            var givenMethodNode = GetMethodNode(context);
            var sut = new MethodFinder();

            var actualResult = sut.FindFor(givenMethodNode);

            actualResult.Identifier.Name.Should().Be(context.ExpectedMethodName);
        }

        [Theory]
        [ContextData(typeof(IFindingMethodTestContext))]
        public void GivenSampleSource_MethodHasExpectedReturnType(IFindingMethodTestContext context)
        {
            var givenMethodNode = GetMethodNode(context);
            var sut = new MethodFinder();

            var actualResult = sut.FindFor(givenMethodNode);

            actualResult.ReturnType.Should().Be(context.ExpectedReturnType);
        }

        [Theory]
        [ContextData(typeof(IFindingMethodTestContext))]
        public void GivenSampleSource_MethodHasExpectedArgumentTypes(IFindingMethodTestContext context)
        {
            var givenMethodNode = GetMethodNode(context);
            var sut = new MethodFinder();

            var actualResult = sut.FindFor(givenMethodNode);

            actualResult.ArgumentTypes
                .Should().BeEquivalentTo(context.ExpectedArgumentTypes, options => options.WithStrictOrdering());
        }

        [Theory]
        [ContextData(typeof(IFindingMethodTestContext))]
        public void GivenSampleSource_MethodHasExpectedAccessModifier(IFindingMethodTestContext context)
        {
            var givenMethodNode = GetMethodNode(context);
            var sut = new MethodFinder();

            var actualResult = sut.FindFor(givenMethodNode);

            actualResult.AccessModifier.Should().Be(context.ExpectedAccess);
        }

        private MethodDeclarationSyntax GetMethodNode(IFindingMethodTestContext context)
            => CSharpSyntaxTree.ParseText(context.GivenSource)
                .GetRoot()
                .DescendantNodes().OfType<MethodDeclarationSyntax>()
                .Single();
    }
}
