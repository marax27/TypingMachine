using System.Collections.Generic;
using TypingMachine.Entities;

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
            TypeIdentifier.Create("IQueryHandler", new List<TypeIdentifier>());
    }

    class PredefinedTypeContext : IFindingTypeTestContext
    {
        public string GivenSource => "string";
        public TypeIdentifier ExpectedResult =>
            TypeIdentifier.Create("string", new List<TypeIdentifier>());
    }

    class GenericTypeWithSingleParameterContext : IFindingTypeTestContext
    {
        public string GivenSource => "IEnumerable<float>";
        public TypeIdentifier ExpectedResult =>
            TypeIdentifier.Create("IEnumerable", new List<TypeIdentifier>
            {
                TypeIdentifier.Create("float", new List<TypeIdentifier>())
            });
    }

    class GenericTypeWithNestedParametersContext : IFindingTypeTestContext
    {
        public string GivenSource => "BaseService<int, ILogger<ApiController>, IFunctor<TIn, TOut>>";
        public TypeIdentifier ExpectedResult =>
            TypeIdentifier.Create("BaseService", new List<TypeIdentifier>
            {
                TypeIdentifier.Create("int", new List<TypeIdentifier>()),
                TypeIdentifier.Create("ILogger", new List<TypeIdentifier>
                {
                    TypeIdentifier.Create("ApiController", new List<TypeIdentifier>())
                }),
                TypeIdentifier.Create("IFunctor", new List<TypeIdentifier>
                {
                    TypeIdentifier.Create("TIn", new List<TypeIdentifier>()),
                    TypeIdentifier.Create("TOut", new List<TypeIdentifier>()),
                })
            });
    }
}
