using System.Threading.Tasks;
using TypingMachine.FileSystem;

namespace TypingMachine.Demo
{
    internal class Program
    {
        private const string CodeBasePath = @"C:\workspace\Episememe";

        private static void Main(string[] args)
        {
            Start().Wait();
        }

        private static async Task Start()
        {
            var codeBase = new CodeBase(CodeBasePath);
            var types = await codeBase.FindAllTypesAsync();
            var visitor = new TypePrintout();

            foreach (var type in types)
            {
                type.Accept(visitor);
            }
        }
    }
}
