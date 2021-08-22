using System.Collections.Generic;
using TypingMachine.Domain;
using TypingMachine.Tests.Utilities;

namespace TypingMachine.Tests.CodeFinding.TypeFinderTests
{
    public interface IFindingTypeTestContext
    {
        string GivenSource { get; }
        TypeIdentifier ExpectedResult { get; }
    }

    class SimpleTypeContext : IFindingTypeTestContext
    {
        public string GivenSource => "IQueryHandler";
        public TypeIdentifier ExpectedResult =>
            "IQueryHandler".AsSimpleTypeId();
    }

    class PredefinedTypeContext : IFindingTypeTestContext
    {
        public string GivenSource => "string";
        public TypeIdentifier ExpectedResult =>
            "string".AsSimpleTypeId();
    }

    class GenericTypeWithSingleParameterContext : IFindingTypeTestContext
    {
        public string GivenSource => "IEnumerable<float>";
        public TypeIdentifier ExpectedResult =>
            "IEnumerable".AsGenericTypeId("float");
    }

    class GenericTypeWithNestedParametersContext : IFindingTypeTestContext
    {
        public string GivenSource => "BaseService<int, ILogger<ApiController>, IFunctor<TIn, TOut>>";
        public TypeIdentifier ExpectedResult =>
            TypeIdentifier.Create("BaseService", new List<TypeIdentifier>
            {
                "int".AsSimpleTypeId(),
                "ILogger".AsGenericTypeId("ApiController"),
                "IFunctor".AsGenericTypeId("TIn", "TOut")
            });
    }

    class NullableTypeContext : IFindingTypeTestContext
    {
        public string GivenSource => "string?";
        public TypeIdentifier ExpectedResult => "string".AsSimpleTypeId();
    }

    class OneDimensionalArrayTypeContext : IFindingTypeTestContext
    {
        public string GivenSource => "int[]";
        public TypeIdentifier ExpectedResult => "int".AsSimpleTypeId();
    }

    class ThreeDimensionalArrayTypeContext : IFindingTypeTestContext
    {
        public string GivenSource => "double[][][]";
        public TypeIdentifier ExpectedResult => "double".AsSimpleTypeId();
    }
}
