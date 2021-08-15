using System.Collections;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp;
using TypingMachine.CodeWalkers;
using Xunit;

namespace TypingMachine.Tests.CodeWalking.TypeWalkerTests
{
    public class WhenDiscoveringInterfaceEntity
    {
        [Theory]
        [ClassData(typeof(TestContexts))]
        public void GivenSampleSourceCode_ReturnExpectedNamespace(IDiscoveringInterfaceEntityTestContext context)
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
                new object[] {new EmptyInterfaceContext()},
                new object[] {new GenericInterfaceContext()},
                new object[] {new InterfaceWith1BaseTypeContext()},
                new object[] {new InterfaceInNamespaceContext()},
            };

            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
