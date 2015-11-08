using System;
using System.Data;
using System.Data.OleDb;

namespace Carrot.Model
{
	#region OrderItem数据实体
	/// <summary>
	/// OrderItem数据实体
	/// 创建时间：2015-10-31 01:38:28
	/// 负责人：Carrot
	/// 模板制作：王毅
	/// 特别说明：本文件为自动生成,请勿修改！
	/// </summary>
    [Serializable]
	public class OrderItemInfo
	{
        ///<summary>
		/// 字段名： 
		/// 大小：4
		/// 是否允许为空：False
		///</summary>
        private int? orderItemID;
        
        ///<summary>
		/// 字段名：订单号 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
        private string orderIID;
        
        ///<summary>
		/// 字段名：商品 
		/// 大小：16
		/// 是否允许为空：True
		///</summary>
        private Guid? goodsOrBucketGuid;
        
        ///<summary>
		/// 字段名：数量 
		/// 大小：4
		/// 是否允许为空：True
		///</summary>
        private int? oNum;
        
        ///<summary>
		/// 字段名：单项金额 
		/// 大小：8
		/// 是否允许为空：True
		///</summary>
        private double? oPrice;
        
        ///<summary>
		/// 字段名： 
		/// 大小：8
		/// 是否允许为空：True
		///</summary>
        private double? oSale;
        
        ///<summary>
		/// 字段名：类型 0：水 1：桶 2：桶押金 
		/// 大小：4
		/// 是否允许为空：True
		///</summary>
        private int? oType;
        
        ///<summary>
		/// 字段名：订单中商品状态（水 0：未到 1：配送中 2：已完成 3：退回 ) （空桶 0:未退 1：正在退回 2：已退）（桶押金 0：有效 1：无效） 
		/// 大小：4
		/// 是否允许为空：True
		///</summary>
        private int? orderItemStatus;
        
        ///<summary>
		/// 字段名：用户名（优化查询） 
		/// 大小：16
		/// 是否允许为空：True
		///</summary>
        private Guid? userID;
        
        ///<summary>
		/// 字段名：对应的用户存水 
		/// 大小：16
		/// 是否允许为空：True
		///</summary>
        private Guid? userGoodsGuid;
        
		#region 公共属性
		
		///<summary>
		/// 字段名： 
		/// 大小：4
		/// 是否允许为空：False
		///</summary>
		[ColumnAttribute(1, OleDbType.Integer, 4)]
		public int? OrderItemID
        {
            get { return orderItemID; }
            set { orderItemID = value; }
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
		/// 字段名：商品 
		/// 大小：16
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.Guid, 36)]
		public Guid? GoodsOrBucketGuid
        {
            get { return goodsOrBucketGuid; }
            set { goodsOrBucketGuid = value; }
        }

		///<summary>
		/// 字段名：数量 
		/// 大小：4
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.Integer, 4)]
		public int? ONum
        {
            get { return oNum; }
            set { oNum = value; }
        }

		///<summary>
		/// 字段名：单项金额 
		/// 大小：8
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.Double, 8)]
		public double? OPrice
        {
            get { return oPrice; }
            set { oPrice = value; }
        }

		///<summary>
		/// 字段名： 
		/// 大小：8
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.Double, 8)]
		public double? OSale
        {
            get { return oSale; }
            set { oSale = value; }
        }

		///<summary>
		/// 字段名：类型 0：水 1：桶 2：桶押金 
		/// 大小：4
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.Integer, 4)]
		public int? OType
        {
            get { return oType; }
            set { oType = value; }
        }

		///<summary>
		/// 字段名：订单中商品状态（水 0：未到 1：配送中 2：已完成 3：退回 ) （空桶 0:未退 1：正在退回 2：已退）（桶押金 0：有效 1：无效） 
		/// 大小：4
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(-1, OleDbType.Integer, 4)]
		public int? OrderItemStatus
        {
            get { return orderItemStatus; }
            set { orderItemStatus = value; }
        }

		///<summary>
		/// 字段名：用户名（优化查询） 
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
		/// 字段名：对应的用户存水 
		/// 大小：16
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.Guid, 36)]
		public Guid? UserGoodsGuid
        {
            get { return userGoodsGuid; }
            set { userGoodsGuid = value; }
        }
	
		#endregion
		
	}
	#endregion
}