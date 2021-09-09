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
                    .Build("IHelloService".AsSimpleId())
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
            var inParameter = "TIn".AsSimpleId();
            var outParameter = "TOut".AsSimpleId();

            return new List<TypeEntity>
            {
                new InterfaceBuilder()
                    .WithMethods(
                        new List<MethodEntity>
                        {
                            new MethodBuilder()
                                .WithArgumentTypes(new []{inParameter})
                                .Build("Compute".AsSimpleId(), outParameter)
                        }
                    )
                    .WithAccess(AccessModifier.Protected)
                    .Build("IFunctor".AsGenericId("TIn", "TOut"))
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
            var queryParameter = "TQuery".AsSimpleId();
            var resultParameter = "TResult".AsSimpleId();

            return new List<TypeEntity>
            {
                new InterfaceBuilder()
                    .WithBaseTypes(
                        new List<Identifier>
                        {
                            "IHandler".AsGenericId("TQuery", "TResult")
                        }
                    )
                    .WithAccess(AccessModifier.Internal)
                    .Build("IQueryHandler".AsGenericId("TQuery", "TResult"))
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
                .Build("IQuery".AsGenericId("TIn"))
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
                .Build("IService".AsSimpleId())
        };
    }
}
