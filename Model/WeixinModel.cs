using System;
using System.Data;
using System.Data.OleDb;

namespace Carrot.Model
{
	#region Weixin数据实体
	/// <summary>
	/// Weixin数据实体
	/// 创建时间：2015-10-29 12:05:16
	/// 负责人：Carrot
	/// 模板制作：王毅
	/// 特别说明：本文件为自动生成,请勿修改！
	/// </summary>
    [Serializable]
	public class WeixinInfo
	{
        ///<summary>
		/// 字段名： 
		/// 大小：16
		/// 是否允许为空：False
		///</summary>
        private Guid? weixinID;
        
        ///<summary>
		/// 字段名：发送方帐号（一个OpenID） 
		/// 大小：-1
		/// 是否允许为空：True
		///</summary>
        private string wXFromUserName;
        
        ///<summary>
		/// 字段名：开发者微信号 
		/// 大小：-1
		/// 是否允许为空：True
		///</summary>
        private string wXToUserName;
        
        ///<summary>
		/// 字段名：消息类型 
		/// 大小：10
		/// 是否允许为空：True
		///</summary>
        private string wXMsgType;
        
        ///<summary>
		/// 字段名：消息内容 
		/// 大小：500
		/// 是否允许为空：True
		///</summary>
        private string wXMsg;
        
        ///<summary>
		/// 字段名： 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
        private string wXCreatime;
        
        ///<summary>
		/// 字段名：地理位置维度 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
        private string wXLocationX;
        
        ///<summary>
		/// 字段名：地理位置经度 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
        private string wXLocationY;
        
        ///<summary>
		/// 字段名：地理位置信息 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
        private string wXLabel;
        
        ///<summary>
		/// 字段名：地图缩放大小 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
        private string wXScale;
        
        ///<summary>
		/// 字段名：图片链接 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
        private string wXPicUrl;
        
        ///<summary>
		/// 字段名：回复数据 
		/// 大小：-1
		/// 是否允许为空：True
		///</summary>
        private string wXReContent;
        
        ///<summary>
		/// 字段名：菜单的响应动作类型 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
        private string wXEvent;
        
        ///<summary>
		/// 字段名：菜单KEY值 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
        private string wXEventKey;
        
        ///<summary>
		/// 字段名：来自账号 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
        private string wXFromAccess;
        
        ///<summary>
		/// 字段名： 
		/// 大小：100
		/// 是否允许为空：True
		///</summary>
        private string wXmediaId;
        
        ///<summary>
		/// 字段名： 
		/// 大小：100
		/// 是否允许为空：True
		///</summary>
        private string wXFormat;
        
        ///<summary>
		/// 字段名： 
		/// 大小：100
		/// 是否允许为空：True
		///</summary>
        private string wXMsgID;
        
        ///<summary>
		/// 字段名： 
		/// 大小：-1
		/// 是否允许为空：True
		///</summary>
        private string wXScanResult;
        
        ///<summary>
		/// 字段名： 
		/// 大小：8
		/// 是否允许为空：True
		///</summary>
        private DateTime? creatime;
        
		#region 公共属性
		
		///<summary>
		/// 字段名： 
		/// 大小：16
		/// 是否允许为空：False
		///</summary>
		[ColumnAttribute(2, OleDbType.Guid, 36)]
		public Guid? WeixinID
        {
            get { return weixinID; }
            set { weixinID = value; }
        }

		///<summary>
		/// 字段名：发送方帐号（一个OpenID） 
		/// 大小：-1
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.VarChar, -1)]
		public string WXFromUserName
        {
            get { return wXFromUserName; }
            set { wXFromUserName = value; }
        }

		///<summary>
		/// 字段名：开发者微信号 
		/// 大小：-1
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.VarChar, -1)]
		public string WXToUserName
        {
            get { return wXToUserName; }
            set { wXToUserName = value; }
        }

		///<summary>
		/// 字段名：消息类型 
		/// 大小：10
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.VarChar, 10)]
		public string WXMsgType
        {
            get { return wXMsgType; }
            set { wXMsgType = value; }
        }

		///<summary>
		/// 字段名：消息内容 
		/// 大小：500
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.VarChar, 500)]
		public string WXMsg
        {
            get { return wXMsg; }
            set { wXMsg = value; }
        }

		///<summary>
		/// 字段名： 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.VarChar, 50)]
		public string WXCreatime
        {
            get { return wXCreatime; }
            set { wXCreatime = value; }
        }

		///<summary>
		/// 字段名：地理位置维度 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.VarChar, 50)]
		public string WXLocationX
        {
            get { return wXLocationX; }
            set { wXLocationX = value; }
        }

		///<summary>
		/// 字段名：地理位置经度 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.VarChar, 50)]
		public string WXLocationY
        {
            get { return wXLocationY; }
            set { wXLocationY = value; }
        }

		///<summary>
		/// 字段名：地理位置信息 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.VarChar, 50)]
		public string WXLabel
        {
            get { return wXLabel; }
            set { wXLabel = value; }
        }

		///<summary>
		/// 字段名：地图缩放大小 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.VarChar, 50)]
		public string WXScale
        {
            get { return wXScale; }
            set { wXScale = value; }
        }

		///<summary>
		/// 字段名：图片链接 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.VarChar, 50)]
		public string WXPicUrl
        {
            get { return wXPicUrl; }
            set { wXPicUrl = value; }
        }

		///<summary>
		/// 字段名：回复数据 
		/// 大小：-1
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.VarChar, -1)]
		public string WXReContent
        {
            get { return wXReContent; }
            set { wXReContent = value; }
        }

		///<summary>
		/// 字段名：菜单的响应动作类型 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.VarChar, 50)]
		public string WXEvent
        {
            get { return wXEvent; }
            set { wXEvent = value; }
        }

		///<summary>
		/// 字段名：菜单KEY值 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.VarChar, 50)]
		public string WXEventKey
        {
            get { return wXEventKey; }
            set { wXEventKey = value; }
        }

		///<summary>
		/// 字段名：来自账号 
		/// 大小：50
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.VarChar, 50)]
		public string WXFromAccess
        {
            get { return wXFromAccess; }
            set { wXFromAccess = value; }
        }

		///<summary>
		/// 字段名： 
		/// 大小：100
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.VarChar, 100)]
		public string WXmediaId
        {
            get { return wXmediaId; }
            set { wXmediaId = value; }
        }

		///<summary>
		/// 字段名： 
		/// 大小：100
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.VarChar, 100)]
		public string WXFormat
        {
            get { return wXFormat; }
            set { wXFormat = value; }
        }

		///<summary>
		/// 字段名： 
		/// 大小：100
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.VarChar, 100)]
		public string WXMsgID
        {
            get { return wXMsgID; }
            set { wXMsgID = value; }
        }

		///<summary>
		/// 字段名： 
		/// 大小：-1
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.VarChar, -1)]
		public string WXScanResult
        {
            get { return wXScanResult; }
            set { wXScanResult = value; }
        }

		///<summary>
		/// 字段名： 
		/// 大小：8
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.Date, 8)]
		public DateTime? Creatime
        {
            get { return creatime; }
            set { creatime = value; }
        }
	
		#endregion
		
	}
	#endregion
}