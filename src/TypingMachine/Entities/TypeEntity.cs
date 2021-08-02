﻿using System;
using System.Collections.Generic;
using TypingMachine.Abstractions;

namespace TypingMachine.Entities
{
    public abstract class TypeEntity
    {
        public TypeIdentifier Identifier { get; }
        public IReadOnlyList<MethodEntity> Methods { get; }
        public IReadOnlyList<TypeIdentifier> BaseTypes { get; }

        public abstract void Accept(ITypeVisitor visitor);

        protected TypeEntity(TypeIdentifier identifier, IReadOnlyList<MethodEntity> methods, IReadOnlyList<TypeIdentifier> baseTypes)
        {
            Identifier = identifier ?? throw new ArgumentNullException(nameof(identifier));
            Methods = methods ?? throw new ArgumentNullException(nameof(methods));
            BaseTypes = baseTypes ?? throw new ArgumentNullException(nameof(baseTypes));
        }
    }
}
