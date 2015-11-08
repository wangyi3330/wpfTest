
using System;
using System.Configuration;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Data.OleDb;
using Carrot.DataCommon;
using Carrot.Model;
namespace Carrot.DBUtility
{
    public class DBHelper
    {
        private const int CommandTimeout = 500;
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        internal static string connString = DataUtility.connString;
        /// <summary>
        /// 定义Hashtable用来缓存参数
        /// </summary>
        private static Dictionary<String, OleDbParameter[]> parmCache = new Dictionary<string, OleDbParameter[]>();

        /// <summary>
        /// 执行单条增删改操作
        /// </summary>
        /// <param name="strSQL">增删改语句</param>
        /// <returns></returns>
        internal bool ExecuteSQL(string connString, string strSQL)
        {
            bool ReturnResult = false;
            OleDbConnection Conn = new OleDbConnection(connString);
            Conn.Open();
            try
            {

                OleDbCommand MyCommand = new OleDbCommand();
                MyCommand.Connection = Conn;
                MyCommand.CommandText = strSQL;
                MyCommand.ExecuteNonQuery();

                ReturnResult = true;
            }
            catch (Exception Ex)
            {
                ReturnResult = false;
                throw Ex;
            }
            finally
            {
                Conn.Close();
            }
            return ReturnResult;
        }

        /// <summary>
        /// 返回查询结果集
        /// </summary>
        /// <param name="strSQL">查询字符串</param>
        /// <returns></returns>
        internal DataTable ExecuteSQLDT(string connString, string strSQL)
        {
            DataTable DT = new DataTable();
            OleDbConnection Conn = new OleDbConnection(connString);
            OleDbDataAdapter OleDbCommand = new OleDbDataAdapter(strSQL, Conn);
            try
            {
                OleDbCommand.Fill(DT);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return DT;
        }

        /// <summary>
        /// 执行SQL命令并返回影响的行数
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">SQL语句（存储过程名）</param>
        /// <param name="commandParameters">命令参数</param>
        /// <returns>影响的行数</returns>
        internal static int ExecuteNonQuery(OleDbConnection connection, CommandType cmdType, string cmdText, params OleDbParameter[] commandParameters)
        {
            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandTimeout = CommandTimeout;
            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// 执行SQL命令并返回影响的行数
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">SQL语句（存储过程名）</param>
        /// <returns>影响的行数</returns>
        internal static int ExecuteNonQuery(OleDbConnection connection, CommandType cmdType, string cmdText)
        {
            return ExecuteNonQuery(connection, cmdType, cmdText, null);
        }

       
        /// <summary>
        /// 执行SQL命令并返回影响的行数
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="cmdType">命名类型</param>
        /// <param name="cmdText">SQL语句（存储过程名）</param>
        /// <returns>影响的行数</returns>
        internal static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                return ExecuteNonQuery(conn, cmdType, cmdText, null);
            }
        }

        /// <summary>
        /// 执行SQL命令并返回影响的行数
        /// </summary>
        /// <param name="trans">事务对象</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">SQL语句（存储过程名）</param>
        /// <param name="commandParameters">命令参数</param>
        /// <returns>影响的行数</returns>
        internal static int ExecuteNonQuery(OleDbTransaction trans, CommandType cmdType, string cmdText, params OleDbParameter[] commandParameters)
        {
            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandTimeout = CommandTimeout;
            cmd.Transaction = trans;
            PrepareCommand(cmd, trans.Connection, null, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// 执行SQL命令并返回影响的行数
        /// </summary>
        /// <param name="trans">事务对象</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">SQL语句（存储过程名）</param>
        /// <param name="commandParameters">命令参数</param>
        /// <returns>影响的行数</returns>
        internal static int ExecuteNonQuery(OleDbTransaction trans, CommandType cmdType, string cmdText)
        {
            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandTimeout = CommandTimeout;
            cmd.Transaction = trans;
            PrepareCommand(cmd, trans.Connection, null, cmdType, cmdText, null);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// 执行SQL命令并返回影响的行数
        /// </summary>
        /// <param name="trans">事务对象</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">SQL语句（存储过程名）</param>
        /// <param name="commandParameters">命令参数</param>
        /// <param name="Identity">返回标示</param>
        /// <returns>影响的行数</returns>
        internal static int ExecuteNonQuery(OleDbTransaction trans, CommandType cmdType, string cmdText, out int Identity)
        {
            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandTimeout = CommandTimeout;
            cmd.Transaction = trans;
            PrepareCommand(cmd, trans.Connection, null, cmdType, cmdText, null);
            Identity = Convert.ToInt32(cmd.ExecuteScalar());
            cmd.Parameters.Clear();
            return Identity;
        }

        /// <summary>
        /// 执行SQL命令并返回结果集
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">SQL语句（存储过程名）</param>
        /// <param name="commandParameters">命令参数</param>
        /// <returns>影响的行数</returns>
        internal static DataSet ExecuteDataSet(OleDbConnection connection, CommandType cmdType, string cmdText, params OleDbParameter[] commandParameters)
        {
            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandTimeout = CommandTimeout;
            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            OleDbDataAdapter adp = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            cmd.Parameters.Clear();
            return ds;
        }

        /// <summary>
        /// 执行SQL命令并返回结果集
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">SQL语句（存储过程名）</param>
        /// <returns>影响的行数</returns>
        internal static DataSet ExecuteDataSet(OleDbConnection connection, CommandType cmdType, string cmdText)
        {
            return ExecuteDataSet(connection, cmdType, cmdText, null);
        }

        /// <summary>
        /// 执行SQL命令并返回结果集
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="cmdType">命名类型</param>
        /// <param name="cmdText">SQL语句（存储过程名）</param>
        /// <param name="commandParameters">命令参数集合</param>
        /// <returns>影响的行数</returns>
        internal static DataSet ExecuteDataSet(string connectionString, CommandType cmdType, string cmdText, params OleDbParameter[] commandParameters)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                return ExecuteDataSet(conn, cmdType, cmdText, commandParameters);
            }
        }

        /// <summary>
        /// 执行SQL命令并返回结果集
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="cmdType">命名类型</param>
        /// <param name="cmdText">SQL语句（存储过程名）</param>
        /// <returns>影响的行数</returns>
        internal static DataSet ExecuteDataSet(string connectionString, CommandType cmdType, string cmdText)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                return ExecuteDataSet(conn, cmdType, cmdText, null);
            }
        }

        /// <summary>
        /// 执行 SQL 语句返回DataTable
        /// </summary>
        /// <param name="strSQL"></param>
        /// <Addtime>2008-01-19</Addtime>
        /// <returns></returns>
        internal static DataTable GetDataTable(string connectionString, CommandType cmdType, string cmdText)
        {
            DataTable dt = ExecuteDataSet(connectionString, cmdType, cmdText).Tables[0];
            return dt;
        }

        /// <summary>
        /// 执行SQL命令并返回结果集
        /// </summary>
        /// <param name="trans">事务对象</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">SQL语句（存储过程名）</param>
        /// <param name="commandParameters">命令参数</param>
        /// <returns>影响的行数</returns>
        internal static DataSet ExecuteDataSet(OleDbTransaction trans, CommandType cmdType, string cmdText, params OleDbParameter[] commandParameters)
        {
            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandTimeout = CommandTimeout;
            cmd.Transaction = trans;
            PrepareCommand(cmd, trans.Connection, null, cmdType, cmdText, commandParameters);
            OleDbDataAdapter adp = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            cmd.Parameters.Clear();
            return ds;
        }

        /// <summary>
        /// 执行SQL命令并返回结果集
        /// </summary>
        /// <param name="trans">事务对象</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">SQL语句（存储过程名）</param>
        /// <returns>影响的行数</returns>
        internal static DataSet ExecuteDataSet(OleDbTransaction trans, CommandType cmdType, string cmdText)
        {
            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandTimeout = CommandTimeout;
            cmd.Transaction = trans;
            PrepareCommand(cmd, trans.Connection, null, cmdType, cmdText, null);
            OleDbDataAdapter adp = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            cmd.Parameters.Clear();
            return ds;
        }

        /// <summary>
        /// 执行SQL命令并返回Reader对象
        /// </summary>
        /// <param name="conn">连接对象</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">SQL语句（存储过程名）</param>
        /// <param name="commandParameters">命令参数</param>
        /// <returns>返回结果</returns>
        internal static OleDbDataReader ExecuteReader(OleDbConnection conn, CommandType cmdType, string cmdText, params OleDbParameter[] commandParameters)
        {
            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandTimeout = CommandTimeout;
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                OleDbDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        /// <summary>
        /// 执行SQL命令并返回Reader对象
        /// </summary>
        /// <param name="conn">连接对象</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">SQL语句（存储过程名）</param>
        /// <returns>返回结果</returns>
        internal static OleDbDataReader ExecuteReader(OleDbConnection conn, CommandType cmdType, string cmdText)
        {
            return ExecuteReader(conn, cmdType, cmdText);
        }

        /// <summary>
        /// 执行SQL命令并返回Reader对象
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">SQL语句（存储过程名）</param>
        /// <param name="commandParameters">命令参数</param>
        /// <returns>返回结果</returns>
        internal static OleDbDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params OleDbParameter[] commandParameters)
        {
            OleDbConnection conn = new OleDbConnection(connectionString);
            return ExecuteReader(conn, cmdType, cmdText, commandParameters);
        }

        /// <summary>
        /// 执行SQL命令并返回Reader对象
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">SQL语句（存储过程名）</param>
        /// <returns>返回结果</returns>
        internal static OleDbDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText)
        {
            OleDbConnection conn = new OleDbConnection(connectionString);

            return ExecuteReader(conn, cmdType, cmdText, null);

        }

        /// <summary>
        /// 执行SQL命令并返回Reader对象
        /// </summary>
        /// <param name="trans">事务对象</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">SQL语句（存储过程名）</param>
        /// <param name="commandParameters">命令参数</param>
        /// <returns>返回结果</returns>
        internal static OleDbDataReader ExecuteReader(OleDbTransaction trans, CommandType cmdType, string cmdText, params OleDbParameter[] commandParameters)
        {
            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandTimeout = CommandTimeout;
            cmd.Transaction = trans;
            try
            {
                PrepareCommand(cmd, trans.Connection, null, cmdType, cmdText, commandParameters);
                OleDbDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                trans.Connection.Close();
                throw;
            }
        }

        /// <summary>
        /// 执行SQL命令并返回Reader对象
        /// </summary>
        /// <param name="trans">事务对象</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">SQL语句（存储过程名）</param>
        /// <returns>返回结果</returns>
        internal static OleDbDataReader ExecuteReader(OleDbTransaction trans, CommandType cmdType, string cmdText)
        {
            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandTimeout = CommandTimeout;
            cmd.Transaction = trans;
            try
            {
                PrepareCommand(cmd, trans.Connection, null, cmdType, cmdText, null);
                OleDbDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                trans.Connection.Close();
                throw;
            }
        }

        /// <summary>
        /// 执行SQL命令并返回Scalar
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        /// <returns>返回结果</returns>
        internal static object ExecuteScalar(OleDbConnection connection, CommandType cmdType, string cmdText, params OleDbParameter[] commandParameters)
        {
            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandTimeout = CommandTimeout;
            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// 执行SQL命令并返回Scalar
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">命令文本</param>
        /// <returns>返回结果</returns>
        internal static object ExecuteScalar(OleDbConnection connection, CommandType cmdType, string cmdText)
        {
            return ExecuteScalar(connection, cmdType, cmdText);
        }



        /// <summary>
        /// 执行SQL命令并返回Scalar
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">命令文本</param>
        /// <returns>返回结果</returns>
        internal static object ExecuteScalar(String connectionString, CommandType cmdType, string cmdText)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                return ExecuteScalar(conn, cmdType, cmdText, null);
            }
        }

        /// <summary>
        /// 执行SQL命令并返回Scalar
        /// </summary>
        /// <param name="trans">事务对象</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        /// <returns>返回结果</returns>
        internal static object ExecuteScalar(OleDbTransaction trans, CommandType cmdType, string cmdText, params OleDbParameter[] commandParameters)
        {
            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandTimeout = CommandTimeout;
            cmd.Transaction = trans;
            PrepareCommand(cmd, trans.Connection, null, cmdType, cmdText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// 执行SQL命令并返回Scalar
        /// </summary>
        /// <param name="trans">事务对象</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">命令文本</param>
        /// <returns>返回结果</returns>
        internal static object ExecuteScalar(OleDbTransaction trans, CommandType cmdType, string cmdText)
        {
            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandTimeout = CommandTimeout;
            cmd.Transaction = trans;
            PrepareCommand(cmd, trans.Connection, null, cmdType, cmdText, null);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// 添加参数数组到缓存中
        /// </summary>
        /// <param name="cacheKey">缓存的Key</param>
        /// <param name="cmdParms">要被缓存的参数数组</param>
        internal static void CacheParameters(String cacheKey, params OleDbParameter[] commandParameters)
        {
            parmCache.Add(cacheKey, commandParameters);
        }

        /// <summary>
        /// 从缓存中获取参数
        /// </summary>
        /// <param name="cacheKey">缓存的Key</param>
        /// <returns>缓存参数数组</returns>
        internal static OleDbParameter[] GetCachedParameters(String cacheKey)
        {
            OleDbParameter[] cachedParms = (OleDbParameter[])parmCache[cacheKey];

            if (cachedParms == null)
                return null;

            OleDbParameter[] clonedParms = new OleDbParameter[cachedParms.Length];

            for (int i = 0, j = cachedParms.Length; i < j; i++)
                clonedParms[i] = (OleDbParameter)((ICloneable)cachedParms[i]).Clone();

            return clonedParms;
        }

        /// <summary>
        /// 准备SQL命令参数
        /// </summary>
        /// <param name="cmd">命令对象</param>
        /// <param name="conn">连接对象</param>
        /// <param name="trans">事务对象</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">SQL语句（存储过程名）</param>
        /// <param name="cmdParms">命令参数集合</param>
        private static void PrepareCommand(OleDbCommand cmd, OleDbConnection conn, OleDbTransaction trans, CommandType cmdType, string cmdText, OleDbParameter[] cmdParms)
        {

            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (OleDbParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }

        /// <summary>
        /// 获取自增1ID
        /// </summary>
        /// <param name="FieldName"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        internal static int GetMaxID(string FieldName, string TableName)
        {
            string strsql = "select max(" + FieldName + ")+1 from " + TableName;
            object obj = GetSingle(strsql);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return int.Parse(obj.ToString());
            }
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        internal static object GetSingle(string SQLString)
        {
            using (OleDbConnection connection = new OleDbConnection(connString))
            {
                using (OleDbCommand cmd = new OleDbCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (Exception Ex)
                    {
                        throw Ex;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        internal static object GetSingle(string SQLString, params OleDbParameter[] cmdParms)
        {
            using (OleDbConnection connection = new OleDbConnection(connString))
            {
                using (OleDbCommand cmd = new OleDbCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, CommandType.Text, SQLString, cmdParms);
                        object obj = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (Exception Ex)
                    {
                        throw Ex;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 查询是否存在
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        internal static bool Exists(string strSql)
        {
            object obj = GetSingle(strSql);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 查询是否存在
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        internal static bool Exists(string strSql, params OleDbParameter[] cmdParms)
        {
            object obj = GetSingle(strSql, cmdParms);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 分析SQL列表
        /// </summary>
        /// <param name="spList"></param>
        /// <returns></returns>
        private static List<OleDbParams> SetOleDbParams(List<OleDbParams> spList)
        {
            OleDbParams sp;
            List<OleDbParams> tempspList = new List<OleDbParams>();
            StringBuilder cmdText = new StringBuilder();
            List<OleDbParameter> cmdParms = new List<OleDbParameter>();
            foreach (OleDbParams item in spList)
            {
                cmdText.Append(item.SqlTxt);
                cmdText.Append(";");
                cmdParms.AddRange(item.Parameter);

                if (cmdParms.Count > 1000)
                {
                    sp = new OleDbParams(cmdText.ToString(), cmdParms.ToArray());
                    tempspList.Add(sp);
                    cmdText.Clear();
                    cmdParms.Clear();
                }
            }
            if (!string.IsNullOrEmpty(cmdText.ToString()))
            {
                sp = new OleDbParams(cmdText.ToString(), cmdParms.ToArray());
                tempspList.Add(sp);
            }
            return tempspList;
        }
        /// <summary>
        /// 执行SQL命令并返回影响的行数
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="cmdType">命名类型</param>
        /// <param name="cmdText">SQL语句（存储过程名）</param>
        /// <param name="commandParameters">命令参数集合</param>
        /// <returns>影响的行数</returns>
        internal static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params OleDbParameter[] commandParameters)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                //记录操作-----------
                string val = "";
                int i = 1;
                foreach (OleDbParameter item in commandParameters)
                {
                    if (i < commandParameters.Length)
                        val += item.Value + "&";
                    else
                        val += item.Value;
                    i++;
                }
                //string sql = @"Insert Into [T_ReleaseTable] (ReleaseTableGuid,ReleaseSql,ReleaseWhere,ReleaseClientGuid,ReleaseOrder)Values(?,?,?,?,?)";
                //OleDbParameter[] parms = new OleDbParameter[5];
                //parms[0] = new OleDbParameter("ReleaseTableGuid", Guid.NewGuid());
                //parms[1] = new OleDbParameter("ReleaseSql", cmdText);
                //parms[2] = new OleDbParameter("ReleaseWhere", val);
                //parms[3] = new OleDbParameter("ReleaseClientGuid", clientGuid);
                //parms[4] = new OleDbParameter("ReleaseOrder", DateTime.Now.Ticks);

                //ExecuteNonQuery(conn, cmdType, sql, parms);
                //-------------
                return ExecuteNonQuery(conn, cmdType, cmdText, commandParameters);
            }
        }

        /// <summary>
        /// 批量执行SQL语句（带事务）
        /// </summary>
        /// <param name="spList"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        internal static bool ExecuteSQL(List<OleDbParams> spList, OleDbTransaction tran)
        {
            List<OleDbParams> tempspList = SetOleDbParams(spList);

            foreach (OleDbParams item in tempspList)
            {
                //记录操作-----------
                string val = "";
                int i = 1;
                foreach (OleDbParameter item2 in item.Parameter)
                {
                    if (i < item.Parameter.Length)
                        val += item2.Value + "&";
                    else
                        val += item2.Value;
                    i++;
                }
                //string sql = @"Insert Into [T_ReleaseTable] (ReleaseTableGuid,ReleaseSql,ReleaseWhere,ReleaseClientGuid,ReleaseOrder)Values(?,?,?,?,?)";
                //OleDbParameter[] parms = new OleDbParameter[5];
                //parms[0] = new OleDbParameter("ReleaseTableGuid", Guid.NewGuid());
                //parms[1] = new OleDbParameter("ReleaseSql", item.SqlTxt);
                //parms[2] = new OleDbParameter("ReleaseWhere", val);
                //parms[3] = new OleDbParameter("ReleaseClientGuid", clientGuid); 
                //parms[4] = new OleDbParameter("ReleaseOrder", DateTime.Now.Ticks);

               // ExecuteScalar(tran, CommandType.Text, sql, parms);
                //-------------

                ExecuteScalar(tran, CommandType.Text, item.SqlTxt, item.Parameter);
            }
            return true;
        }

        /// <summary>
        /// 执行SQL命令并返回Scalar
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        /// <returns>返回结果</returns>
        internal static object ExecuteScalar(String connectionString, CommandType cmdType, string cmdText, params OleDbParameter[] commandParameters)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                //记录操作-----------
                string val = "";
                int i = 1;
                foreach (OleDbParameter item in commandParameters)
                {
                    if (i < commandParameters.Length)
                        val += item.Value + "&";
                    else
                        val += item.Value;
                    i++;
                }
                //string sql = @"Insert Into [T_ReleaseTable] (ReleaseTableGuid,ReleaseSql,ReleaseWhere,ReleaseClientGuid,ReleaseOrder)Values(?,?,?,?,?)";
                //OleDbParameter[] parms = new OleDbParameter[5];
                //parms[0] = new OleDbParameter("ReleaseTableGuid", Guid.NewGuid());
                //parms[1] = new OleDbParameter("ReleaseSql", cmdText);
                //parms[2] = new OleDbParameter("ReleaseWhere", val);
                //parms[3] = new OleDbParameter("ReleaseClientGuid", clientGuid);
                //parms[4] = new OleDbParameter("ReleaseOrder", DateTime.Now.Ticks);

                //ExecuteScalar(conn, cmdType, sql, parms);
                //-------------
                return ExecuteScalar(conn, cmdType, cmdText, commandParameters);
            }
        }
        /// <summary>
        /// 批量查询SQL列表
        /// </summary>
        /// <param name="spList"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        internal static DataSet ExecuteToDataSet(List<OleDbParams> spList)
        {
            DataSet ds = new DataSet();
            List<OleDbParams> tempspList = SetOleDbParams(spList);

            foreach (OleDbParams item in tempspList)
            {
                ds.Merge(ExecuteDataSet(connString, CommandType.Text, item.SqlTxt, item.Parameter));
            }
            return ds;
        }
    }
}
