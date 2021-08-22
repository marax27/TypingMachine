using Moq;
using TypingMachine.Abstractions;
using TypingMachine.Domain;
using TypingMachine.Domain.Builders;
using TypingMachine.Tests.Utilities;
using Xunit;

namespace TypingMachine.Tests.TypeVisitorTests
{
    public class WhenVisitingTypeEntity
    {
        [Fact]
        public void GivenClassEntity_VisitClassExactlyOnce()
        {
            var givenEntity = new ClassBuilder()
                .Build("ConcreteClass".AsSimpleTypeId());
            var typeVisitor = new Mock<ITypeVisitor>();

            givenEntity.Accept(typeVisitor.Object);

            typeVisitor.Verify(visitor => visitor.VisitClass(It.IsAny<ClassEntity>()), Times.Once);
        }

        [Fact]
        public void GivenInterfaceEntity_VisitInterfaceExactlyOnce()
        {
            var givenEntity = new InterfaceBuilder()
                .Build("IExampleProvider".AsSimpleTypeId());
            var typeVisitor = new Mock<ITypeVisitor>();

            givenEntity.Accept(typeVisitor.Object);

            typeVisitor.Verify(visitor => visitor.VisitInterface(It.IsAny<InterfaceEntity>()), Times.Once);
        }
    }
}
