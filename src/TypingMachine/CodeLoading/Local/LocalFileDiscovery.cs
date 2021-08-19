using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TypingMachine.CodeLoading.Local
{
    public class LocalFileDiscovery : IFileDiscovery
    {
        private readonly string _rootPath;

        public LocalFileDiscovery(string rootPath)
        {
            _rootPath = rootPath ?? throw new ArgumentNullException(nameof(rootPath));
        }

        public IReadOnlyCollection<IFile> DiscoverSourceFiles()
        {
            return Directory
                .EnumerateFiles(_rootPath, "*.cs", SearchOption.AllDirectories)
                .Select(path => new LocalFile(Path.GetRelativePath(_rootPath, path), _rootPath))
                .ToList();
        }
    }
}
