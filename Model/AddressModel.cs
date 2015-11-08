using System;
using System.Data;
using System.Data.OleDb;

namespace Carrot.Model
{
	#region Address数据实体
	/// <summary>
	/// Address数据实体
	/// 创建时间：2015-10-29 05:42:11
	/// 负责人：Carrot
	/// 模板制作：王毅
	/// 特别说明：本文件为自动生成,请勿修改！
	/// </summary>
    [Serializable]
	public class AddressInfo
	{
        ///<summary>
		/// 字段名： 
		/// 大小：4
		/// 是否允许为空：False
		///</summary>
        private int? addressID;
        
        ///<summary>
		/// 字段名： 
		/// 大小：16
		/// 是否允许为空：True
		///</summary>
        private Guid? userID;
        
        ///<summary>
		/// 字段名：配送地址 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
        private string address;
        
        ///<summary>
		/// 字段名：默认地址０默认１备用 
		/// 大小：4
		/// 是否允许为空：True
		///</summary>
        private int? addressStatus;
        
        ///<summary>
		/// 字段名：坐标 
		/// 大小：200
		/// 是否允许为空：True
		///</summary>
        private string locationData;
        
		#region 公共属性
		
		///<summary>
		/// 字段名： 
		/// 大小：4
		/// 是否允许为空：False
		///</summary>
		[ColumnAttribute(1, OleDbType.Integer, 4)]
		public int? AddressID
        {
            get { return addressID; }
            set { addressID = value; }
        }

		///<summary>
		/// 字段名： 
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
		/// 字段名：默认地址０默认１备用 
		/// 大小：4
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.Integer, 4)]
		public int? AddressStatus
        {
            get { return addressStatus; }
            set { addressStatus = value; }
        }

		///<summary>
		/// 字段名：坐标 
		/// 大小：200
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.VarChar, 200)]
		public string LocationData
        {
            get { return locationData; }
            set { locationData = value; }
        }
	
		#endregion
		
	}
	#endregion
}