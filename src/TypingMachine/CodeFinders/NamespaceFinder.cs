using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TypingMachine.Entities;

namespace TypingMachine.CodeFinders
{
    public class NamespaceFinder
    {
        public NamespaceEntity FindFor(SyntaxNode node)
        {
            var foundSections = new List<string>();

            var currentNode = node;
            while (true)
            {
                var namespaceNode = GetNamespaceNode(currentNode);
                if (namespaceNode == null)
                    break;

                var newSections = namespaceNode.Name.ToString().Split(".");
                foundSections.AddRange(newSections.Reverse());
                currentNode = namespaceNode;
            }

            foundSections.Reverse();
            return foundSections.Count > 0
                ? NamespaceEntity.Create(foundSections)
                : NamespaceEntity.NoNamespace;
        }

        private NamespaceDeclarationSyntax? GetNamespaceNode(SyntaxNode node)
        {
            var parent = node.Parent;
            while (parent is { } and not NamespaceDeclarationSyntax) parent = parent.Parent;

            return parent as NamespaceDeclarationSyntax;
        }
    }
}
