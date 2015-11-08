using Carrot.Model;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Carrot.BLL
{
    public class AjaxBLL
    {
        /// <summary>
        /// 列表排序
        /// </summary>
        /// <param name="className"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public static int UpdateOrder(string className, string[] order)
        {
            string[] _className = className.Split('|');
            string keyName = _className[1];
            object entity = PublicBLL.StrToClass(_className[0]);
            PropertyInfo[] properties = entity.GetType().GetProperties();
            List<ModelWhereParams> l = new List<ModelWhereParams>();
            for (int i = 0; i < order.Length; i++)
            {
                //约束object
                entity = entity.GetType().InvokeMember("Refection", BindingFlags.CreateInstance, null, null, null);
                foreach (PropertyInfo item in properties)
                {
                    ColumnAttribute attr = (ColumnAttribute)item.GetCustomAttributes(typeof(ColumnAttribute), false)[0];
                    //查找排序字段
                    if (attr.IsPrimaryKey == -1)
                        item.SetValue(entity, i + 1, null);
                }
                List<WhereParameter> wp = new List<WhereParameter>();
                wp.Add(new WhereParameter(keyName, WhereParameter.Query.等于, order[i].ToString()));
                l.Add(new ModelWhereParams(entity, wp));
            }
            //执行操作
            return PublicBLL.Update(l, "");
        }
        
    }

}
