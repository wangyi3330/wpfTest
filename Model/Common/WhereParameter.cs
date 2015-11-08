using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Carrot.DataCommon;

namespace Carrot.Model
{
    public class WhereParameter
    {
        public enum Kuohao
        {
            左括号,
            右括号,
            双左括号,
            双右括号
        }
        public enum Query
        {
            等于空,
            不等于空,
            等于,
            不等于,
            大于,
            小于,
            大于等于,
            小于等于,
            包含,
            不包含,
            包含于,
            不包含于,
            IN,
            NOTIN
        };
        private string _kuohao;
        /// <summary>
        /// 关系括号
        /// </summary>
        public string kuohao
        {
            get
            {
                switch (_kuohao)
                {
                    case "双左括号": return "((";
                    case "双右括号": return "))";
                    case "左括号": return "(";
                    case "右括号": return ")";
                    case "": return "";
                    default: return "";
                }
            }
            set { _kuohao = value; }
        }
        public enum OrAnd
        {
            Or,
            And
        };
        private string orAndQuery;
        /// <summary>
        /// 与后一条件的关系
        /// </summary>
        public string OrAndQuery
        {
            get
            {
                switch (orAndQuery)
                {
                    case "Or": return " OR";
                    case "And": return " AND";
                    case "": return "";
                    default: return " AND";
                }
            }
            set { orAndQuery = value; }
        }
        private string andQuery;
        /// <summary>
        /// 符号
        /// </summary>
        public string AndQuery
        {
            get
            {
                switch (andQuery)
                {
                    case "等于空": return _null;
                    case "不等于空": return _nonull;
                    case "等于": return _and;
                    case "不等于": return _no;
                    case "大于": return _dayu;
                    case "小于": return _xiaoyu;
                    case "大于等于": return _dayuand;
                    case "小于等于": return _xiaoyuand;
                    case "IN": return _in;
                    case "NOTIN": return _notin;
                    case "包含":
                        if (DataUtility.bdate == "ora")
                            return _like1ora;
                        else
                            return _like1;
                    case "不包含":
                        if (DataUtility.bdate == "ora")
                            return _nolike1ora;
                        else
                            return _nolike1;
                    case "包含于":
                        if (DataUtility.bdate == "ora")
                            return _likeora;
                        else
                            return _like;
                    case "不包含于":
                        if (DataUtility.bdate == "ora")
                            return _nolikeora;
                        else
                            return _nolike;
                    default: return _and;
                }
            }
            set { andQuery = value; }
        }
        /// <summary>
        /// 反转换
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static OrAnd unOrAndQuery(string value)
        {
            switch (value.Trim().ToUpper())
            {
                case "OR": return OrAnd.Or;
                case "AND": return OrAnd.And;
                default: return OrAnd.And;
            }
        }
        /// <summary>
        /// 反转换
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Query unAndQuery(string value)
        {
            switch (value)
            {
                case _null: return Query.等于空;
                case _nonull: return Query.不等于空;
                case _and: return Query.等于;
                case _no: return Query.不等于;
                case _dayu: return Query.大于;
                case _xiaoyu: return Query.小于;
                case _dayuand: return Query.大于等于;
                case _xiaoyuand: return Query.小于等于;
                case _like1: return Query.包含;
                case _nolike1: return Query.不包含;
                case _like: return Query.包含于;
                case _nolike: return Query.不包含于;
                case _like1ora: return Query.包含;
                case _nolike1ora: return Query.不包含;
                case _likeora: return Query.包含于;
                case _nolikeora: return Query.不包含于;
                case _in: return Query.IN;
                case _notin: return Query.NOTIN;
                default: return Query.等于;
            }
        }
        /// <summary>
        /// 反转换
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Kuohao unKuohao(string value)
        {
            switch (value.Trim())
            {
                case ")": return Kuohao.右括号;
                case "(": return Kuohao.左括号;
                case "))": return Kuohao.双右括号;
                case "((": return Kuohao.双左括号;
                default: return Kuohao.左括号;
            }
        }
        /// <summary>
        /// IN
        /// </summary>
        public const string _in = " IN ({0})";
        /// <summary>
        /// NotIN
        /// </summary>
        public const string _notin = " NOT IN ({0}) ";
        /// <summary>
        /// 等于null
        /// </summary>
        public const string _null = " IS NULL ";
        /// <summary>
        /// 不等于null
        /// </summary>
        public const string _nonull = " IS NOT NULL ";
        /// <summary>
        /// 等于
        /// </summary>
        public const string _and = " ={0} ";
        /// <summary>
        /// 不等于
        /// </summary>
        public const string _no = " <>{0} ";
        /// <summary>
        /// 大于
        /// </summary>
        public const string _dayu = " >{0} ";
        /// <summary>
        /// 小于
        /// </summary>
        public const string _xiaoyu = " <{0} ";
        /// <summary>
        /// 大于等于
        /// </summary>
        public const string _dayuand = " >={0} ";
        /// <summary>
        /// 小于等于
        /// </summary>
        public const string _xiaoyuand = " <={0} ";
        /// <summary>
        /// 包含
        /// </summary>
        public const string _like1 = " LIKE '%'+{0}+'%' ";
        /// <summary>
        /// 包含Ora
        /// </summary>
        public const string _like1ora = " LIKE '%'||{0}||'%' ";
        /// <summary>
        /// 不包含
        /// </summary>
        public const string _nolike1 = " NOT LIKE '%'+{0}+'%' ";
        /// <summary>
        /// 不包含ora
        /// </summary>
        public const string _nolike1ora = " NOT LIKE '%'||{0}||'%' ";
        /// <summary>
        /// 包含于
        /// </summary>
        public const string _like = " LIKE '%'+CONVERT(NVARCHAR(MAX),{0})+'%' ";
        /// <summary>
        /// 包含于ora
        /// </summary>
        public const string _likeora = " LIKE '%'||to_char({0})||'%' ";
        /// <summary>
        /// 不包含于
        /// </summary>
        public const string _nolike = " NOT LIKE '%'+CONVERT(NVARCHAR(MAX),{0})+'%' ";
        /// <summary>
        /// 不包含于ora
        /// </summary>
        public const string _nolikeora = " NOT LIKE '%'||to_char({0})||'%' ";
        private string parameterName;
        /// <summary>
        /// 参数名字
        /// </summary>
        public string ParameterName
        {
            get
            {
                return DataUtility.ConvertSql2(this.parameterName);
            }
            set
            {
                this.parameterName = value;
            }
        }
        private object value;
        /// <summary>
        /// 值
        /// </summary>
        public object Value
        {
            get
            {
                if (this.value == null)
                    return this.value;
                else
                    return this.value.ToString().ToLower();
            }
            set { this.value = value; }
        }

        /// <summary>
        /// 查询条件
        /// </summary>
        /// <param name="parameterName">参数名（如果是多表请注明字段所属的表名称）</param>
        /// <param name="query">运算符号</param>
        /// <param name="value">值</param>
        public WhereParameter(string ParameterName, Query AndQuery, object Value)
        {
            this.parameterName = ParameterName;
            this.Value = Value;
            this.AndQuery = (string)AndQuery.ToString();
            this.OrAndQuery = "";
            this.kuohao = "";
        }
        /// <summary>
        /// 查询条件(带关系)
        /// </summary>
        /// <param name="parameterName">参数名（如果是多表请注明字段所属的表名称）</param>
        /// <param name="query">运算符号</param>
        /// <param name="value">值</param>
        /// <param name="orquery">与后一条件的关系</param>
        public WhereParameter(string ParameterName, Query AndQuery, object Value, OrAnd OrAndQuery)
        {
            this.ParameterName = ParameterName;
            this.Value = Value;
            this.AndQuery = (string)AndQuery.ToString();
            this.OrAndQuery = (string)OrAndQuery.ToString();
            this.kuohao = "";
        }
        /// <summary>
        /// 查询条件(带复杂关系)
        /// </summary>
        /// <param name="parameterName">参数名（如果是多表请注明字段所属的表名称）</param>
        /// <param name="query">运算符号</param>
        /// <param name="value">值</param>
        /// <param name="orquery">与后一条件的关系</param>
        /// <param name="kuohao">关系括号</param>
        public WhereParameter(string ParameterName, Query AndQuery, object Value, Kuohao kuohao)
        {
            this.ParameterName = ParameterName;
            this.Value = Value;
            this.AndQuery = (string)AndQuery.ToString();
            this.OrAndQuery = "";
            this.kuohao = (string)kuohao.ToString();
        }
        /// <summary>
        /// 查询条件(带复杂关系)
        /// </summary>
        /// <param name="parameterName">参数名（如果是多表请注明字段所属的表名称）</param>
        /// <param name="query">运算符号</param>
        /// <param name="value">值</param>
        /// <param name="orquery">与后一条件的关系</param>
        /// <param name="kuohao">关系括号</param>
        public WhereParameter(string ParameterName, Query AndQuery, object Value, OrAnd OrAndQuery, Kuohao kuohao)
        {
            this.ParameterName = ParameterName;
            this.Value = Value;
            this.AndQuery = (string)AndQuery.ToString();
            this.OrAndQuery = (string)OrAndQuery.ToString();
            this.kuohao = (string)kuohao.ToString();
        }
    }
}
