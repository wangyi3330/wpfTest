using System;
using System.Data;
using System.Data.OleDb;

namespace Carrot.Model
{
	#region BankRecord数据实体
	/// <summary>
	/// BankRecord数据实体
	/// 创建时间：2015-10-28 04:23:40
	/// 负责人：Carrot
	/// 模板制作：王毅
	/// 特别说明：本文件为自动生成,请勿修改！
	/// </summary>
    [Serializable]
	public class E_BankRecord_UserGoodsModel : UserGoodsInfo
	{
        ///<summary>
		/// 字段名： 
		/// 大小：4
		/// 是否允许为空：False
		///</summary>
        private int? bankID;
        
        ///<summary>
		/// 字段名：流水号 
		/// 大小：100
		/// 是否允许为空：True
		///</summary>
        private string buyID;
        
        ///<summary>
		/// 字段名： 
		/// 大小：8
		/// 是否允许为空：True
		///</summary>
        private DateTime? creattime;
        
        ///<summary>
		/// 字段名： 
		/// 大小：50
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
		public int? BankID
        {
            get { return bankID; }
            set { bankID = value; }
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
		/// 字段名： 
		/// 大小：50
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