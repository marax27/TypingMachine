using FluentAssertions;
using System;
using System.Collections.Generic;
using TypingMachine.Domain;
using TypingMachine.Domain.Builders;
using TypingMachine.Tests.Utilities;
using Xunit;

namespace TypingMachine.Tests.Domain.TypeEntities
{
    public class WhenCreatingClassEntity
    {
        [Fact]
        public void GivenValidData_CreateEntity()
        {
            Action act = () =>
            {
                var entity = new ClassBuilder()
                    .WithFields(GivenFields)
                    .WithBaseTypes(GivenBaseTypes)
                    .WithMethods(GivenMethods)
                    .Build(GivenIdentifier);
            };

            act.Should().NotThrow();
        }

        [Fact]
        public void GivenNullIdentifier_ThrowExpectedException()
        {
            Action act = () =>
            {
                var entity = new ClassBuilder()
                    .WithFields(GivenFields)
                    .WithBaseTypes(GivenBaseTypes)
                    .WithMethods(GivenMethods)
                    .Build(null);
            };

            act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("identifier");
        }

        [Fact]
        public void GivenNullMethods_ThrowExpectedException()
        {
            Action act = () =>
            {
                var entity = new ClassBuilder()
                    .WithFields(GivenFields)
                    .WithBaseTypes(GivenBaseTypes)
                    .WithMethods(null)
                    .Build(GivenIdentifier);
            };

            act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("methods");
        }

        [Fact]
        public void GivenNullBaseTypes_ThrowExpectedException()
        {
            Action act = () =>
            {
                var entity = new ClassBuilder()
                    .WithFields(GivenFields)
                    .WithBaseTypes(null)
                    .WithMethods(GivenMethods)
                    .Build(GivenIdentifier);
            };

            act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("baseTypes");
        }

        [Fact]
        public void GivenNullFields_ThrowExpectedException()
        {
            Action act = () =>
            {
                var entity = new ClassBuilder()
                    .WithFields(null)
                    .WithBaseTypes(GivenBaseTypes)
                    .WithMethods(GivenMethods)
                    .Build(GivenIdentifier);
            };

            act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("fields");
        }

        [Fact]
        public void GivenNullNamespace_ThrowExpectedException()
        {
            Action act = () =>
            {
                var entity = new ClassBuilder()
                    .WithFields(null)
                    .WithBaseTypes(GivenBaseTypes)
                    .WithMethods(GivenMethods)
                    .WithNamespace(null)
                    .Build(GivenIdentifier);
            };

            act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("namespaceId");
        }

        [Fact]
        public void GivenNullUsingDirectives_ThrowExpectedException()
        {
            Action act = () =>
            {
                var entity = new ClassBuilder()
                    .WithMethods(GivenMethods)
                    .WithBaseTypes(GivenBaseTypes)
                    .WithUsingDirectives(null)
                    .Build(GivenIdentifier);
            };

            act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("usingDirectives");
        }

        private Identifier GivenIdentifier => "IService".AsSimpleId();

        private IReadOnlyList<MethodEntity> GivenMethods => new List<MethodEntity>
        {
            new MethodBuilder()
                .Build("Calculate".AsSimpleId(), "int".AsSimpleId())
        };

        private IReadOnlyList<Identifier> GivenBaseTypes => new List<Identifier>
        {
            "T".AsSimpleId()
        };

        private IReadOnlyList<FieldEntity> GivenFields => new List<FieldEntity>
        {
            new FieldBuilder().Build("_logger", "Logger".AsSimpleId())
        };
    }
}
