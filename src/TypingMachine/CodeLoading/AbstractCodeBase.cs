using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp;
using TypingMachine.CodeWalkers;
using TypingMachine.Entities;

namespace TypingMachine.CodeLoading
{
    public abstract class AbstractCodeBase : ICodeBase
    {
        private readonly TypeWalker _typeWalker = new();

        public async Task<IReadOnlyCollection<TypeEntity>> DiscoverTypesAsync()
        {
            var allFiles = await DiscoverCodeFilesAsync();
            
            var tasks = allFiles
                .Select(ProcessCodeFile)
                .ToArray();
            await Task.WhenAll(tasks);

            return tasks.SelectMany(task => task.Result).ToList();
        }

        protected abstract Task<IReadOnlyCollection<IFile>> DiscoverCodeFilesAsync();

        private async Task<IReadOnlyCollection<TypeEntity>> ProcessCodeFile(IFile file)
        {
            var sourceCode = await file.ReadSourceAsync();
            var root = await CSharpSyntaxTree.ParseText(sourceCode).GetRootAsync();
            return _typeWalker.FindAll(root);
        }
    }
}
