using System;
using System.Data;
using System.Data.OleDb;

namespace Carrot.Model
{
	#region WaterSite数据实体
	/// <summary>
	/// WaterSite数据实体
	/// 创建时间：2015-10-30 11:36:15
	/// 负责人：Carrot
	/// 模板制作：王毅
	/// 特别说明：本文件为自动生成,请勿修改！
	/// </summary>
    [Serializable]
	public class WaterSiteInfo
	{
        ///<summary>
		/// 字段名： 
		/// 大小：16
		/// 是否允许为空：False
		///</summary>
        private Guid? waterSiteID;
        
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
		/// 字段名：水站状态 0：开启 1：关闭 
		/// 大小：4
		/// 是否允许为空：True
		///</summary>
        private int? waterSiteStatus;
        
		#region 公共属性
		
		///<summary>
		/// 字段名： 
		/// 大小：16
		/// 是否允许为空：False
		///</summary>
		[ColumnAttribute(2, OleDbType.Guid, 36)]
		public Guid? WaterSiteID
        {
            get { return waterSiteID; }
            set { waterSiteID = value; }
        }

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
		/// 字段名：水站状态 0：开启 1：关闭 
		/// 大小：4
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.Integer, 4)]
		public int? WaterSiteStatus
        {
            get { return waterSiteStatus; }
            set { waterSiteStatus = value; }
        }
	
		#endregion
		
	}
	#endregion
}