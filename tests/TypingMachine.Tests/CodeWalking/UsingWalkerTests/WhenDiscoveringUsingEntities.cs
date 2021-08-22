using System.Collections;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp;
using TypingMachine.CodeParsing.Walkers;
using Xunit;

namespace TypingMachine.Tests.CodeWalking.UsingWalkerTests
{
    public class WhenDiscoveringUsingEntities
    {
        [Theory]
        [ClassData(typeof(TestContexts))]
        public void GivenSampleSourceCode_ReturnExpectedResult(IDiscoveringUsingEntitiesTestContext context)
        {
            var givenRootNode = CSharpSyntaxTree.ParseText(context.GivenSource).GetRoot();
            var sut = new UsingWalker();

            var actualResult = sut.FindAll(givenRootNode);

            actualResult.Should().BeEquivalentTo(context.ExpectedResult);
        }

        private class TestContexts : IEnumerable<object[]>
        {
            private readonly IReadOnlyCollection<object[]> _data = new List<object[]>
            {
                new object[] {new ZeroUsingDirectivesContext()},
                new object[] {new OneUsingDirectiveContext()},
                new object[] {new ThreeUsingDirectivesContext()},
                new object[] {new StaticImportContext()},
                new object[] {new AliasDirectiveContext()},
            };

            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
