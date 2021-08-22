using System.Collections;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp;
using TypingMachine.CodeParsing.Walkers;
using Xunit;

namespace TypingMachine.Tests.CodeWalking.TypeWalkerTests
{
    public class WhenDiscoveringClassEntity
    {
        [Theory]
        [ClassData(typeof(TestContexts))]
        public void GivenSampleSourceCode_ReturnExpectedNamespace(IDiscoveringClassEntityTestContext context)
        {
            var givenRootNode = CSharpSyntaxTree.ParseText(context.GivenSource).GetRoot();
            var sut = new TypeWalker();

            var actualResult = sut.FindAll(givenRootNode);

            actualResult.Should().BeEquivalentTo(context.ExpectedResult);
        }

        private class TestContexts : IEnumerable<object[]>
        {
            private readonly IReadOnlyCollection<object[]> _data = new List<object[]>
            {
                new object[] {new EmptyClassContext()},
                new object[] {new ClassWithMultipleBaseTypesContext()},
                new object[] {new ClassWithFieldsContext()},
                new object[] {new ClassInNamespaceContext()},
                new object[] {new ClassWithSeveralUsingDirectivesContext()},
            };

            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
