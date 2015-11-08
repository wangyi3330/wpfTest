using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data.OleDb;

namespace Carrot.Model
{
    public class OleDbParams
    {
        /// <summary>
        /// 对象和参数打包
        /// </summary>
        /// <param name="sqlTxt">存放SQL语句</param>
        /// <param name="parameter">存放传递的参数</param>
        public OleDbParams(string sqlTxt, OleDbParameter[] parameter)
        {
            SqlTxt = sqlTxt;
            Parameter = parameter;
        }
        private string sqlTxt;
        /// <summary>
        /// 存放SQL语句
        /// </summary>
        public string SqlTxt
        {
            get { return sqlTxt; }
            set { sqlTxt = value; }
        }
        private OleDbParameter[] parameter;
        /// <summary>
        /// 存放传递的参数
        /// </summary>
        public OleDbParameter[] Parameter
        {
            get { return parameter; }
            set { parameter = value; }
        }
    }
}
