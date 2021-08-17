using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TypingMachine.CodeLoading.FileSystem
{
    public class FileSystemCodeBase : AbstractCodeBase
    {
        private readonly string _rootPath;

        public FileSystemCodeBase(string rootPath)
        {
            _rootPath = rootPath ?? throw new ArgumentNullException(nameof(rootPath));
        }

        protected override Task<IReadOnlyCollection<IFile>> DiscoverCodeFilesAsync()
        {
            return Task.FromResult(GetSourceFiles());
        }

        private IReadOnlyCollection<IFile> GetSourceFiles()
        {
            return Directory
                .EnumerateFiles(_rootPath, "*.cs", SearchOption.AllDirectories)
                .Select(path => new FileSystemFile(Path.GetRelativePath(_rootPath, path), _rootPath))
                .ToList();
        }
    }
}
