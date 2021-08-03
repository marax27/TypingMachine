using System.Collections.Generic;
using TypingMachine.Entities;

namespace TypingMachine.Tests.CodeWalking.UsingWalkerTests
{
    public interface IDiscoveringUsingEntitiesTestContext
    {
        string GivenSource { get; }
        IReadOnlyCollection<UsingEntity> ExpectedResult { get; }
    }

    class ZeroUsingDirectivesContext : IDiscoveringUsingEntitiesTestContext
    {
        public string GivenSource => @"
namespace Application.Controllers
{
    class HelloController {}
}
";

        public IReadOnlyCollection<UsingEntity> ExpectedResult
            => new List<UsingEntity>();
    }

    class OneUsingDirectiveContext : IDiscoveringUsingEntitiesTestContext
    {
        public string GivenSource => @"
using System.Data;

namespace Application.Controllers
{
    class HelloController {}
}
";

        public IReadOnlyCollection<UsingEntity> ExpectedResult
            => new List<UsingEntity>
            {
                UsingEntity.Create(NamespaceEntity.Create(new []{"System", "Data"}))
            };
    }

    class ThreeUsingDirectivesContext : IDiscoveringUsingEntitiesTestContext
    {
        public string GivenSource => @"
using System;
using System.Collections.Generic;

using Application.Utilities;

namespace Application.Controllers
{
    class HelloController {}
}
";

        public IReadOnlyCollection<UsingEntity> ExpectedResult
            => new List<UsingEntity>
            {
                UsingEntity.Create(NamespaceEntity.Create(new []{"System"})),
                UsingEntity.Create(NamespaceEntity.Create(new []{"System", "Collections", "Generic"})),
                UsingEntity.Create(NamespaceEntity.Create(new []{"Application", "Utilities"}))
            };
    }
}
