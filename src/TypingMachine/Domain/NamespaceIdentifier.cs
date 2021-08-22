using System;
using System.Collections.Generic;
using System.Linq;

namespace TypingMachine.Domain
{
    public class NamespaceIdentifier : IEquatable<NamespaceIdentifier>
    {
        public IReadOnlyList<string> Sections { get; }

        public string GetFullName()
        {
            var result = string.Join(".", Sections);
            return string.IsNullOrEmpty(result) ? "<root>" : result;
        }

        private NamespaceIdentifier(IReadOnlyList<string> sections)
        {
            Sections = sections;
        }

        public static NamespaceIdentifier Create(IReadOnlyList<string> sections)
        {
            if (sections == null)
                throw new ArgumentNullException(nameof(sections));
            if (sections.Count == 0)
                throw new ArgumentOutOfRangeException(nameof(sections), $"No sections provided. To represent root-level namespace, use {nameof(NoNamespace)}.");
            return new NamespaceIdentifier(sections);
        }

        public static NamespaceIdentifier NoNamespace
            => new(new List<string>());

        public bool Equals(NamespaceIdentifier other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Sections.SequenceEqual(other.Sections);
        }

        public override bool Equals(object obj) => Equals(obj as NamespaceIdentifier);
        public override int GetHashCode() => GetFullName().GetHashCode();
        public static bool operator ==(NamespaceIdentifier left, NamespaceIdentifier right) => Equals(left, right);
        public static bool operator !=(NamespaceIdentifier left, NamespaceIdentifier right) => !Equals(left, right);
    }
}
