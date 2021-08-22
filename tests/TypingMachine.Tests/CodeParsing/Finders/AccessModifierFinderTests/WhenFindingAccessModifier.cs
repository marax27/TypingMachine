using FluentAssertions;
using TypingMachine.CodeParsing.Finders;
using TypingMachine.Tests.Utilities;
using Xunit;

namespace TypingMachine.Tests.CodeParsing.Finders.AccessModifierFinderTests
{
    public class WhenFindingAccessModifier
    {
        [Theory]
        [ContextData(typeof(IFindingAccessModifierTestContexts))]
        public void GivenSampleType_ReturnExpectedAccessModifier(IFindingAccessModifierTestContexts context)
        {
            var sut = new AccessModifierFinder();

            var actualResult = sut.FindFor(context.GivenNode, context.GivenDefault);

            actualResult.Should().Be(context.ExpectedResult);
        }
    }
}
