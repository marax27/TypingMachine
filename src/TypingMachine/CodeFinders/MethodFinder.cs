using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TypingMachine.Entities;

namespace TypingMachine.CodeFinders
{
    public class MethodFinder
    {
        public MethodEntity FindFor(MethodDeclarationSyntax methodNode)
        {
            var typeFinder = new TypeFinder();

            var name = methodNode.Identifier.ValueText;
            var returnType = typeFinder.FindFor(methodNode.ReturnType);
            var argumentTypes = methodNode.ParameterList.Parameters
                .Select(parameter => typeFinder.FindFor(parameter.Type))
                .ToList();

            return MethodEntity.Create(name, returnType, argumentTypes);
        }
    }
}
