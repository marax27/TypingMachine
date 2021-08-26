using System.Collections.Generic;
using System.Linq;
using TypingMachine.Domain;

namespace TypingMachine.Tests.Utilities
{
    internal static class TypeIdentifierExtensions
    {
        public static Identifier AsSimpleTypeId(this string typeName)
        {
            return Identifier.Create(typeName, new List<Identifier>());
        }

        public static Identifier AsGenericTypeId(this string typeName, params string[] parameters)
        {
            var parameterEntities = parameters.Select(p => p.AsSimpleTypeId());
            return Identifier.Create(typeName, parameterEntities.ToList());
        }
    }
}
