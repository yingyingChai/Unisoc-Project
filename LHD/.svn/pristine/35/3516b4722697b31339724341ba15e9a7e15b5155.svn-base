using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Xml;
namespace KaYi.Database
{
	public class DALGateway<T> where T : new()
	{
		private string m_TableName = string.Empty;
		private string m_PKField = string.Empty;
		private Dictionary<string, string> m_PropertyFieldMap = new Dictionary<string, string>();
		private Dictionary<string, string> m_ColumnsType = new Dictionary<string, string>();
		public string TableName
		{
			get
			{
				return this.m_TableName;
			}
			set
			{
				this.m_TableName = value;
			}
		}
		public void LoadSchema(string tableName)
		{
			this.TableName = tableName;
			if (Statics.xdoc == null)
			{
				Statics.LoadXml();
			}
			try
			{
				foreach (XmlNode xmlNode in Statics.xdoc.SelectSingleNode(string.Format("//dbSchema/table[@name='{0}']", tableName.Trim().ToUpper())).ChildNodes)
				{
					this.AddFieldMap(xmlNode.Attributes["propertyname"].Value, xmlNode.Attributes["name"].Value);
					if (xmlNode.Attributes["columnType"] == null)
					{
						this.m_ColumnsType.Add(xmlNode.Attributes["propertyname"].Value, "0");
					}
					else
					{
						this.m_ColumnsType.Add(xmlNode.Attributes["propertyname"].Value, xmlNode.Attributes["columnType"].Value);
					}
					if (xmlNode.Attributes["PKField"] != null && xmlNode.Attributes["PKField"].Value == "1")
					{
						this.m_PKField = xmlNode.Attributes["name"].Value;
					}
				}
			}
			catch (Exception arg_15B_0)
			{
				throw arg_15B_0;
			}
		}
		private void BindFieldsToProperties(ref T entity, DataRow row)
		{
			PropertyInfo[] properties = entity.GetType().GetProperties();
			for (int i = 0; i < properties.Length; i++)
			{
				PropertyInfo propertyInfo = properties[i];
				string text = null;
				this.m_PropertyFieldMap.TryGetValue(propertyInfo.Name, out text);
				if (text != null)
				{
					if (row[text] == DBNull.Value)
					{
						propertyInfo.SetValue(entity, null, null);
					}
					else
					{
						try
						{
							propertyInfo.SetValue(entity, Convert.ChangeType(row[text], propertyInfo.PropertyType), null);
						}
						catch (Exception)
						{
							try
							{
								object value = new EnumConverter(propertyInfo.PropertyType).ConvertFrom(row[text]);
								propertyInfo.SetValue(entity, value, null);
							}
							catch (Exception)
							{
								propertyInfo.SetValue(entity, null, null);
							}
						}
					}
				}
			}
		}
		private IList<T> getRecordsBySqlStatement(string SQLStatement, int pageIndex, int pageSize)
		{
			IList<T> list = new List<T>();
			DataTable dataTable = DALService.Database.ExeSQLReturnDataSet(SQLStatement, this.m_TableName, pageIndex, pageSize).Tables[this.m_TableName];
			if (dataTable != null && dataTable.Rows.Count > 0)
			{
				foreach (DataRow row in dataTable.Rows)
				{
					T item = Activator.CreateInstance<T>();
					this.BindFieldsToProperties(ref item, row);
					list.Add(item);
				}
			}
			return list;
		}
		private IList<T> getRecordsBySqlStatement(string SQLStatement)
		{
			IList<T> list = new List<T>();
			DataTable dataTable = DALService.Database.ExeSQLReturnDataSet(SQLStatement, this.m_TableName).Tables[this.m_TableName];
			if (dataTable != null && dataTable.Rows.Count > 0)
			{
				foreach (DataRow row in dataTable.Rows)
				{
					T item = Activator.CreateInstance<T>();
					this.BindFieldsToProperties(ref item, row);
					list.Add(item);
				}
			}
			return list;
		}
		private void AddFieldMap(string PropertyName, string ColumnName)
		{
			this.m_PropertyFieldMap.Add(PropertyName, ColumnName);
		}
		private string generateWhereStatement(Condition condition)
		{
			string empty = string.Empty;
			this.m_PropertyFieldMap.TryGetValue(condition.Property, out empty);
			return DALService.Database.GetCompareExpression(empty, condition);
		}
		private string generateOrderStatementByCondition(OrderExpression orderExpression)
		{
			string empty = string.Empty;
			this.m_PropertyFieldMap.TryGetValue(orderExpression.FieldName, out empty);
			string text = string.Format(" order by {0} ", empty);
			if (orderExpression.Desc)
			{
				text += " desc ";
			}
			return text;
		}
		private string getSelectCmdFromConditions(Conditions conditions)
		{
			if (conditions == null)
			{
				return "";
			}
			string text = string.Empty;
			if (conditions.ConditionExpressions.Count > 0)
			{
				foreach (Condition current in conditions.ConditionExpressions)
				{
					text += this.generateWhereStatement(current);
					Connector connector = conditions.Connector;
					if (connector != Connector.OR)
					{
						if (connector == Connector.AND)
						{
							text += " AND ";
						}
					}
					else
					{
						text += " OR  ";
					}
				}
			}
			if (text != string.Empty)
			{
				text = " where (" + text.Substring(0, text.Length - 4) + ")  ";
			}
			return text;
		}
		public string generateWhereStatementByConditionS(Conditions conditions, IList<string> conditionExpressions)
		{
			string text = this.getSelectCmdFromConditions(conditions);
			if (conditionExpressions != null && conditionExpressions.Count > 0)
			{
				foreach (string current in conditionExpressions)
				{
					Connector connector = conditions.Connector;
					if (connector != Connector.OR)
					{
						if (connector == Connector.AND)
						{
							text = text + " and " + current;
						}
					}
					else
					{
						text = text + " or  " + current;
					}
				}
			}
			if (text.StartsWith(" and ") || text.StartsWith(" or  "))
			{
				text = text.Substring(5, text.Length - 5);
				text = " where " + text;
			}
			return text;
		}
		public string generateWhereStatementByConditionS(Conditions conditions, IList<string> conditionExpressions, Connector connector)
		{
			string text = this.getSelectCmdFromConditions(conditions);
			if (conditionExpressions != null && conditionExpressions.Count > 0)
			{
				foreach (string current in conditionExpressions)
				{
					if (current != "" && current != null)
					{
						if (connector != Connector.OR)
						{
							if (connector == Connector.AND)
							{
								text = text + " and " + current;
							}
						}
						else
						{
							text = text + " or  " + current;
						}
					}
				}
			}
			if (text.StartsWith(" and ") || text.StartsWith(" or  "))
			{
				text = text.Substring(5, text.Length - 5);
				text = " where " + text;
			}
			return text;
		}
		public string generateWhereStatementByConditionS(Conditions conditions, string conditionExpression)
		{
			string text = this.getSelectCmdFromConditions(conditions);
			if (conditionExpression != string.Empty && conditionExpression != null)
			{
				if (text == string.Empty)
				{
					text = " where " + conditionExpression;
				}
				else
				{
					Connector connector = conditions.Connector;
					if (connector != Connector.OR)
					{
						if (connector == Connector.AND)
						{
							text = text + " and " + conditionExpression;
						}
					}
					else
					{
						text = text + " or " + conditionExpression;
					}
				}
			}
			return text;
		}
		private string generateWhereStatementByConditionS(Conditions conditions)
		{
			string text = string.Empty;
			if (conditions.ConditionExpressions.Count > 0)
			{
				foreach (Condition current in conditions.ConditionExpressions)
				{
					text += this.generateWhereStatement(current);
					Connector connector = conditions.Connector;
					if (connector != Connector.OR)
					{
						if (connector == Connector.AND)
						{
							text += " AND ";
						}
					}
					else
					{
						text += " OR  ";
					}
				}
				text = " where " + text.Substring(0, text.Length - 4);
			}
			return text;
		}
		public int GetRecordCountByConditions(Conditions conditions)
		{
			string text = string.Empty;
			text = string.Format("Select count(*) as RecordCount from {0}", this.m_TableName);
			if (conditions != null)
			{
				text += this.generateWhereStatementByConditionS(conditions);
			}
			return Convert.ToInt32(DALService.Database.ExeSQLReturnDataSet(text, this.m_TableName).Tables[0].Rows[0][0]);
		}
		public int GetRecordCountByConditions(Conditions conditions, string conditionExpression)
		{
			string text = string.Empty;
			text = string.Format("Select count(*) as RecordCount from {0}", this.m_TableName);
			text += this.generateWhereStatementByConditionS(conditions, conditionExpression);
			return Convert.ToInt32(DALService.Database.ExeSQLReturnDataSet(text, this.m_TableName).Tables[0].Rows[0][0]);
		}
		public int GetRecordCountBySQLStatementS(IList<string> sqlStatements, Connector connector)
		{
			string text = string.Format("select * from {0}", this.m_TableName);
			Conditions conditions = new Conditions();
			conditions.Connector = connector;
			if (sqlStatements != null && sqlStatements.Count > 0)
			{
				text += this.generateWhereStatementByConditionS(conditions, sqlStatements);
			}
			return this.GetRecordCountBySQLStatement(text);
		}
		public int GetRecordCountBySQLStatement(string sqlStatement)
		{
			string sQLStatement = string.Empty;
			sQLStatement = string.Format("Select count(*) as RecordCount from ({0}) as V_TEMP", sqlStatement);
			DataSet dataSet = DALService.Database.ExeSQLReturnDataSet(sQLStatement, this.m_TableName);
			int result;
			if (dataSet.Tables.Count > 0)
			{
				result = Convert.ToInt32(dataSet.Tables[0].Rows[0][0]);
			}
			else
			{
				result = 0;
			}
			return result;
		}
		public T getRecord(Conditions conditions)
		{
			IList<T> records = this.getRecords(1, conditions, null);
			if (records.Count > 0)
			{
				return records[0];
			}
			return default(T);
		}
		public IList<T> getRecordsBySQLStatement(string sqlStatement, int pageIndex, int pageSize)
		{
			return this.getRecordsBySqlStatement(sqlStatement, pageIndex, pageSize);
		}
		public IList<T> getRecords(int pageIndex, int pageSize, Conditions conditions, OrderExpression orderExpression, string conditionExpression)
		{
			if (conditionExpression != null)
			{
				conditionExpression = conditionExpression.Trim();
			}
			int num = 0;
			IList<string> list = null;
			if (conditionExpression != null && conditionExpression != "")
			{
				list = new List<string>();
				list.Add(conditionExpression);
			}
			return this.getRecords(pageIndex, pageSize, conditions, orderExpression, list, Connector.AND, out num);
		}
		public IList<T> getRecords(int pageIndex, int pageSize, Conditions conditions, OrderExpression orderExpression, IList<string> conditionExpressions, Connector connector, out int recordCount)
		{
			if (pageSize == -1)
			{
				pageSize = 999999999;
			}
			string text = string.Empty;
			string sQLStatement = string.Empty;
			string arg_1C_0 = string.Empty;
			string text2 = string.Empty;
			int num = pageSize * pageIndex;
			text2 = string.Format("select {0} from {1} ", this.m_PKField, this.m_TableName);
			if ((conditions != null && conditions.ConditionExpressions.Count > 0) || (conditionExpressions != null && conditionExpressions.Count > 0))
			{
				text = this.generateWhereStatementByConditionS(conditions, conditionExpressions, connector);
				text2 += text;
			}
			string text3 = string.Empty;
			if (orderExpression != null)
			{
				text3 = this.generateOrderStatementByCondition(orderExpression);
				sQLStatement = string.Format("select top {0} * from (select ROW_NUMBER() over ({1}) as RowNumber,* from {2} {3} ) as V_TEMP where V_TEMP.RowNumber>{4}", new object[]
				{
					pageSize,
					text3,
					this.m_TableName,
					text,
					num
				});
			}
			else
			{
				sQLStatement = string.Format("select top {0} * from (select ROW_NUMBER() over ({1}) as RowNumber,* from {2} {3} ) as V_TEMP where V_TEMP.RowNumber>{4}", new object[]
				{
					pageSize,
					" order by " + this.m_PKField,
					this.m_TableName,
					text,
					num
				});
			}
			IList<T> arg_111_0 = this.getRecordsBySqlStatement(sQLStatement);
			recordCount = this.GetRecordCountBySQLStatement(text2);
			return arg_111_0;
		}
		public IList<T> getRecordsWithCustomerOrderStatement(int pageIndex, int pageSize, Conditions conditions, string orderByStatement, IList<string> conditionExpressions, Connector connector, out int recordCount)
		{
			if (pageSize == -1)
			{
				pageSize = 999999999;
			}
			string text = string.Empty;
			string sQLStatement = string.Empty;
			string arg_1C_0 = string.Empty;
			string text2 = string.Empty;
			int num = pageSize * pageIndex;
			text2 = string.Format("select {0} from {1} ", this.m_PKField, this.m_TableName);
			if ((conditions != null && conditions.ConditionExpressions.Count > 0) || (conditionExpressions != null && conditionExpressions.Count > 0))
			{
				text = this.generateWhereStatementByConditionS(conditions, conditionExpressions, connector);
				text2 += text;
			}
			string arg_76_0 = string.Empty;
			sQLStatement = string.Format("select top {0} * from (select ROW_NUMBER() over ({1}) as RowNumber,* from {2} {3} ) as V_TEMP where V_TEMP.RowNumber>{4}", new object[]
			{
				pageSize,
				orderByStatement,
				this.m_TableName,
				text,
				num
			});
			IList<T> arg_BD_0 = this.getRecordsBySqlStatement(sQLStatement);
			recordCount = this.GetRecordCountBySQLStatement(text2);
			return arg_BD_0;
		}
		public IList<T> getRecords(int pageIndex, int pageSize, Conditions conditions, OrderExpression orderExpression)
		{
			string text = string.Empty;
			text = string.Format("select * from {0}", this.m_TableName);
			if (conditions != null)
			{
				text += this.generateWhereStatementByConditionS(conditions);
			}
			if (orderExpression != null)
			{
				text += this.generateOrderStatementByCondition(orderExpression);
			}
			return this.getRecordsBySqlStatement(text, pageIndex, pageSize);
		}
		public IList<T> getRecords(int RecordCount, Conditions conditions, OrderExpression orderExpression)
		{
			int num = 0;
			return this.getRecords(0, RecordCount, conditions, orderExpression, null, Connector.AND, out num);
		}
		public int AddNew(T newEntity)
		{
			PropertyInfo[] arg_1E_0 = newEntity.GetType().GetProperties();
			string text = string.Empty;
			string text2 = string.Empty;
			PropertyInfo[] array = arg_1E_0;
			for (int i = 0; i < array.Length; i++)
			{
				PropertyInfo propertyInfo = array[i];
				string text3 = null;
				this.m_PropertyFieldMap.TryGetValue(propertyInfo.Name, out text3);
				string a = null;
				this.m_ColumnsType.TryGetValue(propertyInfo.Name, out a);
				if (text3 != null && a == "0")
				{
					string a2 = DALService.Database.DB_TYPE;
					if (!(a2 == "SQLSERVER"))
					{
						if (a2 == "OLEDB")
						{
							text = text + "[" + text3 + "],";
						}
					}
					else
					{
						text = text + text3 + ",";
					}
					a2 = DALService.Database.DB_TYPE;
					if (!(a2 == "SQLSERVER"))
					{
						if (a2 == "OLEDB")
						{
							a2 = propertyInfo.PropertyType.Name;
							if (!(a2 == "String"))
							{
								if (!(a2 == "Int32"))
								{
									if (!(a2 == "DateTime"))
									{
										if (!(a2 == "Boolean"))
										{
											text2 = text2 + "'" + DataFormatUtilities.processValues(propertyInfo.GetValue(newEntity, null)).ToString() + "',";
										}
										else
										{
											if ((bool)propertyInfo.GetValue(newEntity, null))
											{
												text2 += "true,";
											}
											else
											{
												text2 += "false,";
											}
										}
									}
									else
									{
										if (propertyInfo.GetValue(newEntity, null) != null)
										{
											text2 = text2 + "#" + DataFormatUtilities.processValues(propertyInfo.GetValue(newEntity, null)).ToString() + "#,";
										}
										else
										{
											text2 += "#1901-01-01#,";
										}
									}
								}
								else
								{
									text2 = text2 + DataFormatUtilities.processValues(propertyInfo.GetValue(newEntity, null)).ToString() + ",";
								}
							}
							else
							{
								text2 = text2 + "'" + DataFormatUtilities.processValues(propertyInfo.GetValue(newEntity, null)).ToString() + "',";
							}
						}
					}
					else
					{
						if (propertyInfo.GetValue(newEntity, null) is bool)
						{
							string str = "0";
							if (Convert.ToBoolean(propertyInfo.GetValue(newEntity, null)))
							{
								str = "1";
							}
							text2 = text2 + "'" + str + "',";
						}
						else
						{
							text2 = text2 + "'" + DataFormatUtilities.processValues(propertyInfo.GetValue(newEntity, null)).ToString() + "',";
						}
					}
				}
			}
			text = text.Substring(0, text.Length - 1);
			text2 = text2.Substring(0, text2.Length - 1);
			string sQLStatement = string.Format("insert into {0} ({1}) values ({2})", this.m_TableName, text, text2);
			DALService.Database.ExeSQLStatement(sQLStatement);
			return 0;
		}
		public int UpdateByFieldValue(string PKField, string PKValue, T newEntity)
		{
			PropertyInfo[] arg_18_0 = newEntity.GetType().GetProperties();
			string text = string.Empty;
			PropertyInfo[] array = arg_18_0;
			for (int i = 0; i < array.Length; i++)
			{
				PropertyInfo propertyInfo = array[i];
				string text2 = null;
				this.m_PropertyFieldMap.TryGetValue(propertyInfo.Name, out text2);
				string a = null;
				this.m_ColumnsType.TryGetValue(propertyInfo.Name, out a);
				if (text2 != null && a == "0")
				{
					string dB_TYPE = DALService.Database.DB_TYPE;
					if (!(dB_TYPE == "SQLSERVER"))
					{
						if (dB_TYPE == "OLEDB")
						{
							Type arg_126_0 = propertyInfo.PropertyType;
							if (propertyInfo.GetValue(newEntity, null) is string || propertyInfo.GetValue(newEntity, null) is DateTime)
							{
								text += string.Format("[{0}]='{1}',", text2, DataFormatUtilities.processValues(propertyInfo.GetValue(newEntity, null)).ToString());
							}
							if (propertyInfo.GetValue(newEntity, null) is int || propertyInfo.GetValue(newEntity, null) is int || propertyInfo.GetValue(newEntity, null) is double || propertyInfo.GetValue(newEntity, null) is double || propertyInfo.GetValue(newEntity, null) is float || propertyInfo.GetValue(newEntity, null) is float || propertyInfo.GetValue(newEntity, null) is decimal || propertyInfo.GetValue(newEntity, null) is decimal)
							{
								text += string.Format("[{0}]={1},", text2, DataFormatUtilities.processValues(propertyInfo.GetValue(newEntity, null)).ToString());
							}
							if (propertyInfo.GetValue(newEntity, null) is bool || propertyInfo.GetValue(newEntity, null) is bool)
							{
								text += string.Format("[{0}]={1},", text2, DataFormatUtilities.processValues(propertyInfo.GetValue(newEntity, null)).ToString());
							}
						}
					}
					else
					{
						if (propertyInfo.GetValue(newEntity, null) is bool)
						{
							string arg = "0";
							if (Convert.ToBoolean(propertyInfo.GetValue(newEntity, null)))
							{
								arg = "1";
							}
							text += string.Format("{0}='{1}',", text2, arg);
						}
						else
						{
							text += string.Format("{0}='{1}',", text2, DataFormatUtilities.processValues(propertyInfo.GetValue(newEntity, null)).ToString());
						}
					}
				}
			}
			text = text.Substring(0, text.Length - 1);
			string sQLStatement = string.Format("update {0} set {1} where {2}='{3}'", new object[]
			{
				this.m_TableName,
				text,
				PKField,
				PKValue
			});
			DALService.Database.ExeSQLStatement(sQLStatement);
			return 0;
		}
		public void UpdateDefaultAddressForFalseByUserId(string field, string value)
		{
			string sQLStatement = string.Format("update  {0} set DefaultAddress=0 where {1}='{2}'", this.m_TableName, field, value);
			DALService.Database.ExeSQLStatement(sQLStatement);
		}
		public void DeleteByFieldValue(string field, string value)
		{
			string sQLStatement = string.Format("delete from {0} where {1}='{2}'", this.m_TableName, field, value);
			DALService.Database.ExeSQLStatement(sQLStatement);
		}
		public DataTable getDataTableBySqlStatement(string SQLStatement)
		{
			return DALService.Database.ExeSQLReturnDataSet(SQLStatement, "RESULT").Tables["RESULT"];
		}
		public void ExecuteSQLStatement(string SQLStatement)
		{
			DALService.Database.ExeSQLStatement(SQLStatement);
		}
		public IList<T> getRecordsWithOrderStatement(int pageIndex, int pageSize, Conditions conditions, string orderStatement, IList<string> conditionExpressions, out int recordCount)
		{
			string text = string.Empty;
			string sqlStatement = string.Empty;
			text = string.Format("select * from {0}", this.m_TableName);
			if (conditions != null)
			{
				text += this.generateWhereStatementByConditionS(conditions, conditionExpressions);
			}
			sqlStatement = text;
			if (orderStatement != null)
			{
				text += orderStatement;
			}
			IList<T> arg_52_0 = this.getRecordsBySqlStatement(text, pageIndex, pageSize);
			recordCount = this.GetRecordCountBySQLStatement(sqlStatement);
			return arg_52_0;
		}
	}
}
