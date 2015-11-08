using System;
using System.Data;
using System.Data.OleDb;

namespace Carrot.Model
{
	#region UserInfo数据实体
	/// <summary>
	/// UserInfo数据实体
	/// 创建时间：2015-10-29 12:05:12
	/// 负责人：Carrot
	/// 模板制作：王毅
	/// 特别说明：本文件为自动生成,请勿修改！
	/// </summary>
    [Serializable]
	public class UserInfoInfo
	{
        ///<summary>
		/// 字段名： 
		/// 大小：16
		/// 是否允许为空：False
		///</summary>
        private Guid? userID;
        
        ///<summary>
		/// 字段名：用户名 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
        private string userName;
        
        ///<summary>
		/// 字段名：用户密码 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
        private string userPassword;
        
        ///<summary>
		/// 字段名：性别 0男1女 
		/// 大小：4
		/// 是否允许为空：True
		///</summary>
        private int? userGender;
        
        ///<summary>
		/// 字段名：手机号 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
        private string userPhoneNum;
        
        ///<summary>
		/// 字段名：昵称 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
        private string userNike;
        
        ///<summary>
		/// 字段名：头像 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
        private string userPic;
        
        ///<summary>
		/// 字段名：用户类型 0：用户 1：送水工 
		/// 大小：4
		/// 是否允许为空：True
		///</summary>
        private int? userType;
        
        ///<summary>
		/// 字段名：注册时间 
		/// 大小：3
		/// 是否允许为空：True
		///</summary>
        private DateTime? creatData;
        
        ///<summary>
		/// 字段名：优惠券 
		/// 大小：8
		/// 是否允许为空：True
		///</summary>
        private double? usableCoupon;
        
		#region 公共属性
		
		///<summary>
		/// 字段名： 
		/// 大小：16
		/// 是否允许为空：False
		///</summary>
		[ColumnAttribute(2, OleDbType.Guid, 36)]
		public Guid? UserID
        {
            get { return userID; }
            set { userID = value; }
        }

		///<summary>
		/// 字段名：用户名 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.VarChar, 50)]
		public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

		///<summary>
		/// 字段名：用户密码 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.VarChar, 50)]
		public string UserPassword
        {
            get { return userPassword; }
            set { userPassword = value; }
        }

		///<summary>
		/// 字段名：性别 0男1女 
		/// 大小：4
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.Integer, 4)]
		public int? UserGender
        {
            get { return userGender; }
            set { userGender = value; }
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
		/// 字段名：头像 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.VarChar, 50)]
		public string UserPic
        {
            get { return userPic; }
            set { userPic = value; }
        }

		///<summary>
		/// 字段名：用户类型 0：用户 1：送水工 
		/// 大小：4
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.Integer, 4)]
		public int? UserType
        {
            get { return userType; }
            set { userType = value; }
        }

		///<summary>
		/// 字段名：注册时间 
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
		/// 字段名：优惠券 
		/// 大小：8
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.Double, 8)]
		public double? UsableCoupon
        {
            get { return usableCoupon; }
            set { usableCoupon = value; }
        }
	
		#endregion
		
	}
	#endregion
}