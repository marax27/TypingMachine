using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TypingMachine.Domain;

namespace TypingMachine.CodeParsing.Finders
{
    public class TypeFinder
    {
        public TypeIdentifier FindFor(TypeSyntax typeNode)
        {
            switch (typeNode)
            { 
                case GenericNameSyntax genericNode:
                {
                    var name = genericNode.Identifier.ValueText;
                    var parameters = genericNode.TypeArgumentList.Arguments.Select(FindFor);
                    return TypeIdentifier.Create(name, parameters.ToList());
                }
                case NullableTypeSyntax nullableNode:
                {
                    return FindFor(nullableNode.ElementType);
                }
                case ArrayTypeSyntax arrayNode:
                {
                    return FindFor(arrayNode.ElementType);
                }
                default:
                {
                    return TypeIdentifier.Create(typeNode.ToString(), new List<TypeIdentifier>());
                }
            }
        }
    }
}
