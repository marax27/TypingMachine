using System.Collections.Generic;
using TypingMachine.Domain;

namespace TypingMachine.CodeLoading
{
    public interface ICodeBase
    {
        IReadOnlyCollection<TypeEntity> DiscoverTypes();
    }
}
