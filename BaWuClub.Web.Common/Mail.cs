using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace BaWuClub.Web.Common
{
    public class Mail
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="from">发送邮件地址</param>
        /// <param name="password">发送邮件的密码</param>
        /// <param name="host">Smtp服务器地址</param>
        /// <param name="port">Smtp服务器的端口</param>
        /// <param name="timeout">Smtp超时的时间</param>
        /// <param name="to">接收邮件地址</param>
        /// <param name="subject">邮件的主题</param>
        /// <param name="body">邮件的内容</param>
        /// <param name="displayName">显示的名称</param>

        public Mail(string from, string password, string host,int port,int timeout,string to, string subject, string body, string displayName)
        {
            From = from;
            To = to;
            PassWord = password;
            Subject = subject;
            Body = body;
            DisplayName = displayName;
            TimeOut = timeout;
            Host = host;
            Port = port;
        }

        public string SendEmailAsync()
        {
            SmtpClient smtp = new SmtpClient(Host);
            smtp.Port = Port;
            smtp.Credentials=new System.Net.NetworkCredential(From,PassWord);
            smtp.Timeout = TimeOut;//timeout
            smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            MailMessage mailMsg = new MailMessage(new MailAddress(From, DisplayName), new MailAddress(To));
            mailMsg.Body = Body;
            mailMsg.Subject = Subject;
            try
            {
                smtp.Send(mailMsg);
                return "发送完成";
            }
            catch (Exception e) {
                return e.Message;
            }
        }

        public string From { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public int TimeOut { get; set; }
        public string To { get; set; }
        public string PassWord { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        public string DisplayName { get; set; }
    }
}