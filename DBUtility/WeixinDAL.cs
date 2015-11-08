using Carrot.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace Carrot.DBUtility
{
    public class WeixinDAL
    {

        public static void Auth(string Token, string signature)
        {
            string echoStr = System.Web.HttpContext.Current.Request.QueryString["echoStr"];
            if (CheckSignature(Token, signature))
            {
                if (!string.IsNullOrEmpty(echoStr))
                {
                    System.Web.HttpContext.Current.Response.Write(echoStr);
                    System.Web.HttpContext.Current.Response.End();
                }
            }
        }
        /// <summary>
        /// 验证微信签名
        /// </summary>
        /// * 将token、timestamp、nonce三个参数进行字典序排序
        /// * 将三个参数字符串拼接成一个字符串进行sha1加密
        /// * 开发者获得加密后的字符串可与signature对比，标识该请求来源于微信。
        /// <returns></returns>
        private static bool CheckSignature(string Token, string signature)
        {

            string timestamp = System.Web.HttpContext.Current.Request.QueryString["timestamp"];

            string nonce = System.Web.HttpContext.Current.Request.QueryString["nonce"];

            string[] ArrTmp = { Token, timestamp, nonce };

            Array.Sort(ArrTmp);     //字典排序

            string tmpStr = string.Join("", ArrTmp);

            tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");

            tmpStr = tmpStr.ToLower();

            if (tmpStr == signature)
                return true;
            else
                return false;
        }

        public delegate void WxDelegate(WeixinInfo wx, string WeixinAccess, string corpId);
        public delegate void WxDelegate2(WeixinInfo wx, string remsg, string WeixinAccess, string corpId);

        public static void Handle(string postStr, WxDelegate2 ResponseMsg, WxDelegate EventHandle, string WeixinAccess, string corpId)
        {

            //封装请求类

            XmlDocument doc = new XmlDocument();

            doc.LoadXml(postStr);

            XmlElement rootElement = doc.DocumentElement;


            XmlNode MsgType = rootElement.SelectSingleNode("MsgType");


            WeixinInfo wx = new WeixinInfo();

            wx.WXToUserName = rootElement.SelectSingleNode("ToUserName").InnerText;

            wx.WXFromUserName = rootElement.SelectSingleNode("FromUserName").InnerText;

            wx.WXCreatime = rootElement.SelectSingleNode("CreateTime").InnerText;

            wx.WXMsgType = MsgType.InnerText;
            try
            {
                wx.WXEvent = rootElement.SelectSingleNode("Event").InnerText;

                wx.WXEventKey = rootElement.SelectSingleNode("EventKey").InnerText;
            }
            catch
            {
            }


            if (wx.WXMsgType == "text")
            {
                wx.WXMsg = rootElement.SelectSingleNode("Content").InnerText;
                ResponseMsg(wx, "", WeixinAccess, corpId);
            }
            else if (wx.WXMsgType == "event")
            {
                if (wx.WXEvent != null)
                    if (wx.WXEvent.ToLower() == "click")
                    {

                    }
                    else if (wx.WXEvent.ToLower() == "location_select")
                    {
                        wx.WXLocationX = rootElement.SelectSingleNode("Location_X").InnerText;

                        wx.WXLocationY = rootElement.SelectSingleNode("Location_Y").InnerText;

                        wx.WXScale = rootElement.SelectSingleNode("Scale").InnerText;

                        wx.WXLabel = rootElement.SelectSingleNode("Label").InnerText;
                    }
                    else if (wx.WXEvent.ToLower() == "scancode_waitmsg")
                    {
                        wx.WXScanResult = rootElement.SelectSingleNode("ScanCodeInfo").SelectSingleNode("ScanResult").InnerText;
                    }
                    else if (wx.WXEvent.ToLower() == "view")
                    {

                    }
                    else if (wx.WXEvent.ToLower() == "scancode_push")
                    {

                    }
                    else if (wx.WXEvent.ToLower() == "pic_sysphoto")
                    {

                    }
                    else if (wx.WXEvent.ToLower() == "pic_photo_or_album")
                    {

                    }
                    else if (wx.WXEvent.ToLower() == "pic_weixin")
                    {

                    }
                    else if (wx.WXEvent.ToLower() == "media_id")
                    {

                    }
                    else if (wx.WXEvent.ToLower() == "view_limited")
                    {

                    }

                EventHandle(wx, WeixinAccess, corpId);
            }

            else if (wx.WXMsgType == "image")
            {

                wx.WXPicUrl = rootElement.SelectSingleNode("PicUrl").InnerText;

            }
            else if (wx.WXMsgType == "voice")
            {
                //wx.WXMediaId = rootElement.SelectSingleNode("MediaId").InnerText;
                wx.WXMsgID = rootElement.SelectSingleNode("MsgID").InnerText;
                wx.WXFormat = rootElement.SelectSingleNode("Format").InnerText;
                ResponseMsg(wx, "", WeixinAccess, corpId);
            }
        }


        public static string Getaccess_token(string WeixinAccess)
        {
            return Getaccess_token(WeixinAccess, false);
        }
        /// <summary>
        /// 获取access_token （有效时间7200秒)
        /// </summary>
        /// <param name="WeixinAccess">账号</param>
        /// <param name="access_token">获取的access_token</param>
        /// <param name="IsCompulsory">是否强制获取</param>
        public static string Getaccess_token(string WeixinAccess, bool IsCompulsory)
        {
            string access_token = "";
            string js_ticket = "";
            try
            {
                List<WhereParameter> wp = new List<WhereParameter>();
                wp.Add(new WhereParameter("WeixinAccess", WhereParameter.Query.等于, WeixinAccess));
                WeixinAccessInfo waInfo = PublicDAL.GetList(new WeixinAccessInfo(), wp, "", true, "")[0];
                if (waInfo.AccessToken == null || DateTime.Now.Subtract((DateTime)waInfo.CreatTime).TotalSeconds >= 7200 || IsCompulsory)
                {
                    string content = "";
                    string jscontent = "";
                    if (WeixinAccess == "HXBGtoken" || WeixinAccess == "HXBG2token")
                        content = GetPage("https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid=" + waInfo.Appid + "&corpsecret=" + waInfo.Secret);
                    else
                        content = GetPage("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=" + waInfo.Appid + "&secret=" + waInfo.Secret);
                    JArray ja = (JArray)JsonConvert.DeserializeObject("[" + content + "]");
                    string ja1a = ja[0]["access_token"].ToString();
                    access_token = ja1a;

                    jscontent = GetPage("https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token=" + access_token + "&type=jsapi");

                    try
                    {
                        JArray ja2 = (JArray)JsonConvert.DeserializeObject("[" + jscontent + "]");
                        string ja2_1 = ja2[0]["ticket"].ToString();
                        js_ticket = ja2_1;
                    }
                    catch
                    {
                    }
                    WeixinAccessInfo _wainfo = new WeixinAccessInfo();
                    _wainfo.AccessToken = access_token;
                    _wainfo.Jsapiticket = js_ticket;
                    _wainfo.CreatTime = DateTime.Now;
                    PublicDAL.Update(new ModelWhereParams(_wainfo, wp),"");
                }
                else
                    access_token = waInfo.AccessToken;
            }
            catch
            {
            }
            return access_token;
        }


        /// <summary>
        /// 发送Get（获取access_token用）
        /// </summary>
        /// <param name="url"></param>
        /// <param name="access_token"></param>
        /// <returns></returns>
        public static string GetPage(string url)
        {
            ServicePointManager.ServerCertificateValidationCallback += RemoteCertificateValidate;//验证服务器证书回调自动验证
            Stream instream = null;
            StreamReader sr = null;
            HttpWebResponse response = null;
            HttpWebRequest request = null;
            Encoding encoding = Encoding.UTF8;
            // 准备请求...
            try
            {
                // 设置参数
                request = WebRequest.Create(url) as HttpWebRequest;
                CookieContainer cookieContainer = new CookieContainer();
                request.CookieContainer = cookieContainer;
                request.AllowAutoRedirect = true;
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //发送请求并获取相应回应数据
                response = request.GetResponse() as HttpWebResponse;
                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                instream = response.GetResponseStream();
                sr = new StreamReader(instream, encoding);
                //返回结果网页（html）代码
                string content = sr.ReadToEnd();
                string err = string.Empty;
                return content;
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                return string.Empty;
            }
        }

        /// <summary>
        /// 发送Get（普通用）
        /// </summary>
        /// <param name="url"></param>
        /// <param name="access_token"></param>
        /// <returns></returns>
        public static string GetPage2(string url, string WeixinAccess)
        {
            string access_token = Getaccess_token(WeixinAccess);
            Stream instream = null;
            StreamReader sr = null;
            HttpWebResponse response = null;
            HttpWebRequest request = null;
            Encoding encoding = Encoding.UTF8;
            // 准备请求...
            try
            {
                // 设置参数
                request = WebRequest.Create(url + access_token) as HttpWebRequest;
                CookieContainer cookieContainer = new CookieContainer();
                request.CookieContainer = cookieContainer;
                request.AllowAutoRedirect = true;
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //发送请求并获取相应回应数据
                response = request.GetResponse() as HttpWebResponse;
                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                instream = response.GetResponseStream();
                sr = new StreamReader(instream, encoding);
                //返回结果网页（html）代码
                string content = sr.ReadToEnd();
                string err = string.Empty;
                JArray ja = (JArray)JsonConvert.DeserializeObject("[" + content + "]");
                try
                {
                    string ja1a = ja[0]["errcode"].ToString();
                    if (ja1a == "42001")
                    {
                        Getaccess_token(WeixinAccess, true);
                        //PostPage(url, json, Token, ref access_token);
                    }
                }
                catch
                {
                }

                return content;
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                return string.Empty;
            }
        }

        /// <summary>
        /// 发送Post
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public static string PostPage(string url, string json, string WeixinAccess)
        {
            string access_token = Getaccess_token(WeixinAccess);
            Stream outstream = null;
            Stream instream = null;
            StreamReader sr = null;
            HttpWebResponse response = null;
            HttpWebRequest request = null;
            Encoding encoding = Encoding.UTF8;
            byte[] data = encoding.GetBytes(json);
            // 准备请求...
            try
            {
                // 设置参数https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=APPID&secret=APPSECRET
                request = WebRequest.Create(url + access_token) as HttpWebRequest;
                CookieContainer cookieContainer = new CookieContainer();
                request.CookieContainer = cookieContainer;
                request.AllowAutoRedirect = true;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                outstream = request.GetRequestStream();
                outstream.Write(data, 0, data.Length);
                outstream.Close();
                //发送请求并获取相应回应数据
                response = request.GetResponse() as HttpWebResponse;
                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                instream = response.GetResponseStream();
                sr = new StreamReader(instream, encoding);
                //返回结果网页（html）代码
                string content = sr.ReadToEnd();
                string err = string.Empty;
                try
                {
                    JArray ja = (JArray)JsonConvert.DeserializeObject("[" + content + "]");
                    string ja1a = ja[0]["errcode"].ToString();
                    if (ja1a == "42001")
                    {
                        Getaccess_token(WeixinAccess, true);
                        //PostPage(url, json, Token, ref access_token);
                    }
                }
                catch { }
                return content;
            }
            catch
            {
                return "";
            }

        }
        /// <summary>
        /// 发送Post
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public static string PostPage2(string url, string data)
        {
            Stream outstream = null;
            Stream instream = null;
            StreamReader sr = null;
            HttpWebResponse response = null;
            HttpWebRequest request = null;
            Encoding encoding = Encoding.UTF8;
            byte[] postBytes = encoding.GetBytes(data);
            // 准备请求...
            try
            {
                // 设置参数https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=APPID&secret=APPSECRET
                request = WebRequest.Create(url) as HttpWebRequest;
                CookieContainer cookieContainer = new CookieContainer();
                request.CookieContainer = cookieContainer;
                request.AllowAutoRedirect = true;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = postBytes.Length;
                outstream = request.GetRequestStream();
                outstream.Write(postBytes, 0, postBytes.Length);
                outstream.Close();
                //发送请求并获取相应回应数据
                response = request.GetResponse() as HttpWebResponse;
                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                instream = response.GetResponseStream();
                sr = new StreamReader(instream, encoding);
                //返回结果网页（html）代码
                string content = sr.ReadToEnd();
                string err = string.Empty;
                try
                {
                    JArray ja = (JArray)JsonConvert.DeserializeObject("[" + content + "]");
                    content = ja[0]["data"].ToString();

                }
                catch { }
                return content;
            }
            catch
            {
                return "";
            }

        }

        private static bool RemoteCertificateValidate(object sender, X509Certificate cert,

X509Chain chain, SslPolicyErrors error)
        {
            // trust any certificate!!!
            System.Console.WriteLine("Warning, trust any certificate");
            //为了通过证书验证，总是返回true
            return true;
        }
        #region 其他


        //回复类型
        public class ReplyType
        {
            /// <summary>
            /// 企业普通文本消息
            /// </summary>
            public static string QyMessage_Text
            {
                get
                {
                    return @"";
                }
            }
            /// <summary>
            /// 普通文本消息
            /// </summary>
            public static string Message_Text
            {
                get
                {
                    return @"<xml>
                            <ToUserName><![CDATA[{0}]]></ToUserName>
                            <FromUserName><![CDATA[{1}]]></FromUserName>
                            <CreateTime>{2}</CreateTime>
                            <MsgType><![CDATA[text]]></MsgType>
                            <Content><![CDATA[{3}]]></Content>
                            </xml>";
                }
            }
            /// <summary>
            /// 图文消息主体
            /// </summary>
            public static string Message_News_Main
            {
                get
                {
                    return @"<xml>
                            <ToUserName><![CDATA[{0}]]></ToUserName>
                            <FromUserName><![CDATA[{1}]]></FromUserName>
                            <CreateTime>{2}</CreateTime>
                            <MsgType><![CDATA[news]]></MsgType>
                            <ArticleCount>{3}</ArticleCount>
                            <Articles>
                            {4}
                            </Articles>
                            </xml> ";
                }
            }
            /// <summary>
            /// 图文消息项
            /// </summary>
            public static string Message_News_Item
            {
                get
                {
                    return @"<item>
                            <Title><![CDATA[{0}]]></Title> 
                            <Description><![CDATA[{1}]]></Description>
                            <PicUrl><![CDATA[{2}]]></PicUrl>
                            <Url><![CDATA[{3}]]></Url>
                            </item>";
                }
            }
        }

        /// <summary>
        /// unix时间转换为datetime
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        private DateTime UnixTimeToTime(string timeStamp)
        {

            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));

            long lTime = long.Parse(timeStamp + "0000000");

            TimeSpan toNow = new TimeSpan(lTime);

            return dtStart.Add(toNow);

        }

        /// <summary>
        /// datetime转换为unixtime
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private int ConvertDateTimeInt(System.DateTime time)
        {

            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));

            return (int)(time - startTime).TotalSeconds;

        }

        /// <summary>
        /// 调用百度地图，返回坐标信息
        /// </summary>
        /// <param name="y">经度</param>
        /// <param name="x">纬度</param>
        /// <returns></returns>
        public string GetMapInfo(string x, string y)
        {
            try
            {
                string res = string.Empty;

                string parame = string.Empty;

                string url = "http://maps.googleapis.com/maps/api/geocode/xml";

                parame = "latlng=" + x + "," + y + "&language=zh-CN&sensor=false";//此key为个人申请

                res = webRequestPost(url, parame);


                XmlDocument doc = new XmlDocument();


                doc.LoadXml(res);

                XmlElement rootElement = doc.DocumentElement;

                string Status = rootElement.SelectSingleNode("status").InnerText;

                if (Status == "OK")
                {

                    //仅获取城市

                    XmlNodeList xmlResults = rootElement.SelectSingleNode("/GeocodeResponse").ChildNodes;

                    for (int i = 0; i < xmlResults.Count; i++)
                    {

                        XmlNode childNode = xmlResults[i];

                        if (childNode.Name == "status")
                        {

                            continue;

                        }


                        string city = "0";

                        for (int w = 0; w < childNode.ChildNodes.Count; w++)
                        {

                            for (int q = 0; q < childNode.ChildNodes[w].ChildNodes.Count; q++)
                            {

                                XmlNode childeTwo = childNode.ChildNodes[w].ChildNodes[q];


                                if (childeTwo.Name == "long_name")
                                {

                                    city = childeTwo.InnerText;

                                }

                                else if (childeTwo.InnerText == "locality")
                                {

                                    return city;

                                }

                            }

                        }

                        return city;

                    }

                }

            }

            catch (Exception ex)
            {

                //WriteTxt("map异常:" + ex.Message.ToString() + "Struck:" + ex.StackTrace.ToString());

                return "0";

            }


            return "0";

        }

        /// <summary>
        /// Post 提交调用抓取
        /// </summary>
        /// <param name="url">提交地址</param>
        /// <param name="param">参数</param>
        /// <returns>string</returns>
        public string webRequestPost(string url, string param)
        {

            byte[] bs = System.Text.Encoding.UTF8.GetBytes(param);


            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url + "?" + param);

            req.Method = "Post";

            req.Timeout = 120 * 1000;

            req.ContentType = "application/x-www-form-urlencoded;";

            req.ContentLength = bs.Length;


            using (Stream reqStream = req.GetRequestStream())
            {

                reqStream.Write(bs, 0, bs.Length);

                reqStream.Flush();

            }

            using (WebResponse wr = req.GetResponse())
            {

                //在这里对接收到的页面内容进行处理 


                Stream strm = wr.GetResponseStream();


                StreamReader sr = new StreamReader(strm, System.Text.Encoding.UTF8);


                string line;


                System.Text.StringBuilder sb = new System.Text.StringBuilder();


                while ((line = sr.ReadLine()) != null)
                {

                    sb.Append(line + System.Environment.NewLine);

                }

                sr.Close();

                strm.Close();

                return sb.ToString();

            }
        #endregion
        }
    }
}
