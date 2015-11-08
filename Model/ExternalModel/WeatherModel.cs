using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Carrot.Model
{
    [Serializable]
    public class WeatherInfo
    {
        private string province;//省份
        private string city;//城市
        private int code;//代号
        private string citypic;//城市图片名称
        private DateTime lastTime;//最后更新时间
        private string temperature;// 气温
        private string weatherState;//概况
        private string wind;//风向和风力
        private string stratIco;//天气趋势开始图片名称(以下称：图标一)
        private string endIco;//天气气趋势结束图片名称(以下称：图标二)
        private string today;//现在的天气实况
        private string shenghuozhishu;//天气和生活指数
        private string tomorrow;//明天天气
        private string tomorrowWeatherState;//明天概况
        private string tomorrowWind;//明天风向和风力
        private string tomorrowStratIco;//明天天气趋势开始图片名称(以下称：图标一)
        private string tomorrowEndIco;//明天天气气趋势结束图片名称(以下称：图标二)
        private string after;//后天天气
        private string afterWeatherState;//后天概况
        private string afterWind;//后天风向和风力
        private string afterStratIco;//后天天气趋势开始图片名称(以下称：图标一)
        private string afterEndIco;//后天天气气趋势结束图片名称(以下称：图标二)
        private string cityContent;//城市或地区的介绍

        /// <summary>
        /// 省份
        /// </summary>
        public string Province
        {
            get { return province; }
            set { province = value; }
        }
        /// <summary>
        /// 城市
        /// </summary>
        public string City
        {
            get { return city; }
            set { city = value; }
        }

        /// <summary>
        /// 代号
        /// </summary>
        public int Code
        {
            get { return code; }
            set { code = value; }
        }
        /// <summary>
        /// 城市图片名称
        /// </summary>
        public string Citypic
        {
            get { return citypic; }
            set { citypic = value; }
        }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime LastTime
        {
            get { return lastTime; }
            set { lastTime = value; }
        }

        /// <summary>
        /// 气温
        /// </summary>
        public string Temperature
        {
            get { return temperature; }
            set { temperature = value; }
        }

        /// <summary>
        /// 概况
        /// </summary>
        public string WeatherState
        {
            get { return weatherState; }
            set { weatherState = value; }
        }

        /// <summary>
        /// 风向和风力
        /// </summary>
        public string Wind
        {
            get { return wind; }
            set { wind = value; }
        }
        /// <summary>
        /// 天气趋势开始图片名称(以下称：图标一)
        /// </summary>
        public string StratIco
        {
            get { return stratIco; }
            set { stratIco = value; }
        }

        /// <summary>
        /// 天气气趋势结束图片名称(以下称：图标二)
        /// </summary>
        public string EndIco
        {
            get { return endIco; }
            set { endIco = value; }
        }

        /// <summary>
        /// 现在的天气实况
        /// </summary>
        public string Today
        {
            get { return today; }
            set { today = value; }
        }

        /// <summary>
        /// 天气和生活指数
        /// </summary>
        public string Shenghuozhishu
        {
            get { return shenghuozhishu; }
            set { shenghuozhishu = value; }
        }

        /// <summary>
        /// 明天天气
        /// </summary>
        public string Tomorrow
        {
            get { return tomorrow; }
            set { tomorrow = value; }
        }

        /// <summary>
        /// 明天概况
        /// </summary>
        public string TomorrowWeatherState
        {
            get { return tomorrowWeatherState; }
            set { tomorrowWeatherState = value; }
        }

        /// <summary>
        /// 明天风向和风力
        /// </summary>
        public string TomorrowWind
        {
            get { return tomorrowWind; }
            set { tomorrowWind = value; }
        }
        /// <summary>
        /// 明天天气趋势开始图片名称(以下称：图标一)
        /// </summary>
        public string TomorrowStratIco
        {
            get { return tomorrowStratIco; }
            set { tomorrowStratIco = value; }
        }

        /// <summary>
        /// 明天天气气趋势结束图片名称(以下称：图标二)
        /// </summary>
        public string TomorrowEndIco
        {
            get { return tomorrowEndIco; }
            set { tomorrowEndIco = value; }
        }

        /// <summary>
        /// 后天天气
        /// </summary>
        public string After
        {
            get { return after; }
            set { after = value; }
        }

        /// <summary>
        /// 后天概况
        /// </summary>
        public string AfterWeatherState
        {
            get { return afterWeatherState; }
            set { afterWeatherState = value; }
        }

        /// <summary>
        /// 后天风向和风力
        /// </summary>
        public string AfterWind
        {
            get { return afterWind; }
            set { afterWind = value; }
        }
        /// <summary>
        /// 后天天气趋势开始图片名称(以下称：图标一)
        /// </summary>
        public string AfterStratIco
        {
            get { return afterStratIco; }
            set { afterStratIco = value; }
        }
        /// <summary>
        /// 后天天气气趋势结束图片名称(以下称：图标二)
        /// </summary>
        public string AfterEndIco
        {
            get { return afterEndIco; }
            set { afterEndIco = value; }
        }

        /// <summary>
        /// 城市或地区的介绍
        /// </summary>
        public string CityContent
        {
            get { return cityContent; }
            set { cityContent = value; }
        }
    }
}
