using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaWuClub.Web.Dal;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Xml.Linq;

namespace BaWuClub.Web.Controllers
{
    public class RssController : Controller
    {
        //
        // GET: /Rss/
        private ClubEntities club;

        public RssResult Index() {
            List<RssItem> list = new List<RssItem>();
            List<Article> articles = new List<Article>();
            using (club = new ClubEntities()) {
                articles = club.Articles.OrderByDescending(a=>a.PutDate).Where(a=>a.Status==1).Take(10).ToList<Article>();
            }
            if (articles != null) {
                foreach (var a in articles) {
                    var desc=Common.HtmlCommon.ClearHtml(a.Context);
                    list.Add(new RssItem() {Title=a.Title,Description=(desc.Length>200?desc.Substring(0,199):desc),PutDate=a.PutDate });
                }
            }
            return new RssResult(list);
        }
    }

    public class RssItem {
        public int Id { get; set; }
        public string Title { set; get; }
        public string Description { get; set; }
        public DateTime? PutDate { get; set; }
    }

    public class RssResult : ActionResult {
        public List<RssItem> Items { set;get; }

        public RssResult(List<RssItem> list) {
            Items = list;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.ContentType = "application/rss+xml";
            XDocument xdoc = RssHepler.GetRss(Items);
            context.HttpContext.Response.Write(xdoc.ToString());
            //throw new NotImplementedException();
        }
    }

    public static class RssHepler{
        public static XDocument GetRss(List<RssItem> list) {
            XElement channel = new XElement("channel",new XElement[]{
            new XElement("title","八五电商俱乐部RSS"),
            new XElement("link","http://www.bawu.com")
            });
            foreach (var a in list)
            {
                XElement item = new XElement("item");
                item.Add(new XElement[]{
                    new XElement("title",a.Title),
                    new XElement("description",a.Description),
                    new XElement("link","http://www.bawu.com/column/show"+a.Id),
                    new XElement("pubdate",a.PutDate)
                });
                channel.Add(item);
            }
            XDocument doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
                new XProcessingInstruction("xml-stylesheet","type='text/xsl' href='/content/css/rss.css'"),
                new XElement("rss", new XAttribute("version", "2.0"), new XElement(channel))
                );
            return doc;
        }
    }
}
