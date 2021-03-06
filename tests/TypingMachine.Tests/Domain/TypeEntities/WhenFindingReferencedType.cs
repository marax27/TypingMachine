using FluentAssertions;
using System;
using System.Collections.Generic;
using TypingMachine.Domain;
using TypingMachine.Domain.Builders;
using TypingMachine.Tests.Utilities;
using Xunit;

namespace TypingMachine.Tests.Domain.TypeEntities
{
    public class WhenFindingReferencedType
    {
        [Fact]
        public void GivenEmptyCandidateList_ReturnNull()
        {
            var sut = new ClassBuilder()
                .WithNamespace(DomainNamespace)
                .WithUsingDirectives(new List<UsingEntity>())
                .Build("PlaceOrderCommandHandler".AsSimpleId());
            var givenReferencedTypeId = "PlaceOrderCommand".AsSimpleId();
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
                .Build("PlaceOrderCommandHandler".AsSimpleId());
            var givenReferencedTypeId = "PlaceOrderCommand".AsSimpleId();
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
                .Build("PlaceOrderCommandHandler".AsSimpleId());
            var givenReferencedTypeId = "HelperService".AsSimpleId();
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
                .Build("PlaceOrderCommandHandler".AsSimpleId());
            var givenReferencedTypeId = "HelperService".AsSimpleId();
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
        public void GivenReferencedGenericTypeId_ReturnExpectedType()
        {
            var sut = new ClassBuilder()
                .WithNamespace(DomainNamespace)
                .WithUsingDirectives(new List<UsingEntity>
                {
                    UsingEntity.Create(UtilitiesNamespace)
                })
                .Build("PlaceOrderCommandHandler".AsSimpleId());
            var givenReferencedTypeId = "IFunctor".AsGenericId("string", "int");
            var givenCandidates = new List<TypeEntity>
            {
                _helperServiceType,
                _placeOrderCommandType,
                _deleteOrderCommandType,
                _deleteOrderCommandHandlerType,
                _functorServiceType
            };

            var actualResult = sut.FindReferencedType(givenReferencedTypeId, givenCandidates);

            actualResult.Should().BeSameAs(_functorServiceType);
        }

        [Fact]
        public void GivenSelfReferenceAndRootTypeNotOneOfCandidates_ReturnNull()
        {
            var sut = new ClassBuilder()
                .WithNamespace(DomainNamespace)
                .Build("PlaceOrderCommandHandler".AsSimpleId());
            var givenReferencedTypeId = "PlaceOrderCommandHandler".AsSimpleId();
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
                .Build("PlaceOrderCommandHandler".AsSimpleId());
            var givenReferencedTypeId = "PlaceOrderCommandHandler".AsSimpleId();
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
            .Build("PlaceOrderCommand".AsSimpleId());

        private readonly ClassEntity _deleteOrderCommandType = new ClassBuilder()
            .WithNamespace(DomainNamespace)
            .Build("DeleteOrderCommand".AsSimpleId());

        private readonly ClassEntity _deleteOrderCommandHandlerType = new ClassBuilder()
            .WithNamespace(DomainNamespace)
            .Build("DeleteOrderCommandHandler".AsSimpleId());

        private readonly ClassEntity _helperServiceType = new ClassBuilder()
            .WithNamespace(UtilitiesNamespace)
            .Build("HelperService".AsSimpleId());

        private readonly InterfaceEntity _functorServiceType = new InterfaceBuilder()
            .WithNamespace(UtilitiesNamespace)
            .Build("IFunctor".AsGenericId("TIn", "TOut"));

        private static NamespaceIdentifier DomainNamespace => "Application.Domain".AsNamespace();
        private static NamespaceIdentifier UtilitiesNamespace => "Application.Utilities".AsNamespace();
    }
}
