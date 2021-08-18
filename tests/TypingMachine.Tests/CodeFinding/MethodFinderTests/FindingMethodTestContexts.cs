using System.Collections.Generic;
using TypingMachine.Entities;
using TypingMachine.Tests.Utilities;

namespace TypingMachine.Tests.CodeFinding.MethodFinderTests
{
    public interface IFindingMethodTestContext
    {
        string GivenSource { get; }

        string ExpectedMethodName { get; }
        TypeIdentifier ExpectedReturnType { get; }
        List<TypeIdentifier> ExpectedArgumentTypes { get; }
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

        public TypeIdentifier ExpectedReturnType
            => "float".AsSimpleTypeId();

        public List<TypeIdentifier> ExpectedArgumentTypes
            => new List<TypeIdentifier>
                {
                    "int".AsSimpleTypeId()
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

        public TypeIdentifier ExpectedReturnType
            => "void".AsSimpleTypeId();

        public List<TypeIdentifier> ExpectedArgumentTypes
            => new List<TypeIdentifier>
                {
                    "IFunctor".AsSimpleTypeId(),
                    "double".AsSimpleTypeId(),
                    "IEnumerable".AsGenericTypeId("int")
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

        public TypeIdentifier ExpectedReturnType
            => "int".AsSimpleTypeId();

        public List<TypeIdentifier> ExpectedArgumentTypes
            => new();
        public AccessModifier ExpectedAccess => AccessModifier.Private;
    }
}
