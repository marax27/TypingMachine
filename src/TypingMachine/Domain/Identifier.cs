using System;
using System.Collections.Generic;
using System.Linq;
using TypingMachine.Abstractions;

namespace TypingMachine.Domain
{
    public class Identifier : IIdentifier, IEquatable<Identifier>
    {
        public string Name { get; }
        public IReadOnlyList<Identifier> Parameters { get; }

        public int Arity => Parameters.Count;

        private Identifier(string name, IReadOnlyList<Identifier> parameters)
        {
            Name = name;
            Parameters = parameters;
        }

        public static Identifier Create(string name, IReadOnlyList<Identifier> parameters)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("String is empty or whitespace-only.", nameof(name));
            if (name.Contains("<") || name.Contains(">"))
                throw new ArgumentException("Type name contains generic parameters. They must be passed separately, as parameters.", nameof(name));
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            return new Identifier(name, parameters);
        }

        public string GetFullName()
        {
            var parameters = Parameters.Count > 0
                ? "<" + string.Join(", ", Parameters.Select(p => p.GetFullName())) + ">"
                : "";
            return Name + parameters;
        }

        public bool Equals(Identifier other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name
                   && Parameters.Count == other.Parameters.Count
                   && Parameters.SequenceEqual(other.Parameters);
        }

        public override bool Equals(object obj) => Equals(obj as Identifier);
        public override int GetHashCode() => GetFullName().GetHashCode();
        public static bool operator ==(Identifier left, Identifier right) => Equals(left, right);
        public static bool operator !=(Identifier left, Identifier right) => !Equals(left, right);
    }
}