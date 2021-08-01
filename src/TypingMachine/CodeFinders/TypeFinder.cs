using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TypingMachine.Entities;

namespace TypingMachine.CodeFinders
{
    public class TypeFinder
    {
        public TypeIdentifier FindFor(TypeSyntax typeNode)
        {
            if (typeNode is GenericNameSyntax node)
            {
                var name = node.Identifier.ValueText;
                var parameters = node.TypeArgumentList.Arguments.Select(FindFor);
                return TypeIdentifier.Create(name, parameters.ToList());
            }

            return TypeIdentifier.Create(typeNode.ToString(), new List<TypeIdentifier>());
        }
    }
}
