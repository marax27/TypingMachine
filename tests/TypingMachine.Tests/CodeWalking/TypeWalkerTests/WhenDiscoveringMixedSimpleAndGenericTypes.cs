using System.Linq;
using FluentAssertions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using TypingMachine.CodeWalkers;
using Xunit;

namespace TypingMachine.Tests.CodeWalking.TypeWalkerTests
{
    public class WhenDiscoveringMixedSimpleAndGenericTypes
    {
        [Fact]
        public void GivenMixedSimpleAndGenericTypes_ReturnExpectedNumberOfTypes()
        {
            var sut = new TypeWalker();

            var actualResult = sut.FindAll(GivenRootNode);

            actualResult.Should().HaveCount(6);
        }

        [Fact]
        public void GivenMixedSimpleAndGenericTypes_EachResultTypeHasUniqueIdentifier()
        {
            var sut = new TypeWalker();

            var actualResult = sut.FindAll(GivenRootNode);

            actualResult
                .Select(type => type.Identifier)
                .Should().OnlyHaveUniqueItems();
        }

        private SyntaxNode GivenRootNode => CSharpSyntaxTree.ParseText(GivenSourceCode).GetRoot();

        private const string GivenSourceCode = @"
namespace Examples
{
    interface IHello<TIn, TOut> { }

    interface IHello<T> : IHello<T, int> { }

    interface IHello : IHello<IEnumerable<int>> { }

    abstract class Hello<T> : IHello<T> { }

    class Hello : Hello<string> { }

    class Hello<T1, T2> : Hello<IDictionary<T1, T2>> { }
}
";
    }
}
