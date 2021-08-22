using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp;
using TypingMachine.CodeParsing.Walkers;
using TypingMachine.Tests.Utilities;
using Xunit;

namespace TypingMachine.Tests.CodeParsing.Walkers.TypeWalkerTests
{
    public class WhenDiscoveringInterfaceEntity
    {
        [Theory]
        [ContextData(typeof(IDiscoveringInterfaceEntityTestContext))]
        public void GivenSampleSourceCode_ReturnExpectedNamespace(IDiscoveringInterfaceEntityTestContext context)
        {
            var givenRootNode = CSharpSyntaxTree.ParseText(context.GivenSource).GetRoot();
            var sut = new TypeWalker();

            var actualResult = sut.FindAll(givenRootNode);

            actualResult.Should().BeEquivalentTo(context.ExpectedResult);
        }
    }
}
