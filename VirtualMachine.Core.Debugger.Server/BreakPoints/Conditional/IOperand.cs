namespace VirtualMachine.Core.Debugger.Server.BreakPoints.Conditional
{
    public interface IOperand
    {
        Word Value { get; }

        Word Evaluate(IReadOnlyMemory memory);
    }
}