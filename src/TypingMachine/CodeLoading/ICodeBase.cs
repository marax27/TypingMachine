using System.Collections.Generic;
using TypingMachine.Entities;

namespace TypingMachine.CodeLoading
{
    public interface ICodeBase
    {
        IReadOnlyCollection<TypeEntity> DiscoverTypes();
    }
}
