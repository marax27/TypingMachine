using System.IO;
using System.Threading.Tasks;

namespace TypingMachine.CodeLoading.FileSystem
{
    public class FileSystemFile : IFile
    {
        private readonly string _rootPath;

        public string RelativePath { get; }

        public FileSystemFile(string relativePath, string rootPath)
        {
            RelativePath = relativePath;
            _rootPath = rootPath;
        }

        public Task<string> ReadSourceAsync()
        {
            var filePath = Path.Join(_rootPath, RelativePath);
            return File.ReadAllTextAsync(filePath);
        }
    }
}
