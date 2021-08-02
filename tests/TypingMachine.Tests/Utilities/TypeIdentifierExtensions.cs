using System.Collections.Generic;
using System.Linq;
using TypingMachine.Entities;

namespace TypingMachine.Tests.Utilities
{
    public static class TypeIdentifierExtensions
    {
        public static TypeIdentifier AsSimpleTypeId(this string typeName)
        {
            return TypeIdentifier.Create(typeName, new List<TypeIdentifier>());
        }

        public static TypeIdentifier AsGenericTypeId(this string typeName, params string[] parameters)
        {
            var parameterEntities = parameters.Select(p => p.AsSimpleTypeId());
            return TypeIdentifier.Create(typeName, parameterEntities.ToList());
        }
    }
}
