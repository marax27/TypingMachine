using System;
using System.Collections.Generic;
using FluentAssertions;
using TypingMachine.Builders;
using TypingMachine.Entities;
using TypingMachine.Tests.Utilities;
using Xunit;

namespace TypingMachine.Tests.Entities.TypeEntities
{
    public class WhenCreatingInterfaceEntity
    {
        [Fact]
        public void GivenValidData_CreateEntity()
        {
            Action act = () =>
            {
                var entity = new InterfaceBuilder()
                    .WithMethods(GivenMethods)
                    .WithBaseTypes(GivenBaseTypes)
                    .Build(GivenIdentifier);
            };

            act.Should().NotThrow();
        }

        [Fact]
        public void GivenNullIdentifier_ThrowExpectedException()
        {
            Action act = () =>
            {
                var entity = new InterfaceBuilder()
                    .WithMethods(GivenMethods)
                    .WithBaseTypes(GivenBaseTypes)
                    .Build(null);
            };

            act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("identifier");
        }

        [Fact]
        public void GivenNullMethods_ThrowExpectedException()
        {
            Action act = () =>
            {
                var entity = new InterfaceBuilder()
                    .WithMethods(null)
                    .WithBaseTypes(GivenBaseTypes)
                    .Build(GivenIdentifier);
            };

            act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("methods");
        }

        [Fact]
        public void GivenNullBaseTypes_ThrowExpectedException()
        {
            Action act = () =>
            {
                var entity = new InterfaceBuilder()
                    .WithMethods(GivenMethods)
                    .WithBaseTypes(null)
                    .Build(GivenIdentifier);
            };

            act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("baseTypes");
        }

        [Fact]
        public void GivenNullNamespace_ThrowExpectedException()
        {
            Action act = () =>
            {
                var entity = new InterfaceBuilder()
                    .WithMethods(GivenMethods)
                    .WithBaseTypes(GivenBaseTypes)
                    .WithNamespace(null)
                    .Build(GivenIdentifier);
            };

            act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("namespaceId");
        }

        private TypeIdentifier GivenIdentifier => "IQueryHandler".AsSimpleTypeId();

        private IReadOnlyList<MethodEntity> GivenMethods => new List<MethodEntity>
        {
            MethodEntity.Create(
                "Calculate",
                "int".AsSimpleTypeId(),
                new List<TypeIdentifier>() )
        };

        private IReadOnlyList<TypeIdentifier> GivenBaseTypes => new List<TypeIdentifier>
        {
            "IService".AsSimpleTypeId()
        };
    }
}
