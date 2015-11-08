using Carrot.DBUtility;
using Carrot.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Carrot.BLL
{
    public class DALToBLL
    {
        #region 插入
        /// <summary>
        /// 插入错误日志
        /// </summary>
        /// <param name="ex"></param>
        public static int InsertError(Exception ex, string Operator)
        {
            return PublicDAL.InsertError(ex, Operator);
        }
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="ai"></param>
        public static int Insert<T>(T t, string Operator)
        {
            return PublicDAL.Insert(t, Operator);
        }
        /// <summary>
        /// 批量插入(不同类)
        /// </summary>
        /// <param name="ai"></param>
        public static int Insert(List<object> ai, string Operator)
        {
            return PublicDAL.Insert(ai, Operator);
        }
        /// <summary>
        /// 批量插入(同类)
        /// </summary>
        /// <param name="ai"></param>
        public static int Insert<T>(List<T> ai, string Operator)
        {
            return PublicDAL.Insert(ai, Operator);
        }
        /// <summary>
        /// 批量插入(同类)
        /// </summary>
        /// <param name="ai"></param>
        public static int Insert(List<ModelWhereParams> ai, string Operator)
        {
            return PublicDAL.Insert(ai, Operator);
        }
        /// <summary>
        /// 根据sql返回table
        /// </summary>
        /// <param name="returnsql"></param>
        /// <returns></returns>
        public static DataTable GetSQLList(string returnsql)
        {
            return PublicDAL.GetSQLList(returnsql);
        }
        /// <summary>
        /// 根据sql返回table
        /// </summary>
        /// <param name="returnsql"></param>
        /// <returns></returns>

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
            return PublicDAL.Update(entity, Operator);
        }
        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="ai"></param>
        public static int Update(List<ModelWhereParams> ai, string Operator)
        {
            return PublicDAL.Update(ai, Operator);
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
            return PublicDAL.Delete(entity, Operator);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ai"></param>
        public static int Delete(List<ModelWhereParams> ai, string Operator)
        {
            return PublicDAL.Delete(ai, Operator);
        }
        #endregion

        #region 链表查询
        /// <summary>
        /// 链表查询(带排序)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">表</param>
        /// <param name="whereParameter">查询参数</param>
        /// <param name="t">返回实体类型</param>
        /// <param name="Select">Select</param>
        /// <param name="Order">排序</param>
        /// <returns>返回实体</returns>
        public static List<T> GetLists<T>(List<JoinModel> entity, T t, string Select, string Order) where T : new()
        {
            return GetLists(entity, new List<WhereParameter>(), t, Select, Order);
        }
        /// <summary>
        /// 链表查询(不带排序)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">表</param>
        /// <param name="whereParameter">查询参数</param>
        /// <param name="t">返回实体类型</param>
        /// <param name="Select">Select</param>
        /// <param name="Order">排序</param>
        /// <returns>返回实体</returns>
        public static List<T> GetLists<T>(List<JoinModel> entity, List<WhereParameter> whereParameter, T t) where T : new()
        {
            return GetLists(entity, whereParameter, t, "", "");
        }
        /// <summary>
        /// 链表查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="whereParameter"></param>
        /// <param name="t"></param>
        /// <param name="Select"></param>
        /// <param name="Order"></param>
        /// <returns></returns>
        public static List<T> GetLists<T>(List<JoinModel> entity, List<WhereParameter> whereParameter, T t, string Select, string Order) where T : new()
        {
            return GetLists(entity, whereParameter, t, Select, true, Order);
        }
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
            return PublicDAL.GetLists(entity, whereParameter, t, Select, IsSaveSelect, Order);
        }
        #endregion

        #region 获取列表
        /// <summary>
        /// 获取列表(批量)
        /// </summary>
        /// <param name="whereParameter">参数</param>
        /// <param name="Select">子查询</param>
        /// <param name="t">返回实体</param>
        /// <returns>成功：List<T> | 不成功：返回空实体</returns>
        public static List<object> GetLists(List<ModelWhereParams> entityList)
        {
            return PublicDAL.GetLists(entityList);
        }
        /// <summary>
        /// 获取T实体
        /// </summary>
        /// <param name="whereParameter">参数</param>
        /// <returns>成功：List<T> | 不成功：null</returns>
        public static T GetDetailInfo<T>(T t, WhereParameter whereParameter) where T : new()
        {
            List<T> List = GetList(t, whereParameter);
            if (List.Count > 0)
                return List[0];
            else
                return new T();
        }
        /// <summary>
        /// 获取T实体
        /// </summary>
        /// <param name="whereParameter">参数</param>
        /// <returns>成功：List<T> | 不成功：null</returns>
        public static T GetDetailInfo<T>(T t, List<WhereParameter> whereParameter) where T : new()
        {
            List<T> List = GetList(t, whereParameter);
            if (List.Count > 0)
                return List[0];
            else
                return new T();
        }
        /// <summary>
        /// 获取T列表
        /// </summary>
        /// <param name="whereParameter">参数</param>
        /// <returns>成功：List<T> | 不成功：null</returns>
        public static List<T> GetList<T>(T t) where T : new()
        {
            return GetList(t, new List<WhereParameter>(), "", "");
        }
        /// <summary>
        /// 获取T列表
        /// </summary>
        /// <param name="whereParameter">参数</param>
        /// <returns>成功：List<T> | 不成功：null</returns>
        public static List<T> GetList<T>(T t, string Select, string Order) where T : new()
        {
            return GetList(t, new List<WhereParameter>(), Select, Order);
        }
        /// <summary>
        /// 获取T列表(带单参数)
        /// </summary>
        /// <param name="whereParameter">参数</param>
        /// <returns>成功：List<T> | 不成功：null</returns>
        public static List<T> GetList<T>(T t, WhereParameter whereParameter) where T : new()
        {

            List<WhereParameter> wp = new List<WhereParameter>();
            wp.Add(whereParameter);
            return GetList(t, wp);
        }
        /// <summary>
        /// 获取T列表(带单参数)
        /// </summary>
        /// <param name="whereParameter">参数</param>
        /// <returns>成功：List<T> | 不成功：null</returns>
        public static List<T> GetList<T>(T t, WhereParameter whereParameter, string Select, string Order) where T : new()
        {
            List<WhereParameter> wp = new List<WhereParameter>();
            wp.Add(whereParameter);
            return GetList(t, wp, Select, Order);
        }
        /// <summary>
        /// 获取T列表(带参数)
        /// </summary>
        /// <param name="whereParameter">参数</param>
        /// <returns>成功：List<T> | 不成功：null</returns>
        public static List<T> GetList<T>(T t, List<WhereParameter> whereParameter) where T : new()
        {
            return GetList(t, whereParameter, "", "");
        }
        /// <summary>
        /// 获取T列表(带参数|带Select|带排序)
        /// </summary>
        /// <param name="whereParameter">参数</param>
        /// <param name="Select">子查询</param>
        /// <param name="t">返回实体</param>
        /// <returns>成功：List<T> | 不成功：返回空实体</returns>
        public static List<T> GetList<T>(T entity, List<WhereParameter> whereParameter, string Select, string Order) where T : new()
        {
            return PublicDAL.GetList(entity, whereParameter, Select, true, Order);
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
            return PublicDAL.GetList(entity, whereParameter, Select, IsSaveSelect, Order);
        }
        /// <summary>
        /// 获取T列表(带参数|带Select|带排序)
        /// </summary>
        /// <param name="whereParameter">参数</param>
        /// <param name="Select">子查询</param>
        /// <param name="t">返回实体</param>
        /// <returns>成功：List<T> | 不成功：返回空实体</returns>
        public static List<T> GetList<T>(T entity, WhereParameter whereParameter, string Select, bool IsSaveSelect, string Order) where T : new()
        {
            List<WhereParameter> wp = new List<WhereParameter>();
            wp.Add(whereParameter);
            return PublicDAL.GetList(entity, wp, Select, IsSaveSelect, Order);
        }
        #endregion


        public static DataTable GetAccount_Colum(int Account_ColumTypeID, string AccountID)
        {
            return AccountDAL.GetAccount_Colum(Account_ColumTypeID, AccountID);
        }
    }
}
