using System;
using System.Data;
using System.Data.OleDb;

namespace Carrot.Model
{
	#region UserGoods数据实体
	/// <summary>
	/// UserGoods数据实体
	/// 创建时间：2015-10-31 01:33:40
	/// 负责人：Carrot
	/// 模板制作：王毅
	/// 特别说明：本文件为自动生成,请勿修改！
	/// </summary>
    [Serializable]
	public class UserGoodsInfo
	{
        ///<summary>
		/// 字段名： 
		/// 大小：16
		/// 是否允许为空：False
		///</summary>
        private Guid? userGoodsGuid;
        
        ///<summary>
		/// 字段名：商品ID 
		/// 大小：16
		/// 是否允许为空：True
		///</summary>
        private Guid? goodsOrBucketGuid;
        
        ///<summary>
		/// 字段名：（水 0：未到 1：配送中 2：已完成 ) （空桶 0：未退 1：正在退回 2：已退）（桶押金 0：有效 1：无效） 
		/// 大小：4
		/// 是否允许为空：True
		///</summary>
        private int? uStatus;
        
        ///<summary>
		/// 字段名：类型 0：水 1：桶２：桶押金 
		/// 大小：4
		/// 是否允许为空：True
		///</summary>
        private int? uType;
        
        ///<summary>
		/// 字段名：用户名 
		/// 大小：16
		/// 是否允许为空：True
		///</summary>
        private Guid? userID;
        
        ///<summary>
		/// 字段名：计数专用数据库中必须为１ 
		/// 大小：4
		/// 是否允许为空：True
		///</summary>
        private int? uNum;
        
        ///<summary>
		/// 字段名：价格 
		/// 大小：8
		/// 是否允许为空：True
		///</summary>
        private double? uPrice;
        
        ///<summary>
		/// 字段名：折扣 
		/// 大小：8
		/// 是否允许为空：True
		///</summary>
        private double? uSale;
        
        ///<summary>
		/// 字段名：是否是赠送 
		/// 大小：4
		/// 是否允许为空：True
		///</summary>
        private int? uIsGiven;
        
        ///<summary>
		/// 字段名：流水号 
		/// 大小：100
		/// 是否允许为空：True
		///</summary>
        private string buyID;
        
        ///<summary>
		/// 字段名：购买时间 
		/// 大小：8
		/// 是否允许为空：True
		///</summary>
        private DateTime? creattime;
        
		#region 公共属性
		
		///<summary>
		/// 字段名： 
		/// 大小：16
		/// 是否允许为空：False
		///</summary>
		[ColumnAttribute(2, OleDbType.Guid, 36)]
		public Guid? UserGoodsGuid
        {
            get { return userGoodsGuid; }
            set { userGoodsGuid = value; }
        }

		///<summary>
		/// 字段名：商品ID 
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
		/// 字段名：（水 0：未到 1：配送中 2：已完成 ) （空桶 0：未退 1：正在退回 2：已退）（桶押金 0：有效 1：无效） 
		/// 大小：4
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.Integer, 4)]
		public int? UStatus
        {
            get { return uStatus; }
            set { uStatus = value; }
        }

		///<summary>
		/// 字段名：类型 0：水 1：桶２：桶押金 
		/// 大小：4
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.Integer, 4)]
		public int? UType
        {
            get { return uType; }
            set { uType = value; }
        }

		///<summary>
		/// 字段名：用户名 
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
		/// 字段名：计数专用数据库中必须为１ 
		/// 大小：4
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.Integer, 4)]
		public int? UNum
        {
            get { return uNum; }
            set { uNum = value; }
        }

		///<summary>
		/// 字段名：价格 
		/// 大小：8
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.Double, 8)]
		public double? UPrice
        {
            get { return uPrice; }
            set { uPrice = value; }
        }

		///<summary>
		/// 字段名：折扣 
		/// 大小：8
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.Double, 8)]
		public double? USale
        {
            get { return uSale; }
            set { uSale = value; }
        }

		///<summary>
		/// 字段名：是否是赠送 
		/// 大小：4
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.Integer, 4)]
		public int? UIsGiven
        {
            get { return uIsGiven; }
            set { uIsGiven = value; }
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
		/// 字段名：购买时间 
		/// 大小：8
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.Date, 8)]
		public DateTime? Creattime
        {
            get { return creattime; }
            set { creattime = value; }
        }
	
		#endregion
		
	}
	#endregion
}