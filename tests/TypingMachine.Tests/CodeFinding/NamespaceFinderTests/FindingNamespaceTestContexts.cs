using TypingMachine.Domain;

namespace TypingMachine.Tests.CodeFinding.NamespaceFinderTests
{
    public interface IFindingNamespaceTestContext
    {
        string GivenSource { get; }
        NamespaceIdentifier ExpectedResult { get; }
    }

    class SampleContext : IFindingNamespaceTestContext
    {
        public string GivenSource => @"
namespace Corporation.ProjectName.Gui
{
    public class SampleClass
    {
        private readonly int _secretValue;
        public string secondField;
    }
}
";

        public NamespaceIdentifier ExpectedResult => NamespaceIdentifier.Create(new[] { "Corporation", "ProjectName", "Gui" });
    }

    class TwoNamespacesSideBySideContext : IFindingNamespaceTestContext
    {
        public string GivenSource => @"
namespace ProjectNamespace
{
}

namespace OtherNamespace
{
    public class AnotherClass
    {
        private readonly int _secretValue;
        public string secondField;
    }
}
";

        public NamespaceIdentifier ExpectedResult => NamespaceIdentifier.Create(new[] { "OtherNamespace" });
    }

    class NodeOutsideNamespaceContext : IFindingNamespaceTestContext
    {
        public string GivenSource => @"
class RootClass
{
    private readonly int _secretValue;
    public string secondField;
}

namespace IrrelevantNamespace
{
}
";

        public NamespaceIdentifier ExpectedResult => NamespaceIdentifier.NoNamespace;
    }

    class SampleNestedContext : IFindingNamespaceTestContext
    {
        public string GivenSource => @"
namespace Corporation.ImportantProject
{
    namespace Example
    {
        namespace Abstractions
        {
            public interface IFirst {}
            public interface ISecond {}
        }


        public class Service
        {
            private IFirst _first;
            private ISecond _second;
        }
    }
}
";
        public NamespaceIdentifier ExpectedResult
            => NamespaceIdentifier.Create(new[] { "Corporation", "ImportantProject", "Example" });
    }
}
