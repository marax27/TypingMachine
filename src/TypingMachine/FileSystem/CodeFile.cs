using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp;
using TypingMachine.Abstractions;
using TypingMachine.CodeWalkers;
using TypingMachine.Entities;

namespace TypingMachine.FileSystem
{
    public class CodeFile : ICodeFile
    {
        private readonly string _path;

        public CodeFile(string path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("String is null or whitespace-only.", nameof(path));
            _path = path;
        }

        public async Task<IReadOnlyCollection<TypeEntity>> FindTypesAsync()
        {
            var typeWalker = new TypeWalker();

            var sourceCode = await File.ReadAllTextAsync(_path);
            var rootNode = await CSharpSyntaxTree.ParseText(sourceCode).GetRootAsync();
            return typeWalker.FindAll(rootNode);
        }
    }
}
