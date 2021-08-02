using System;
using System.Collections.Generic;
using FluentAssertions;
using TypingMachine.Entities;
using Xunit;

namespace TypingMachine.Tests.Entities.TypeEntities
{
    public class WhenCreatingClassEntity
    {
        [Fact]
        public void GivenValidData_CreateEntity()
        {
            Action act = () =>
            {
                var entity = new ClassEntity(GivenIdentifier, GivenMethods, GivenBaseTypes, GivenFields);
            };

            act.Should().NotThrow();
        }

        [Fact]
        public void GivenNullIdentifier_ThrowExpectedException()
        {
            Action act = () =>
            {
                var entity = new ClassEntity(null, GivenMethods, GivenBaseTypes, GivenFields);
            };

            act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("identifier");
        }

        [Fact]
        public void GivenNullMethods_ThrowExpectedException()
        {
            Action act = () =>
            {
                var entity = new ClassEntity(GivenIdentifier, null, GivenBaseTypes, GivenFields);
            };

            act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("methods");
        }

        [Fact]
        public void GivenNullBaseTypes_ThrowExpectedException()
        {
            Action act = () =>
            {
                var entity = new ClassEntity(GivenIdentifier, GivenMethods, null, GivenFields);
            };

            act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("baseTypes");
        }

        [Fact]
        public void GivenNullFields_ThrowExpectedException()
        {
            Action act = () =>
            {
                var entity = new ClassEntity(GivenIdentifier, GivenMethods, GivenBaseTypes, null);
            };

            act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("fields");
        }

        private TypeIdentifier GivenIdentifier => TypeIdentifier.Create("IService", new List<TypeIdentifier>());

        private IReadOnlyList<MethodEntity> GivenMethods => new List<MethodEntity>
        {
            MethodEntity.Create(
                "Calculate",
                TypeIdentifier.Create("int", new List<TypeIdentifier>()),
                new List<TypeIdentifier>() )
        };

        private IReadOnlyList<TypeIdentifier> GivenBaseTypes => new List<TypeIdentifier>
        {
            TypeIdentifier.Create("T", new List<TypeIdentifier>())
        };

        private IReadOnlyList<FieldEntity> GivenFields => new List<FieldEntity>
        {
            FieldEntity.Create("_logger", TypeIdentifier.Create("Logger", new List<TypeIdentifier>()))
        };
    }
}
