using VirtualMachine.Core;
using VirtualMachine.Core.Debugger.Model;
using VirtualMachine.Core.Debugger.Server.BreakPoints.Conditional;

namespace VirtualMachine.Core.Debugger.Server.BreakPoints
{
	public class BreakPointsConverter
	{
		public IBreakPoint FromDto(BreakPointDto dto)
		{
			if (dto.Condition != null)
				return new ConditionalBreakPoint(
					new Word(dto.Address), 
					dto.Name, 
					ConditionExpression.Parse(dto.Condition));
			
			return new BreakPoint(new Word(dto.Address), dto.Name);
		}

		public BreakPointDto ToDto(IBreakPoint bp)
		{
			
			
			return new BreakPointDto
			{
				Name = bp.Name,
				Address = bp.Address.ToUInt()
			};
		}

		private class BreakPoint : IBreakPoint
		{
			public BreakPoint(Word address, string name)
			{
				Address = address;
				Name = name;
			}

			public Word Address { get; }
			public string Name { get; }
			public bool ShouldStop(IReadOnlyMemory memory) => true;
		}
		
		private class ConditionalBreakPoint : BreakPoint
		{
			public ConditionalBreakPoint(Word address, string name, ConditionExpression condition) 
				: base(address, name)
			{
				Condition = condition;
			}
			public ConditionExpression Condition { get; }
			public new bool ShouldStop(IReadOnlyMemory memory) => Condition.Evaluate(memory);
		}
	}
	
	
}