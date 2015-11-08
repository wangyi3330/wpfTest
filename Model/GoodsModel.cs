using System;
using System.Data;
using System.Data.OleDb;

namespace Carrot.Model
{
	#region Goods数据实体
	/// <summary>
	/// Goods数据实体
	/// 创建时间：2015-10-29 12:05:12
	/// 负责人：Carrot
	/// 模板制作：王毅
	/// 特别说明：本文件为自动生成,请勿修改！
	/// </summary>
    [Serializable]
	public class GoodsInfo
	{
        ///<summary>
		/// 字段名： 
		/// 大小：16
		/// 是否允许为空：False
		///</summary>
        private Guid? goodsGuid;
        
        ///<summary>
		/// 字段名：名称 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
        private string goodsName;
        
        ///<summary>
		/// 字段名：照片 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
        private string goodsPic;
        
        ///<summary>
		/// 字段名：排序 
		/// 大小：200
		/// 是否允许为空：True
		///</summary>
        private string goodsDes;
        
        ///<summary>
		/// 字段名：状态0：平价1：促销2：推荐 
		/// 大小：4
		/// 是否允许为空：True
		///</summary>
        private int? goodsStatus;
        
        ///<summary>
		/// 字段名：价格 
		/// 大小：8
		/// 是否允许为空：True
		///</summary>
        private double? goodsPrice;
        
        ///<summary>
		/// 字段名：数量 
		/// 大小：4
		/// 是否允许为空：True
		///</summary>
        private int? goodsStock;
        
        ///<summary>
		/// 字段名：折扣价 
		/// 大小：8
		/// 是否允许为空：True
		///</summary>
        private double? goodsSale;
        
        ///<summary>
		/// 字段名：发布时间 
		/// 大小：8
		/// 是否允许为空：True
		///</summary>
        private DateTime? creattime;
        
        ///<summary>
		/// 字段名：所用桶 
		/// 大小：16
		/// 是否允许为空：True
		///</summary>
        private Guid? gBucketGuid;
        
        ///<summary>
		/// 字段名：买X增X 1|1 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
        private string goodsGiven;
        
		#region 公共属性
		
		///<summary>
		/// 字段名： 
		/// 大小：16
		/// 是否允许为空：False
		///</summary>
		[ColumnAttribute(2, OleDbType.Guid, 36)]
		public Guid? GoodsGuid
        {
            get { return goodsGuid; }
            set { goodsGuid = value; }
        }

		///<summary>
		/// 字段名：名称 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.VarChar, 50)]
		public string GoodsName
        {
            get { return goodsName; }
            set { goodsName = value; }
        }

		///<summary>
		/// 字段名：照片 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.VarChar, 50)]
		public string GoodsPic
        {
            get { return goodsPic; }
            set { goodsPic = value; }
        }

		///<summary>
		/// 字段名：排序 
		/// 大小：200
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.VarChar, 200)]
		public string GoodsDes
        {
            get { return goodsDes; }
            set { goodsDes = value; }
        }

		///<summary>
		/// 字段名：状态0：平价1：促销2：推荐 
		/// 大小：4
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.Integer, 4)]
		public int? GoodsStatus
        {
            get { return goodsStatus; }
            set { goodsStatus = value; }
        }

		///<summary>
		/// 字段名：价格 
		/// 大小：8
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.Double, 8)]
		public double? GoodsPrice
        {
            get { return goodsPrice; }
            set { goodsPrice = value; }
        }

		///<summary>
		/// 字段名：数量 
		/// 大小：4
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.Integer, 4)]
		public int? GoodsStock
        {
            get { return goodsStock; }
            set { goodsStock = value; }
        }

		///<summary>
		/// 字段名：折扣价 
		/// 大小：8
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.Double, 8)]
		public double? GoodsSale
        {
            get { return goodsSale; }
            set { goodsSale = value; }
        }

		///<summary>
		/// 字段名：发布时间 
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
		/// 字段名：所用桶 
		/// 大小：16
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.Guid, 36)]
		public Guid? GBucketGuid
        {
            get { return gBucketGuid; }
            set { gBucketGuid = value; }
        }

		///<summary>
		/// 字段名：买X增X 1|1 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.VarChar, 50)]
		public string GoodsGiven
        {
            get { return goodsGiven; }
            set { goodsGiven = value; }
        }
	
		#endregion
		
	}
	#endregion
}