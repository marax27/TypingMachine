using System.Collections.Generic;
using TypingMachine.Entities;

namespace TypingMachine.Tests.CodeWalking.TypeWalkerTests
{
    public interface IDiscoveringInterfaceEntityTestContext
    {
        string GivenSource { get; }

        IReadOnlyCollection<TypeEntity> ExpectedResult { get; }
    }

    class EmptyInterfaceContext : IDiscoveringInterfaceEntityTestContext
    {
        public string GivenSource => @"
namespace Application.Services
{
    protected interface IHelloService {}
}
";

        public IReadOnlyCollection<TypeEntity> ExpectedResult
            => new List<TypeEntity>
            {
                new InterfaceEntity(
                    TypeIdentifier.Create("IHelloService", new List<TypeIdentifier>()),
                    new List<MethodEntity>(),
                    new List<TypeIdentifier>()
                )
            };
    }

    class GenericInterfaceContext : IDiscoveringInterfaceEntityTestContext
    {
        public string GivenSource => @"
namespace Application.Services
{
    protected interface IFunctor<TIn, TOut>
    {
        TOut Compute(TIn arg);
    }
}
";

        public IReadOnlyCollection<TypeEntity> ExpectedResult => GetExpectedResult();

        private IReadOnlyCollection<TypeEntity> GetExpectedResult()
        {
            var inParameter = TypeIdentifier.Create("TIn", new List<TypeIdentifier>());
            var outParameter = TypeIdentifier.Create("TOut", new List<TypeIdentifier>());

            return new List<TypeEntity>
            {
                new InterfaceEntity(
                    TypeIdentifier.Create("IFunctor", new List<TypeIdentifier>
                    {
                        inParameter,
                        outParameter,
                    }),
                    new List<MethodEntity>
                    {
                        MethodEntity.Create(
                            "Compute",
                            outParameter,
                            new List<TypeIdentifier> {inParameter}
                        )
                    },
                    new List<TypeIdentifier>()
                )
            };
        }
    }

    class InterfaceWith1BaseTypeContext : IDiscoveringInterfaceEntityTestContext
    {
        public string GivenSource => @"
interface IQueryHandler<TQuery, TResult> : IHandler<TQuery, TResult> {}
";

        public IReadOnlyCollection<TypeEntity> ExpectedResult => GetExpectedResult();

        private IReadOnlyCollection<TypeEntity> GetExpectedResult()
        {
            var queryParameter = TypeIdentifier.Create("TQuery", new List<TypeIdentifier>());
            var resultParameter = TypeIdentifier.Create("TResult", new List<TypeIdentifier>());

            return new List<TypeEntity>
            {
                new InterfaceEntity(
                    TypeIdentifier.Create("IQueryHandler", new List<TypeIdentifier> {queryParameter, resultParameter}),
                    new List<MethodEntity>(),
                    new List<TypeIdentifier>
                    {
                        TypeIdentifier.Create("IHandler", new List<TypeIdentifier> {queryParameter, resultParameter})
                    }
                )
            };
        }
    }
}
