using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TypingMachine.Domain;
using TypingMachine.Domain.Builders;

namespace TypingMachine.CodeParsing.Finders
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

            var parameters = methodNode.TypeParameterList?.Parameters;

            var methodIdentifier = Identifier.Create(name, GetMethodIdentifierParameters(methodNode));

            return new MethodBuilder()
                .WithArgumentTypes(argumentTypes)
                .WithAccess(_accessModifierFinder.FindFor(methodNode, AccessModifier.Private))
                .Build(methodIdentifier, returnType);
        }

        private IReadOnlyList<Identifier> GetMethodIdentifierParameters(MethodDeclarationSyntax methodNode)
        {
            IEnumerable<TypeParameterSyntax> parameters = methodNode.TypeParameterList?.Parameters.AsEnumerable()
                                                          ?? new List<TypeParameterSyntax>();

            return parameters
                .Select(parameter => parameter.Identifier.ValueText)
                .Select(text => Identifier.Create(text, new List<Identifier>()))
                .ToList();
        }
    }
}
