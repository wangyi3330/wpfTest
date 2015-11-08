using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;
using ThoughtWorks.QRCode.Codec.Util;
using System.Drawing.Imaging;
using System.IO;
using System.Data;
using System.Text.RegularExpressions;

namespace Carrot.BLL
{
    public class PromptBLL
    {
        static int i = 0;
        /// <summary>
        /// 注册脚本
        /// </summary>
        /// <param name="page"></param>
        public static void RegisterScript(Page page, string script)
        {
            i++;
            page.ClientScript.RegisterStartupScript(page.GetType(), "js" + i, script, true);
        }
        /// <summary>
        /// 显示提示信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="content"></param>
        public static void ShowMsg(Page page, string content)
        {
            i++;
            page.ClientScript.RegisterStartupScript(page.GetType(), "ShowMsg" + i, "ShowMsg('" + content + "');", true);
        }
        /// <summary>
        /// 显示提示信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="content"></param>
        public static void ShowMsg(Page page, string content, int type)
        {
            i++;
            page.ClientScript.RegisterStartupScript(page.GetType(), "ShowMsg" + i, "ShowMsg2('" + content + "',1);", true);
        }
        /// <summary>
        /// 注册脚本(AJAX)
        /// </summary>
        /// <param name="page"></param>
        public static void AjaxRegisterScript(UpdatePanel UP, string script)
        {
            i++;
            ScriptManager.RegisterStartupScript(UP, UP.GetType(), "js" + i, script, true);
        }
        /// <summary>
        /// 显示提示信息(AJAX)
        /// </summary>
        /// <param name="page"></param>
        /// <param name="content"></param>
        public static void AjaxShowMsg(UpdatePanel UP, string content)
        {
            i++;
            ScriptManager.RegisterStartupScript(UP, UP.GetType(), "ShowMsg" + i, "ShowMsg('" + content + "');", true);
        }
        /// <summary>
        /// 显示提示信息(AJAX)
        /// </summary>
        /// <param name="page"></param>
        /// <param name="content"></param>
        public static void AjaxShowMsg(UpdatePanel UP, string content, int type)
        {
            i++;
            ScriptManager.RegisterStartupScript(UP, UP.GetType(), "ShowMsg" + i, "ShowMsg2('" + content + "',1);", true);
        }
        /// <summary>
        /// 区域警告
        /// </summary>
        /// <param name="page"></param>
        /// <param name="id"></param>
        public static void Warning(Page page, string id)
        {
            Warning(page, id, null);
        }
        /// <summary>
        /// 区域警告(带信息提示)
        /// </summary>
        /// <param name="page"></param>
        /// <param name="content"></param>
        public static void Warning(Page page, string id, string msg)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "Warning" + id, "Warning('" + id + "','" + msg + "');", true);
        }


        /// <summary>
        /// 区域警告(AJAX)
        /// </summary>
        /// <param name="page"></param>
        /// <param name="id"></param>
        public static void AjaxWarning(UpdatePanel UP, string id)
        {
            AjaxWarning(UP, id, null);
        }
        /// <summary>
        /// 区域警告(AJAX|带信息提示)
        /// </summary>
        /// <param name="page"></param>
        /// <param name="content"></param>
        public static void AjaxWarning(UpdatePanel UP, string id, string msg)
        {
            ScriptManager.RegisterStartupScript(UP, UP.GetType(), "Warning" + id, "Warning('" + id + "','" + msg + "');", true);
        }
        /// <summary>
        /// 格式化为货币
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string FormatNumber(string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;
            double result = 0;
            if (!double.TryParse(s, out result))
                return s;
            string r = System.Text.RegularExpressions.Regex.Replace(s.ToString(), @"\d+?(?=(?:\d{3})+\.)", "$0,");
            r = System.Text.RegularExpressions.Regex.Replace(r, @"(?<=\.(?:\d{3})+)\d+?", ",$0");
            r = System.Text.RegularExpressions.Regex.Replace(r + " ", @"(?<=\D*)\d+?(?=(?:\d{3})+\D+)", "$0,").Trim();
            if (r.Substring(0) == "(")
                r = "-" + r.TrimStart('(').TrimEnd(')');
            return r;
        }
        /// <summary>
        /// 返回指定格式
        /// </summary>
        /// <param name="Date1"></param>
        /// <param name="Date2"></param>
        /// <param name="Interval"></param>
        /// <returns></returns>
        public static int DateDiff(DateTime Date1, DateTime Date2, string Interval)
        {
            double dblYearLen = 365;//年的长度，365天   
            double dblMonthLen = (365 / 12);//每个月平均的天数   
            System.TimeSpan objT;
            objT = Date2.Subtract(Date1);
            switch (Interval)
            {
                case "y"://返回日期的年份间隔   
                    return System.Convert.ToInt32(objT.Days / dblYearLen);
                case "M"://返回日期的月份间隔   
                    return System.Convert.ToInt32(objT.Days / dblMonthLen);
                case "d"://返回日期的天数间隔   
                    return objT.Days;
                case "h"://返回日期的小时间隔   
                    return objT.Hours;
                case "m"://返回日期的分钟间隔   
                    return objT.Minutes;
                case "s"://返回日期的秒钟间隔   
                    return objT.Seconds;
                case "ms"://返回时间的微秒间隔   
                    return objT.Milliseconds;
                default:
                    break;
            }
            return 0;
        }
        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="txtEncodeData">二维码内容</param>
        /// <param name="SavePath">系统根路径</param>
        /// <param name="TagID">标签编号</param>
        /// <returns></returns>
        public static string CreatQRCodeImage(string txtEncodeData, string SavePath, string TagID)
        {
            string errorimg = "/admin/Images/m_nopic.gif";
            if (!System.IO.File.Exists(SavePath + "/AssetsCode/" + TagID + ".png"))
            {
                if (txtEncodeData.Trim() == String.Empty)
                {
                    return errorimg;
                }

                QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
                String encoding = "Byte";
                if (encoding == "Byte")
                {
                    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                }
                else if (encoding == "AlphaNumeric")
                {
                    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC;
                }
                else if (encoding == "Numeric")
                {
                    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.NUMERIC;
                }
                try
                {
                    int scale = Convert.ToInt16(4);
                    qrCodeEncoder.QRCodeScale = scale;
                }
                catch (Exception ex)
                {
                    return errorimg;
                }
                try
                {
                    int version = Convert.ToInt16(7);
                    qrCodeEncoder.QRCodeVersion = version;
                }
                catch (Exception ex)
                {
                    return errorimg;
                }

                string errorCorrect = "M";
                if (errorCorrect == "L")
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
                else if (errorCorrect == "M")
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                else if (errorCorrect == "Q")
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;
                else if (errorCorrect == "H")
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
                try
                {
                    String ls_fileName = TagID + ".png";
                    String ls_savePath = SavePath + "/AssetsCode/" + ls_fileName;

                    qrCodeEncoder.Encode(txtEncodeData, System.Text.Encoding.UTF8).Save(ls_savePath);
                    return "/AssetsCode/" + ls_fileName;
                }
                catch (Exception ex)
                {
                    return errorimg;
                }
            }
            else
                return SavePath + "/AssetsCode/" + TagID + ".png";
        }

        public static System.Drawing.Bitmap CreatQRCodeImage(string txtEncodeData)
        {


            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            String encoding = "Byte";
            if (encoding == "Byte")
            {
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            }
            else if (encoding == "AlphaNumeric")
            {
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC;
            }
            else if (encoding == "Numeric")
            {
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.NUMERIC;
            }
            try
            {
                int scale = Convert.ToInt16(4);
                qrCodeEncoder.QRCodeScale = scale;
            }
            catch
            {

            }
            try
            {
                int version = Convert.ToInt16(7);
                qrCodeEncoder.QRCodeVersion = version;
            }
            catch
            {

            }

            string errorCorrect = "M";
            if (errorCorrect == "L")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
            else if (errorCorrect == "M")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            else if (errorCorrect == "Q")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;
            else if (errorCorrect == "H")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
            try
            {
                return qrCodeEncoder.Encode(txtEncodeData, System.Text.Encoding.UTF8);
            }
            catch
            {

            }
            return null;
        }
        /// <summary>
        /// 生成线性图标
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="X">X轴</param>
        /// <param name="Y">Y轴数组</param>
        /// <param name="SavePath">临时文件保存地址</param>
        /// <param name="type">显示类型（1：线性|2：线性+柱形）</param>
        public static void GetLine(Dictionary<string, string> dt, string SavePath)
        {
            StringBuilder xml = new StringBuilder();
            StringBuilder xmlmouth = new StringBuilder();
            StringBuilder xmldata = new StringBuilder();
            StringBuilder xmldata2 = new StringBuilder();
            xml.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            xml.Append("<chart>");
            xml.Append("<series>");
            xml.Append("{0}");
            xml.Append("</series>");
            xml.Append("<graphs>");
            xml.Append("{1}");
            xml.Append("</graphs>");
            xml.Append("</chart>");

            List<string> str = new List<string>();
            int i = 0;
            xmldata.Append("<graph gid='2' title='人数'>");
            foreach (KeyValuePair<string, string> a in dt)
            {
                i++;
                xmlmouth.Append("<value xid=\"" + i + "\">" + a.Key + "</value>");
                xmldata.Append("<value xid=\"" + i + "\" >" + a.Value + "</value>");
            }
            xmldata.Append("</graph>");


            File.WriteAllText(SavePath, string.Format(xml.ToString(), xmlmouth.ToString(), xmldata.ToString()));
        }
        /// <summary>
        /// 生成线性图标
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="X">X轴</param>
        /// <param name="Y">Y轴数组</param>
        /// <param name="SavePath">临时文件保存地址</param>
        /// <param name="type">显示类型（1：线性|2：线性+柱形）</param>
        /// <param name="isupdate">是否更新</param>
        public static void GetLine(DataTable dt, string X, Dictionary<string, string> Y, string SavePath, int type)
        {
            StringBuilder xml = new StringBuilder();
            StringBuilder xmlmouth = new StringBuilder();
            StringBuilder xmldata = new StringBuilder();
            StringBuilder xmldata2 = new StringBuilder();
            xml.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            xml.Append("<chart>");
            xml.Append("<series>");
            xml.Append("{0}");
            xml.Append("</series>");
            xml.Append("<graphs>");
            xml.Append("{1}");
            xml.Append("</graphs>");
            xml.Append("</chart>");

            List<TempList> dic = new List<TempList>();
            int ii = 0;
            List<string> str = new List<string>();
            foreach (DataRow item in dt.Rows)
            {
                ii++;
                string _x = "";
                if (X == "")
                    _x = item[0].ToString();
                else
                    _x = item[X].ToString();

                xmlmouth.Append("<value xid=\"" + ii + "\">" + _x + "</value>");

                foreach (KeyValuePair<string, string> a in Y)
                {
                    string _y = "";
                    _y = item[a.Value].ToString();
                    TempList tl = new TempList();
                    tl.key = a.Key;
                    tl.value = "<value xid=\"" + ii + "\" >" + _y + "</value>";
                    dic.Add(tl);

                }
            }
            int i = 1;
            if (type == 1)
                i = 2;
            foreach (KeyValuePair<string, string> a in Y)
            {

                xmldata.Append("<graph gid=\"" + i + "\" title=\"" + a.Key + "\">");
                foreach (TempList entry in dic)
                {
                    if (entry.key == a.Key)
                        xmldata.Append(entry.value);
                }
                xmldata.Append("</graph>");
                i++;

            }
            File.WriteAllText(SavePath, string.Format(xml.ToString(), xmlmouth.ToString(), xmldata.ToString()));
        }
        class TempList
        {
            public string key { get; set; }
            public string value { get; set; }
        }

        /// <summary>
        /// 饼图
        /// </summary>
        public static void GetAmpie(DataTable dt, string X, string Y, string SavePath)
        {
            StringBuilder xmlAmpie = new StringBuilder();
            xmlAmpie.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            xmlAmpie.Append("<pie>");
            int maxnumber = 0;
            string pull_out = "pull_out=\"true\"";
            foreach (DataRow item in dt.Rows)
            {
                string _x = "";
                if (X == "")
                    _x = item[0].ToString();
                else
                    _x = item[X].ToString();
                string _y = "";
                if (Y == "")
                    _y = item[1].ToString();
                else
                    _y = item[Y].ToString();

                if (maxnumber != 0)
                    pull_out = "";
                xmlAmpie.Append("<slice title=\"" + _x + "\" " + pull_out + ">" + _y + "</slice>");
                maxnumber = 1;
            }
            xmlAmpie.Append("</pie>");
            File.WriteAllText(SavePath, xmlAmpie.ToString());
        }
        /// <summary>
        /// 将数据库中的附件格式转换成显示格式
        /// </summary>
        /// <param name="Attachment"></param>
        /// <returns></returns>
        public static string ConvertAttachment(string Attachment, string Path)
        {
            string result = "";
            if (!string.IsNullOrEmpty(Attachment))
            {
                string[] _Attachment = Attachment.Trim('|').Split('|');
                foreach (string item in _Attachment)
                {
                    string[] name = item.Split(',');
                    result += "<a href='" + Path + name[1] + "' target='_blank'>" + name[0] + " 点击下载</a>&nbsp;&nbsp;";
                }
            }
            else result = "无";
            return result;
        }
        /// <summary>
        /// 获取HTML中的图片路径
        /// </summary>
        /// <param name="sHtmlText"></param>
        /// <returns></returns>
        public static string GetHtmlImageUrlList(string sHtmlText, int backCount)
        {
            // 定义正则表达式用来匹配 img 标签
            Regex regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);

            // 搜索匹配的字符串
            MatchCollection matches = regImg.Matches(sHtmlText);

            int i = 0;
            string sUrlList = "";

            // 取得匹配项列表
            foreach (Match match in matches)
            {
                i++;
                sUrlList += match.Groups["imgUrl"].Value + "|";
                if (backCount == i)
                    return sUrlList.TrimEnd('|');
            }

            return sUrlList.TrimEnd('|');
        }
        /// <summary>
        /// 去掉HTML中图片
        /// </summary>
        /// <param name="strhtml"></param>
        /// <returns></returns>
        public static string RemoveHtmlImg(string strhtml)
        {
            string stroutput = strhtml;
            Regex regex = new Regex(@"<[^>]+>|</[^>]+>");
            stroutput = regex.Replace(stroutput, "");
            return stroutput;
        }
        /// <summary>
        /// 去除HTML标记
        /// </summary>
        /// <param name="Htmlstring">包括HTML的源码 </param>
        /// <returns>已经去除后的文字</returns>
        public static string NoHTML(string Htmlstring)
        {
            //删除脚本
            Htmlstring = Htmlstring.Replace("\r\n", "");
            Htmlstring = Regex.Replace(Htmlstring, @"<script.*?</script>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<style.*?</style>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<.*?>", "", RegexOptions.IgnoreCase);
            //删除HTML
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            Htmlstring = Htmlstring.Replace("<", "");
            Htmlstring = Htmlstring.Replace(">", "");
            Htmlstring = Htmlstring.Replace("\r\n", "");
            return Htmlstring;
        }
    }
}
