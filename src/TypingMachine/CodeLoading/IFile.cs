using System.Threading.Tasks;

namespace TypingMachine.CodeLoading
{
    public interface IFile
    {
        string RelativePath { get; }

        Task<string> ReadSourceAsync();
        string ReadSource() => ReadSourceAsync().Result;
    }
}
