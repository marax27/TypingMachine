using System.Collections.Generic;
using System.Threading.Tasks;
using TypingMachine.Entities;

namespace TypingMachine.Abstractions
{
    public interface ICodeBase
    {
        Task<IReadOnlyCollection<TypeEntity>> FindAllTypesAsync();
    }
}
