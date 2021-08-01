using System;
using System.Collections.Generic;
using System.Linq;

namespace TypingMachine.Entities
{
    public class NamespaceEntity : IEquatable<NamespaceEntity>
    {
        public ICollection<string> Sections { get; }

        public string GetFullName()
        {
            var result = string.Join(".", Sections);
            return string.IsNullOrEmpty(result) ? "<root>" : result;
        }

        private NamespaceEntity(ICollection<string> sections)
        {
            Sections = sections;
        }

        public static NamespaceEntity Create(ICollection<string> sections)
        {
            if (sections == null)
                throw new ArgumentNullException(nameof(sections));
            if (sections.Count == 0)
                throw new ArgumentOutOfRangeException(nameof(sections), $"No sections provided. To represent root-level namespace, use {nameof(NoNamespace)}.");
            return new NamespaceEntity(sections);
        }

        public static NamespaceEntity NoNamespace
            => new(new List<string>());

        public bool Equals(NamespaceEntity other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Sections.SequenceEqual(other.Sections);
        }

        public override bool Equals(object obj) => Equals(obj as NamespaceEntity);
        public override int GetHashCode() => GetFullName().GetHashCode();
        public static bool operator ==(NamespaceEntity left, NamespaceEntity right) => Equals(left, right);
        public static bool operator !=(NamespaceEntity left, NamespaceEntity right) => !Equals(left, right);
    }
}
