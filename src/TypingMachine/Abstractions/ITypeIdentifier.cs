namespace TypingMachine.Abstractions
{
    public interface ITypeIdentifier
    {
        string Name { get; }
        int Arity { get; }
    }
}
