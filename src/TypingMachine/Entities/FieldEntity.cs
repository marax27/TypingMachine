using System;

namespace TypingMachine.Entities
{
    public class FieldEntity
    {
        public string Name { get; }
        public TypeIdentifier Type { get; }

        private FieldEntity(string name, TypeIdentifier type)
        {
            Name = name;
            Type = type;
        }

        public static FieldEntity Create(string name, TypeIdentifier type)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("String is empty or whitespace-only.", nameof(name));
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return new FieldEntity(name, type);
        }
    }
}
