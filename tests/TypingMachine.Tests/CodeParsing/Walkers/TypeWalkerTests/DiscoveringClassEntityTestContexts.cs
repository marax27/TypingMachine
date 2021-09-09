using System.Collections.Generic;
using TypingMachine.Domain;
using TypingMachine.Domain.Builders;
using TypingMachine.Tests.Utilities;

namespace TypingMachine.Tests.CodeParsing.Walkers.TypeWalkerTests
{
    public interface IDiscoveringClassEntityTestContext
    {
        string GivenSource { get; }
        IReadOnlyCollection<ClassEntity> ExpectedResult { get; }
    }

    class EmptyClassContext : IDiscoveringClassEntityTestContext
    {
        public string GivenSource => @"public class HelloController {}";

        public IReadOnlyCollection<ClassEntity> ExpectedResult
            => new List<ClassEntity>
            {
                new ClassBuilder()
                    .WithAccess(AccessModifier.Public)
                    .Build("HelloController".AsSimpleId())
            };
    }

    class ClassWithMultipleBaseTypesContext : IDiscoveringClassEntityTestContext
    {
        public string GivenSource => @"
public class HelloController : IController, BaseController<Context>
{
    public HelloController() {}
}
";

        public IReadOnlyCollection<ClassEntity> ExpectedResult
            => new List<ClassEntity>
            {
                new ClassBuilder()
                    .WithBaseTypes(
                        new List<Identifier>
                        {
                            "IController".AsSimpleId(),
                            "BaseController".AsGenericId("Context")
                        }
                    )
                    .WithAccess(AccessModifier.Public)
                    .Build("HelloController".AsSimpleId())
            };
    }

    class ClassWithFieldsContext : IDiscoveringClassEntityTestContext
    {
        public string GivenSource => @"
public class HelloController
{
    public int magicValue = 123;
    private readonly ILogger<HelloController> _logger;
    public HelloController(ILogger<HelloController> logger)
    {
        _logger = logger;
    }
}
";

        public IReadOnlyCollection<ClassEntity> ExpectedResult
            => new List<ClassEntity>
            {
                new ClassBuilder()
                    .WithFields(
                        new List<FieldEntity>
                        {
                            new FieldBuilder()
                                .WithAccess(AccessModifier.Public)
                                .Build("magicValue", "int".AsSimpleId()),
                            new FieldBuilder()
                                .WithAccess(AccessModifier.Private)
                                .Build("_logger", "ILogger".AsGenericId("HelloController"))
                        }
                    )
                    .WithAccess(AccessModifier.Public)
                    .Build("HelloController".AsSimpleId())
            };
    }

    class ClassInNamespaceContext : IDiscoveringClassEntityTestContext
    {
        public string GivenSource => @"
namespace Business.Controllers
{
    public class HelloController {}
}
";

        public IReadOnlyCollection<ClassEntity> ExpectedResult => new List<ClassEntity>
        {
            new ClassBuilder()
                .WithNamespace("Business.Controllers".AsNamespace())
                .WithAccess(AccessModifier.Public)
                .Build("HelloController".AsSimpleId())
        };
    }

    class ClassWithSeveralUsingDirectivesContext : IDiscoveringClassEntityTestContext
    {
        public string GivenSource => @"
using System;
using System.Collections.Generic;
using Project.Core;

class HelloController {}
";

        public IReadOnlyCollection<ClassEntity> ExpectedResult => new List<ClassEntity>
        {
            new ClassBuilder()
                .WithUsingDirectives(
                    new List<UsingEntity>
                    {
                        UsingEntity.Create("System".AsNamespace()),
                        UsingEntity.Create("System.Collections.Generic".AsNamespace()),
                        UsingEntity.Create("Project.Core".AsNamespace()),
                    }
                )
                .WithAccess(AccessModifier.Internal)
                .Build("HelloController".AsSimpleId())
        };
    }
}
