using TypingMachine.Entities;

namespace TypingMachine.Builders
{
    public class FieldBuilder
    {
        public FieldEntity Build(string name, TypeIdentifier type)
        {
            return FieldEntity.Create(name, type);
        }
    }
}
