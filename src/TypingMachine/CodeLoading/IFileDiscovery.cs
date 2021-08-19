using System.Collections.Generic;

namespace TypingMachine.CodeLoading
{
    public interface IFileDiscovery
    {
        IReadOnlyCollection<IFile> DiscoverSourceFiles();
    }
}
