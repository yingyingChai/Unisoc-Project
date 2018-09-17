//============================================//
//数据库操作类
//HYUO
//2014-08-30
//Ver 1.0.1.2356
//============================================//
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;


namespace Spreadtrum.LHD.MessageCenter
{
    /// <summary>
    /// 数据库操作类
    /// </summary>
    public class DbOperation
    {
        //从配置文件(App.config)中获取连接字符串
        public static string connectionString = ConfigurationManager.AppSettings["dbConnectString"].ToString();
        //定义SqlConnection
        private SqlConnection objSqlConn = null;

        /// <summary>
        /// 连接数据库
        /// </summary>
        private void GetConnection()
        {
            try
            {
                if(objSqlConn == null)
                {
                    objSqlConn = new SqlConnection(connectionString);
                    objSqlConn.Open();
                }
            }
            catch (Exception ex)  
            {
                throw ex;
            } 
        }

        /// <summary>
        /// 根据查询SQL字符串，获取DataTable
        /// </summary>
        ///<param name = "strSelectSql">查询SQL字符串</param>
        ///<returns>返回DataSet</returns>
        public DataTable GetDataTable(string strSelectSql)
        {
            try
            {
                GetConnection();
                DataTable dtTarget = new DataTable();
                using (SqlDataAdapter objSqlDataAdapter = new SqlDataAdapter(strSelectSql, objSqlConn)) 
                {
                    objSqlDataAdapter.Fill(dtTarget);
                }
                return dtTarget;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objSqlConn != null)
                {
                    objSqlConn.Close();
                    objSqlConn.Dispose();
                    objSqlConn = null;
                }
            }
        }

        /// <summary>
        /// 根据查询SQL字符串数组，获取DataSet
        /// </summary>
        ///<param name = "strSelectSqlSet">查询SQL字符串数组</param>
        ///<returns>返回DataSet</returns>
        public DataSet GetDataSet(string[] strSelectSqlSet)
        {
            try
            {
                GetConnection();
                DataSet dsTarget = new DataSet();
                if (strSelectSqlSet.Length > 0)
                {
                    for (int i = 0; i < strSelectSqlSet.Length; i++)
                    {
                        dsTarget.Tables.Add(GetDataTable(strSelectSqlSet[i]));
                        dsTarget.Tables[i].TableName = "DT" + i.ToString();
                    }
                }
                return dsTarget;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objSqlConn != null)
                {
                    objSqlConn.Close();
                    objSqlConn.Dispose();
                    objSqlConn = null;
                }
            }
        }

        /// <summary>
        /// 将DataGridView的数据源更新到数据库中
        /// </summary>
        /// <param name="dtResult">数据源</param>
        /// <returns>执行状态</returns>
        public bool UpdateDataSource(DataTable dtResult, string strDestinationTableName)
        {
            try
            {
                StringBuilder sbSQL = new StringBuilder();
                sbSQL.AppendLine("TRUNCATE TABLE [dbo].[" + strDestinationTableName + "]");
                ExecuteNonQuery(sbSQL.ToString());
                ExecuteSqlBulkCopy(dtResult, strDestinationTableName);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 执行非查询SQL字符串，如Insert，Update，Delete
        /// </summary>
        ///<param name = "strSql">非查询SQL字符串</param>
        ///<returns>返回受影响的行数</returns>
        public int ExecuteNonQuery(string strSql)
        {
            try
            {
                GetConnection();
                int intResult = 0;
                using (SqlCommand objSqlCmd = new SqlCommand(strSql, objSqlConn))
                {
                    intResult = objSqlCmd.ExecuteNonQuery();
                }
                return intResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objSqlConn != null)
                {
                    objSqlConn.Close();
                    objSqlConn.Dispose();
                    objSqlConn = null;
                }
            }
        }

        /// <summary>
        /// 执行数据快速插入或复制,要求数据源DataTable的表结构与目标表完全一致
        /// </summary>
        ///<param name = "dtSource">数据源DataTable</param>
        ///<param name = "strDestinationTableName">目标表名称</param>
        ///<returns>返回执行状态</returns>
        public bool ExecuteSqlBulkCopy(DataTable dtSource, string strDestinationTableName)
        {
            try
            {
                GetConnection();
                using (SqlBulkCopy objSqlBulkCopy = new SqlBulkCopy(objSqlConn))
                {
                    objSqlBulkCopy.DestinationTableName = strDestinationTableName;
                    for (int i = 0; i < dtSource.Columns.Count; i++)
                    {
                        objSqlBulkCopy.ColumnMappings.Add(i, i);
                    }
                    objSqlBulkCopy.WriteToServer(dtSource);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objSqlConn != null)
                {
                    objSqlConn.Close();
                    objSqlConn.Dispose();
                    objSqlConn = null;
                }
            }
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="strStoreProcName">存储过程名称</param>
        /// <param name="strParametersName">参数名集合</param>
        /// <param name="strParameterValue">参数值集合</param>
        /// <returns>执行状态</returns>
        public bool ExecuteStoreProcedure(string strStoreProcName, string[] strParametersName, string[] strParameterValue)
        {
            try
            {
                GetConnection();
                using (SqlCommand objSqlCmd = new SqlCommand(strStoreProcName, objSqlConn))
                {
                    objSqlCmd.CommandType = CommandType.StoredProcedure;
                    int intParaCount = strParametersName.Length;
                    for (int i = 0; i < intParaCount; i++)
                    {
                        objSqlCmd.Parameters.Add(strParametersName[i], SqlDbType.NVarChar);
                        objSqlCmd.Parameters[strParametersName[i]].Value = strParameterValue[i];
                    }
                    objSqlCmd.ExecuteNonQuery();
                }
                return true;
            }                                
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objSqlConn != null)
                {
                    objSqlConn.Close();
                    objSqlConn.Dispose();
                    objSqlConn = null;
                }
            }
        }
    }
}