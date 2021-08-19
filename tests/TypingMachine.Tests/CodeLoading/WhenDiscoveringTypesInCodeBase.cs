using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using TypingMachine.CodeLoading;
using Xunit;

namespace TypingMachine.Tests.CodeLoading
{
    public class WhenDiscoveringTypesInCodeBase
    {
        [Fact]
        public void GivenZeroFiles_ReturnZeroTypes()
        {
            var fileDiscovery = new Mock<IFileDiscovery>();
            fileDiscovery.Setup(fd => fd.DiscoverSourceFiles())
                .Returns(Array.Empty<IFile>());
            var sut = new CodeBase(fileDiscovery.Object);

            var resultTypes = sut.DiscoverTypes();

            resultTypes.Should().BeEmpty();
        }

        [Fact]
        public void GivenSampleFiles_ReturnExpectedNumberOfTypes()
        {
            var fileDiscovery = new Mock<IFileDiscovery>();
            fileDiscovery.Setup(fd => fd.DiscoverSourceFiles())
                .Returns(GivenSampleCodeFiles);
            var sut = new CodeBase(fileDiscovery.Object);

            var resultTypes = sut.DiscoverTypes();

            resultTypes.Should().HaveCount(4);
        }

        private IReadOnlyCollection<IFile> GivenSampleCodeFiles => new List<IFile>
        {
            new FileStub
            {
                RelativePath = "src/IService.cs",
                SourceCode = @"
namespace Project
{
    public interface IService {}
    public abstract class BaseService : IService {}
}
"
            },
            new FileStub
            {
                RelativePath = "src/Services.cs",
                SourceCode = @"
namespace Project
{
    class AService : BaseService {}
    class BService : BaseService {}
}
"
            }
        };
    }
}
