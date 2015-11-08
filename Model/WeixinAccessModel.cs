using System;
using System.Data;
using System.Data.OleDb;

namespace Carrot.Model
{
	#region WeixinAccess数据实体
	/// <summary>
	/// WeixinAccess数据实体
	/// 创建时间：2015-10-29 12:05:16
	/// 负责人：Carrot
	/// 模板制作：王毅
	/// 特别说明：本文件为自动生成,请勿修改！
	/// </summary>
    [Serializable]
	public class WeixinAccessInfo
	{
        ///<summary>
		/// 字段名：账号 
		/// 大小：50
		/// 是否允许为空：False
		///</summary>
        private string weixinAccess;
        
        ///<summary>
		/// 字段名：Access_token 
		/// 大小：512
		/// 是否允许为空：True
		///</summary>
        private string accessToken;
        
        ///<summary>
		/// 字段名：创建时间 
		/// 大小：8
		/// 是否允许为空：True
		///</summary>
        private DateTime? creatTime;
        
        ///<summary>
		/// 字段名： 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
        private string appid;
        
        ///<summary>
		/// 字段名： 
		/// 大小：100
		/// 是否允许为空：True
		///</summary>
        private string secret;
        
        ///<summary>
		/// 字段名： 
		/// 大小：512
		/// 是否允许为空：True
		///</summary>
        private string jsapiticket;
        
		#region 公共属性
		
		///<summary>
		/// 字段名：账号 
		/// 大小：50
		/// 是否允许为空：False
		///</summary>
		[ColumnAttribute(1, OleDbType.VarChar, 50)]
		public string WeixinAccess
        {
            get { return weixinAccess; }
            set { weixinAccess = value; }
        }

		///<summary>
		/// 字段名：Access_token 
		/// 大小：512
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.VarChar, 512)]
		public string AccessToken
        {
            get { return accessToken; }
            set { accessToken = value; }
        }

		///<summary>
		/// 字段名：创建时间 
		/// 大小：8
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.Date, 8)]
		public DateTime? CreatTime
        {
            get { return creatTime; }
            set { creatTime = value; }
        }

		///<summary>
		/// 字段名： 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.VarChar, 50)]
		public string Appid
        {
            get { return appid; }
            set { appid = value; }
        }

		///<summary>
		/// 字段名： 
		/// 大小：100
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.VarChar, 100)]
		public string Secret
        {
            get { return secret; }
            set { secret = value; }
        }

		///<summary>
		/// 字段名： 
		/// 大小：512
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.VarChar, 512)]
		public string Jsapiticket
        {
            get { return jsapiticket; }
            set { jsapiticket = value; }
        }
	
		#endregion
		
	}
	#endregion
}