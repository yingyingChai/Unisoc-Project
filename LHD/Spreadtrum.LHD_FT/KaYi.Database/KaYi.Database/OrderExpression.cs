using System;
namespace KaYi.Database
{
	public class OrderExpression
	{
		private string m_FieldName = string.Empty;
		private bool m_Desc;
		public string FieldName
		{
			get
			{
				return this.m_FieldName;
			}
			set
			{
				this.m_FieldName = value;
			}
		}
		public bool Desc
		{
			get
			{
				return this.m_Desc;
			}
			set
			{
				this.m_Desc = value;
			}
		}
		public OrderExpression(string fieldName, bool desc)
		{
			this.m_FieldName = fieldName;
			this.m_Desc = desc;
		}
	}
}
