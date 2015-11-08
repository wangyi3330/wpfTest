using System;
using System.Data;
using System.Data.OleDb;

namespace Carrot.Model
{
	#region TransactionRecord数据实体
	/// <summary>
	/// TransactionRecord数据实体
	/// 创建时间：2015-10-29 12:05:12
	/// 负责人：Carrot
	/// 模板制作：王毅
	/// 特别说明：本文件为自动生成,请勿修改！
	/// </summary>
    [Serializable]
	public class TransactionRecordInfo
	{
        ///<summary>
		/// 字段名： 
		/// 大小：16
		/// 是否允许为空：False
		///</summary>
        private Guid? transactionID;
        
        ///<summary>
		/// 字段名： 
		/// 大小：16
		/// 是否允许为空：True
		///</summary>
        private Guid? userID;
        
        ///<summary>
		/// 字段名：充值金额 
		/// 大小：8
		/// 是否允许为空：True
		///</summary>
        private double? transactionMoney;
        
        ///<summary>
		/// 字段名：充值时间 
		/// 大小：8
		/// 是否允许为空：True
		///</summary>
        private DateTime? creattime;
        
        ///<summary>
		/// 字段名： 
		/// 大小：16
		/// 是否允许为空：True
		///</summary>
        private Guid? userGoodsGuid;
        
        ///<summary>
		/// 字段名：流水号 
		/// 大小：100
		/// 是否允许为空：True
		///</summary>
        private string buyID;
        
		#region 公共属性
		
		///<summary>
		/// 字段名： 
		/// 大小：16
		/// 是否允许为空：False
		///</summary>
		[ColumnAttribute(2, OleDbType.Guid, 36)]
		public Guid? TransactionID
        {
            get { return transactionID; }
            set { transactionID = value; }
        }

		///<summary>
		/// 字段名： 
		/// 大小：16
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.Guid, 36)]
		public Guid? UserID
        {
            get { return userID; }
            set { userID = value; }
        }

		///<summary>
		/// 字段名：充值金额 
		/// 大小：8
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.Double, 8)]
		public double? TransactionMoney
        {
            get { return transactionMoney; }
            set { transactionMoney = value; }
        }

		///<summary>
		/// 字段名：充值时间 
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
		/// 字段名： 
		/// 大小：16
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.Guid, 36)]
		public Guid? UserGoodsGuid
        {
            get { return userGoodsGuid; }
            set { userGoodsGuid = value; }
        }

		///<summary>
		/// 字段名：流水号 
		/// 大小：100
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.VarChar, 100)]
		public string BuyID
        {
            get { return buyID; }
            set { buyID = value; }
        }
	
		#endregion
		
	}
	#endregion
}