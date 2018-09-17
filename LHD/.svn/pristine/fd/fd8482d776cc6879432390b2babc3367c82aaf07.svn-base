using System;
namespace KaYi.Database
{
	public class Condition
	{
		private string m_Property = string.Empty;
		private Operator m_Operator;
		private object m_OperandA;
		private object m_OperandB;
		public object OperandA
		{
			get
			{
				return this.m_OperandA;
			}
			set
			{
				this.m_OperandA = value;
			}
		}
		public object OperandB
		{
			get
			{
				return this.m_OperandB;
			}
			set
			{
				this.m_OperandB = value;
			}
		}
		public string Property
		{
			get
			{
				return this.m_Property;
			}
			set
			{
				this.m_Property = value;
			}
		}
		public Operator Operator
		{
			get
			{
				return this.m_Operator;
			}
			set
			{
				this.m_Operator = value;
			}
		}
		public Condition(string propertyName, Operator op, object operandA, object operandB)
		{
			this.m_Property = propertyName;
			this.m_Operator = op;
			this.m_OperandA = operandA;
			this.m_OperandB = operandB;
		}
		public Condition(string propertyName, Operator op, object operandA)
		{
			this.m_Property = propertyName;
			this.m_Operator = op;
			this.m_OperandA = operandA;
		}
	}
}
