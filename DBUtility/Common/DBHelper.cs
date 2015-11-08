
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
        /// ���ݿ������ַ���
        /// </summary>
        internal static string connString = DataUtility.connString;
        /// <summary>
        /// ����Hashtable�����������
        /// </summary>
        private static Dictionary<String, OleDbParameter[]> parmCache = new Dictionary<string, OleDbParameter[]>();

        /// <summary>
        /// ִ�е�����ɾ�Ĳ���
        /// </summary>
        /// <param name="strSQL">��ɾ�����</param>
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
        /// ���ز�ѯ�����
        /// </summary>
        /// <param name="strSQL">��ѯ�ַ���</param>
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
        /// ִ��SQL�������Ӱ�������
        /// </summary>
        /// <param name="connection">���Ӷ���</param>
        /// <param name="cmdType">��������</param>
        /// <param name="cmdText">SQL��䣨�洢��������</param>
        /// <param name="commandParameters">�������</param>
        /// <returns>Ӱ�������</returns>
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
        /// ִ��SQL�������Ӱ�������
        /// </summary>
        /// <param name="connection">���Ӷ���</param>
        /// <param name="cmdType">��������</param>
        /// <param name="cmdText">SQL��䣨�洢��������</param>
        /// <returns>Ӱ�������</returns>
        internal static int ExecuteNonQuery(OleDbConnection connection, CommandType cmdType, string cmdText)
        {
            return ExecuteNonQuery(connection, cmdType, cmdText, null);
        }

       
        /// <summary>
        /// ִ��SQL�������Ӱ�������
        /// </summary>
        /// <param name="connectionString">�����ַ���</param>
        /// <param name="cmdType">��������</param>
        /// <param name="cmdText">SQL��䣨�洢��������</param>
        /// <returns>Ӱ�������</returns>
        internal static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                return ExecuteNonQuery(conn, cmdType, cmdText, null);
            }
        }

        /// <summary>
        /// ִ��SQL�������Ӱ�������
        /// </summary>
        /// <param name="trans">�������</param>
        /// <param name="cmdType">��������</param>
        /// <param name="cmdText">SQL��䣨�洢��������</param>
        /// <param name="commandParameters">�������</param>
        /// <returns>Ӱ�������</returns>
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
        /// ִ��SQL�������Ӱ�������
        /// </summary>
        /// <param name="trans">�������</param>
        /// <param name="cmdType">��������</param>
        /// <param name="cmdText">SQL��䣨�洢��������</param>
        /// <param name="commandParameters">�������</param>
        /// <returns>Ӱ�������</returns>
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
        /// ִ��SQL�������Ӱ�������
        /// </summary>
        /// <param name="trans">�������</param>
        /// <param name="cmdType">��������</param>
        /// <param name="cmdText">SQL��䣨�洢��������</param>
        /// <param name="commandParameters">�������</param>
        /// <param name="Identity">���ر�ʾ</param>
        /// <returns>Ӱ�������</returns>
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
        /// ִ��SQL������ؽ����
        /// </summary>
        /// <param name="connection">���Ӷ���</param>
        /// <param name="cmdType">��������</param>
        /// <param name="cmdText">SQL��䣨�洢��������</param>
        /// <param name="commandParameters">�������</param>
        /// <returns>Ӱ�������</returns>
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
        /// ִ��SQL������ؽ����
        /// </summary>
        /// <param name="connection">���Ӷ���</param>
        /// <param name="cmdType">��������</param>
        /// <param name="cmdText">SQL��䣨�洢��������</param>
        /// <returns>Ӱ�������</returns>
        internal static DataSet ExecuteDataSet(OleDbConnection connection, CommandType cmdType, string cmdText)
        {
            return ExecuteDataSet(connection, cmdType, cmdText, null);
        }

        /// <summary>
        /// ִ��SQL������ؽ����
        /// </summary>
        /// <param name="connectionString">�����ַ���</param>
        /// <param name="cmdType">��������</param>
        /// <param name="cmdText">SQL��䣨�洢��������</param>
        /// <param name="commandParameters">�����������</param>
        /// <returns>Ӱ�������</returns>
        internal static DataSet ExecuteDataSet(string connectionString, CommandType cmdType, string cmdText, params OleDbParameter[] commandParameters)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                return ExecuteDataSet(conn, cmdType, cmdText, commandParameters);
            }
        }

        /// <summary>
        /// ִ��SQL������ؽ����
        /// </summary>
        /// <param name="connectionString">�����ַ���</param>
        /// <param name="cmdType">��������</param>
        /// <param name="cmdText">SQL��䣨�洢��������</param>
        /// <returns>Ӱ�������</returns>
        internal static DataSet ExecuteDataSet(string connectionString, CommandType cmdType, string cmdText)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                return ExecuteDataSet(conn, cmdType, cmdText, null);
            }
        }

        /// <summary>
        /// ִ�� SQL ��䷵��DataTable
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
        /// ִ��SQL������ؽ����
        /// </summary>
        /// <param name="trans">�������</param>
        /// <param name="cmdType">��������</param>
        /// <param name="cmdText">SQL��䣨�洢��������</param>
        /// <param name="commandParameters">�������</param>
        /// <returns>Ӱ�������</returns>
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
        /// ִ��SQL������ؽ����
        /// </summary>
        /// <param name="trans">�������</param>
        /// <param name="cmdType">��������</param>
        /// <param name="cmdText">SQL��䣨�洢��������</param>
        /// <returns>Ӱ�������</returns>
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
        /// ִ��SQL�������Reader����
        /// </summary>
        /// <param name="conn">���Ӷ���</param>
        /// <param name="cmdType">��������</param>
        /// <param name="cmdText">SQL��䣨�洢��������</param>
        /// <param name="commandParameters">�������</param>
        /// <returns>���ؽ��</returns>
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
        /// ִ��SQL�������Reader����
        /// </summary>
        /// <param name="conn">���Ӷ���</param>
        /// <param name="cmdType">��������</param>
        /// <param name="cmdText">SQL��䣨�洢��������</param>
        /// <returns>���ؽ��</returns>
        internal static OleDbDataReader ExecuteReader(OleDbConnection conn, CommandType cmdType, string cmdText)
        {
            return ExecuteReader(conn, cmdType, cmdText);
        }

        /// <summary>
        /// ִ��SQL�������Reader����
        /// </summary>
        /// <param name="connectionString">�����ַ���</param>
        /// <param name="cmdType">��������</param>
        /// <param name="cmdText">SQL��䣨�洢��������</param>
        /// <param name="commandParameters">�������</param>
        /// <returns>���ؽ��</returns>
        internal static OleDbDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params OleDbParameter[] commandParameters)
        {
            OleDbConnection conn = new OleDbConnection(connectionString);
            return ExecuteReader(conn, cmdType, cmdText, commandParameters);
        }

        /// <summary>
        /// ִ��SQL�������Reader����
        /// </summary>
        /// <param name="connectionString">�����ַ���</param>
        /// <param name="cmdType">��������</param>
        /// <param name="cmdText">SQL��䣨�洢��������</param>
        /// <returns>���ؽ��</returns>
        internal static OleDbDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText)
        {
            OleDbConnection conn = new OleDbConnection(connectionString);

            return ExecuteReader(conn, cmdType, cmdText, null);

        }

        /// <summary>
        /// ִ��SQL�������Reader����
        /// </summary>
        /// <param name="trans">�������</param>
        /// <param name="cmdType">��������</param>
        /// <param name="cmdText">SQL��䣨�洢��������</param>
        /// <param name="commandParameters">�������</param>
        /// <returns>���ؽ��</returns>
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
        /// ִ��SQL�������Reader����
        /// </summary>
        /// <param name="trans">�������</param>
        /// <param name="cmdType">��������</param>
        /// <param name="cmdText">SQL��䣨�洢��������</param>
        /// <returns>���ؽ��</returns>
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
        /// ִ��SQL�������Scalar
        /// </summary>
        /// <param name="connection">���Ӷ���</param>
        /// <param name="cmdType">��������</param>
        /// <param name="cmdText">�����ı�</param>
        /// <param name="commandParameters">�������</param>
        /// <returns>���ؽ��</returns>
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
        /// ִ��SQL�������Scalar
        /// </summary>
        /// <param name="connection">���Ӷ���</param>
        /// <param name="cmdType">��������</param>
        /// <param name="cmdText">�����ı�</param>
        /// <returns>���ؽ��</returns>
        internal static object ExecuteScalar(OleDbConnection connection, CommandType cmdType, string cmdText)
        {
            return ExecuteScalar(connection, cmdType, cmdText);
        }



        /// <summary>
        /// ִ��SQL�������Scalar
        /// </summary>
        /// <param name="connectionString">�����ַ���</param>
        /// <param name="cmdType">��������</param>
        /// <param name="cmdText">�����ı�</param>
        /// <returns>���ؽ��</returns>
        internal static object ExecuteScalar(String connectionString, CommandType cmdType, string cmdText)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                return ExecuteScalar(conn, cmdType, cmdText, null);
            }
        }

        /// <summary>
        /// ִ��SQL�������Scalar
        /// </summary>
        /// <param name="trans">�������</param>
        /// <param name="cmdType">��������</param>
        /// <param name="cmdText">�����ı�</param>
        /// <param name="commandParameters">�������</param>
        /// <returns>���ؽ��</returns>
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
        /// ִ��SQL�������Scalar
        /// </summary>
        /// <param name="trans">�������</param>
        /// <param name="cmdType">��������</param>
        /// <param name="cmdText">�����ı�</param>
        /// <returns>���ؽ��</returns>
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
        /// ��Ӳ������鵽������
        /// </summary>
        /// <param name="cacheKey">�����Key</param>
        /// <param name="cmdParms">Ҫ������Ĳ�������</param>
        internal static void CacheParameters(String cacheKey, params OleDbParameter[] commandParameters)
        {
            parmCache.Add(cacheKey, commandParameters);
        }

        /// <summary>
        /// �ӻ����л�ȡ����
        /// </summary>
        /// <param name="cacheKey">�����Key</param>
        /// <returns>�����������</returns>
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
        /// ׼��SQL�������
        /// </summary>
        /// <param name="cmd">�������</param>
        /// <param name="conn">���Ӷ���</param>
        /// <param name="trans">�������</param>
        /// <param name="cmdType">��������</param>
        /// <param name="cmdText">SQL��䣨�洢��������</param>
        /// <param name="cmdParms">�����������</param>
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
        /// ��ȡ����1ID
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
        /// ִ��һ�������ѯ�����䣬���ز�ѯ�����object����
        /// </summary>
        /// <param name="SQLString">�����ѯ������</param>
        /// <returns>��ѯ�����object��</returns>
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
        /// ִ��һ�������ѯ�����䣬���ز�ѯ�����object����
        /// </summary>
        /// <param name="SQLString">�����ѯ������</param>
        /// <returns>��ѯ�����object��</returns>
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
        /// ��ѯ�Ƿ����
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
        /// ��ѯ�Ƿ����
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
        /// ����SQL�б�
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
        /// ִ��SQL�������Ӱ�������
        /// </summary>
        /// <param name="connectionString">�����ַ���</param>
        /// <param name="cmdType">��������</param>
        /// <param name="cmdText">SQL��䣨�洢��������</param>
        /// <param name="commandParameters">�����������</param>
        /// <returns>Ӱ�������</returns>
        internal static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params OleDbParameter[] commandParameters)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                //��¼����-----------
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
        /// ����ִ��SQL��䣨������
        /// </summary>
        /// <param name="spList"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        internal static bool ExecuteSQL(List<OleDbParams> spList, OleDbTransaction tran)
        {
            List<OleDbParams> tempspList = SetOleDbParams(spList);

            foreach (OleDbParams item in tempspList)
            {
                //��¼����-----------
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
        /// ִ��SQL�������Scalar
        /// </summary>
        /// <param name="connectionString">�����ַ���</param>
        /// <param name="cmdType">��������</param>
        /// <param name="cmdText">�����ı�</param>
        /// <param name="commandParameters">�������</param>
        /// <returns>���ؽ��</returns>
        internal static object ExecuteScalar(String connectionString, CommandType cmdType, string cmdText, params OleDbParameter[] commandParameters)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                //��¼����-----------
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
        /// ������ѯSQL�б�
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
