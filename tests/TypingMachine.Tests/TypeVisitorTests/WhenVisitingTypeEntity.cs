using System.Collections.Generic;
using Moq;
using TypingMachine.Abstractions;
using TypingMachine.Entities;
using TypingMachine.Tests.Utilities;
using Xunit;

namespace TypingMachine.Tests.TypeVisitorTests
{
    public class WhenVisitingTypeEntity
    {
        [Fact]
        public void GivenClassEntity_VisitClassExactlyOnce()
        {
            var givenEntity = new ClassEntity(
                "ConcreteClass".AsSimpleTypeId(),
                new List<MethodEntity>(),
                new List<TypeIdentifier>(),
                new List<FieldEntity>()
            );
            var typeVisitor = new Mock<ITypeVisitor>();

            givenEntity.Accept(typeVisitor.Object);

            typeVisitor.Verify(visitor => visitor.VisitClass(It.IsAny<ClassEntity>()), Times.Once);
        }

        [Fact]
        public void GivenInterfaceEntity_VisitInterfaceExactlyOnce()
        {
            var givenEntity = new InterfaceEntity(
                "IExampleProvider".AsSimpleTypeId(),
                new List<MethodEntity>(),
                new List<TypeIdentifier>()
            );
            var typeVisitor = new Mock<ITypeVisitor>();

            givenEntity.Accept(typeVisitor.Object);

            typeVisitor.Verify(visitor => visitor.VisitInterface(It.IsAny<InterfaceEntity>()), Times.Once);
        }
    }
}
