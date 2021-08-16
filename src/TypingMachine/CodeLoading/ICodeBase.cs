using System.Collections.Generic;
using System.Threading.Tasks;
using TypingMachine.Entities;

namespace TypingMachine.CodeLoading
{
    public interface ICodeBase
    {
        Task<IReadOnlyCollection<TypeEntity>> DiscoverTypesAsync();
        IReadOnlyCollection<TypeEntity> DiscoverTypes() => DiscoverTypesAsync().Result;
    }
}
