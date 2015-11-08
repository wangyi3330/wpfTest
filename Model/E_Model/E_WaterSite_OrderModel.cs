using System;
using System.Data;
using System.Data.OleDb;

namespace Carrot.Model
{
	#region WaterSite数据实体
	/// <summary>
	/// WaterSite数据实体
	/// 创建时间：2015-09-30 10:39:48
	/// 负责人：Carrot
	/// 模板制作：王毅
	/// 特别说明：本文件为自动生成,请勿修改！
	/// </summary>
    [Serializable]
	public class E_WaterSite_OrderModel:OrderInfo
	{
        ///<summary>
        /// 字段名：姓名 
        /// 大小：50
        /// 是否允许为空：True
        ///</summary>
        private string workerNike;
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

        #region 公共属性
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
	
		#endregion
		
	}
	#endregion
}