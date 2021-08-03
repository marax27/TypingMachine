using System;
using System.Linq;
using TypingMachine.Abstractions;
using TypingMachine.Entities;

namespace TypingMachine.Demo
{
    internal class TypePrintout : ITypeVisitor
    {
        public void VisitClass(ClassEntity entity)
        {
            WriteCommon(entity, ConsoleColor.Yellow);
            WriteFields(entity);
        }

        public void VisitInterface(InterfaceEntity entity)
        {
            WriteCommon(entity, ConsoleColor.Cyan);
        }

        private void WriteCommon(TypeEntity type, ConsoleColor mainColor)
        {
            var baseColor = Console.ForegroundColor;
            Console.Write(" > ");
            Console.ForegroundColor = mainColor;
            Console.WriteLine(type.Identifier.GetFullName());

            Console.ForegroundColor = baseColor;
            var baseTypesText = string.Join(", ", type.BaseTypes.Select(bt => bt.GetFullName()));
            if (baseTypesText != "")
            {
                Console.Write("    ^ ");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(baseTypesText);
            }

            Console.ForegroundColor = ConsoleColor.Magenta;
            foreach (var method in type.Methods)
            {
                var arguments = string.Join(
                    ", ",
                    method.ArgumentTypes.Select(arg => arg.GetFullName())
                );
                Console.WriteLine($"    ({arguments}) -> {method.ReturnType.GetFullName()}");
            }

            Console.ForegroundColor = baseColor;
        }

        private static void WriteFields(ClassEntity entity)
        {
            var fieldsText = string.Join(", ", entity.Fields.Select(f => f.Type.GetFullName()));
            if (fieldsText != "")
            {
                var baseColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"    # {fieldsText}");
                Console.ForegroundColor = baseColor;
            }
        }
    }
}
