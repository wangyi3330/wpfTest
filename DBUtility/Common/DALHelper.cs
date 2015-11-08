using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Carrot.Model;
using System.Web;
using System.Data.OleDb;
using Carrot.DataCommon;
using System.Configuration;


namespace Carrot.DBUtility
{
    public class DALHelper : DBHelper
    {

        #region 公共静态方法


        /// <summary>
        /// 检查是否为空
        /// </summary>
        /// <param name="str">检查值</param>
        /// <returns>返回值</returns>
        public static object IsEmpty(string str)
        {
            if (string.IsNullOrEmpty(str))
                return DBNull.Value;
            else
                return str;
        }
        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="ex"></param>
        public static int InsertError(Exception ex, string Operator)
        {

            string ID = DateTime.Now.Ticks.ToString();
            DBHelper dbhelper = new DBHelper();
            string strSQL = @"INSERT INTO T_ErrorLog (ErrorSource,ErrorMessage,StackTrace,TargetSite,Operator) Values('" + ex.Source + "','" + ex.Message + "','" + ex.StackTrace + "','" + ex.TargetSite.Name + "','" + Operator + @"'); SELECT  @@IDENTITY as ID";

            object obj = ExecuteScalar(connString, CommandType.Text, strSQL).ToString();
            return Convert.ToInt32(obj);
        }
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="ex"></param>
        public static void InsertLog(string sql, string Operator)
        {
            DBHelper dbhelper = new DBHelper();
            string strSQL = @"INSERT INTO T_Log
                                        (LogSql,Operator)
                                           Values
                                        ('" + sql + "','" + Operator + "')";

            ExecuteNonQuery(connString, CommandType.Text, strSQL).ToString();
        }
        #endregion

        #region 插入新记录

        /// <summary>
        /// 插入新记录
        /// </summary>
        /// <param name="entity">插入数据</param>
        /// <returns>成功：返回插入ID | 不成功：返回错误代号</returns>
        public static int Insert<T>(T entity, string Operator)
        {
            return Insert(entity, null, Operator);
        }

        /// <summary>
        /// 插入新记录(单条/带事务)
        /// </summary>
        /// <param name="entity">插入数据</param>
        /// <returns>成功：返回插入ID | 不成功：抛出异常</returns>
        public static int Insert<T>(T entity, OleDbTransaction tran, string Operator)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSqlValues = new StringBuilder();
            List<OleDbParameter> pl = new List<OleDbParameter>();
            PropertyInfo[] properties = entity.GetType().GetProperties();
            //初始化SQL
            strSql.Append("INSERT INTO [T_" + entity.GetType().Name.Remove(entity.GetType().Name.Length - 4) + "]  (");
            strSqlValues.Append(")VALUES(");
            //分析Model
            FillStrSql(entity, ref pl, ref strSql, ref strSqlValues, properties, 1);
            //组合SQL
            string returnsql = strSql.ToString() + strSqlValues.Append(")").ToString();
            //填充参数
            OleDbParameter[] parms = pl.ToArray();
            try
            {
                if (tran == null)
                    ExecuteScalar(connString, CommandType.Text, returnsql, parms);
                else
                    ExecuteScalar(tran, CommandType.Text, returnsql, parms);

                InsertLog(returnsql.Replace("'", ""), Operator);
                return 0;

            }
            catch (Exception ex)
            {
                return DALHelper.InsertError(ex, Operator);
            }
        }

        /// <summary>
        /// 批量插入新记录(批量/带事务)
        /// </summary>
        /// <param name="entity">插入数据</param>
        /// <returns>成功：返回插入ID | 不成功：抛出异常</returns>
        public static int Insert<T>(List<T> entityList, OleDbTransaction tran, string Operator)
        {
            if (entityList.Count <= 0)
                return 1;
            List<OleDbParams> opList = new List<OleDbParams>();
            foreach (T entity in entityList)
            {
                StringBuilder strSql = new StringBuilder();
                StringBuilder strSqlValues = new StringBuilder();
                List<OleDbParameter> pl = new List<OleDbParameter>();
                PropertyInfo[] properties = entity.GetType().GetProperties();
                //初始化SQL
                strSql.Append("INSERT INTO [T_" + entity.GetType().Name.Remove(entity.GetType().Name.Length - 4) + "] (");
                strSqlValues.Append(")VALUES(");
                //分析Model
                FillStrSql(entity, ref pl, ref strSql, ref strSqlValues, properties, 1);
                //组合SQL
                string returnsql = strSql.ToString() + strSqlValues.Append(")").ToString();
                //填充参数
                OleDbParameter[] parms = pl.ToArray();
                opList.Add(new OleDbParams(returnsql, parms));
            }
            try
            {
                ExecuteSQL(opList, tran);
                return 0;
            }
            catch (Exception ex)
            {
                return DALHelper.InsertError(ex, Operator);
            }
        }
        /// <summary>
        /// 批量插入新记录(批量/带事务)
        /// </summary>
        /// <param name="entityList">插入数据</param>
        /// <param name="tran">事务</param>
        /// <returns></returns>
        public static int Insert(List<ModelWhereParams> entityList, OleDbTransaction tran, string Operator)
        {
            if (entityList.Count <= 0)
                return 1;
            List<OleDbParams> opList = new List<OleDbParams>();
            foreach (ModelWhereParams entity in entityList)
            {
                if (entity.WhereParameter == null || entity.WhereParameter.Count > 0)
                {
                    StringBuilder strSql = new StringBuilder();
                    StringBuilder strSqlValues = new StringBuilder();
                    List<OleDbParameter> pl = new List<OleDbParameter>();
                    PropertyInfo[] properties = entity.Model.GetType().GetProperties();
                    //初始化SQL
                    strSql.Append("INSERT INTO [T_" + entity.Model.GetType().Name.Remove(entity.Model.GetType().Name.Length - 4) + "] (");
                    strSqlValues.Append(")VALUES(");
                    //分析Model
                    FillStrSql(entity.Model, ref pl, ref strSql, ref strSqlValues, properties, 1);
                    //组合SQL
                    string returnsql = strSql.ToString() + strSqlValues.Append(")").ToString();
                    //填充参数
                    OleDbParameter[] parms = pl.ToArray();
                    opList.Add(new OleDbParams(returnsql, parms));
                }
            }
            try
            {
                ExecuteSQL(opList, tran);
                return 0;
            }
            catch (Exception ex)
            {
                return DALHelper.InsertError(ex, Operator);
            }
        }
        #endregion

        #region 更新记录
        /// <summary>
        /// 根据参数更新记录(单条/带事务)
        /// </summary>
        /// <param name="entity">更新数据</param>
        /// <param name="whereParameter">更新条件</param>
        /// <returns>成功：1 | 不成功：抛出异常</returns>
        public static int Update(ModelWhereParams entity, OleDbTransaction tran, string Operator)
        {
            if (entity.WhereParameter == null || entity.WhereParameter.Count > 0)
            {
                StringBuilder strSql = new StringBuilder();
                StringBuilder strSqlValues = new StringBuilder();//无效但不能删除
                StringBuilder strSqlWhere = new StringBuilder();
                List<OleDbParameter> pl = new List<OleDbParameter>();
                PropertyInfo[] properties = entity.Model.GetType().GetProperties();
                //初始化SQL
                strSql.Append("UPDATE [T_" + entity.Model.GetType().Name.Remove(entity.Model.GetType().Name.Length - 4) + "] SET ");
                //分析Model
                FillStrSql(entity.Model, ref pl, ref strSql, ref strSqlValues, properties, 2);
                //组合SQL
                string returnsql = FillStrWhere(entity.WhereParameter, strSql, ref pl);
                //填充参数
                OleDbParameter[] parms = pl.ToArray();
                try
                {
                    if (tran == null)
                        ExecuteNonQuery(connString, CommandType.Text, returnsql, parms);
                    else
                        ExecuteNonQuery(tran, CommandType.Text, returnsql, parms);

                    InsertLog(returnsql.Replace("'", ""), Operator);
                    return 0;
                }
                catch (Exception ex)
                {
                    return DALHelper.InsertError(ex, Operator);
                }
            }
            else
                return 1;
        }
        /// <summary>
        /// 批量根据参数更新记录(批量/带事务)
        /// </summary>
        /// <param name="entity">更新数据</param>
        /// <param name="whereParameter">更新条件</param>
        /// <returns>成功：1 | 不成功：抛出异常</returns>
        public static int Update(List<ModelWhereParams> entityList, OleDbTransaction tran, string Operator)
        {
            if (entityList.Count <= 0)
                return 1;
            List<OleDbParams> opList = new List<OleDbParams>();
            foreach (ModelWhereParams entity in entityList)
            {
                if (entity.WhereParameter == null || entity.WhereParameter.Count > 0)
                {
                    StringBuilder strSql = new StringBuilder();
                    StringBuilder strSqlValues = new StringBuilder();//无效但不能删除
                    StringBuilder strSqlWhere = new StringBuilder();
                    List<OleDbParameter> pl = new List<OleDbParameter>();
                    PropertyInfo[] properties = entity.Model.GetType().GetProperties();
                    //初始化SQL
                    strSql.Append("UPDATE [T_" + entity.Model.GetType().Name.Remove(entity.Model.GetType().Name.Length - 4) + "] SET ");
                    //分析Model
                    FillStrSql(entity.Model, ref pl, ref strSql, ref strSqlValues, properties, 2);
                    //组合SQL
                    string returnsql = FillStrWhere(entity.WhereParameter, strSql, ref pl);
                    //填充参数
                    OleDbParameter[] parms = pl.ToArray();
                    opList.Add(new OleDbParams(returnsql, parms));
                }
            }
            try
            {
                if (ExecuteSQL(opList, tran))
                    return 0;
                else
                    return 1;
            }
            catch (Exception ex)
            {
                return DALHelper.InsertError(ex, Operator);
            }
        }
        #endregion

        #region 删除记录

        /// <summary>
        /// 根据参数删除记录
        /// </summary>
        /// <param name="whereParameter">参数</param>
        /// <returns>成功：1 | 不成功：返回错误代号</returns>
        public static int Delete(ModelWhereParams entity, string Operator)
        {
            try
            {
                return Delete(entity, null, Operator);
            }
            catch
            {
                return 1;
            }

        }
        /// <summary>
        /// 根据参数删除记录(带事务)
        /// </summary>
        /// <param name="whereParameter">参数</param>
        /// <returns>成功：1 | 不成功：抛出异常</returns>
        private static int Delete(ModelWhereParams entity, OleDbTransaction tran, string Operator)
        {
            if (entity.WhereParameter == null || entity.WhereParameter.Count > 0)
            {
                StringBuilder strSqlWhere = new StringBuilder();
                List<OleDbParameter> pl = new List<OleDbParameter>();
                strSqlWhere.Append("DELETE FROM [T_" + entity.Model.GetType().Name.Remove(entity.Model.GetType().Name.Length - 4) + "] ");
                //组合SQL
                string returnsql = FillStrWhere(entity.WhereParameter, strSqlWhere, ref pl);
                //填充参数
                OleDbParameter[] parms = pl.ToArray();
                try
                {
                    if (tran == null)
                        ExecuteNonQuery(connString, CommandType.Text, returnsql, parms);
                    else
                        ExecuteNonQuery(tran, CommandType.Text, returnsql, parms);

                    InsertLog(returnsql.Replace("'", ""), Operator);
                    return 0;
                }
                catch (Exception ex)
                {
                    return DALHelper.InsertError(ex, Operator);
                }
            }
            else
                return 1;
        }
        /// <summary>
        /// 批量根据参数删除记录(带事务)
        /// </summary>
        /// <param name="whereParameter">参数</param>
        /// <returns>成功：1 | 不成功：抛出异常</returns>
        public static int Delete(List<ModelWhereParams> entityList, OleDbTransaction tran, string Operator)
        {
            if (entityList.Count <= 0)
                return 1;
            List<OleDbParams> opList = new List<OleDbParams>();
            foreach (ModelWhereParams entity in entityList)
            {
                if (entity.WhereParameter == null || entity.WhereParameter.Count > 0)
                {
                    StringBuilder strSqlWhere = new StringBuilder();
                    List<OleDbParameter> pl = new List<OleDbParameter>();
                    strSqlWhere.Append("DELETE FROM [T_" + entity.Model.GetType().Name.Remove(entity.Model.GetType().Name.Length - 4) + "] ");
                    //组合SQL
                    string returnsql = FillStrWhere(entity.WhereParameter, strSqlWhere, ref pl);
                    //填充参数
                    OleDbParameter[] parms = pl.ToArray();
                    opList.Add(new OleDbParams(returnsql, parms));
                }
            }
            try
            {
                return ExecuteSQL(opList, tran) ? 0 : 1;
            }
            catch (Exception ex)
            {
                return DALHelper.InsertError(ex, Operator);
            }
        }
        #endregion

        #region 多表查询

        /// <summary>
        /// 链表查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">表</param>
        /// <param name="whereParameter">查询参数</param>
        /// <param name="t">返回实体类型</param>
        /// <param name="Select">Select</param>
        /// <param name="Order">排序</param>
        /// <returns>返回实体</returns>
        public static List<T> GetLists<T>(List<JoinModel> entity, List<WhereParameter> whereParameter, T t, string Select, bool IsSaveSelect, string Order) where T : new()
        {
            StringBuilder strSql = new StringBuilder();//总体sql
            StringBuilder strSqlSelect = new StringBuilder();//select部分
            StringBuilder strSqlFrom = new StringBuilder();//from链表部分
            StringBuilder strSqlWhere = new StringBuilder();//条件部分
            List<OleDbParameter> pl = new List<OleDbParameter>();//参数部分
            strSql.Append("SELECT ");
            strSql.Append(Select);

            int i = 0;
            foreach (JoinModel jm in entity)
            {
                if (IsSaveSelect)
                {
                    PropertyInfo[] properties = jm.ModelA.GetType().GetProperties();
                    foreach (PropertyInfo item in properties)
                    {
                        strSqlSelect.Append(jm.DBTableNameA + "." + item.Name + ",");
                    }
                    PropertyInfo[] properties2 = jm.ModelB.GetType().GetProperties();
                    foreach (PropertyInfo item in properties2)
                    {
                        strSqlSelect.Append(jm.DBTableNameB + "." + item.Name + ",");
                    }
                }
                i++;
                string maintable = jm.DBTableNameA;
                if (i != 1)
                    maintable = "";
                strSqlFrom.Append(maintable + string.Format(jm.JoinParam, jm.DBTableNameB, jm.JoinPropertyNameA, jm.JoinPropertyNameB, jm.JoinPropertyNameC));
            }
            strSql.Append(strSqlSelect.ToString().TrimEnd(','));

            strSql.Append(" FROM ");
            strSql.Append(strSqlFrom.ToString());
            if (!string.IsNullOrEmpty(Order))
                Order = " " + Order;
            //组合SQL
            string returnsql = FillStrWhere(whereParameter, strSql, ref pl) + Order;
            //填充参数
            OleDbParameter[] parms = pl.ToArray();
            List<T> returnList = new List<T>();
            try
            {
                using (OleDbDataReader rdr = ExecuteReader(connString, CommandType.Text, returnsql, parms))
                {
                    //组织查询结果
                    while (rdr.Read())
                    {
                        t = new T();
                        FillClass(ref t, rdr);
                        returnList.Add(t);
                    }
                }
                return returnList;
            }
            catch (Exception ex)
            {
                InsertError(ex, "");
                return null;
            }
        }

        /// <summary>
        /// 获取列表(批量)
        /// </summary>
        /// <param name="whereParameter">参数</param>
        /// <param name="Select">子查询</param>
        /// <param name="t">返回实体</param>
        /// <returns>成功：List<T> | 不成功：返回空实体</returns>
        public static List<object> GetLists(List<ModelWhereParams> entityList)
        {
            if (entityList.Count <= 0)
                return new List<object>();
            List<OleDbParams> opList = new List<OleDbParams>();
            foreach (ModelWhereParams entity in entityList)
            {
                StringBuilder strSql = new StringBuilder();
                StringBuilder strSqlWhere = new StringBuilder();
                StringBuilder strSqlValues = new StringBuilder();
                List<OleDbParameter> pl = new List<OleDbParameter>();
                PropertyInfo[] properties = entity.Model.GetType().GetProperties();

                strSql.Append("SELECT ");

                strSql.Append(entity.Select);

                //分析Model
                FillStrSql(entity.Model, ref pl, ref strSql, ref strSqlValues, properties, 3);
                strSql.Append(" FROM [T_" + entity.Model.GetType().Name.Remove(entity.Model.GetType().Name.Length - 4) + "] ").ToString();
                if (!string.IsNullOrEmpty(entity.Order))
                    entity.Order = " ORDER BY " + entity.Order;
                //组合SQL
                string returnsql = FillStrWhere(entity.WhereParameter, strSql, ref pl) + entity.Order;
                //填充参数
                OleDbParameter[] parms = pl.ToArray();
                opList.Add(new OleDbParams(returnsql, parms));
            }
            try
            {
                return FillDataSetToObject(ExecuteToDataSet(opList), entityList);
            }
            catch (Exception ex)
            {
                DALHelper.InsertError(ex, "");
            }
            return new List<object>();
        }
        #endregion

        /// <summary>
        /// 根据sql返回table
        /// </summary>
        /// <param name="returnsql"></param>
        /// <returns></returns>
        public static DataTable GetSQLList(string returnsql)
        {
            DataSet ds = ExecuteDataSet(connString, CommandType.Text, returnsql);
            if (ds.Tables.Count > 0)
                return ds.Tables[0];
            else
                return null;
        }
        /// <summary>
        /// 根据sql返回table
        /// 考勤表
        /// </summary>
        /// <param name="returnsql"></param>
        /// <returns></returns>
        public static DataTable GetSQLList(string returnsql, string connString)
        {
            DataSet ds = ExecuteDataSet(connString, CommandType.Text, returnsql);
            if (ds.Tables.Count > 0)
                return ds.Tables[0];
            else
                return null;
        }
        #region 获取列表
        /// <summary>
        /// 获取T列表(带参数|带Select|带排序)
        /// </summary>
        /// <param name="whereParameter">参数</param>
        /// <param name="Select">子查询</param>
        /// <param name="t">返回实体</param>
        /// <returns>成功：List<T> | 不成功：返回空实体</returns>
        public static List<T> GetList<T>(T entity, List<WhereParameter> whereParameter, string Select, bool IsSaveSelect, string Order) where T : new()
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSqlWhere = new StringBuilder();
            StringBuilder strSqlValues = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(Select);
            List<OleDbParameter> pl = new List<OleDbParameter>();
            PropertyInfo[] properties = entity.GetType().GetProperties();
            //分析Model
            FillStrSql(entity, ref pl, ref strSql, ref strSqlValues, properties, 3);
            if (!IsSaveSelect)
            {
                strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(Select);
            }
            if (!string.IsNullOrEmpty(Order))
                Order = " ORDER BY " + Order;
            //组合SQL
            strSql.Append(" FROM [T_" + entity.GetType().Name.Remove(entity.GetType().Name.Length - 4) + "] ").ToString();
            string returnsql = FillStrWhere(whereParameter, strSql, ref pl) + Order;
            OleDbParameter[] parms = pl.ToArray();
            List<T> returnList = new List<T>();
            try
            {
                using (OleDbDataReader rdr = ExecuteReader(connString, CommandType.Text, returnsql, parms))
                {
                    //组织查询结果
                    while (rdr.Read())
                    {
                        entity = new T();
                        FillClass(ref entity, rdr);
                        returnList.Add(entity);
                    }
                }

                return returnList;
            }
            catch (Exception ex)
            {
                DALHelper.InsertError(ex, "");
                return new List<T>();
            }
        }
        #endregion

        #region 内部公共函数
        /// <summary>
        /// 填充Sql语句主干
        /// </summary>
        /// <typeparam name="T">任意对象</typeparam>
        /// <param name="entity">对象模版</param>
        /// <param name="pl">参数</param>
        /// <param name="strSql">sql前端</param>
        /// <param name="strSqlValues">sql后端</param>
        /// <param name="properties">对象反射属性数组</param>
        /// <param name="type">1:插入|2:修改|3:查询</param>
        private static void FillStrSql<T>(T entity, ref List<OleDbParameter> pl, ref StringBuilder strSql, ref StringBuilder strSqlValues, PropertyInfo[] properties, int type)
        {
            foreach (PropertyInfo item in properties)
            {
                ColumnAttribute attr = (ColumnAttribute)item.GetCustomAttributes(typeof(ColumnAttribute), false)[0];
                //获取字段名
                string name = item.Name;
                //获取值
                object value = item.GetValue(entity, null);

                if (value != null && attr.IsPrimaryKey != 1 && type != 3)
                {
                    OleDbParameter param = new OleDbParameter(name, attr.ColumnType, attr.WordNumber);
                    if (value.ToString() == new DateTime().ToString())
                        value = null;
                    param.Value = value;
                    if (type == 1)//添加
                    {
                        strSql.Append("" + name + ",");
                        strSqlValues.Append(" ? ,");
                        pl.Add(param);
                    }
                    else if (type == 2 && attr.IsPrimaryKey != 2)//修改
                    {
                        strSql.Append("" + name + "= ? ,");
                        pl.Add(param);
                    }
                }
                else if (type == 3)
                    strSql.Append("" + name + ",");
            }
            if (strSql.Length > 0)
                strSql = strSql.Remove(strSql.Length - 1, 1);
            if (strSqlValues.Length > 0)
                strSqlValues = strSqlValues.Remove(strSqlValues.Length - 1, 1);
        }

        /// <summary>
        /// 填充SQL语句条件
        /// </summary>
        /// <param name="whereParameter"></param>
        /// <param name="strSqlWhere"></param>
        /// <param name="pl"></param>
        /// <returns></returns>
        private static string FillStrWhere(List<WhereParameter> whereParameter, StringBuilder strSqlWhere, ref List<OleDbParameter> pl)
        {
            if (whereParameter == null)//全查
                return strSqlWhere.ToString();
            if (whereParameter.Count > 0)
                strSqlWhere.Append(" WHERE ");
            int num = 0;
            //分析查询参数
            foreach (WhereParameter item in whereParameter)
            {
                string leftkuohao = "";
                string rightkuohao = "";
                num++;
                if (item.kuohao == ")")
                    rightkuohao = ")";
                else if (item.kuohao == "(")
                    leftkuohao = "(";
                else if (item.kuohao == "((")
                    leftkuohao = "((";
                else if (item.kuohao == "))")
                    rightkuohao = "))";
                if (WhereParameter._like == item.AndQuery || WhereParameter._nolike == item.AndQuery)
                {
                    strSqlWhere.Append(leftkuohao + " ? " + string.Format(item.AndQuery.ToString(), item.ParameterName) + rightkuohao + item.OrAndQuery);
                    pl.Add(new OleDbParameter(num.ToString(), item.Value));
                }
                else if (WhereParameter._null == item.AndQuery || WhereParameter._nonull == item.AndQuery)
                    strSqlWhere.Append(" " + leftkuohao + item.ParameterName + item.AndQuery.ToString() + rightkuohao + item.OrAndQuery);
                else if (WhereParameter._notin == item.AndQuery || WhereParameter._in == item.AndQuery)
                    strSqlWhere.Append(" " + leftkuohao + item.ParameterName + string.Format(item.AndQuery.ToString(), item.Value) + rightkuohao + item.OrAndQuery);
                else
                {
                    strSqlWhere.Append(" " + leftkuohao + item.ParameterName + string.Format(item.AndQuery.ToString(), " ? ") + rightkuohao + item.OrAndQuery);
                    pl.Add(new OleDbParameter(num.ToString(), item.Value));
                }
            }
            return strSqlWhere.ToString().TrimEnd('A', 'N', 'D').TrimEnd('O', 'R');
        }

        /// <summary>
        /// 填充对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t">Model类</param>
        /// <param name="rdr">数据库获取结果</param>
        private static void FillClass<T>(ref T t, OleDbDataReader rdr)
        {
            try
            {


                if (t == null)
                    return;
                PropertyInfo[] properties = t.GetType().GetProperties();
                foreach (PropertyInfo item in properties)
                {
                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        if (item.Name.ToUpper() == rdr.GetName(i).ToUpper() && rdr[i] != null && rdr[i].ToString() != "")
                        {
                            ColumnAttribute attr = (ColumnAttribute)item.GetCustomAttributes(typeof(ColumnAttribute), false)[0];
                            if (attr.IsPrimaryKey == 2)
                                item.SetValue(t, new Guid(rdr[i].ToString()), null);
                            else
                            {
                                switch (attr.ColumnType)
                                {
                                    case OleDbType.DBDate: item.SetValue(t, DateTime.Parse(rdr[i].ToString()), null); break;
                                    case OleDbType.Date: item.SetValue(t, DateTime.Parse(rdr[i].ToString()), null); break;
                                    case OleDbType.Double: item.SetValue(t, double.Parse(rdr[i].ToString()), null); break;
                                    case OleDbType.Decimal: item.SetValue(t, float.Parse(rdr[i].ToString()), null); break;
                                    case OleDbType.Integer: item.SetValue(t, int.Parse(rdr[i].ToString()), null); break;
                                    case OleDbType.VarChar: item.SetValue(t, rdr[i].ToString(), null); break;
                                    case OleDbType.Char: item.SetValue(t, rdr[i].ToString(), null); break;
                                    case OleDbType.Guid: item.SetValue(t, new Guid(rdr[i].ToString()), null); break;
                                }

                            }
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        /// <summary>
        /// DataSet转换Class
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="entityList"></param>
        /// <returns></returns>
        private static List<object> FillDataSetToObject(DataSet ds, List<ModelWhereParams> entityList)
        {
            List<object> returnList = new List<object>();
            List<object> objList = null;
            for (int i = 0; i < entityList.Count; i++)
            {
                objList = new List<object>();
                PropertyInfo[] properties = entityList[i].Model.GetType().GetProperties();
                for (int ii = 0; ii < ds.Tables[i].Rows.Count; ii++)//一行
                {
                    DataRow dr = ds.Tables[i].Rows[ii];
                    //约束object
                    entityList[i].Model = entityList[i].Model.GetType().InvokeMember("Refection", BindingFlags.CreateInstance, null, null, null);
                    foreach (PropertyInfo item in properties)
                    {
                        foreach (DataColumn dcc in dr.Table.Columns)
                        {
                            if (item.Name.ToUpper() == dcc.ColumnName.ToUpper() && dr[dcc].ToString() != "")
                            {
                                ColumnAttribute attr = (ColumnAttribute)item.GetCustomAttributes(typeof(ColumnAttribute), false)[0];
                                if (attr.IsPrimaryKey == 2)
                                    item.SetValue(entityList[i].Model, new Guid(dr[dcc].ToString()), null);
                                else
                                {
                                    switch (attr.ColumnType)
                                    {
                                        case OleDbType.Date: item.SetValue(entityList[i].Model, DateTime.Parse(dr[dcc].ToString()), null); break;
                                        case OleDbType.Double: item.SetValue(entityList[i].Model, double.Parse(dr[dcc].ToString()), null); break;
                                        case OleDbType.Integer: item.SetValue(entityList[i].Model, int.Parse(dr[dcc].ToString()), null); break;
                                        case OleDbType.VarChar: item.SetValue(entityList[i].Model, dr[dcc].ToString(), null); break;
                                        case OleDbType.Char: item.SetValue(entityList[i].Model, dr[dcc].ToString(), null); break;
                                        case OleDbType.Guid: item.SetValue(entityList[i].Model, new Guid(dr[dcc].ToString()), null); break;
                                    }
                                }
                                break;
                            }
                        }
                    }
                    objList.Add(entityList[i].Model);
                }
                returnList.Add(objList);
            }
            return returnList;
        }


        #endregion
    }
}