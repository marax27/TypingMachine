using System;

namespace TypingMachine.Domain
{
    public class FieldEntity : BaseTypeMember
    {
        public string Name { get; }
        public Identifier Type { get; }

        private FieldEntity(string name, Identifier type, AccessModifier accessModifier)
            : base(accessModifier)
        {
            Name = name;
            Type = type;
        }

        public static FieldEntity Create(string name, Identifier type, AccessModifier accessModifier)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("String is empty or whitespace-only.", nameof(name));
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return new FieldEntity(name, type, accessModifier);
        }
    }
}
