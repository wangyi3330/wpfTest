using System;
using System.Data;
using System.Data.OleDb;

namespace Carrot.Model
{
	#region Worker数据实体
	/// <summary>
	/// Worker数据实体
	/// 创建时间：2015-11-02 08:18:19
	/// 负责人：Carrot
	/// 模板制作：王毅
	/// 特别说明：本文件为自动生成,请勿修改！
	/// </summary>
    [Serializable]
	public class WorkerInfo
	{
        ///<summary>
		/// 字段名： 
		/// 大小：16
		/// 是否允许为空：False
		///</summary>
        private Guid? workerID;
        
        ///<summary>
		/// 字段名：用户名 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
        private string workerName;
        
        ///<summary>
		/// 字段名：密码 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
        private string workerPassword;
        
        ///<summary>
		/// 字段名：手机号 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
        private string workerPhoneNum;
        
        ///<summary>
		/// 字段名：姓名 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
        private string workerNike;
        
        ///<summary>
		/// 字段名：头像 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
        private string workerPic;
        
        ///<summary>
		/// 字段名：审核状态　０通过　１审核中　２未通过 
		/// 大小：4
		/// 是否允许为空：True
		///</summary>
        private int? auditStatus;
        
        ///<summary>
		/// 字段名： 
		/// 大小：500
		/// 是否允许为空：True
		///</summary>
        private string auditMsg;
        
        ///<summary>
		/// 字段名： 
		/// 大小：3
		/// 是否允许为空：True
		///</summary>
        private DateTime? creatData;
        
        ///<summary>
		/// 字段名：牌照号 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
        private string vehicleNo;
        
        ///<summary>
		/// 字段名：运载车型 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
        private string vehicleType;
        
        ///<summary>
		/// 字段名：用户等级 
		/// 大小：4
		/// 是否允许为空：True
		///</summary>
        private int? workerLevel;
        
        ///<summary>
		/// 字段名： 
		/// 大小：200
		/// 是否允许为空：True
		///</summary>
        private string workerLocationData;
        
		#region 公共属性
		
		///<summary>
		/// 字段名： 
		/// 大小：16
		/// 是否允许为空：False
		///</summary>
		[ColumnAttribute(2, OleDbType.Guid, 36)]
		public Guid? WorkerID
        {
            get { return workerID; }
            set { workerID = value; }
        }

		///<summary>
		/// 字段名：用户名 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.VarChar, 50)]
		public string WorkerName
        {
            get { return workerName; }
            set { workerName = value; }
        }

		///<summary>
		/// 字段名：密码 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.VarChar, 50)]
		public string WorkerPassword
        {
            get { return workerPassword; }
            set { workerPassword = value; }
        }

		///<summary>
		/// 字段名：手机号 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.VarChar, 50)]
		public string WorkerPhoneNum
        {
            get { return workerPhoneNum; }
            set { workerPhoneNum = value; }
        }

		///<summary>
		/// 字段名：姓名 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.VarChar, 50)]
		public string WorkerNike
        {
            get { return workerNike; }
            set { workerNike = value; }
        }

		///<summary>
		/// 字段名：头像 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.VarChar, 50)]
		public string WorkerPic
        {
            get { return workerPic; }
            set { workerPic = value; }
        }

		///<summary>
		/// 字段名：审核状态　０通过　１审核中　２未通过 
		/// 大小：4
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.Integer, 4)]
		public int? AuditStatus
        {
            get { return auditStatus; }
            set { auditStatus = value; }
        }

		///<summary>
		/// 字段名： 
		/// 大小：500
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.VarChar, 500)]
		public string AuditMsg
        {
            get { return auditMsg; }
            set { auditMsg = value; }
        }

		///<summary>
		/// 字段名： 
		/// 大小：3
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.DBDate, 3)]
		public DateTime? CreatData
        {
            get { return creatData; }
            set { creatData = value; }
        }

		///<summary>
		/// 字段名：牌照号 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.VarChar, 50)]
		public string VehicleNo
        {
            get { return vehicleNo; }
            set { vehicleNo = value; }
        }

		///<summary>
		/// 字段名：运载车型 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.VarChar, 50)]
		public string VehicleType
        {
            get { return vehicleType; }
            set { vehicleType = value; }
        }

		///<summary>
		/// 字段名：用户等级 
		/// 大小：4
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.Integer, 4)]
		public int? WorkerLevel
        {
            get { return workerLevel; }
            set { workerLevel = value; }
        }

		///<summary>
		/// 字段名： 
		/// 大小：200
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.VarChar, 200)]
		public string WorkerLocationData
        {
            get { return workerLocationData; }
            set { workerLocationData = value; }
        }
	
		#endregion
		
	}
	#endregion
}