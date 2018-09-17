using System;
using System.Collections.Generic;
namespace KaYi.Database
{
	public class Conditions
	{
		private IList<Condition> m_ConditionExpressions = new List<Condition>();
		private Connector m_Connector;
		public Connector Connector
		{
			get
			{
				return this.m_Connector;
			}
			set
			{
				this.m_Connector = value;
			}
		}
		public IList<Condition> ConditionExpressions
		{
			get
			{
				return this.m_ConditionExpressions;
			}
			set
			{
				this.m_ConditionExpressions = value;
			}
		}
	}
}
