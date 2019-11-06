using System;

namespace VirtualMachine.Core.Debugger.Server.BreakPoints.Conditional
{
    public class ConditionExpression
    {
        public static ConditionExpression Parse(string source)
        {
            var parts = source.Split(' ');

            if (parts.Length != 3)
                throw new ArgumentException("Format of expression is incorrect");

            var leftOperand = ParseOperand(parts[0]);
            var rightOperand = ParseOperand(parts[2]);

            return new ConditionExpression(leftOperand, rightOperand, parts[1]);
        }

        private static IOperand ParseOperand(string source)
        {
            if (source.StartsWith("mem"))
                return MemoryAddressOperand.Parse(source);
            else
                return ConstantOperand.Parse(source);
        }

        private static bool EvaluateExpression(int left, int right, string compareSign)
        {
            var compareResult = left.CompareTo(right);

            switch (compareSign)
            {
                case "==": return compareResult == 0;
                case "!=": return compareResult != 0;
                case "<=": return compareResult <= 0;
                case "<": return compareResult < 0;
                case ">=": return compareResult >= 0;
                case ">": return compareResult > 0;
                default:
                    throw new ArgumentException("Unknown compare operation");
            }
        }

        public IOperand LeftOperand { get; }
        public IOperand RightOperand { get; }
        public string Operation { get; }

        public bool Evaluate(IReadOnlyMemory memory)
        {
            var leftValue = LeftOperand.Evaluate(memory).ToInt();
            var rightValue = RightOperand.Evaluate(memory).ToInt();
            var result = EvaluateExpression(leftValue, rightValue, Operation);
            return result;
        }

        public override string ToString() => LeftOperand + " " + Operation + " " + RightOperand;

        private ConditionExpression(IOperand left, IOperand right, string operation)
        {
            LeftOperand = left;
            Operation = operation;
            RightOperand = right;
        }
    }
}