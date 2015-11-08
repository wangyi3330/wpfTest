using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Carrot.DBUtility;
using Carrot.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Carrot.BLL
{
    public class WeixinNew
    {
        private static string HXBFAccess_token;
        public void Handle(string postStr, string WeixinAccess, string Appid)
        {
            WeixinDAL.Getaccess_token(WeixinAccess);
            WeixinDAL.Handle(EncryptDecryptMsg(postStr, 0, Appid), ResponseMsg, EventHandle, WeixinAccess, Appid);
        }
        public void Auth(string WeixinAccess)
        {
            string signature = System.Web.HttpContext.Current.Request.QueryString["signature"];
            WeixinDAL.Auth(WeixinAccess, signature);
        }
        //事件处理
        public void EventHandle(WeixinInfo wx, string WeixinAccess, string Appid)
        {
            string remsg = "";
            string responseContent = "";
            if (wx.WXEvent != null)
            {
                //菜单单击事件
                if (wx.WXEvent.Equals("CLICK"))
                {
                    if (wx.WXEventKey.Equals("click1"))
                    {
                        responseContent = string.Format(WeixinDAL.ReplyType.Message_News_Main,
                             wx.WXFromUserName,
                             wx.WXToUserName,
                             DateTime.Now.Ticks,
                             "1",
                              string.Format(WeixinDAL.ReplyType.Message_News_Item, "测试通过", "",
                              "" + DateTime.Now.Ticks,
                              ""));
                    }
                    else if (wx.WXEventKey.Equals("click2"))
                    {

                    }

                }
                else if (wx.WXEvent.Equals("location_select"))//接收地理位置
                {
                    if (wx.WXEventKey.Equals("rselfmenu_2_0"))
                    {

                    }
                }
                else if (wx.WXEvent.Equals("scancode_waitmsg"))//二维码
                {

                }
                else if (wx.WXEvent.Equals("subscribe"))//关注事件
                {


                }
                else if (wx.WXEvent.Equals("unsubscribe"))//取消事件
                {

                }

                System.Web.HttpContext.Current.Response.Write(EncryptDecryptMsg(responseContent, 1, Appid));
                wx.WeixinID = Guid.NewGuid();
                wx.WXReContent = responseContent;
                wx.WXFromAccess = WeixinAccess;
                PublicBLL.Insert(wx, "");
            }
        }
        /// <summary>
        /// OAuth2.0认证
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="WeixinAccess"></param>
        public void OAuth2(string Code, string WeixinAccess)
        {
            string userinfo = GetPage("https://qyapi.weixin.qq.com/cgi-bin/user/getuserinfo?code=" + Code + "&agentid=0&access_token=", WeixinAccess);
            JArray ja = (JArray)JsonConvert.DeserializeObject("[" + userinfo + "]");
            string ja1a = ja[0]["UserId"].ToString();
            if (ja1a != "")
            {
                //PublicBLL.LoginFromAccountID(ja1a);
            }

        }
        private void ResponseMsg(WeixinInfo wx, string _remsg, string WeixinAccess, string Appid)
        {
            string responseContent = "";
            string remsg = "";

            responseContent = string.Format(WeixinDAL.ReplyType.Message_Text,
                           wx.WXFromUserName,
                           wx.WXToUserName,
                           DateTime.Now.Ticks,
                           _remsg + remsg);

            System.Web.HttpContext.Current.Response.Write(EncryptDecryptMsg(responseContent, 1, Appid));
            wx.WeixinID = Guid.NewGuid();
            wx.WXCreatime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            wx.WXReContent = responseContent;
            wx.WXFromAccess = WeixinAccess;
            PublicBLL.Insert(wx,"");
        }

        /// <summary>
        /// 加密/解密
        /// </summary>
        /// <param name="responseContent"></param>
        /// <param name="type">1加密0解密</param>
        /// <returns></returns>
        public string EncryptDecryptMsg(string responseContent, int type, string corpId)
        {
            string token = "HXBF_token";//从配置文件获取Token
            string encodingAESKey = "woI2SHPTYmPxgR1Utzlp85Gt5G2rFX3zEnDf0nI3UXJ";//从配置文件获取EncodingAESKey


            string sReqTimeStamp = System.Web.HttpContext.Current.Request.QueryString["timestamp"];

            string sReqNonce = System.Web.HttpContext.Current.Request.QueryString["nonce"];

            string sReqMsgSig = System.Web.HttpContext.Current.Request.QueryString["msg_signature"];

            WXBizMsgCrypt wxcpt = new WXBizMsgCrypt(token, encodingAESKey, corpId);

            string sEncryptMsg = ""; //xml格式的密文
            int ret = 0;
            if (type == 1)
                ret = wxcpt.EncryptMsg(responseContent, sReqTimeStamp, sReqNonce, ref sEncryptMsg);
            else
                ret = wxcpt.DecryptMsg(sReqMsgSig, sReqTimeStamp, sReqNonce, responseContent, ref sEncryptMsg);

            if (ret != 0)
            {
                return "";
            }
            return sEncryptMsg;
        }
        /// <summary>
        /// Get发送
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static string GetPage(string url, string WeixinAccess)
        {
            return WeixinDAL.GetPage2(url, WeixinAccess);
        }
        /// <summary>
        /// Post发送
        /// </summary>
        /// <param name="url"></param>
        /// <param name="json"></param>
        /// <param name="Token"></param>
        /// <param name="access_token"></param>
        /// <returns></returns>
        public static string PostPage(string url, string json, string WeixinAccess)
        {
            return WeixinDAL.PostPage(url, json, WeixinAccess);
        }
    }
}
