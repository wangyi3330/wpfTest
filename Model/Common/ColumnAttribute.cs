using System.Diagnostics;
using System;
using System.ComponentModel;
using System.Data.OleDb;

namespace Carrot.Model
{
    /// <summary>
    /// 是否为2:GUID 1:自增ID 0:普通字段 | 列所对应的表的数据类型 | 字数
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class ColumnAttribute : Attribute
    {
        private int _IsPrimaryKey = 0;
        private OleDbType _ColumnType;
        private int _WordNumber;

        /// <summary>
        ///  字数限制
        /// </summary>
        public int WordNumber
        {
            get { return _WordNumber; }
            set { _WordNumber = value; }
        }

        /// <summary>
        /// 是否为2:GUID|1:自增ID|0:普通字段|-1:排序字段
        /// </summary>
        public int IsPrimaryKey
        {
            get
            {
                return _IsPrimaryKey;
            }
            set
            {
                this._IsPrimaryKey = value;
            }
        }

        /// <summary>
        /// 列所对应的表的数据类型
        /// </summary>
        public OleDbType ColumnType
        {
            get
            {
                return _ColumnType;
            }
            set
            {
                _ColumnType = value;
            }
        }




        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="IsPrimaryKey">是否为2:GUID|1:自增ID|0:普通字段|-1:排序字段</param>
        /// <param name="strSQLType">列所对应的表的数据类型</param>
        /// <param name="WordNumber">字数</param>
        public ColumnAttribute(int IsPrimaryKey, OleDbType strSQLType, int WordNumber)
        {
            this._ColumnType = strSQLType;
            this._IsPrimaryKey = IsPrimaryKey;
            this._WordNumber = WordNumber;
        }
    }
}
