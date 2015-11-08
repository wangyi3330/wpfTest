using System;
using System.Data;
using System.Data.OleDb;

namespace Carrot.Model
{
	#region OrderEvaluate数据实体
	/// <summary>
	/// OrderEvaluate数据实体
	/// 创建时间：2015-10-29 12:05:12
	/// 负责人：Carrot
	/// 模板制作：王毅
	/// 特别说明：本文件为自动生成,请勿修改！
	/// </summary>
    [Serializable]
	public class OrderEvaluateInfo
	{
        ///<summary>
		/// 字段名： 
		/// 大小：4
		/// 是否允许为空：False
		///</summary>
        private int? orderEvaluateID;
        
        ///<summary>
		/// 字段名：订单号 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
        private string orderIID;
        
        ///<summary>
		/// 字段名：评价 0:好评 1:中评 2:差评 
		/// 大小：4
		/// 是否允许为空：True
		///</summary>
        private int? evaluation;
        
        ///<summary>
		/// 字段名：描述 
		/// 大小：-1
		/// 是否允许为空：True
		///</summary>
        private string description;
        
        ///<summary>
		/// 字段名： 
		/// 大小：8
		/// 是否允许为空：True
		///</summary>
        private DateTime? creattime;
        
        ///<summary>
		/// 字段名： 用户名 
		/// 大小：16
		/// 是否允许为空：True
		///</summary>
        private Guid? userID;
        
		#region 公共属性
		
		///<summary>
		/// 字段名： 
		/// 大小：4
		/// 是否允许为空：False
		///</summary>
		[ColumnAttribute(1, OleDbType.Integer, 4)]
		public int? OrderEvaluateID
        {
            get { return orderEvaluateID; }
            set { orderEvaluateID = value; }
        }

		///<summary>
		/// 字段名：订单号 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(-1, OleDbType.VarChar, 50)]
		public string OrderIID
        {
            get { return orderIID; }
            set { orderIID = value; }
        }

		///<summary>
		/// 字段名：评价 0:好评 1:中评 2:差评 
		/// 大小：4
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.Integer, 4)]
		public int? Evaluation
        {
            get { return evaluation; }
            set { evaluation = value; }
        }

		///<summary>
		/// 字段名：描述 
		/// 大小：-1
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.VarChar, -1)]
		public string Description
        {
            get { return description; }
            set { description = value; }
        }

		///<summary>
		/// 字段名： 
		/// 大小：8
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.Date, 8)]
		public DateTime? Creattime
        {
            get { return creattime; }
            set { creattime = value; }
        }

		///<summary>
		/// 字段名： 用户名 
		/// 大小：16
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.Guid, 36)]
		public Guid? UserID
        {
            get { return userID; }
            set { userID = value; }
        }
	
		#endregion
		
	}
	#endregion
}