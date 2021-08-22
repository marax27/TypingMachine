using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TypingMachine.Domain;

namespace TypingMachine.Tests.CodeFinding.AccessModifierFinderTests
{
    public interface IFindingAccessModifierTestContexts
    {
        MemberDeclarationSyntax GivenNode { get; }
        AccessModifier GivenDefault { get; }
        AccessModifier ExpectedResult { get; }
    }

    abstract class BaseFindingAccessModifierTestContexts : IFindingAccessModifierTestContexts
    {
        public MemberDeclarationSyntax GivenNode
            => CSharpSyntaxTree.ParseText(GivenSource)
                .GetRoot()
                .DescendantNodes()
                .OfType<ClassDeclarationSyntax>()
                .Single();

        public abstract AccessModifier GivenDefault { get; }
        public abstract AccessModifier ExpectedResult { get; }

        protected abstract string GivenSource { get; }
    }

    class SimplePublicClassContext : BaseFindingAccessModifierTestContexts
    {
        public override AccessModifier GivenDefault => AccessModifier.Private;
        public override AccessModifier ExpectedResult => AccessModifier.Public;
        protected override string GivenSource => @"
public class SampleClass
{
    private Logger _logger;
}";
    }

    class ExplicitInternalClassContext : BaseFindingAccessModifierTestContexts
    {
        public override AccessModifier GivenDefault => AccessModifier.Private;
        public override AccessModifier ExpectedResult => AccessModifier.Internal;
        protected override string GivenSource => "internal class SampleClass { }";
    }

    class NoAccessModifierSpecifiedPrivateDefaultContext : BaseFindingAccessModifierTestContexts
    {
        public override AccessModifier GivenDefault => AccessModifier.Private;
        public override AccessModifier ExpectedResult => AccessModifier.Private;
        protected override string GivenSource => "class SampleClass { }";
    }

    class NoAccessModifierSpecifiedInternalDefaultContext : BaseFindingAccessModifierTestContexts
    {
        public override AccessModifier GivenDefault => AccessModifier.Internal;
        public override AccessModifier ExpectedResult => AccessModifier.Internal;
        protected override string GivenSource => "class SampleClass { }";
    }

    class ProtectedClassContext : BaseFindingAccessModifierTestContexts
    {
        public override AccessModifier GivenDefault => AccessModifier.Private;
        public override AccessModifier ExpectedResult => AccessModifier.Protected;
        protected override string GivenSource => "protected class SampleClass { }";
    }

    class PrivateClassContext : BaseFindingAccessModifierTestContexts
    {
        public override AccessModifier GivenDefault => AccessModifier.Internal;
        public override AccessModifier ExpectedResult => AccessModifier.Private;
        protected override string GivenSource => "private class GenericClass<T1, T2> { }";
    }

    class PrivateProtectedClassContext : BaseFindingAccessModifierTestContexts
    {
        public override AccessModifier GivenDefault => AccessModifier.Internal;
        public override AccessModifier ExpectedResult => AccessModifier.PrivateProtected;
        protected override string GivenSource => "private protected class Service { }";
    }

    class ProtectedInternalClassContext : BaseFindingAccessModifierTestContexts
    {
        public override AccessModifier GivenDefault => AccessModifier.Internal;
        public override AccessModifier ExpectedResult => AccessModifier.ProtectedInternal;
        protected override string GivenSource => "protected internal class Service { }";
    }
}
