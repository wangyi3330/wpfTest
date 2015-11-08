using System;
using System.Data;
using System.Data.OleDb;

namespace Carrot.Model
{
	#region Cart数据实体
	/// <summary>
	/// Cart数据实体
	/// 创建时间：2015-09-24 01:19:35
	/// 负责人：Carrot
	/// 模板制作：王毅
	/// 特别说明：本文件为自动生成,请勿修改！
	/// </summary>
    [Serializable]
	public class E_Cart_GoodsModel:GoodsInfo
	{
        ///<summary>
		/// 字段名： 
		/// 大小：16
		/// 是否允许为空：False
		///</summary>
        private Guid? cartID;
        
        ///<summary>
		/// 字段名： 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
        private Guid? userID;
        
        ///<summary>
		/// 字段名： 
		/// 大小：16
		/// 是否允许为空：True
		///</summary>
        private Guid? goodsGuid;
        
        ///<summary>
		/// 字段名： 
		/// 大小：4
		/// 是否允许为空：True
		///</summary>
        private int? goodsNum;
        
        ///<summary>
		/// 字段名：添加时间 
		/// 大小：8
		/// 是否允许为空：True
		///</summary>
        private DateTime? creatDate;
        
		#region 公共属性
		
		///<summary>
		/// 字段名： 
		/// 大小：16
		/// 是否允许为空：False
		///</summary>
		[ColumnAttribute(2, OleDbType.Guid, 36)]
		public Guid? CartID
        {
            get { return cartID; }
            set { cartID = value; }
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

		///<summary>
		/// 字段名： 
		/// 大小：16
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.Guid, 36)]
		public Guid? GoodsGuid
        {
            get { return goodsGuid; }
            set { goodsGuid = value; }
        }

		///<summary>
		/// 字段名： 
		/// 大小：4
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.Integer, 4)]
		public int? GoodsNum
        {
            get { return goodsNum; }
            set { goodsNum = value; }
        }

		///<summary>
		/// 字段名：添加时间 
		/// 大小：8
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.Date, 8)]
		public DateTime? CreatDate
        {
            get { return creatDate; }
            set { creatDate = value; }
        }
	
		#endregion
		
	}
	#endregion
}