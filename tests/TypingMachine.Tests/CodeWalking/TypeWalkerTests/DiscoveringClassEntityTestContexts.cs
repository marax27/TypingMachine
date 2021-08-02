using System.Collections.Generic;
using TypingMachine.Entities;

namespace TypingMachine.Tests.CodeWalking.TypeWalkerTests
{
    public interface IDiscoveringClassEntityTestContext
    {
        string GivenSource { get; }
        IReadOnlyCollection<ClassEntity> ExpectedResult { get; }
    }

    class EmptyClassContext : IDiscoveringClassEntityTestContext
    {
        public string GivenSource => @"
namespace Controllers
{
    public class HelloController {}
}
";

        public IReadOnlyCollection<ClassEntity> ExpectedResult
            => new List<ClassEntity>
            {
                new ClassEntity(
                    TypeIdentifier.Create("HelloController", new List<TypeIdentifier>()), 
                    new List<MethodEntity>(),
                    new List<TypeIdentifier>(),
                    new List<FieldEntity>()
                )
            };
    }

    class ClassWithMultipleBaseTypesContext : IDiscoveringClassEntityTestContext
    {
        public string GivenSource => @"
namespace Controllers
{
    public class HelloController : IController, BaseController<Context>
    {
        public HelloController() {}
    }
}
";

        public IReadOnlyCollection<ClassEntity> ExpectedResult
            => new List<ClassEntity>
            {
                new ClassEntity(
                    TypeIdentifier.Create("HelloController", new List<TypeIdentifier>()),
                    new List<MethodEntity>(),
                    new List<TypeIdentifier>
                    {
                        TypeIdentifier.Create("IController", new List<TypeIdentifier>()),
                        TypeIdentifier.Create("BaseController", new List<TypeIdentifier>
                        {
                            TypeIdentifier.Create("Context", new List<TypeIdentifier>())
                        })
                    },
                    new List<FieldEntity>()
                )
            };
    }

    class ClassWithFieldsContext : IDiscoveringClassEntityTestContext
    {
        public string GivenSource => @"
namespace Controllers
{
    public class HelloController
    {
        public int magicValue = 123;
        private readonly ILogger<HelloController> _logger;

        public HelloController(ILogger<HelloController> logger)
        {
            _logger = logger;
        }
    }
}
";

        public IReadOnlyCollection<ClassEntity> ExpectedResult
            => new List<ClassEntity>
            {
                new ClassEntity(
                    TypeIdentifier.Create("HelloController", new List<TypeIdentifier>()),
                    new List<MethodEntity>(),
                    new List<TypeIdentifier>(),
                    new List<FieldEntity>
                    {
                        FieldEntity.Create(
                            "magicValue",
                            TypeIdentifier.Create("int", new List<TypeIdentifier>())
                        ),
                        FieldEntity.Create(
                            "_logger",
                            TypeIdentifier.Create("ILogger", new List<TypeIdentifier>
                            {
                                TypeIdentifier.Create("HelloController", new List<TypeIdentifier>())
                            })
                        )
                    }
                )
            };
    }
}
