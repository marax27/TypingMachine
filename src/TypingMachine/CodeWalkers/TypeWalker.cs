using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TypingMachine.CodeFinders;
using TypingMachine.Entities;

namespace TypingMachine.CodeWalkers
{
    public class TypeWalker : CSharpSyntaxWalker
    {
        private readonly TypeFinder _typeFinder = new();
        private readonly MethodFinder _methodFinder = new();
        private readonly FieldFinder _fieldFinder = new();


        private ICollection<TypeEntity> _types;

        public IReadOnlyCollection<TypeEntity> FindAll(SyntaxNode rootNode)
        {
            _types = new HashSet<TypeEntity>();
            Visit(rootNode);
            return _types.ToHashSet();
        }

        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            base.VisitClassDeclaration(node);

            _types.Add(
                new ClassEntity(
                    CreateIdentifier(node),
                    FindMethods(node),
                    FindBaseTypes(node),
                    FindFields(node)
                )
            );
        }

        public override void VisitInterfaceDeclaration(InterfaceDeclarationSyntax node)
        {
            base.VisitInterfaceDeclaration(node);

            _types.Add(
                new InterfaceEntity(
                    CreateIdentifier(node),
                    FindMethods(node),
                    FindBaseTypes(node)
                )
            );
        }

        private TypeIdentifier CreateIdentifier(TypeDeclarationSyntax node)
        {
            var typeName = node.Identifier.ValueText;
            var parameterTypes = node.TypeParameterList?.Parameters
                .Select(parameter => TypeIdentifier.Create(parameter.Identifier.ValueText, new List<TypeIdentifier>()))
                .ToList();
            return TypeIdentifier.Create(typeName, parameterTypes ?? new List<TypeIdentifier>());
        }

        private IReadOnlyList<MethodEntity> FindMethods(SyntaxNode node)
        {
            return node.ChildNodes()
                .OfType<MethodDeclarationSyntax>()
                .Select(methodNode => _methodFinder.FindFor(methodNode))
                .ToList();
        }

        private IReadOnlyList<TypeIdentifier> FindBaseTypes(BaseTypeDeclarationSyntax node)
        {
            return node.BaseList == null
                ? new List<TypeIdentifier>()
                : node.BaseList.Types
                    .Select(baseType => _typeFinder.FindFor(baseType.Type))
                    .ToList();
        }

        private IReadOnlyList<FieldEntity> FindFields(SyntaxNode node)
        {
            var fieldNodes = node.ChildNodes().OfType<FieldDeclarationSyntax>();
            return _fieldFinder
                .FindFor(fieldNodes)
                .ToList();
        }
    }
}
