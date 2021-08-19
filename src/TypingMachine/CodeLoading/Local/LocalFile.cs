using System.IO;

namespace TypingMachine.CodeLoading.Local
{
    public class LocalFile : IFile
    {
        private readonly string _rootPath;

        public string RelativePath { get; }

        public LocalFile(string relativePath, string rootPath)
        {
            RelativePath = relativePath;
            _rootPath = rootPath;
        }

        public string ReadSource()
        {
            var filePath = Path.Join(_rootPath, RelativePath);
            return File.ReadAllText(filePath);
        }
    }
}
