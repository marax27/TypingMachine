using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using TypingMachine.CodeWalkers;
using TypingMachine.Entities;

namespace TypingMachine.CodeLoading
{
    public class CodeBase : ICodeBase
    {
        private readonly IFileDiscovery _fileDiscovery;

        public CodeBase(IFileDiscovery fileDiscovery)
        {
            _fileDiscovery = fileDiscovery;
        }

        public IReadOnlyCollection<TypeEntity> DiscoverTypes()
        {
            var allFiles = _fileDiscovery.DiscoverSourceFiles();
            var allTypes = allFiles.Select(ProcessCodeFile);
            return allTypes.SelectMany(types => types).ToList();
        }

        private IReadOnlyCollection<TypeEntity> ProcessCodeFile(IFile file)
        {
            var sourceCode = file.ReadSource();
            var root = CSharpSyntaxTree.ParseText(sourceCode).GetRoot();
            return new TypeWalker().FindAll(root);
        }
    }
}
