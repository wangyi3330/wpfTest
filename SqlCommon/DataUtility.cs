using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Carrot.DataCommon
{
    public class DataUtility
    {
        public static string bdate = "sql";
        public static string connString = ConfigurationManager.ConnectionStrings["Conn"].ToString();
        public static string getYYYYMMDD(string param)
        {
            return "Convert(varchar(100)," + param + ",112)";
        }
        public static string substr(string param, int length)
        {
            return "SUBSTRING(" + param + ",0," + length + ")";
        }
        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static string ConvertSql(string sql)
        {
            return sql;
        }
        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static string ConvertSql2(string sql)
        {
            return sql;
        }
    }
}
