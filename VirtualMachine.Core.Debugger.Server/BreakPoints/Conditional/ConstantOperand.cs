using System;

namespace VirtualMachine.Core.Debugger.Server.BreakPoints.Conditional
{
    public class ConstantOperand : IOperand
    {
        public static ConstantOperand Parse(string source)
        {
            var intValue = Convert.ToInt32(source, 16);
            var word = new Word(intValue);
            return new ConstantOperand(word);
        }

        public Word Value { get; }
        public Word Evaluate(IReadOnlyMemory _) => Value;

        public override string ToString() => $"0x{Value.ToInt():X}";

        private ConstantOperand(Word word)
        {
            Value = word;
        }
    }
}