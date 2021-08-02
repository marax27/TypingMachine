using TypingMachine.Entities;

namespace TypingMachine.Abstractions
{
    public interface ITypeVisitor
    {
        void VisitClass(ClassEntity entity);
        void VisitInterface(InterfaceEntity entity);
    }
}
