using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace BaWuClub.Web.Common
{
    class SendSMS
    {
        public void post(string phone,string text)
        {
            //短信内容
            string content = System.Web.HttpUtility.UrlEncode(text, Encoding.GetEncoding("GB2312"));

            string postData = "type=send&ua=账号&pwd=密码&gwid=企业ID&mobile="+phone+"&msg="+text;
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
            if (result.Length > 0){
                //判断成功与否
            }
            sr.Close();
        }
    }
}
