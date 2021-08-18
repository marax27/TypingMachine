namespace TypingMachine.Entities
{
    /// <summary>
    /// Contains common properties of all type members (fields, methods, properties etc.)
    /// </summary>
    public abstract class BaseTypeMember
    {
        public AccessModifier AccessModifier { get; }

        protected BaseTypeMember(AccessModifier accessModifier)
        {
            AccessModifier = accessModifier;
        }
    }
}
