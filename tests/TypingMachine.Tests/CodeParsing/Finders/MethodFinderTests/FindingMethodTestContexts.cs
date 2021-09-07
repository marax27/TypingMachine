using System.Collections.Generic;
using TypingMachine.Domain;
using TypingMachine.Tests.Utilities;

namespace TypingMachine.Tests.CodeParsing.Finders.MethodFinderTests
{
    public interface IFindingMethodTestContext
    {
        string GivenSource { get; }

        string ExpectedMethodName { get; }
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

        public string ExpectedMethodName
            => "GetSquared";

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

        public string ExpectedMethodName
            => "Process";

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

        public string ExpectedMethodName
            => "Process";

        public Identifier ExpectedReturnType
            => "int".AsSimpleId();

        public List<Identifier> ExpectedArgumentTypes
            => new();
        public AccessModifier ExpectedAccess => AccessModifier.Private;
    }

    class GenericMethodContext : IFindingMethodTestContext
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

        public string ExpectedMethodName
            => "GenerateSequence";

        public Identifier ExpectedReturnType
            => "IEnumerable".AsGenericId("T");

        public List<Identifier> ExpectedArgumentTypes
            => new();

        public AccessModifier ExpectedAccess
            => AccessModifier.Public;
    }
}
