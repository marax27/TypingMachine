using System;
using System.Collections.Generic;
using FluentAssertions;
using TypingMachine.Builders;
using TypingMachine.Entities;
using TypingMachine.Tests.Utilities;
using Xunit;

namespace TypingMachine.Tests.Entities.TypeEntities
{
    public class WhenFindingReferencedType
    {
        [Fact]
        public void GivenEmptyCandidateList_ReturnNull()
        {
            var sut = new ClassBuilder()
                .WithNamespace(DomainNamespace)
                .WithUsingDirectives(new List<UsingEntity>())
                .Build("PlaceOrderCommandHandler".AsSimpleTypeId());
            var givenReferencedTypeId = "PlaceOrderCommand".AsSimpleTypeId();
            var givenCandidates = Array.Empty<TypeEntity>();

            var actualResult = sut.FindReferencedType(givenReferencedTypeId, givenCandidates);

            actualResult.Should().BeNull();
        }

        [Fact]
        public void GivenZeroUsingDirectivesAndMatchingTypeInTheSameNamespace_ReturnExpectedType()
        {
            var sut = new ClassBuilder()
                .WithNamespace(DomainNamespace)
                .WithUsingDirectives(new List<UsingEntity>())
                .Build("PlaceOrderCommandHandler".AsSimpleTypeId());
            var givenReferencedTypeId = "PlaceOrderCommand".AsSimpleTypeId();
            var givenCandidates = new List<TypeEntity>
            {
                _helperServiceType,
                _placeOrderCommandType,
                _deleteOrderCommandType,
                _deleteOrderCommandHandlerType
            };

            var actualResult = sut.FindReferencedType(givenReferencedTypeId, givenCandidates);

            actualResult.Should().BeSameAs(_placeOrderCommandType);
        }

        [Fact]
        public void GivenMatchingTypeInUnimportedNamespace_ReturnNull()
        {
            var sut = new ClassBuilder()
                .WithNamespace(DomainNamespace)
                .WithUsingDirectives(new List<UsingEntity>())
                .Build("PlaceOrderCommandHandler".AsSimpleTypeId());
            var givenReferencedTypeId = "HelperService".AsSimpleTypeId();
            var givenCandidates = new List<TypeEntity>
            {
                _helperServiceType,
                _placeOrderCommandType,
                _deleteOrderCommandType,
                _deleteOrderCommandHandlerType
            };

            var actualResult = sut.FindReferencedType(givenReferencedTypeId, givenCandidates);

            actualResult.Should().BeNull();
        }

        [Fact]
        public void GivenMatchingTypeInImportedNamespace_ReturnExpectedType()
        {
            var sut = new ClassBuilder()
                .WithNamespace(DomainNamespace)
                .WithUsingDirectives(new List<UsingEntity>
                {
                    UsingEntity.Create(UtilitiesNamespace)
                })
                .Build("PlaceOrderCommandHandler".AsSimpleTypeId());
            var givenReferencedTypeId = "HelperService".AsSimpleTypeId();
            var givenCandidates = new List<TypeEntity>
            {
                _helperServiceType,
                _placeOrderCommandType,
                _deleteOrderCommandType,
                _deleteOrderCommandHandlerType
            };

            var actualResult = sut.FindReferencedType(givenReferencedTypeId, givenCandidates);

            actualResult.Should().BeSameAs(_helperServiceType);
        }

        [Fact]
        public void GivenSelfReferenceAndRootTypeNotOneOfCandidates_ReturnNull()
        {
            var sut = new ClassBuilder()
                .WithNamespace(DomainNamespace)
                .Build("PlaceOrderCommandHandler".AsSimpleTypeId());
            var givenReferencedTypeId = "PlaceOrderCommandHandler".AsSimpleTypeId();
            var givenCandidates = new List<TypeEntity>
            {
                _helperServiceType,
                _placeOrderCommandType,
                _deleteOrderCommandType,
                _deleteOrderCommandHandlerType
            };

            var actualResult = sut.FindReferencedType(givenReferencedTypeId, givenCandidates);

            actualResult.Should().BeNull();
        }

        [Fact]
        public void GivenSelfReferenceAndRootTypeIsCandidate_ReturnRootType()
        {
            var sut = new ClassBuilder()
                .WithNamespace(DomainNamespace)
                .Build("PlaceOrderCommandHandler".AsSimpleTypeId());
            var givenReferencedTypeId = "PlaceOrderCommandHandler".AsSimpleTypeId();
            var givenCandidates = new List<TypeEntity>
            {
                _helperServiceType,
                _placeOrderCommandType,
                _deleteOrderCommandType,
                _deleteOrderCommandHandlerType,
                sut,
            };

            var actualResult = sut.FindReferencedType(givenReferencedTypeId, givenCandidates);

            actualResult.Should().BeSameAs(sut);
        }

        private readonly ClassEntity _placeOrderCommandType = new ClassBuilder()
            .WithNamespace(DomainNamespace)
            .Build("PlaceOrderCommand".AsSimpleTypeId());

        private readonly ClassEntity _deleteOrderCommandType = new ClassBuilder()
            .WithNamespace(DomainNamespace)
            .Build("DeleteOrderCommand".AsSimpleTypeId());

        private readonly ClassEntity _deleteOrderCommandHandlerType = new ClassBuilder()
            .WithNamespace(DomainNamespace)
            .Build("DeleteOrderCommandHandler".AsSimpleTypeId());

        private readonly ClassEntity _helperServiceType = new ClassBuilder()
            .WithNamespace(UtilitiesNamespace)
            .Build("HelperService".AsSimpleTypeId());

        private static NamespaceIdentifier DomainNamespace => "Application.Domain".AsNamespace();
        private static NamespaceIdentifier UtilitiesNamespace => "Application.Utilities".AsNamespace();
    }
}
