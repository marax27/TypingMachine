using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TypingMachine.Builders;
using TypingMachine.Entities;

namespace TypingMachine.CodeFinders
{
    public class MethodFinder
    {
        private readonly AccessModifierFinder _accessModifierFinder = new();

        public MethodEntity FindFor(MethodDeclarationSyntax methodNode)
        {
            var typeFinder = new TypeFinder();

            var name = methodNode.Identifier.ValueText;
            var returnType = typeFinder.FindFor(methodNode.ReturnType);
            var argumentTypes = methodNode.ParameterList.Parameters
                .Select(parameter => typeFinder.FindFor(parameter.Type))
                .ToList();

            return new MethodBuilder()
                .WithArgumentTypes(argumentTypes)
                .WithAccess(_accessModifierFinder.FindFor(methodNode, AccessModifier.Private))
                .Build(name, returnType);
        }
    }
}
