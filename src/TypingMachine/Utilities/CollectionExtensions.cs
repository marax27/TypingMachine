using System.Collections.Generic;
using System.Linq;

namespace TypingMachine.Utilities
{
    public static class CollectionExtensions
    {
        public static IEnumerable<T> ConcatSingle<T>(this IEnumerable<T> source, T newItem)
        {
            return source.Concat(new[] {newItem});
        }
    }
}
