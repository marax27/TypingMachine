using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TypingMachine.Domain;

namespace TypingMachine.CodeParsing.Finders
{
    public class AccessModifierFinder
    {
        public AccessModifier FindFor(MemberDeclarationSyntax node, AccessModifier defaultModifier)
        {
            var modifiers = node.Modifiers
                .Select(modifier => modifier.ValueText)
                .ToArray();

            if (modifiers.Contains("private") && modifiers.Contains("protected"))
                return AccessModifier.PrivateProtected;
            else if (modifiers.Contains("protected") && modifiers.Contains("internal"))
                return AccessModifier.ProtectedInternal;
            else if(modifiers.Contains("internal"))
                return AccessModifier.Internal;
            else if (modifiers.Contains("public"))
                return AccessModifier.Public;
            else if (modifiers.Contains("protected"))
                return AccessModifier.Protected;
            else if (modifiers.Contains("private"))
                return AccessModifier.Private;
            else
                return defaultModifier;
        }
    }
}
