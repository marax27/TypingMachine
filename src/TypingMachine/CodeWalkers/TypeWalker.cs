using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TypingMachine.Builders;
using TypingMachine.CodeFinders;
using TypingMachine.Entities;

namespace TypingMachine.CodeWalkers
{
    /// <summary>
    /// Walks over a syntax tree and returns all found types.
    /// The class is not thread-safe - it should not be shared between threads.
    /// </summary>
    public class TypeWalker : CSharpSyntaxWalker
    {
        private readonly TypeFinder _typeFinder = new();
        private readonly MethodFinder _methodFinder = new();
        private readonly FieldFinder _fieldFinder = new();
        private readonly NamespaceFinder _namespaceFinder = new();
        private readonly UsingWalker _usingWalker = new();
        private readonly AccessModifierFinder _accessModifierFinder = new();

        private ICollection<TypeEntity> _types;
        private IReadOnlyCollection<UsingEntity> _usingDirectives;

        public IReadOnlyCollection<TypeEntity> FindAll(SyntaxNode rootNode)
        {
            _types = new HashSet<TypeEntity>();
            _usingDirectives = _usingWalker.FindAll(rootNode);
            Visit(rootNode);
            return _types.ToHashSet();
        }

        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            base.VisitClassDeclaration(node);

            var newClass = new ClassBuilder()
                .WithMethods(FindMethods(node))
                .WithBaseTypes(FindBaseTypes(node))
                .WithFields(FindFields(node))
                .WithNamespace(FindNamespace(node))
                .WithAccess(FindAccess(node))
                .WithUsingDirectives(_usingDirectives)
                .Build(CreateIdentifier(node));

            _types.Add(newClass);
        }

        public override void VisitInterfaceDeclaration(InterfaceDeclarationSyntax node)
        {
            base.VisitInterfaceDeclaration(node);

            var newInterface = new InterfaceBuilder()
                .WithMethods(FindMethods(node))
                .WithBaseTypes(FindBaseTypes(node))
                .WithNamespace(FindNamespace(node))
                .WithAccess(FindAccess(node))
                .WithUsingDirectives(_usingDirectives)
                .Build(CreateIdentifier(node));

            _types.Add(newInterface);
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

        private NamespaceIdentifier FindNamespace(SyntaxNode node)
        {
            return _namespaceFinder.FindFor(node);
        }

        private AccessModifier FindAccess(MemberDeclarationSyntax node)
        {
            return _accessModifierFinder.FindFor(node, AccessModifier.Internal);
        }
    }
}
