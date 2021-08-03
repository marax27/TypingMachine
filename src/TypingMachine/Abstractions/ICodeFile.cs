using System.Collections.Generic;
using System.Threading.Tasks;
using TypingMachine.Entities;

namespace TypingMachine.Abstractions
{
    public interface ICodeFile
    {
        Task<IReadOnlyCollection<TypeEntity>> FindTypesAsync();
    }
}
