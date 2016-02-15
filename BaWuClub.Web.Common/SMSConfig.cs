using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

namespace BaWuClub.Web.Common
{
    [Serializable]
    public class SMSConfig
    {
        /// <summary>
        /// 根据短信接口提供商提供的要求。
        /// 初始化SMS的配置，包括获取"公司ID(gwid)"、"用户登录名(ua)"、"登陆密码(pwd)"、以及包括同一个号码在一分钟、一小时、一天内允许发送的短信数。
        /// </summary>

        private string gwid, ua, pwd,text;
        private int minuteLimit = 0, hourLimit=0,dayLimit=0;

        private NameValueCollection nameValueCollection = (NameValueCollection)System.Web.Configuration.WebConfigurationManager.GetSection("SMSSection");
        
        public SMSConfig() {
            this.gwid = nameValueCollection["gwid"];
            this.ua = nameValueCollection["ua"];
            this.pwd = nameValueCollection["pwd"];
            this.text = nameValueCollection["text"];
            Int32.TryParse(nameValueCollection["minutelimit"], out this.minuteLimit);
            Int32.TryParse(nameValueCollection["hourlimit"], out this.hourLimit);
            Int32.TryParse(nameValueCollection["daylimit"], out this.dayLimit);
        }

        public string Gwid { get { return gwid; } set { gwid = value; } }
        public string UA { get { return ua; } set { ua = value; } }
        public string Pwd { get { return pwd; } set { pwd = value; } }
        public string Text { get { return text; } set { text = value; } }

        public int MinuteLimit { get { return minuteLimit; } set { minuteLimit = value; } }

        public int HourLimit { get { return hourLimit; } set { hourLimit = value; } }

        public int DayLimit { get { return dayLimit; } set { dayLimit = value; } }
    }
}
