using FluentAssertions;
using System.Collections;
using System.Collections.Generic;
using TypingMachine.CodeParsing.Finders;
using Xunit;

namespace TypingMachine.Tests.CodeParsing.Finders.AccessModifierFinderTests
{
    public class WhenFindingAccessModifier
    {
        [Theory]
        [ClassData(typeof(TestContexts))]
        public void GivenSampleType_ReturnExpectedAccessModifier(IFindingAccessModifierTestContexts context)
        {
            var sut = new AccessModifierFinder();

            var actualResult = sut.FindFor(context.GivenNode, context.GivenDefault);

            actualResult.Should().Be(context.ExpectedResult);
        }

        private class TestContexts : IEnumerable<object[]>
        {
            private readonly IReadOnlyCollection<object[]> _data = new List<object[]>
            {
                new object[] { new SimplePublicClassContext() },
                new object[] { new ExplicitInternalClassContext() },
                new object[] { new NoAccessModifierSpecifiedPrivateDefaultContext() },
                new object[] { new NoAccessModifierSpecifiedInternalDefaultContext() },
                new object[] { new ProtectedClassContext() },
                new object[] { new PrivateClassContext() },
                new object[] { new PrivateProtectedClassContext() },
                new object[] { new ProtectedInternalClassContext() },
            };

            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
