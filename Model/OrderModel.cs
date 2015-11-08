using System;
using System.Data;
using System.Data.OleDb;

namespace Carrot.Model
{
	#region Order数据实体
	/// <summary>
	/// Order数据实体
	/// 创建时间：2015-10-31 02:30:59
	/// 负责人：Carrot
	/// 模板制作：王毅
	/// 特别说明：本文件为自动生成,请勿修改！
	/// </summary>
    [Serializable]
	public class OrderInfo
	{
        ///<summary>
		/// 字段名： 
		/// 大小：16
		/// 是否允许为空：False
		///</summary>
        private Guid? orderID;
        
        ///<summary>
		/// 字段名：订单号 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
        private string orderIID;
        
        ///<summary>
		/// 字段名：请求配送时间 
		/// 大小：8
		/// 是否允许为空：True
		///</summary>
        private DateTime? creattime;
        
        ///<summary>
		/// 字段名：订单状态0：等待接单 1：已接单 2：正在派送 3：已送达 4：正在回桶 5：完成 6：已取消 
		/// 大小：4
		/// 是否允许为空：True
		///</summary>
        private int? orderStatus;
        
        ///<summary>
		/// 字段名：订水人 
		/// 大小：16
		/// 是否允许为空：True
		///</summary>
        private Guid? userID;
        
        ///<summary>
		/// 字段名：送水工 
		/// 大小：16
		/// 是否允许为空：True
		///</summary>
        private Guid? workerID;
        
        ///<summary>
		/// 字段名：配送地址 
		/// 大小：4
		/// 是否允许为空：True
		///</summary>
        private int? addressID;
        
        ///<summary>
		/// 字段名：配送水站 
		/// 大小：16
		/// 是否允许为空：True
		///</summary>
        private Guid? waterSiteID;
        
        ///<summary>
		/// 字段名：预计到达时间 
		/// 大小：8
		/// 是否允许为空：True
		///</summary>
        private DateTime? estimatedDeliveryTime;
        
        ///<summary>
		/// 字段名：到达时间 
		/// 大小：8
		/// 是否允许为空：True
		///</summary>
        private DateTime? finalDeliveryTime;
        
        ///<summary>
		/// 字段名：取消原因 
		/// 大小：100
		/// 是否允许为空：True
		///</summary>
        private string withdrawReaon;
        
		#region 公共属性
		
		///<summary>
		/// 字段名： 
		/// 大小：16
		/// 是否允许为空：False
		///</summary>
		[ColumnAttribute(2, OleDbType.Guid, 36)]
		public Guid? OrderID
        {
            get { return orderID; }
            set { orderID = value; }
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
		/// 字段名：请求配送时间 
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
		/// 字段名：订单状态0：等待接单 1：已接单 2：正在派送 3：已送达 4：正在回桶 5：完成 6：已取消 
		/// 大小：4
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(-1, OleDbType.Integer, 4)]
		public int? OrderStatus
        {
            get { return orderStatus; }
            set { orderStatus = value; }
        }

		///<summary>
		/// 字段名：订水人 
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
		/// 字段名：送水工 
		/// 大小：16
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.Guid, 36)]
		public Guid? WorkerID
        {
            get { return workerID; }
            set { workerID = value; }
        }

		///<summary>
		/// 字段名：配送地址 
		/// 大小：4
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.Integer, 4)]
		public int? AddressID
        {
            get { return addressID; }
            set { addressID = value; }
        }

		///<summary>
		/// 字段名：配送水站 
		/// 大小：16
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.Guid, 36)]
		public Guid? WaterSiteID
        {
            get { return waterSiteID; }
            set { waterSiteID = value; }
        }

		///<summary>
		/// 字段名：预计到达时间 
		/// 大小：8
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.Date, 8)]
		public DateTime? EstimatedDeliveryTime
        {
            get { return estimatedDeliveryTime; }
            set { estimatedDeliveryTime = value; }
        }

		///<summary>
		/// 字段名：到达时间 
		/// 大小：8
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.Date, 8)]
		public DateTime? FinalDeliveryTime
        {
            get { return finalDeliveryTime; }
            set { finalDeliveryTime = value; }
        }

		///<summary>
		/// 字段名：取消原因 
		/// 大小：100
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.VarChar, 100)]
		public string WithdrawReaon
        {
            get { return withdrawReaon; }
            set { withdrawReaon = value; }
        }
	
		#endregion
		
	}
	#endregion
}