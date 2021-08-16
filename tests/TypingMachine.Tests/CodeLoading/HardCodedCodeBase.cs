using System.Collections.Generic;
using System.Threading.Tasks;
using TypingMachine.CodeLoading;

namespace TypingMachine.Tests.CodeLoading
{
    internal class HardCodedCodeBase : AbstractCodeBase
    {
        private readonly IReadOnlyCollection<IFile> _files;

        public HardCodedCodeBase(IReadOnlyCollection<IFile> files)
            => _files = files;

        protected override Task<IReadOnlyCollection<IFile>> DiscoverCodeFilesAsync()
            => Task.FromResult(_files);
    }
}
