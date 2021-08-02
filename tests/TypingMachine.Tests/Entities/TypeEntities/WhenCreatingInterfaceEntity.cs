﻿using System;
using System.Collections.Generic;
using FluentAssertions;
using TypingMachine.Entities;
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
                var entity = new InterfaceEntity(GivenIdentifier, GivenMethods, GivenBaseTypes);
            };

            act.Should().NotThrow();
        }

        [Fact]
        public void GivenNullIdentifier_ThrowExpectedException()
        {
            Action act = () =>
            {
                var entity = new InterfaceEntity(null, GivenMethods, GivenBaseTypes);
            };

            act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("identifier");
        }

        [Fact]
        public void GivenNullMethods_ThrowExpectedException()
        {
            Action act = () =>
            {
                var entity = new InterfaceEntity(GivenIdentifier, null, GivenBaseTypes);
            };

            act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("methods");
        }

        [Fact]
        public void GivenNullBaseTypes_ThrowExpectedException()
        {
            Action act = () =>
            {
                var entity = new InterfaceEntity(GivenIdentifier, GivenMethods, null);
            };

            act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("baseTypes");
        }

        private TypeIdentifier GivenIdentifier => TypeIdentifier.Create("IQueryHandler", new List<TypeIdentifier>());

        private IReadOnlyList<MethodEntity> GivenMethods => new List<MethodEntity>
        {
            MethodEntity.Create(
                "Calculate",
                TypeIdentifier.Create("int", new List<TypeIdentifier>()),
                new List<TypeIdentifier>() )
        };

        private IReadOnlyList<TypeIdentifier> GivenBaseTypes => new List<TypeIdentifier>
        {
            TypeIdentifier.Create("IService", new List<TypeIdentifier>())
        };
    }
}