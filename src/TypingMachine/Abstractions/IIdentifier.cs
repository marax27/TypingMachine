namespace TypingMachine.Abstractions
{
    public interface IIdentifier
    {
        string Name { get; }
        int Arity { get; }

        bool IsGeneric() => Arity > 0;
    }
}
