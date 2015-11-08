using System;
using System.Data;
using System.Data.OleDb;

namespace Carrot.Model
{
	#region News数据实体
	/// <summary>
	/// News数据实体
	/// 创建时间：2015-10-29 05:29:53
	/// 负责人：Carrot
	/// 模板制作：王毅
	/// 特别说明：本文件为自动生成,请勿修改！
	/// </summary>
    [Serializable]
	public class NewsInfo
	{
        ///<summary>
		/// 字段名： 
		/// 大小：4
		/// 是否允许为空：False
		///</summary>
        private int? newsID;
        
        ///<summary>
		/// 字段名：公告消息 
		/// 大小：-1
		/// 是否允许为空：True
		///</summary>
        private string newsSummary;
        
        ///<summary>
		/// 字段名：发布时间 
		/// 大小：8
		/// 是否允许为空：True
		///</summary>
        private DateTime? creattime;
        
        ///<summary>
		/// 字段名：公告状态0：发布 1：不发布 
		/// 大小：4
		/// 是否允许为空：True
		///</summary>
        private int? newsStatus;
        
        ///<summary>
		/// 字段名：公告类型 0公用 1订水 2送水 
		/// 大小：4
		/// 是否允许为空：True
		///</summary>
        private int? newsType;
        
		#region 公共属性
		
		///<summary>
		/// 字段名： 
		/// 大小：4
		/// 是否允许为空：False
		///</summary>
		[ColumnAttribute(1, OleDbType.Integer, 4)]
		public int? NewsID
        {
            get { return newsID; }
            set { newsID = value; }
        }

		///<summary>
		/// 字段名：公告消息 
		/// 大小：-1
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.VarChar, -1)]
		public string NewsSummary
        {
            get { return newsSummary; }
            set { newsSummary = value; }
        }

		///<summary>
		/// 字段名：发布时间 
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
		/// 字段名：公告状态0：发布 1：不发布 
		/// 大小：4
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.Integer, 4)]
		public int? NewsStatus
        {
            get { return newsStatus; }
            set { newsStatus = value; }
        }

		///<summary>
		/// 字段名：公告类型 0公用 1订水 2送水 
		/// 大小：4
		/// 是否允许为空：True
		///</summary>
		[ColumnAttribute(0, OleDbType.Integer, 4)]
		public int? NewsType
        {
            get { return newsType; }
            set { newsType = value; }
        }
	
		#endregion
		
	}
	#endregion
}