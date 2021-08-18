using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TypingMachine.Builders;
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
            private readonly AccessModifierFinder _accessModifierFinder = new();

            public override IEnumerable<FieldEntity>? VisitFieldDeclaration(FieldDeclarationSyntax node)
            {
                var declaration = node.Declaration;
                var fields = declaration.Variables
                    .Select(variable => ProcessVariable(variable, node))
                    .ToList();

                return fields;
            }

            private FieldEntity ProcessVariable(VariableDeclaratorSyntax variable, FieldDeclarationSyntax field)
            {
                var access = _accessModifierFinder.FindFor(field, AccessModifier.Private);
                var name = variable.Identifier.ValueText;
                var type = _typeFinder.FindFor(field.Declaration.Type);
                return new FieldBuilder()
                    .WithAccess(access)
                    .Build(name, type);
            }
        }
    }
}
