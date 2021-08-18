using System.Collections.Generic;
using TypingMachine.Builders;
using TypingMachine.Entities;
using TypingMachine.Tests.Utilities;

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
                new FieldBuilder().Build("_logger", "ILogger".AsGenericTypeId("HelloController"))
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
                new FieldBuilder().Build("_first", "int".AsSimpleTypeId()),
                new FieldBuilder().Build("second", "string".AsSimpleTypeId()),
                new FieldBuilder().Build("third", "IService".AsGenericTypeId("int", "int")),
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
                new FieldBuilder().Build("a", "int".AsSimpleTypeId()),
                new FieldBuilder().Build("b", "int".AsSimpleTypeId()),
                new FieldBuilder().Build("c", "int".AsSimpleTypeId()),
            };
    }
}
