using System.Collections.Generic;
using TypingMachine.Domain;
using TypingMachine.Domain.Builders;
using TypingMachine.Tests.Utilities;

namespace TypingMachine.Tests.CodeParsing.Finders.FieldFinderTests
{
    public interface IFindingFieldTestContext
    {
        string GivenSource { get; }
        IEnumerable<FieldEntity> ExpectedResult { get; }
    }

    class SampleFieldContext : IFindingFieldTestContext
    {
        public string GivenSource => @"
namespace Application.Controllers
{
    public class HelloController : BaseController
    {
        private ILogger<HelloController> _logger;

        public HelloController(int a, int b, ILogger<HelloController> logger) {
            _logger = logger;
        }
    }
}
";

        public IEnumerable<FieldEntity> ExpectedResult
            => new List<FieldEntity>
            {
                new FieldBuilder()
                    .WithAccess(AccessModifier.Private)
                    .Build("_logger", "ILogger".AsGenericId("HelloController"))
            };
    }

    class MultipleFieldsContext : IFindingFieldTestContext
    {
        public string GivenSource => @"
namespace Application.Controllers
{
    public class HelloController : BaseController
    {
        private int _first;
        protected readonly string second;
        public readonly IService<int, int> third = null;

        public HelloController() {
            _first = 123;
        }
    }
}
";

        public IEnumerable<FieldEntity> ExpectedResult
            => new List<FieldEntity>
            {
                new FieldBuilder()
                    .WithAccess(AccessModifier.Private)
                    .Build("_first", "int".AsSimpleId()),
                new FieldBuilder()
                    .WithAccess(AccessModifier.Protected)
                    .Build("second", "string".AsSimpleId()),
                new FieldBuilder()
                    .WithAccess(AccessModifier.Public)
                    .Build("third", "IService".AsGenericId("int", "int")),
            };
    }

    class MultipleFieldsInOneLineContext : IFindingFieldTestContext
    {
        public string GivenSource => @"
namespace Application.Controllers
{
    public class HelloController : BaseController
    {
        public int a, b, c;

        public HelloController() {
            a = 123;
            b = 40 - a;
            c = a * b - b;
        }
    }
}
";

        public IEnumerable<FieldEntity> ExpectedResult
            => new List<FieldEntity>
            {
                new FieldBuilder()
                    .WithAccess(AccessModifier.Public)
                    .Build("a", "int".AsSimpleId()),
                new FieldBuilder()
                    .WithAccess(AccessModifier.Public)
                    .Build("b", "int".AsSimpleId()),
                new FieldBuilder()
                    .WithAccess(AccessModifier.Public)
                    .Build("c", "int".AsSimpleId()),
            };
    }
}
