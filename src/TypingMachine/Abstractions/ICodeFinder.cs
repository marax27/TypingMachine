using Microsoft.CodeAnalysis;

namespace TypingMachine.Abstractions
{
    /// <typeparam name="TEntity">Type of entity the finder is designed to find.</typeparam>
    public interface ICodeFinder<out TEntity>
    {
        TEntity FindFor(SyntaxNode initialNode);
    }
}
