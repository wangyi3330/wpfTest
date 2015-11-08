using System;
using System.Data;
using System.Data.OleDb;

namespace Carrot.Model
{
	#region Version数据实体
	/// <summary>
	/// Version数据实体
	/// 创建时间：2015-10-29 12:05:16
	/// 负责人：Carrot
	/// 模板制作：王毅
	/// 特别说明：本文件为自动生成,请勿修改！
	/// </summary>
    [Serializable]
	public class VersionInfo
	{
        ///<summary>
		/// 字段名： 
		/// 大小：4
		/// 是否允许为空：False
		///</summary>
        private int? appVersionID;
        
        ///<summary>
		/// 字段名： 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
        private string newVersion;
        
        ///<summary>
		/// 字段名： 
		/// 大小：-1
		/// 是否允许为空：True
		///</summary>
        private string versionDes;
        
        ///<summary>
		/// 字段名： 
		/// 大小：8
		/// 是否允许为空：True
		///</summary>
        private DateTime? ceartetime;
        
		#region 公共属性
		
		///<summary>
		/// 字段名： 
		/// 大小：4
		/// 是否允许为空：False
		///</summary>
		[ColumnAttribute(1, OleDbType.Integer, 4)]
		public int? AppVersionID
        {
            get { return appVersionID; }
            set { appVersionID = value; }
        }

		///<summary>
		/// 字段名： 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.VarChar, 50)]
		public string NewVersion
        {
            get { return newVersion; }
            set { newVersion = value; }
        }

		///<summary>
		/// 字段名： 
		/// 大小：-1
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.VarChar, -1)]
		public string VersionDes
        {
            get { return versionDes; }
            set { versionDes = value; }
        }

		///<summary>
		/// 字段名： 
		/// 大小：8
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.Date, 8)]
		public DateTime? Ceartetime
        {
            get { return ceartetime; }
            set { ceartetime = value; }
        }
	
		#endregion
		
	}
	#endregion
}