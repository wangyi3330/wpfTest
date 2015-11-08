using System;
using System.Data;
using System.Data.OleDb;

namespace Carrot.Model
{
	#region Config数据实体
	/// <summary>
	/// Config数据实体
	/// 创建时间：2015-10-30 12:03:34
	/// 负责人：Carrot
	/// 模板制作：王毅
	/// 特别说明：本文件为自动生成,请勿修改！
	/// </summary>
    [Serializable]
	public class ConfigInfo
	{
        ///<summary>
		/// 字段名： 
		/// 大小：4
		/// 是否允许为空：False
		///</summary>
        private int? configID;
        
        ///<summary>
		/// 字段名：功能名 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
        private string configKey;
        
        ///<summary>
		/// 字段名：值 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
        private string configVal;
        
        ///<summary>
		/// 字段名：有效期 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
        private string configValidTime;
        
		#region 公共属性
		
		///<summary>
		/// 字段名： 
		/// 大小：4
		/// 是否允许为空：False
		///</summary>
		[ColumnAttribute(1, OleDbType.Integer, 4)]
		public int? ConfigID
        {
            get { return configID; }
            set { configID = value; }
        }

		///<summary>
		/// 字段名：功能名 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.VarChar, 50)]
		public string ConfigKey
        {
            get { return configKey; }
            set { configKey = value; }
        }

		///<summary>
		/// 字段名：值 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.VarChar, 50)]
		public string ConfigVal
        {
            get { return configVal; }
            set { configVal = value; }
        }

		///<summary>
		/// 字段名：有效期 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.VarChar, 50)]
		public string ConfigValidTime
        {
            get { return configValidTime; }
            set { configValidTime = value; }
        }
	
		#endregion
		
	}
	#endregion
}