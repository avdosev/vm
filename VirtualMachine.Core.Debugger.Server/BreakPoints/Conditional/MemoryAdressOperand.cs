using System;
using System.Linq;

namespace VirtualMachine.Core.Debugger.Server.BreakPoints.Conditional
{
    public class MemoryAddressOperand : IOperand
    {
        public static MemoryAddressOperand Parse(string source)
        {
            //Format: mem(hex_const)

            var parts = source.Split('(', ')');

            var arePartsCorrect = parts.Length == 3 
                                  && parts[0].SequenceEqual("mem") 
                                  && parts[2].SequenceEqual("");

            if (!arePartsCorrect)
                throw new ArgumentException("Format of memory address is incorrect");

            var intValue = Convert.ToInt32(parts[1], 16);

            var word = new Word(intValue);
            return new MemoryAddressOperand(word);
        }

        public Word Value { get; }
        public Word Evaluate(IReadOnlyMemory memory) => memory.ReadWord(Value);

        public override string ToString() => $"mem(0x{Value.ToInt():X})";

        private MemoryAddressOperand(Word word)
        {
            Value = word;
        }
    }
}