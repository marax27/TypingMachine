using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TypingMachine.Entities;

namespace TypingMachine.CodeFinders
{
    public class FieldFinder
    {
        public IEnumerable<FieldEntity> FindFor(IEnumerable<SyntaxNode> nodes)
        {
            var result = new List<FieldEntity>();
            var visitor = new FieldVisitor();
            foreach (var node in nodes)
            {
                var newFields = visitor.Visit(node);
                result.AddRange(newFields ?? Array.Empty<FieldEntity>());
            }
            return result;
        }

        private class FieldVisitor : CSharpSyntaxVisitor<IEnumerable<FieldEntity>>
        {
            private readonly TypeFinder _typeFinder = new();

            public override IEnumerable<FieldEntity>? VisitFieldDeclaration(FieldDeclarationSyntax node)
            {
                var declaration = node.Declaration;
                var fields = declaration.Variables
                    .Select(variable => FieldEntity.Create(
                            variable.Identifier.ValueText,
                            _typeFinder.FindFor(declaration.Type)
                        )
                    )
                    .ToList();

                return fields;
            }
        }
    }
}
