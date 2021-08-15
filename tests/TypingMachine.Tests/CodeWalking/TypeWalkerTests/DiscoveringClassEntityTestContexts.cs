using System.Collections.Generic;
using TypingMachine.Builders;
using TypingMachine.Entities;
using TypingMachine.Tests.Utilities;

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
                new ClassBuilder()
                    .Build("HelloController".AsSimpleTypeId())
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
                new ClassBuilder()
                    .WithBaseTypes(
                        new List<TypeIdentifier>
                        {
                            "IController".AsSimpleTypeId(),
                            "BaseController".AsGenericTypeId("Context")
                        }
                    )
                    .Build("HelloController".AsSimpleTypeId())
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
                new ClassBuilder()
                    .WithFields(
                        new List<FieldEntity>
                        {
                            FieldEntity.Create(
                                "magicValue",
                                "int".AsSimpleTypeId()
                            ),
                            FieldEntity.Create(
                                "_logger",
                                "ILogger".AsGenericTypeId("HelloController")
                            )
                        }
                    )
                    .Build("HelloController".AsSimpleTypeId())
            };
    }
}
