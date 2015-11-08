using Carrot.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace Carrot.DBUtility
{
    public class PublicDAL
    {
        #region 插入
        /// <summary>
        /// 插入错误日志
        /// </summary>
        /// <param name="ex"></param>
        public static int InsertError(Exception ex, string Operator)
        {
            return DALHelper.InsertError(ex, Operator);
        }
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="ai"></param>
        public static int Insert<T>(T t, string Operator)
        {
            return DALHelper.Insert(t, Operator);
        }
        /// <summary>
        /// 批量插入(不同类)
        /// </summary>
        /// <param name="ai"></param>
        public static int Insert(List<object> ai, string Operator)
        {
            OleDbConnection Conn = new OleDbConnection(DALHelper.connString);
            Conn.Open();
            OleDbTransaction tran = Conn.BeginTransaction();
            try
            {
                DALHelper.Insert(ai, tran, Operator);
                tran.Commit();
                return 0;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                return 1;
            }
        }
        /// <summary>
        /// 批量插入(同类)
        /// </summary>
        /// <param name="ai"></param>
        public static int Insert<T>(List<T> ai, string Operator)
        {
            OleDbConnection Conn = new OleDbConnection(DALHelper.connString);
            Conn.Open();
            OleDbTransaction tran = Conn.BeginTransaction();
            try
            {
                DALHelper.Insert(ai, tran, Operator);
                tran.Commit();
                return 0;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                return 1;
            }
        }
        /// <summary>
        /// 批量插入(同类)
        /// </summary>
        /// <param name="ai"></param>
        public static int Insert(List<ModelWhereParams> ai, string Operator)
        {
            OleDbConnection Conn = new OleDbConnection(DALHelper.connString);
            Conn.Open();
            OleDbTransaction tran = Conn.BeginTransaction();
            try
            {
                DALHelper.Insert(ai, tran, Operator);
                tran.Commit();
                return 0;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                return 1;
            }
        }
        #endregion

        #region 更新
        /// <summary>
        /// 根据参数更新记录
        /// </summary>
        /// <param name="entity">更新数据</param>
        /// <param name="whereParameter">更新条件</param>
        /// <returns>成功：1 | 不成功：返回错误代号</returns>
        public static int Update(ModelWhereParams entity, string Operator)
        {
            try
            {
                return DALHelper.Update(entity, null, Operator);
            }
            catch
            {
                return 1;
            }
        }
        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="ai"></param>
        public static int Update(List<ModelWhereParams> ai, string Operator)
        {
            OleDbConnection Conn = new OleDbConnection(DALHelper.connString);
            Conn.Open();
            OleDbTransaction tran = Conn.BeginTransaction();
            try
            {
                DALHelper.Update(ai, tran, Operator);
                tran.Commit();
                return 0;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                return 1;
            }
        }
        #endregion

        #region 删除
        /// <summary>
        /// 根据参数删除记录
        /// </summary>
        /// <param name="whereParameter">参数</param>
        /// <returns>成功：1 | 不成功：返回错误代号</returns>
        public static int Delete(ModelWhereParams entity, string Operator)
        {
            return DALHelper.Delete(entity, Operator);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ai"></param>
        public static int Delete(List<ModelWhereParams> ai, string Operator)
        {
            OleDbConnection Conn = new OleDbConnection(DALHelper.connString);
            Conn.Open();
            OleDbTransaction tran = Conn.BeginTransaction();
            try
            {
                DALHelper.Delete(ai, tran, Operator);
                tran.Commit();
                return 0;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                return 1;
            }
        }
        #endregion

        #region 链表查询
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
            return DALHelper.GetLists(entity, whereParameter, t, Select, IsSaveSelect, Order);
        }
        #endregion

        /// <summary>
        /// 根据sql返回table
        /// </summary>
        /// <param name="returnsql"></param>
        /// <returns></returns>
        public static DataTable GetSQLList(string returnsql)
        {
            return DALHelper.GetSQLList(returnsql);
        }
        /// <summary>
        /// 根据sql返回table
        /// </summary>
        /// <param name="returnsql"></param>
        /// <returns></returns>

        #region 获取列表
        public static List<object> GetLists(List<ModelWhereParams> entityList)
        {
            return DALHelper.GetLists(entityList);
        }
        /// <summary>
        /// 获取T列表(带参数|带Select|带排序)
        /// </summary>
        /// <param name="whereParameter">参数</param>
        /// <param name="Select">子查询</param>
        /// <param name="t">返回实体</param>
        /// <returns>成功：List<T> | 不成功：返回空实体</returns>
        public static List<T> GetList<T>(T entity, List<WhereParameter> whereParameter, string Select, bool IsSaveSelect, string Order) where T : new()
        {
            return DALHelper.GetList(entity, whereParameter, Select, IsSaveSelect, Order);
        }

        #endregion
    }
}
