﻿using System.Collections.Generic;
using TypingMachine.Entities;

namespace TypingMachine.Builders
{
    public class InterfaceBuilder
    {
        private IReadOnlyList<MethodEntity> _methods = new List<MethodEntity>();
        private IReadOnlyList<TypeIdentifier> _baseTypes = new List<TypeIdentifier>();
        private NamespaceIdentifier _namespaceId = NamespaceIdentifier.NoNamespace;

        public InterfaceBuilder WithMethods(IReadOnlyList<MethodEntity> methods)
        {
            _methods = methods;
            return this;
        }

        public InterfaceBuilder WithBaseTypes(IReadOnlyList<TypeIdentifier> baseTypes)
        {
            _baseTypes = baseTypes;
            return this;
        }

        public InterfaceBuilder WithNamespace(NamespaceIdentifier namespaceId)
        {
            _namespaceId = namespaceId;
            return this;
        }

        public InterfaceEntity Build(TypeIdentifier typeIdentifier)
            => new(typeIdentifier, _namespaceId, _methods, _baseTypes);
    }
}