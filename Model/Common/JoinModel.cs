using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Carrot.Model
{
    public class JoinModel
    {
        public enum Join
        {
            左联,
            右联,
            外联,
            内联,
            交联
        };

        private string joinParam;
        /// <summary>
        /// A与B实体连接方式
        /// </summary>
        public string JoinParam
        {
            get
            {
                switch (joinParam)
                {
                    case "外联": return " OUTER JOIN {0} ON {1}={2} {3}";
                    case "左联": return " LEFT JOIN {0} ON {1}={2} {3}";
                    case "右联": return " RIGHT JOIN {0} ON {1}={2} {3}";
                    case "内联": return " INNER JOIN {0} ON {1}={2} {3}";
                    case "交联": return " CROSS JOIN {0} ON {1}={2} {3}";
                    case "": return "";
                    default: return " INNER JOIN {0} ON {1}={2} ";
                }
            }
            set { joinParam = value; }
        }
        /// <summary>
        /// 实体关联字段A
        /// </summary>
        public string JoinPropertyNameA { get; set; }
        /// <summary>
        /// 实体关联字段B
        /// </summary>
        public string JoinPropertyNameB { get; set; }
        /// <summary>
        /// 实体关联字段C
        /// </summary>
        public string JoinPropertyNameC { get; set; }
        /// <summary>
        /// 数据库表名称A
        /// </summary>
        public string DBTableNameA { get; set; }
        /// <summary>
        /// 数据库表名称B
        /// </summary>
        public string DBTableNameB { get; set; }
        /// <summary>
        /// 实体类A
        /// </summary>
        public object ModelA { set; get; }
        /// <summary>
        /// 实体类B
        /// </summary>
        public object ModelB { set; get; }

        /// <summary>
        /// 连接表（带关系）
        /// </summary>
        /// <param name="ModelA">实体类A</param>
        /// <param name="JoinPropertyNameA">关联字段名A(不用加表名)</param>
        /// <param name="ModelB">实体类B</param>
        /// <param name="JoinPropertyNameB">关联字段名B(不用加表名)</param>
        /// <param name="JoinParam">A与B实体连接方式</param>
        public JoinModel(object ModelA, string JoinPropertyNameA, object ModelB, string JoinPropertyNameB, Join JoinParam)
        {
            this.ModelA = ModelA;
            this.ModelB = ModelB;
            this.DBTableNameA = "T_" + ModelA.GetType().Name.Remove(ModelA.GetType().Name.Length - 4);
            this.DBTableNameB = "T_" + ModelB.GetType().Name.Remove(ModelB.GetType().Name.Length - 4);
            if (JoinPropertyNameA.IndexOf(DBTableNameA + ".") == -1 && JoinPropertyNameA.IndexOf("SUBSTRING") == -1)
                this.JoinPropertyNameA = DBTableNameA + "." + JoinPropertyNameA + "";
            else
                this.JoinPropertyNameA = JoinPropertyNameA + "";
            if (JoinPropertyNameB.IndexOf(DBTableNameB + ".") == -1 && JoinPropertyNameB.IndexOf("SUBSTRING") == -1)
                this.JoinPropertyNameB = DBTableNameB + "." + JoinPropertyNameB + "";
            else
                this.JoinPropertyNameB = JoinPropertyNameB + "";
            this.JoinParam = (string)JoinParam.ToString();
        }
        public JoinModel(object ModelA, string JoinPropertyNameA, object ModelB, string JoinPropertyNameB, Join JoinParam, string JoinPropertyNameC)
        {
            this.ModelA = ModelA;
            this.ModelB = ModelB;
            this.DBTableNameA = "T_" + ModelA.GetType().Name.Remove(ModelA.GetType().Name.Length - 4);
            this.DBTableNameB = "T_" + ModelB.GetType().Name.Remove(ModelB.GetType().Name.Length - 4);

            if (JoinPropertyNameA.IndexOf(DBTableNameA + ".") == -1 && JoinPropertyNameA.IndexOf("SUBSTRING") == -1)
                this.JoinPropertyNameA = DBTableNameA + "." + JoinPropertyNameA + "";
            else
                this.JoinPropertyNameA = JoinPropertyNameA + "";
            if (JoinPropertyNameB.IndexOf(DBTableNameB + ".") == -1 && JoinPropertyNameB.IndexOf("SUBSTRING") == -1)
                this.JoinPropertyNameB = DBTableNameB + "." + JoinPropertyNameB + "";
            else
                this.JoinPropertyNameB = JoinPropertyNameB + "";
            this.JoinPropertyNameC = " AND " + JoinPropertyNameC + "";
            this.JoinParam = (string)JoinParam.ToString();
        }
    }
}
