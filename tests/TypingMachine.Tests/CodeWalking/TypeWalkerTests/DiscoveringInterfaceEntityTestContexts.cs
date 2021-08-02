using System.Collections.Generic;
using TypingMachine.Entities;
using TypingMachine.Tests.Utilities;

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
                    "IHelloService".AsSimpleTypeId(),
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
            var inParameter = "TIn".AsSimpleTypeId();
            var outParameter = "TOut".AsSimpleTypeId();

            return new List<TypeEntity>
            {
                new InterfaceEntity(
                    "IFunctor".AsGenericTypeId("TIn", "TOut"),
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
            var queryParameter = "TQuery".AsSimpleTypeId();
            var resultParameter = "TResult".AsSimpleTypeId();

            return new List<TypeEntity>
            {
                new InterfaceEntity(
                    "IQueryHandler".AsGenericTypeId("TQuery", "TResult"),
                    new List<MethodEntity>(),
                    new List<TypeIdentifier>
                    {
                        "IHandler".AsGenericTypeId("TQuery", "TResult")
                    }
                )
            };
        }
    }
}
