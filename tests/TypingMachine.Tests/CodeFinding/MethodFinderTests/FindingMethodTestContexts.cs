using System.Collections.Generic;
using TypingMachine.Entities;

namespace TypingMachine.Tests.CodeFinding.MethodFinderTests
{
    public interface IFindingMethodTestContext
    {
        string GivenSource { get; }
        MethodEntity ExpectedResult { get; }
    }

    class SimpleMethodContext : IFindingMethodTestContext
    {
        public string GivenSource => @"
class MathService
{
    public float GetSquared(int value)
    {
        var f = (float)value;
        return f * f;
    }
}
";

        public MethodEntity ExpectedResult =>
            MethodEntity.Create(
                "GetSquared",
                TypeIdentifier.Create("float", new List<TypeIdentifier>()),
                new List<TypeIdentifier>
                {
                    TypeIdentifier.Create("int", new List<TypeIdentifier>())
                }
            );
    }

    class MultipleArgumentsContext : IFindingMethodTestContext
    {
        public string GivenSource => @"
class SecondService
{
    protected void Process(IFunctor functor, double magicValue, IEnumerable<int> integers)
    {
    }
}
";

        public MethodEntity ExpectedResult =>
            MethodEntity.Create(
                "Process",
                TypeIdentifier.Create("void", new List<TypeIdentifier>()),
                new List<TypeIdentifier>
                {
                    TypeIdentifier.Create("IFunctor", new List<TypeIdentifier>()),
                    TypeIdentifier.Create("double", new List<TypeIdentifier>()),
                    TypeIdentifier.Create("IEnumerable", new List<TypeIdentifier>
                    {
                        TypeIdentifier.Create("int", new List<TypeIdentifier>())
                    })
                }
            );
    }
}
