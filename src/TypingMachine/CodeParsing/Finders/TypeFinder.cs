using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TypingMachine.Domain;

namespace TypingMachine.CodeParsing.Finders
{
    public class TypeFinder
    {
        public Identifier FindFor(TypeSyntax typeNode)
        {
            switch (typeNode)
            { 
                case GenericNameSyntax genericNode:
                {
                    var name = genericNode.Identifier.ValueText;
                    var parameters = genericNode.TypeArgumentList.Arguments.Select(FindFor);
                    return Identifier.Create(name, parameters.ToList());
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
                    return Identifier.Create(typeNode.ToString(), new List<Identifier>());
                }
            }
        }
    }
}
