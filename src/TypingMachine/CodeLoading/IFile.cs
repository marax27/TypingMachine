namespace TypingMachine.CodeLoading
{
    public interface IFile
    {
        string RelativePath { get; }

        string ReadSource();
    }
}
