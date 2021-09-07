using System.Collections.Generic;
using TypingMachine.Domain;
using TypingMachine.Tests.Utilities;

namespace TypingMachine.Tests.CodeParsing.Finders.MethodFinderTests
{
    public interface IFindingMethodTestContext
    {
        string GivenSource { get; }

        Identifier ExpectedMethodIdentifier { get; }
        Identifier ExpectedReturnType { get; }
        List<Identifier> ExpectedArgumentTypes { get; }
        AccessModifier ExpectedAccess { get; }
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

        public Identifier ExpectedMethodIdentifier
            => "GetSquared".AsSimpleId();

        public Identifier ExpectedReturnType
            => "float".AsSimpleId();

        public List<Identifier> ExpectedArgumentTypes
            => new List<Identifier>
                {
                    "int".AsSimpleId()
                };

        public AccessModifier ExpectedAccess => AccessModifier.Public;
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

        public Identifier ExpectedMethodIdentifier
            => "Process".AsSimpleId();

        public Identifier ExpectedReturnType
            => "void".AsSimpleId();

        public List<Identifier> ExpectedArgumentTypes
            => new List<Identifier>
                {
                    "IFunctor".AsSimpleId(),
                    "double".AsSimpleId(),
                    "IEnumerable".AsGenericId("int")
                };
        public AccessModifier ExpectedAccess => AccessModifier.Protected;
    }

    class ImplicitAccessModifierContext : IFindingMethodTestContext
    {
        public string GivenSource => @"
class OtherService
{
    int Process()
    {
    }
}
";

        public Identifier ExpectedMethodIdentifier
            => "Process".AsSimpleId();

        public Identifier ExpectedReturnType
            => "int".AsSimpleId();

        public List<Identifier> ExpectedArgumentTypes
            => new();
        public AccessModifier ExpectedAccess => AccessModifier.Private;
    }

    class GenericMethodWith1ParameterContext : IFindingMethodTestContext
    {
        public string GivenSource => @"
class SomeService
{
    public IEnumerable<T> GenerateSequence<T>() where T : new()
    {
        for (int i = 0; i < 5; ++i)
            yield return new T();
    }
}
";

        public Identifier ExpectedMethodIdentifier
            => "GenerateSequence".AsGenericId("T");

        public Identifier ExpectedReturnType
            => "IEnumerable".AsGenericId("T");

        public List<Identifier> ExpectedArgumentTypes
            => new();

        public AccessModifier ExpectedAccess
            => AccessModifier.Public;
    }

    class GenericMethodWith3ParametersContext : IFindingMethodTestContext
    {
        public string GivenSource => @"
class ICalculator
{
    IFunctor<TIn, TOut> Run<TIn, TOut, TAdditional>();
}
";

        public Identifier ExpectedMethodIdentifier
            => "Run".AsGenericId("TIn", "TOut", "TAdditional");

        public Identifier ExpectedReturnType
            => "IFunctor".AsGenericId("TIn", "TOut");

        public List<Identifier> ExpectedArgumentTypes
            => new();

        public AccessModifier ExpectedAccess
            => AccessModifier.Private;
    }
}
