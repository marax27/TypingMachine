using System.Threading.Tasks;
using TypingMachine.CodeLoading;

namespace TypingMachine.Tests.CodeLoading
{
    internal class FileStub : IFile
    {
        public string SourceCode { get; init; }
        public string RelativePath { get; init; }

        public Task<string> ReadSourceAsync() => Task.FromResult(SourceCode);
    }
}
