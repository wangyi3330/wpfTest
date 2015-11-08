using System;
using System.Data;
using System.Data.OleDb;

namespace Carrot.Model
{
	#region OrderItem数据实体
	/// <summary>
	/// OrderItem数据实体
	/// 创建时间：2015-09-25 04:27:35
	/// 负责人：Carrot
	/// 模板制作：王毅
	/// 特别说明：本文件为自动生成,请勿修改！
	/// </summary>
    [Serializable]
	public class E_OrderItem_Goods_BucketModel: GoodsInfo
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
        /// 字段名：订单中商品状态 0：未完成 1：已完成  （对于空桶来说是0：未退 1：退回） 
        /// 大小：4
        /// 是否允许为空：True
        ///</summary>
        private int? orderItemStatus;

        ///<summary>
        /// 字段名：用户名（优化查询） 
        /// 大小：50
        /// 是否允许为空：True
        ///</summary>
        private Guid? userID;
        ///<summary>
        /// 字段名：价钱 
        /// 大小：8
        /// 是否允许为空：True
        ///</summary>
        private double? bucketPrice;

        ///<summary>
        /// 字段名：空桶名称 
        /// 大小：50
        /// 是否允许为空：True
        ///</summary>
        private string bucketName;

        private string bucketDes;
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
        /// 字段名：订单中商品状态 0：未完成 1：已完成  （对于空桶来说是0：未退 1：退回） 
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
        /// 字段名：价钱 
        /// 大小：8
        /// 是否允许为空：True
        ///</summary>
        [ColumnAttribute(0, OleDbType.Double, 8)]
        public double? BucketPrice
        {
            get { return bucketPrice; }
            set { bucketPrice = value; }
        }


        ///<summary>
        /// 字段名：空桶名称 
        /// 大小：50
        /// 是否允许为空：True
        ///</summary>
        [ColumnAttribute(0, OleDbType.VarChar, 50)]
        public string BucketName
        {
            get { return bucketName; }
            set { bucketName = value; }
        }

        ///<summary>
        /// 字段名：空桶描述 
        /// 大小：200
        /// 是否允许为空：True
        ///</summary>
        [ColumnAttribute(0, OleDbType.VarChar, 200)]
        public string BucketDes
        {
            get { return bucketDes; }
            set { bucketDes = value; }
        }
        #endregion
    }
    #endregion
}