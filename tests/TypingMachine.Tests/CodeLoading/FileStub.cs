using TypingMachine.CodeLoading;

namespace TypingMachine.Tests.CodeLoading
{
    internal class FileStub : IFile
    {
        public string SourceCode { get; init; }
        public string RelativePath { get; init; }

        public string ReadSource() => SourceCode;
    }
}
