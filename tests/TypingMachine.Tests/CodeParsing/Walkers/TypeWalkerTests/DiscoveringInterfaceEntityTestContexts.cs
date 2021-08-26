using System.Collections.Generic;
using TypingMachine.Domain;
using TypingMachine.Domain.Builders;
using TypingMachine.Tests.Utilities;

namespace TypingMachine.Tests.CodeParsing.Walkers.TypeWalkerTests
{
    public interface IDiscoveringInterfaceEntityTestContext
    {
        string GivenSource { get; }

        IReadOnlyCollection<TypeEntity> ExpectedResult { get; }
    }

    class EmptyInterfaceContext : IDiscoveringInterfaceEntityTestContext
    {
        public string GivenSource => @"protected interface IHelloService {}
";

        public IReadOnlyCollection<TypeEntity> ExpectedResult
            => new List<TypeEntity>
            {
                new InterfaceBuilder()
                    .WithAccess(AccessModifier.Protected)
                    .Build("IHelloService".AsSimpleTypeId())
            };
    }

    class GenericInterfaceContext : IDiscoveringInterfaceEntityTestContext
    {
        public string GivenSource => @"
protected interface IFunctor<TIn, TOut>
{
    TOut Compute(TIn arg);
}
";

        public IReadOnlyCollection<TypeEntity> ExpectedResult => GetExpectedResult();

        private IReadOnlyCollection<TypeEntity> GetExpectedResult()
        {
            var inParameter = "TIn".AsSimpleTypeId();
            var outParameter = "TOut".AsSimpleTypeId();

            return new List<TypeEntity>
            {
                new InterfaceBuilder()
                    .WithMethods(
                        new List<MethodEntity>
                        {
                            new MethodBuilder()
                                .WithArgumentTypes(new []{inParameter})
                                .Build("Compute", outParameter)
                        }
                    )
                    .WithAccess(AccessModifier.Protected)
                    .Build("IFunctor".AsGenericTypeId("TIn", "TOut"))
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
                new InterfaceBuilder()
                    .WithBaseTypes(
                        new List<Identifier>
                        {
                            "IHandler".AsGenericTypeId("TQuery", "TResult")
                        }
                    )
                    .WithAccess(AccessModifier.Internal)
                    .Build("IQueryHandler".AsGenericTypeId("TQuery", "TResult"))
            };
        }
    }

    class InterfaceInNamespaceContext : IDiscoveringInterfaceEntityTestContext
    {
        public string GivenSource => @"
namespace Business.Domain.Abstractions
{
    interface IQuery<TIn> {}
}
";

        public IReadOnlyCollection<TypeEntity> ExpectedResult => new List<TypeEntity>
        {
            new InterfaceBuilder()
                .WithNamespace("Business.Domain.Abstractions".AsNamespace())
                .WithAccess(AccessModifier.Internal)
                .Build("IQuery".AsGenericTypeId("TIn"))
        };
    }

    class InterfaceWithSeveralUsingDirectivesContext : IDiscoveringInterfaceEntityTestContext
    {
        public string GivenSource => @"
using Something.Local;

public interface IService {}
";

        public IReadOnlyCollection<TypeEntity> ExpectedResult => new List<InterfaceEntity>
        {
            new InterfaceBuilder()
                .WithUsingDirectives(
                    new List<UsingEntity>
                    {
                        UsingEntity.Create("Something.Local".AsNamespace()),
                    }
                )
                .WithAccess(AccessModifier.Public)
                .Build("IService".AsSimpleTypeId())
        };
    }
}
