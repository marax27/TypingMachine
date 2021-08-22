using System;
using System.Collections.Generic;
using System.Linq;
using TypingMachine.Abstractions;

namespace TypingMachine.Domain
{
    public class TypeIdentifier : ITypeIdentifier, IEquatable<TypeIdentifier>
    {
        public string Name { get; }
        public IReadOnlyList<TypeIdentifier> Parameters { get; }

        public int Arity => Parameters.Count;

        private TypeIdentifier(string name, IReadOnlyList<TypeIdentifier> parameters)
        {
            Name = name;
            Parameters = parameters;
        }

        public static TypeIdentifier Create(string name, IReadOnlyList<TypeIdentifier> parameters)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("String is empty or whitespace-only.", nameof(name));
            if (name.Contains("<") || name.Contains(">"))
                throw new ArgumentException("Type name contains generic parameters. They must be passed separately, as parameters.", nameof(name));
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            return new TypeIdentifier(name, parameters);
        }

        public string GetFullName()
        {
            var parameters = Parameters.Count > 0
                ? "<" + string.Join(", ", Parameters.Select(p => p.GetFullName())) + ">"
                : "";
            return Name + parameters;
        }

        public bool Equals(TypeIdentifier other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name
                   && Parameters.Count == other.Parameters.Count
                   && Parameters.SequenceEqual(other.Parameters);
        }

        public override bool Equals(object obj) => Equals(obj as TypeIdentifier);
        public override int GetHashCode() => GetFullName().GetHashCode();
        public static bool operator ==(TypeIdentifier left, TypeIdentifier right) => Equals(left, right);
        public static bool operator !=(TypeIdentifier left, TypeIdentifier right) => !Equals(left, right);
    }
}