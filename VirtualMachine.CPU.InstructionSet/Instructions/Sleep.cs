using System.Threading;
using VirtualMachine.Core;

namespace VirtualMachine.CPU.InstructionSet.Instructions {
    public class Sleep : InstructionBase {
        public Sleep() : base(9, OperandType.Constant, OperandType.Ignored, OperandType.Ignored) { }
        protected override void ExecuteInternal(ICpu cpu, IMemory memory, Operand timeToSleep, Operand op1, Operand op2) {
            Thread.Sleep(timeToSleep.Value.ToInt());
        }
    }
}