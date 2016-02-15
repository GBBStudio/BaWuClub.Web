using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace BaWuClub.Web.Common
{
    public class SendSMS
    {
        private SMSConfig config = null;

        public SendSMS() {
            config = new SMSConfig();
        }

        public string post(string phone,string code){
            string codeText = string.Format(config.Text, code);
            string content = System.Web.HttpUtility.UrlEncode(codeText, Encoding.GetEncoding("GB2312"));
            string postData = "type=send&ua=" + config.UA + "&pwd=" + config.Pwd + "&gwid=" + config.Gwid + "&mobile=" + phone + "&msg=" + codeText;
            Encoding encode = Encoding.GetEncoding("gbk");
            byte[] data = encode.GetBytes(postData);
            Uri url = new Uri("http://api.106msg.com/TXTJK.aspx?");
            HttpWebRequest req = WebRequest.Create(url) as HttpWebRequest;
            req.Method = "POST";
            req.KeepAlive = true;
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = data.Length;
            req.AllowAutoRedirect = true;
            Stream outStream = req.GetRequestStream();
            outStream.Write(data, 0, data.Length);
            outStream.Close();
            outStream.Dispose();
            HttpWebResponse res = req.GetResponse() as HttpWebResponse;
            Stream inStream = res.GetResponseStream();
            StreamReader sr = new StreamReader(inStream, encode);
            string result = sr.ReadToEnd();
            sr.Close();
            return result;
        }
    }

    public enum SMSLimit
    {
        NoLimit,
        MinuteLimit,
        HourLimit,
        DayLimit
    }
}
