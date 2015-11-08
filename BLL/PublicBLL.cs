using Carrot.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace Carrot.BLL
{
    public class PublicBLL : DALToBLL
    {

        /// <summary>
        ///计算两点GPS坐标的距离
        /// </summary>
        /// <param name="n1">第一点的纬度坐标</param>
        /// <param name="e1">第一点的经度坐标</param>
        /// <param name="n2">第二点的纬度坐标</param>
        /// <param name="e2">第二点的经度坐标</param>
        /// <returns></returns>
        public static double GetDistance(double n1, double e1, double n2, double e2)
        {
            double jl_jd = 102834.74258026089786013677476285;
            double jl_wd = 111712.69150641055729984301412873;
            double b = Math.Abs((e1 - e2) * jl_jd);
            double a = Math.Abs((n1 - n2) * jl_wd);
            return Math.Sqrt((a * a + b * b));

        }
        /// <summary>
        /// 更新配送缓存
        /// </summary>
        /// <returns></returns>
        public static List<E_Address_OrderModel> UpdateCache()
        {
            List<WhereParameter> wp = new List<WhereParameter>();
            wp.Add(new WhereParameter("OrderStatus", WhereParameter.Query.等于, "0"));
            List<JoinModel> jm = new List<JoinModel>();
            jm.Add(new JoinModel(new OrderInfo(), "AddressID", new AddressInfo(), "AddressID", JoinModel.Join.左联));
            List<E_Address_OrderModel> orderList = PublicBLL.GetLists(jm, wp, new E_Address_OrderModel()).OrderBy(OrderInfo => OrderInfo.Creattime).ToList();

            //wp = new List<WhereParameter>();
            //wp.Add(new WhereParameter("OType", WhereParameter.Query.等于, "0",WhereParameter.OrAnd.And));//水
            //wp.Add(new WhereParameter("OrderItemStatus", WhereParameter.Query.等于, "0"));//未送
            //jm = new List<JoinModel>();
            //jm.Add(new JoinModel(new OrderItemInfo(), "GoodsOrBucketGuid", new GoodsInfo(), "GoodsGuid", JoinModel.Join.左联));
            //List<E_OrderItem_Goods_BucketModel> orderItemList = PublicBLL.GetLists(jm, wp, new E_OrderItem_Goods_BucketModel()).ToList();


            SiteCache.Insert("OrderList", orderList, 600, System.Web.Caching.CacheItemPriority.NotRemovable);

            return orderList;
        }
        /// <summary>
        /// 判断用户名是否存在
        /// </summary>
        /// <param name="uName">用户名</param>
        /// <param name="uPhoneNum">手机号</param>
        /// <returns></returns>
        public static bool IsCheckUserExist(string uName)
        {
            return PublicBLL.GetList(new UserInfoInfo(), new WhereParameter("UserName", WhereParameter.Query.等于, uName)).Count > 0 ? true : false;
        }
        /// <summary>
        /// 判断手机号是否存在
        /// </summary>
        /// <param name="uPhoneNum"></param>
        /// <returns></returns>
        public static bool IsCheckPhoneNumExist(string uPhoneNum)
        {
            return PublicBLL.GetList(new UserInfoInfo(), new WhereParameter("UserPhoneNum", WhereParameter.Query.等于, uPhoneNum)).Count > 0 ? true : false;
        }

        /// <summary>
        /// 连接短信接口
        /// </summary>
        /// <param name="uPhoneNum">手机号</param>
        /// <param name="msg">内容</param>
        /// <returns>0:成功,1:失败</returns>
        public static int PostSend(string uPhoneNum, string msg)
        {
            string postdate = "uid=150451&pwd=" + HashString("SAadmin12").ToLower() + "&mobile=" + uPhoneNum + "&content=" + System.Web.HttpUtility.UrlEncode(msg, Encoding.GetEncoding("gbk"));
            string url = "http://http.yunsms.cn/tx/";

            HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            myHttpWebRequest.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
            myHttpWebRequest.Method = "POST";
            Stream myRequestStream = myHttpWebRequest.GetRequestStream();
            StreamWriter myStreamWriter = new StreamWriter(myRequestStream);
            myStreamWriter.Write(postdate);
            myStreamWriter.Flush();
            myStreamWriter.Close();
            myRequestStream.Close();

            HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
            Stream myResponseStream = myHttpWebResponse.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            String outdata = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            //XmlDocument doc = new XmlDocument();
            //doc.LoadXml(outdata);
            ////returnstatus  -------返回状态值：成功返回Success 失败返回：Faild
            ////message --------------返回信息提示：见下表
            ////payinfo --------------返回支付方式  后付费，预付费
            ////overage -------------返回已发送条数
            ////sendTotal  ----返回总点数  当支付方式为预付费是返回总充值点数

            //string jsonText = JsonConvert.SerializeXmlNode(doc);

            //JArray json = (JArray)JsonConvert.DeserializeObject("[" + jsonText + "]");
            int returnstatus = outdata == "100" ? 0 : 1;

            return returnstatus;
        }

        /// <summary>
        /// 检测验证码是否过期（默认时间为30分钟）
        /// </summary>
        /// <param name="uPhoneNum">手机号</param>
        /// <param name="uVerifyCode">验证码</param>
        /// <returns></returns>
        public static bool isVerifyCodeTimeout(string uPhoneNum, string uVerifyCode)
        {
            List<WhereParameter> wp = new List<WhereParameter>();
            wp.Add(new WhereParameter("UserPhoneNum", WhereParameter.Query.等于, uPhoneNum, WhereParameter.OrAnd.And));
            wp.Add(new WhereParameter("CreatTime", WhereParameter.Query.大于等于, DateTime.Now.AddMinutes(-30), WhereParameter.OrAnd.And));
            wp.Add(new WhereParameter("VerifyCode", WhereParameter.Query.等于, uVerifyCode));
            return PublicBLL.GetList(new VerifyCodeInfo(), wp).Count > 0 ? false : true;
        }

        /// <summary>
        /// 格式化返回Json
        /// </summary>
        /// <param name="serverStatus"></param>
        /// <param name="returnValue"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public static string returnJson(int serverStatus, string returnValue, string errorMsg)
        {
            StringBuilder str = new StringBuilder();
            str.Append("{serverStatus:'" + serverStatus + "',");
            str.Append("returnValue:" + returnValue + ",");
            str.Append("errorMsg:'" + errorMsg + "'}");
            //return str.ToString();
            JArray json = (JArray)JsonConvert.DeserializeObject("[" + str + "]");
            return json.ToString().TrimStart('[').TrimEnd(']');
        }

        //---------------------------------------------
        private static MD5 md5 = MD5.Create();

        /// <summary> 
        /// 使用utf8编码将字符串散列 
        /// </summary> 
        /// <param name="sourceString">要散列的字符串</param> 
        /// <returns>散列后的字符串</returns> 
        public static string HashString(string sourceString)
        {
            return HashString(Encoding.UTF8, sourceString);
        }
        /// <summary> 
        /// 使用指定的编码将字符串散列 
        /// </summary> 
        /// <param name="encode">编码</param> 
        /// <param name="sourceString">要散列的字符串</param> 
        /// <returns>散列后的字符串</returns> 
        public static string HashString(Encoding encode, string sourceString)
        {
            byte[] source = md5.ComputeHash(encode.GetBytes(sourceString));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < source.Length; i++)
            {
                sBuilder.Append(source[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        /// <summary>
        /// 选择RadioButtonList绑定控件
        /// </summary>
        /// <param name="rbl"></param>
        /// <param name="value"></param>
        public static bool SelectContrl(RadioButtonList rbl, string value)
        {
            for (int i = 0; i < rbl.Items.Count; i++)
            {
                if (rbl.Items[i].Value == value)
                {
                    rbl.SelectedIndex = i;
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 选择RadioButtonList绑定控件
        /// </summary>
        /// <param name="rbl"></param>
        /// <param name="value"></param>
        public static bool SelectContrl(ListBox rbl, string value)
        {
            for (int i = 0; i < rbl.Items.Count; i++)
            {
                if (rbl.Items[i].Value == value)
                {
                    rbl.SelectedIndex = i;
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 选择RadioButtonList绑定控件
        /// </summary>
        /// <param name="rbl"></param>
        /// <param name="value"></param>
        public static bool SelectContrl(CheckBoxList cbl, string[] value)
        {
            for (int i = 0; i < cbl.Items.Count; i++)
            {
                foreach (string item in value)
                {
                    if (cbl.Items[i].Value == item)
                    {
                        cbl.Items[i].Selected = true;
                        return true;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// 选择RadioButtonList绑定控件
        /// </summary>
        /// <param name="rbl"></param>
        /// <param name="value"></param>
        public static bool SelectContrlText(CheckBoxList cbl, string[] text)
        {
            for (int i = 0; i < cbl.Items.Count; i++)
            {
                foreach (string item in text)
                {
                    if (cbl.Items[i].Text == item)
                    {
                        cbl.Items[i].Selected = true;
                        return true;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// 选择DropDownList绑定控件
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="value"></param>
        public static bool SelectContrl(DropDownList ddl, string value)
        {
            for (int i = 0; i < ddl.Items.Count; i++)
            {
                if (!string.IsNullOrEmpty(ddl.Items[i].Value) && !string.IsNullOrEmpty(value) && ddl.Items[i].Value.ToUpper() == value.ToUpper())
                {
                    ddl.SelectedIndex = i;
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 选择DropDownList绑定控件
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="value"></param>
        public static bool SelectContrlText(DropDownList ddl, string text)
        {
            for (int i = 0; i < ddl.Items.Count; i++)
            {
                if (ddl.Items[i].Text == text)
                {
                    ddl.SelectedIndex = i;
                    return true;
                }
            }
            return true;
        }
        /// <summary>
        /// 验证表单
        /// </summary>
        /// <param name="ccpList"></param>
        /// <param name="UP"></param>
        /// <returns></returns>
        public static bool CheckForm(List<CheckControlParams> ccpList, UpdatePanel UP)
        {
            bool ispass = true;
            foreach (CheckControlParams item in ccpList)
            {
                switch (item.Type)
                {
                    case CheckControlParams.ValueType.String:
                        if (string.IsNullOrEmpty(item.Value))
                        {
                            PromptBLL.AjaxWarning(UP, item.Name, item.Errormsg);
                            ispass = false;
                        }
                        break;
                    case CheckControlParams.ValueType.Int:
                        int i;
                        if (item.IsRequired && string.IsNullOrEmpty(item.Value))
                        {
                            PromptBLL.AjaxWarning(UP, item.Name, item.Errormsg);
                            ispass = false;
                        }
                        else if (!string.IsNullOrEmpty(item.Value) && !int.TryParse(item.Value, out i))
                        {
                            PromptBLL.AjaxWarning(UP, item.Name, item.Errormsg);
                            ispass = false;
                        }
                        break;
                    case CheckControlParams.ValueType.Double:
                        double i2;
                        if (item.IsRequired && string.IsNullOrEmpty(item.Value))
                        {
                            PromptBLL.AjaxWarning(UP, item.Name, item.Errormsg);
                            ispass = false;
                        }
                        else if (!string.IsNullOrEmpty(item.Value) && !double.TryParse(item.Value, out i2))
                        {
                            PromptBLL.AjaxWarning(UP, item.Name, item.Errormsg);
                            ispass = false;
                        }
                        break;
                    case CheckControlParams.ValueType.Datetime:
                        DateTime i3;
                        if (item.IsRequired && string.IsNullOrEmpty(item.Value))
                        {
                            PromptBLL.AjaxWarning(UP, item.Name, item.Errormsg);
                            ispass = false;
                        }
                        else if (!string.IsNullOrEmpty(item.Value) && !DateTime.TryParse(item.Value, out i3))
                        {
                            PromptBLL.AjaxWarning(UP, item.Name, item.Errormsg);
                            ispass = false;
                        }
                        break;
                }
            }
            return ispass;
        }
        /// <summary>
        /// 按时间判断是否在线
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string isOnline(object time)
        {
            if (time != null)
            {
                if (Convert.ToDateTime(time) < DateTime.Now.Subtract(new TimeSpan(0, 0, 30)))
                    return "/PublicImages/pimg_offline.png";
                else
                    return "/PublicImages/pimg_online.png";
            }
            return "/PublicImages/pimg_offline.png";
        }
        /// <summary>
        /// 委托验证方法
        /// </summary>
        /// <param name="typeID"></param>
        /// <returns></returns>
        public delegate bool CheckDelegate(string typeID);

        /// <summary>
        /// 判断是否重名
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static bool CheckNameExitis<T>(T t, string ColumnsName, object Value) where T : new()
        {
            return CheckNameExitis(t, ColumnsName, Value, null);
        }
        /// <summary>
        /// 判断是否重名
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static bool CheckNameExitis<T>(T t, string ColumnsName, object Value, object NotValue) where T : new()
        {
            List<WhereParameter> wp = new List<WhereParameter>();
            wp.Add(new WhereParameter(ColumnsName, WhereParameter.Query.等于, Value, WhereParameter.OrAnd.And));
            if (NotValue != null)
                wp.Add(new WhereParameter(ColumnsName, WhereParameter.Query.不等于, NotValue));
            if (GetList(t, wp).Count > 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 批量选择带验证(GridView)
        /// </summary>
        /// <param name="GridView1"></param>
        /// <returns></returns>
        public static List<string> MultiList(GridView gridView, CheckDelegate checkDelegate)
        {
            try
            {
                List<string> tlist = new List<string>();
                List<WhereParameter> wp = new List<WhereParameter>();
                for (int i = 0; i < gridView.Rows.Count; i++)
                {
                    if ((gridView.Rows[i].Cells[0].FindControl("CheckBox1") as CheckBox).Checked)
                    {
                        HiddenField hf_DataKeyID = gridView.Rows[i].Cells[0].FindControl("hf_DataKeyID") as HiddenField;
                        if (checkDelegate == null || checkDelegate(hf_DataKeyID.Value))
                            tlist.Add(hf_DataKeyID.Value);
                    }
                }
                return tlist;
            }
            catch
            {
                return new List<string>();
            }
        }

        /// <summary>
        /// 批量删除(GridView)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gridView"></param>
        /// <param name="t"></param>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public static List<T> MultiDelList<T>(GridView gridView, T t, string KeyID) where T : new()
        {
            return MultiDelList(gridView, t, KeyID, null);
        }
        /// <summary>
        /// 批量删除带验证(GridView)
        /// </summary>
        /// <param name="GridView1"></param>
        /// <returns></returns>
        public static List<T> MultiDelList<T>(GridView gridView, T t, string KeyID, CheckDelegate checkDelegate) where T : new()
        {
            try
            {
                List<WhereParameter> wp = new List<WhereParameter>();
                List<WhereParameter> errorwp = new List<WhereParameter>();
                for (int i = 0; i < gridView.Rows.Count; i++)
                {
                    if ((gridView.Rows[i].Cells[0].FindControl("CheckBox1") as CheckBox).Checked)
                    {
                        HiddenField hf_DataKeyID = gridView.Rows[i].Cells[0].FindControl("hf_DataKeyID") as HiddenField;
                        if (checkDelegate == null || checkDelegate(hf_DataKeyID.Value))
                            wp.Add(new WhereParameter(KeyID, WhereParameter.Query.等于, hf_DataKeyID.Value, WhereParameter.OrAnd.Or));
                        else
                            errorwp.Add(new WhereParameter(KeyID, WhereParameter.Query.等于, hf_DataKeyID.Value, WhereParameter.OrAnd.Or));
                    }
                }
                Delete(new ModelWhereParams(t, wp), "");
                if (errorwp.Count > 0)
                    return GetList(t, errorwp);
                else
                    return new List<T>();
            }
            catch
            {
                return new List<T>();
            }
        }
        /// <summary>
        /// 批量删除(Repeater)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="repeater"></param>
        /// <param name="t"></param>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public static List<T> MultiDelList<T>(Repeater repeater, T t, string KeyID) where T : new()
        {
            return MultiDelList(repeater, t, KeyID, null);
        }
        /// <summary>
        /// 批量删除带验证(Repeater)
        /// </summary>
        /// <param name="Repeater1"></param>
        /// <returns></returns>
        public static List<T> MultiDelList<T>(Repeater repeater, T t, string KeyID, CheckDelegate checkDelegate) where T : new()
        {
            try
            {
                List<WhereParameter> wp = new List<WhereParameter>();
                List<WhereParameter> errorwp = new List<WhereParameter>();
                for (int i = 0; i < repeater.Items.Count; i++)
                {
                    if ((repeater.Items[i].FindControl("CheckBox1") as CheckBox).Checked)
                    {
                        HiddenField hf_DataKeyID = repeater.Items[i].FindControl("hf_DataKeyID") as HiddenField;
                        if (checkDelegate == null || checkDelegate(hf_DataKeyID.Value))
                            wp.Add(new WhereParameter(KeyID, WhereParameter.Query.等于, hf_DataKeyID.Value, WhereParameter.OrAnd.Or));
                        else
                            errorwp.Add(new WhereParameter(KeyID, WhereParameter.Query.等于, hf_DataKeyID.Value, WhereParameter.OrAnd.Or));
                    }
                }
                Delete(new ModelWhereParams(t, wp), "");
                if (errorwp.Count > 0)
                    return GetList(t, errorwp);
                else
                    return new List<T>();
            }
            catch
            {
                return new List<T>();
            }
        }
        /// <summary>
        /// 批量删除(DataList)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataList"></param>
        /// <param name="t"></param>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public static List<T> MultiDelList<T>(DataList dataList, T t, string KeyID) where T : new()
        {
            return MultiDelList(dataList, t, KeyID, null);
        }
        /// <summary>
        /// 批量删除带验证(DataList)
        /// </summary>
        /// <param name="DataList1"></param>
        /// <returns></returns>
        public static List<T> MultiDelList<T>(DataList dataList, T t, string KeyID, CheckDelegate checkDelegate) where T : new()
        {
            try
            {
                List<WhereParameter> wp = new List<WhereParameter>();
                List<WhereParameter> errorwp = new List<WhereParameter>();
                for (int i = 0; i < dataList.Items.Count; i++)
                {
                    if ((dataList.Items[i].FindControl("CheckBox1") as CheckBox).Checked)
                    {
                        HiddenField hf_DataKeyID = dataList.Items[i].FindControl("hf_DataKeyID") as HiddenField;
                        if (checkDelegate == null || checkDelegate(hf_DataKeyID.Value))
                            wp.Add(new WhereParameter(KeyID, WhereParameter.Query.等于, hf_DataKeyID.Value, WhereParameter.OrAnd.Or));
                        else
                            errorwp.Add(new WhereParameter(KeyID, WhereParameter.Query.等于, hf_DataKeyID.Value, WhereParameter.OrAnd.Or));
                    }
                }
                Delete(new ModelWhereParams(t, wp), "");
                if (errorwp.Count > 0)
                    return GetList(t, errorwp);
                else
                    return new List<T>();
            }
            catch
            {
                return new List<T>();
            }
        }

        /// <summary>
        /// 检查权限
        /// </summary>
        /// <param name="AccountGroup"></param>
        /// <param name="Groups"></param>
        /// <returns></returns>
        public static bool CheckGroup(string AccountGroup, string Groups)
        {
            string[] accountGroup = AccountGroup.Trim('|').Split('|');
            string[] groups = Groups.Trim('|').Split('|');

            foreach (string item in groups)
            {
                foreach (var item2 in accountGroup)
                {
                    string[] _accountGroup = item2.Split(',');
                    string[] _groups = item.Split(',');
                    if ((string.IsNullOrEmpty(_groups[0]) || _accountGroup[0].IndexOf(_groups[0]) != -1)
                        && (string.IsNullOrEmpty(_groups[1]) || _accountGroup[1].IndexOf(_groups[1]) != -1)
                        && (string.IsNullOrEmpty(_groups[2]) || _accountGroup[2].IndexOf(_groups[2]) != -1))
                        return true;
                }

            }
            return false;
        }
        /// <summary>
        /// 生成部门岗位规范
        /// </summary>
        /// <param name="DepartParentValue"></param>
        /// <param name="DepartValue"></param>
        /// <param name="PostTypeValue"></param>
        /// <param name="PostValue"></param>
        /// <param name="AccountIds"></param>
        /// <returns></returns>
        public static string CompetenceCustom(string DepartParentValue, string DepartValue, string PostTypeValue, string PostValue, string AccountIds)
        {
            return CompetenceCustom(DepartParentValue, DepartValue, PostTypeValue, PostValue, AccountIds, "", "");
        }
        /// <summary>
        /// 生成部门岗位规范
        /// </summary>
        /// <param name="DepartParentValue"></param>
        /// <param name="DepartValue"></param>
        /// <param name="PostTypeValue"></param>
        /// <param name="PostValue"></param>
        /// <returns></returns>
        public static string CompetenceCustom(string DepartParentValue, string DepartValue, string PostTypeValue, string PostValue, string AccountIds, string IsEdit, string IsFrom)
        {
            string departParentValue = "";
            string departValue = "";
            string postTypeValue = "";
            string postValue = "";
            string isEdit = "";
            string isFrom = "";

            string Result = "";
            if (!string.IsNullOrEmpty(DepartParentValue))
                departParentValue = "_" + DepartParentValue;
            else
                departParentValue = "_-";
            if (!string.IsNullOrEmpty(DepartValue))
                departValue = "_" + DepartValue + "_";
            else
                departValue = "_-_";
            if (!string.IsNullOrEmpty(PostTypeValue))
                postTypeValue = "_" + PostTypeValue;
            else
                postTypeValue = "_-";
            if (!string.IsNullOrEmpty(PostValue))
                postValue = "_" + PostValue + "_";
            else
                postValue = "_-_";

            if (!string.IsNullOrEmpty(IsFrom))
                isFrom = "_" + IsFrom + "_";
            else
                isFrom = "_-_";
            if (!string.IsNullOrEmpty(IsEdit))
                isEdit = IsEdit + "_";
            else
                isEdit = "-_";
            if (!string.IsNullOrEmpty(AccountIds))
            {
                string[] _AccountIds = AccountIds.Trim('_').Split('_');
                foreach (string accountid in _AccountIds)
                {
                    Result += departParentValue + departValue + "," + postTypeValue + postValue + ",_" + accountid + "_," + isFrom + isEdit + "|";
                }
            }
            else
                Result = departParentValue + departValue + "," + postTypeValue + postValue + ",," + isFrom + isEdit + "|";
            return Result;
        }

        /// <summary>
        /// 转换Model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static List<T> FillModel<T>(List<object> obj, T t) where T : new()
        {
            if (obj.Count < 0)
                return null;
            List<T> tList = new List<T>();
            foreach (object objitem in obj)
            {
                T value = (T)objitem;
                tList.Add(value);
            }
            return tList;
        }
        /// <summary>
        /// 根据字符串查找类
        /// </summary>
        /// <param name="className"></param>
        /// <returns></returns>
        public static object StrToClass(string className)
        {
            Assembly tempAssembly = Assembly.Load("Carrot.Model");
            Type typeofControl = tempAssembly.GetType("Carrot.Model." + className);
            return Activator.CreateInstance(typeofControl);
        }

    }
}
