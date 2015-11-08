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
	public class E_OrderEvaluate_OrderModel:OrderInfo
    {
        ///<summary>
        /// 字段名：配送地址 
        /// 大小：50
        /// 是否允许为空：True
        ///</summary>
        private string address;
        ///<summary>
        /// 字段名：水站名称 
        /// 大小：50
        /// 是否允许为空：True
        ///</summary>
        private string waterSiteName;
        ///<summary>
        /// 字段名：水站坐标 
        /// 大小：200
        /// 是否允许为空：True
        ///</summary>
        private string waterSiteLocationData;

        ///<summary>
        /// 字段名：水站地址 
        /// 大小：200
        /// 是否允许为空：True
        ///</summary>
        private string waterSiteAddress;

        ///<summary>
		/// 字段名：水站名称 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.VarChar, 50)]
        public string WaterSiteName
        {
            get { return waterSiteName; }
            set { waterSiteName = value; }
        }
        ///<summary>
        /// 字段名：水站坐标 
        /// 大小：200
        /// 是否允许为空：True
        ///</summary>
        [ColumnAttribute(0, OleDbType.VarChar, 200)]
        public string WaterSiteLocationData
        {
            get { return waterSiteLocationData; }
            set { waterSiteLocationData = value; }
        }

        ///<summary>
        /// 字段名：水站地址 
        /// 大小：200
        /// 是否允许为空：True
        ///</summary>
        [ColumnAttribute(0, OleDbType.VarChar, 200)]
        public string WaterSiteAddress
        {
            get { return waterSiteAddress; }
            set { waterSiteAddress = value; }
        }
        ///<summary>
		/// 字段名：配送地址 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.VarChar, 50)]
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        ///<summary>
        /// 字段名：用户名 
        /// 大小：50
        /// 是否允许为空：True
        ///</summary>
        private string userNike;
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
        /// 字段名：昵称 
        /// 大小：50
        /// 是否允许为空：True
        ///</summary>
        [ColumnAttribute(0, OleDbType.VarChar, 50)]
        public string UserNike
        {
            get { return userNike; }
            set { userNike = value; }
        }
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