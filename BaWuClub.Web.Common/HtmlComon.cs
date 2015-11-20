using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Reflection;

namespace BaWuClub.Web.Common
{
    public static class HtmlCommon
    {
        #region 清楚HTML,JavaScript
        public static string ClearHtml(string htmlString) {
            if (!string.IsNullOrEmpty(htmlString)) {
                htmlString = Regex.Replace(htmlString, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
                htmlString = Regex.Replace(htmlString, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
                htmlString = Regex.Replace(htmlString, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
                htmlString = Regex.Replace(htmlString, @"-->", "", RegexOptions.IgnoreCase);
                htmlString = Regex.Replace(htmlString, @"<!--.*", "", RegexOptions.IgnoreCase);
                htmlString = Regex.Replace(htmlString, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
                htmlString = Regex.Replace(htmlString, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
                htmlString = Regex.Replace(htmlString, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
                htmlString = Regex.Replace(htmlString, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
                htmlString = Regex.Replace(htmlString, @"&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
                htmlString = Regex.Replace(htmlString, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
                htmlString = Regex.Replace(htmlString, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
                htmlString = Regex.Replace(htmlString, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
                htmlString = Regex.Replace(htmlString, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
                htmlString = Regex.Replace(htmlString, @"&#(\d+);", "", RegexOptions.IgnoreCase);
                htmlString.Replace("<", "");
                htmlString.Replace(">", "");
                htmlString.Replace("\r\n", "");
                htmlString = htmlString.Trim();
            }
            return htmlString;
        }
        public static string ClearHtmlTag(string htmlString) {
            return System.Text.RegularExpressions.Regex.Replace(htmlString, "<[^>]*>", "");
        }
        public static string ClearJavascript(string htmlString) {
            htmlString = Regex.Replace(htmlString, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            htmlString = htmlString.Trim();
            return htmlString;
        }
        #endregion

        #region 设置提示信息
        public static string GetHitStr(string statusStr,Status status){
            StringBuilder str = new StringBuilder();
            str.Append("<span");
            str.Append(" class=\""+status.ToString()+"-status\" ");
            str.Append(">" + statusStr + "</span>");
            return str.ToString();
        }

        public static string GetHitStr(Status status,string context=""){
            string responseStr=string.Empty;
            switch (status){
                case Status.success:
                    responseStr = "操作成功！";
                    break;
                case Status.error:
                    responseStr = "操作失败，请稍后重试！";
                    break;
                default:
                    responseStr = "系统异常，请稍后重试！";
                    break;
            }
            return context+responseStr;
        }
        #endregion

        #region 根据状态码获取中文标识
        public static string GetStatusCodeToStr(int statusCode) {
            StatusColor color = (StatusColor)statusCode;
            StringBuilder str = new StringBuilder("<span class=\"color-"+color.ToString()+"\">");
            Type t = color.GetType();
            FieldInfo fi = t.GetField(color.ToString());
            DescriptionAttribute[] arrDesc = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            str.Append(arrDesc[0].Description);
            str.Append("</span>");
            return str.ToString();
        }
        #endregion

        #region 根据Node获取中文标识
        public static string GetNodeCnStr(int node) {
            return node > 0 ? "市" : "省、直辖市、特别行政区";
        }
        #endregion

        #region 获取距离现在的时间
        public static string GetTimeSpan(DateTime time) {
            string timeSpanStr="";
            TimeSpan t1 = new TimeSpan(time.Ticks);
            TimeSpan t2 = new TimeSpan(DateTime.Now.Ticks);
            TimeSpan t = t2 - t1;
            if (t.Days > 0){
                timeSpanStr = t.Days.ToString() + "天前";                
            }else {
                if (t.Hours > 0)
                    timeSpanStr = t.Hours.ToString() + "小时前";
                else
                    timeSpanStr = t.Minutes > 5 ? t.Minutes.ToString()+"分钟前" : "刚刚";
            }
            return timeSpanStr;
        }

        public static string GetAnswerTimeSpan(DateTime time) {
        string timeSpanStr="";
            TimeSpan t1 = new TimeSpan(time.Ticks);
            TimeSpan t2 = new TimeSpan(DateTime.Now.Ticks);
            TimeSpan t = t2 - t1;
            if (t.Days==0){
                timeSpanStr ="今天"+ time.ToString("hh:mm");
            } 
            else if (t.Days ==1) {
                timeSpanStr = "昨天" + time.ToString("hh:mm");
            }
            else {
                timeSpanStr = time.ToString("yyyy年MM月dd日hh:mm");
            }
            return timeSpanStr;
        }
        #endregion

        #region 设置状态码中文字符
        public static string GetStatusNumToStr (int statusNum){
            string str = "";
            switch (statusNum) { 
                case 0:
                    str = "禁用";
                    break;
                case 1:
                    str = "正常";
                    break;
                case 2:
                    str = "热门";
                    break;
                }
            return str;
        }
        #endregion

        #region 获取翻页字符串
        public static string GetPageStr(int pageSize,int currentPage,int count) {
            PagingHelper ph = new PagingHelper(pageSize, currentPage, count);
            return ph.GetPageString();
        }

        public static string GetPageStrPro(string url, int pageSize, int currentPage, int count, int pageshow)
        {
            PagingHelper ph = new PagingHelper(pageSize, currentPage, count,pageshow);
            return ph.GetPageStringPro(url,false);
        }
        #endregion
    }

    public enum StatusColor {
        [Description("禁用")]
        red=0,
        [Description("正常")]
        cyan=1
    }

    public enum Status
    {
        success,
        error,
        warning
    }

    public enum DocType
    {
        word,
        exl,
        ppt,
        pdf
    }
}
