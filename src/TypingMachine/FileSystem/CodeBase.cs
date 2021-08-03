using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TypingMachine.Abstractions;
using TypingMachine.Entities;

namespace TypingMachine.FileSystem
{
    public class CodeBase : ICodeBase
    {
        private readonly string _rootPath;

        public CodeBase(string rootPath)
        {
            if (rootPath == null)
                throw new ArgumentNullException(nameof(rootPath));
            if (string.IsNullOrWhiteSpace(rootPath))
                throw new ArgumentException("String is null or whitespace-only.", nameof(rootPath));
            _rootPath = rootPath;
        }

        public async Task<IReadOnlyCollection<TypeEntity>> FindAllTypesAsync()
        {
            var tasks = FindSourceFiles()
                .Select(sourceFile => sourceFile.FindTypesAsync())
                .ToList();

            var partialResults = await Task.WhenAll(tasks);

            return partialResults
                .SelectMany(part => part)
                .ToList();
        }

        public IReadOnlyCollection<ICodeFile> FindSourceFiles()
            => GetAllFiles(_rootPath)
                .Where(filePath => filePath.EndsWith(".cs"))
                .Select(filePath => new CodeFile(filePath))
                .ToList();

        private IEnumerable<string> GetAllFiles(string directoryPath)
        {
            foreach (var filePath in Directory.GetFiles(directoryPath))
                yield return filePath;

            foreach (var subDirectoryPath in Directory.GetDirectories(directoryPath))
            {
                foreach (var filePath in GetAllFiles(subDirectoryPath))
                    yield return filePath;
            }
        }
    }
}
