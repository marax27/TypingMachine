using System.Collections.Generic;
using FluentAssertions;
using TypingMachine.Domain;
using TypingMachine.Domain.Builders;
using TypingMachine.Tests.Utilities;
using Xunit;

namespace TypingMachine.Tests.Domain.TypeEntities
{
    public class WhenFindingReferencedTypeInParentNamespace
    {
        [Fact]
        public void GivenReferencedTypeInParentNamespace_ReturnExpectedType()
        {
            var sut = new ClassBuilder()
                .WithNamespace(ChildNamespace)
                .Build("SampleEntityBuilder".AsSimpleId());
            var givenReferencedTypeId = "SampleEntity".AsSimpleId();
            var givenCandidates = new List<TypeEntity> {_sampleEntityType};

            var actualResult = sut.FindReferencedType(givenReferencedTypeId, givenCandidates);

            actualResult.Should().BeSameAs(_sampleEntityType);
        }

        private readonly ClassEntity _sampleEntityType = new ClassBuilder()
            .WithNamespace(ParentNamespace)
            .Build("SampleEntity".AsSimpleId());

        private static NamespaceIdentifier ParentNamespace => "Application.Domain".AsNamespace();
        private static NamespaceIdentifier ChildNamespace => "Application.Domain.Creation.EntityBuilders".AsNamespace();
    }
}
