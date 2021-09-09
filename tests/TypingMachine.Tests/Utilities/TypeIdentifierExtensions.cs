using System.Collections.Generic;
using System.Linq;
using TypingMachine.Domain;

namespace TypingMachine.Tests.Utilities
{
    internal static class TypeIdentifierExtensions
    {
        public static Identifier AsSimpleId(this string typeName)
        {
            return Identifier.Create(typeName, new List<Identifier>());
        }

        public static Identifier AsGenericId(this string typeName, params string[] parameters)
        {
            var parameterEntities = parameters.Select(p => p.AsSimpleId());
            return Identifier.Create(typeName, parameterEntities.ToList());
        }
    }
}
