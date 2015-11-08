using System;
using System.Data;
using System.Data.OleDb;

namespace Carrot.Model
{
	#region VerifyCode数据实体
	/// <summary>
	/// VerifyCode数据实体
	/// 创建时间：2015-10-29 12:05:16
	/// 负责人：Carrot
	/// 模板制作：王毅
	/// 特别说明：本文件为自动生成,请勿修改！
	/// </summary>
    [Serializable]
	public class VerifyCodeInfo
	{
        ///<summary>
		/// 字段名： 
		/// 大小：4
		/// 是否允许为空：False
		///</summary>
        private int? verifyCodeID;
        
        ///<summary>
		/// 字段名：手机号 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
        private string userPhoneNum;
        
        ///<summary>
		/// 字段名：验证码 
		/// 大小：4
		/// 是否允许为空：True
		///</summary>
        private int? verifyCode;
        
        ///<summary>
		/// 字段名：时间 
		/// 大小：8
		/// 是否允许为空：True
		///</summary>
        private DateTime? creatTime;
        
		#region 公共属性
		
		///<summary>
		/// 字段名： 
		/// 大小：4
		/// 是否允许为空：False
		///</summary>
		[ColumnAttribute(1, OleDbType.Integer, 4)]
		public int? VerifyCodeID
        {
            get { return verifyCodeID; }
            set { verifyCodeID = value; }
        }

		///<summary>
		/// 字段名：手机号 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.VarChar, 50)]
		public string UserPhoneNum
        {
            get { return userPhoneNum; }
            set { userPhoneNum = value; }
        }

		///<summary>
		/// 字段名：验证码 
		/// 大小：4
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.Integer, 4)]
		public int? VerifyCode
        {
            get { return verifyCode; }
            set { verifyCode = value; }
        }

		///<summary>
		/// 字段名：时间 
		/// 大小：8
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.Date, 8)]
		public DateTime? CreatTime
        {
            get { return creatTime; }
            set { creatTime = value; }
        }
	
		#endregion
		
	}
	#endregion
}