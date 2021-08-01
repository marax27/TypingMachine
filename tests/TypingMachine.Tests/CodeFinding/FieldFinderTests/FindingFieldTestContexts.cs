using System.Collections.Generic;
using FluentAssertions;
using TypingMachine.Entities;

namespace TypingMachine.Tests.CodeFinding.FieldFinderTests
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
                FieldEntity.Create("_logger", TypeIdentifier.Create("ILogger", new List<TypeIdentifier>
                {
                    TypeIdentifier.Create("HelloController", new List<TypeIdentifier>())
                }))
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
                FieldEntity.Create("_first", TypeIdentifier.Create("int", new List<TypeIdentifier>())),
                FieldEntity.Create("second", TypeIdentifier.Create("string", new List<TypeIdentifier>())),
                FieldEntity.Create("third", TypeIdentifier.Create("IService", new List<TypeIdentifier>
                {
                    TypeIdentifier.Create("int", new List<TypeIdentifier>()),
                    TypeIdentifier.Create("int", new List<TypeIdentifier>()),
                })),
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
                FieldEntity.Create("a", TypeIdentifier.Create("int", new List<TypeIdentifier>())),
                FieldEntity.Create("b", TypeIdentifier.Create("int", new List<TypeIdentifier>())),
                FieldEntity.Create("c", TypeIdentifier.Create("int", new List<TypeIdentifier>())),
            };
    }
}
