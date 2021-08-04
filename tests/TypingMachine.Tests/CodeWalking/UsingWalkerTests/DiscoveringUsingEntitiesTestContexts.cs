using System.Collections.Generic;
using TypingMachine.Entities;
using TypingMachine.Tests.Utilities;

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
                UsingEntity.Create("System.Data".AsNamespace())
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
                UsingEntity.Create("System".AsNamespace()),
                UsingEntity.Create("System.Collections.Generic".AsNamespace()),
                UsingEntity.Create("Application.Utilities".AsNamespace())
            };
    }

    class StaticImportContext : IDiscoveringUsingEntitiesTestContext
    {
        public string GivenSource => @"
using static System.Console;

namespace EmptyNamespace {}
";

        public IReadOnlyCollection<UsingEntity> ExpectedResult
            => new List<UsingEntity>
            {
                UsingEntity.Create("System.Console".AsNamespace()),
            };
    }

    class AliasDirectiveContext : IDiscoveringUsingEntitiesTestContext
    {
        public string GivenSource => "using Project = Organization.ProjectName.Project;";

        public IReadOnlyCollection<UsingEntity> ExpectedResult
            => new List<UsingEntity>
            {
                UsingEntity.Create("Organization.ProjectName.Project".AsNamespace()),
            };
    }
}
