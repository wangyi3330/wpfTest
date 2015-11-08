using System;
using System.Data;
using System.Data.OleDb;

namespace Carrot.Model
{
	#region Bucket数据实体
	/// <summary>
	/// Bucket数据实体
	/// 创建时间：2015-10-29 12:05:12
	/// 负责人：Carrot
	/// 模板制作：王毅
	/// 特别说明：本文件为自动生成,请勿修改！
	/// </summary>
    [Serializable]
	public class BucketInfo
	{
        ///<summary>
		/// 字段名： 
		/// 大小：16
		/// 是否允许为空：False
		///</summary>
        private Guid? bucketGuid;
        
        ///<summary>
		/// 字段名：空桶名称 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
        private string bucketName;
        
        ///<summary>
		/// 字段名：空桶描述 
		/// 大小：200
		/// 是否允许为空：True
		///</summary>
        private string bucketDes;
        
        ///<summary>
		/// 字段名：状态 
		/// 大小：4
		/// 是否允许为空：True
		///</summary>
        private int? bucketStatus;
        
        ///<summary>
		/// 字段名：价钱 
		/// 大小：8
		/// 是否允许为空：True
		///</summary>
        private double? bucketPrice;
        
        ///<summary>
		/// 字段名：时间 
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
		public Guid? BucketGuid
        {
            get { return bucketGuid; }
            set { bucketGuid = value; }
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

		///<summary>
		/// 字段名：状态 
		/// 大小：4
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.Integer, 4)]
		public int? BucketStatus
        {
            get { return bucketStatus; }
            set { bucketStatus = value; }
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
		/// 字段名：时间 
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