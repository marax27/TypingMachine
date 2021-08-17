using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using TypingMachine.CodeLoading;
using Xunit;

namespace TypingMachine.Tests.CodeLoading
{
    public class WhenDiscoveringTypesInCodeBase
    {
        [Fact]
        public async Task GivenZeroFiles_ReturnZeroTypes()
        {
            var sut = new HardCodedCodeBase(new List<IFile>());

            var resultTypes = await sut.DiscoverTypesAsync();

            resultTypes.Should().BeEmpty();
        }

        [Fact]
        public async Task GivenSampleFiles_ReturnExpectedNumberOfTypes()
        {
            var sut = new HardCodedCodeBase(GivenSampleCodeFiles);

            var resultTypes = await sut.DiscoverTypesAsync();

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
