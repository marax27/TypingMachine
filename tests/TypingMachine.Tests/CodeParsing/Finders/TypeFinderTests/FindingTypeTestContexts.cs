using System.Collections.Generic;
using TypingMachine.Domain;
using TypingMachine.Tests.Utilities;

namespace TypingMachine.Tests.CodeParsing.Finders.TypeFinderTests
{
    public interface IFindingTypeTestContext
    {
        string GivenSource { get; }
        Identifier ExpectedResult { get; }
    }

    class SimpleTypeContext : IFindingTypeTestContext
    {
        public string GivenSource => "IQueryHandler";
        public Identifier ExpectedResult =>
            "IQueryHandler".AsSimpleId();
    }

    class PredefinedTypeContext : IFindingTypeTestContext
    {
        public string GivenSource => "string";
        public Identifier ExpectedResult =>
            "string".AsSimpleId();
    }

    class GenericTypeWithSingleParameterContext : IFindingTypeTestContext
    {
        public string GivenSource => "IEnumerable<float>";
        public Identifier ExpectedResult =>
            "IEnumerable".AsGenericId("float");
    }

    class GenericTypeWithNestedParametersContext : IFindingTypeTestContext
    {
        public string GivenSource => "BaseService<int, ILogger<ApiController>, IFunctor<TIn, TOut>>";
        public Identifier ExpectedResult =>
            Identifier.Create("BaseService", new List<Identifier>
            {
                "int".AsSimpleId(),
                "ILogger".AsGenericId("ApiController"),
                "IFunctor".AsGenericId("TIn", "TOut")
            });
    }

    class NullableTypeContext : IFindingTypeTestContext
    {
        public string GivenSource => "string?";
        public Identifier ExpectedResult => "string".AsSimpleId();
    }

    class OneDimensionalArrayTypeContext : IFindingTypeTestContext
    {
        public string GivenSource => "int[]";
        public Identifier ExpectedResult => "int".AsSimpleId();
    }

    class ThreeDimensionalArrayTypeContext : IFindingTypeTestContext
    {
        public string GivenSource => "double[][][]";
        public Identifier ExpectedResult => "double".AsSimpleId();
    }
}
