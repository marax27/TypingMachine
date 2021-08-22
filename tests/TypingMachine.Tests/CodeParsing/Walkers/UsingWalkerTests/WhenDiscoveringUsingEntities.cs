using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp;
using TypingMachine.CodeParsing.Walkers;
using TypingMachine.Tests.Utilities;
using Xunit;

namespace TypingMachine.Tests.CodeParsing.Walkers.UsingWalkerTests
{
    public class WhenDiscoveringUsingEntities
    {
        [Theory]
        [ContextData(typeof(IDiscoveringUsingEntitiesTestContext))]
        public void GivenSampleSourceCode_ReturnExpectedResult(IDiscoveringUsingEntitiesTestContext context)
        {
            var givenRootNode = CSharpSyntaxTree.ParseText(context.GivenSource).GetRoot();
            var sut = new UsingWalker();

            var actualResult = sut.FindAll(givenRootNode);

            actualResult.Should().BeEquivalentTo(context.ExpectedResult);
        }
    }
}
