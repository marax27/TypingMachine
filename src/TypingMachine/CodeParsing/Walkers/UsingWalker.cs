using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TypingMachine.Domain;

namespace TypingMachine.CodeParsing.Walkers
{
    /// <summary>
    /// Finds all `using` directives in provided syntax tree.
    /// </summary>
    public class UsingWalker : CSharpSyntaxWalker
    {
        private ICollection<UsingEntity> _usingEntities;

        public IReadOnlyCollection<UsingEntity> FindAll(SyntaxNode rootNode)
        {
            _usingEntities = new List<UsingEntity>();
            Visit(rootNode);
            return _usingEntities.ToList();
        }

        public override void VisitUsingDirective(UsingDirectiveSyntax node)
        {
            base.VisitUsingDirective(node);

            var ns = NamespaceIdentifier.Create(node.Name.ToString().Split('.'));
            _usingEntities.Add(UsingEntity.Create(ns));
        }
    }
}
