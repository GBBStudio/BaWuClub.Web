using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaWuClub.Web.Common
{
    public class PagingHelper
    {
        int pagesize, pagecurrent,pageshow,rowscount,pagecount;

        public PagingHelper(int pagesize, int pagecurrent, int rowscount, int pageshow)
        {
            this.pagesize = pagesize;
            this.pagecurrent = pagecurrent > 0 ? pagecurrent : pagecurrent + 1;
            this.pageshow = pageshow;
            this.rowscount = rowscount;
        }
        public PagingHelper(int pagesize, int pagecurrent, int rowscount)
        {
            this.pagesize = pagesize;
            this.pagecurrent = pagecurrent > 0 ? pagecurrent : pagecurrent + 1;
            this.rowscount = rowscount;
        }
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount {
            set {
                pagecount = value;
            }
            get {
                return (rowscount+pagesize-1)/pagesize;
            }
        }
        /// <summary>
        /// 每页的条目数
        /// </summary>
        public int PageSize { 
             set{pagesize=value;}
            get {return pagesize;}
        }
        /// <summary>
        /// 当前页
        /// </summary>
        public int PageCurrent {
            set { pagecurrent = value; }
            get { return pagecurrent; }
        }
        /// <summary>
        /// 展示的页数编码的个数
        /// </summary>
        public int PageShow {
            set { pageshow = value; }
            get { return pageshow; }
        }
        /// <summary>
        /// 总的行数。从而计算出总的页数
        /// </summary>
        public int RowsCount {
            set { rowscount = value; }
            get { return rowscount; }
        }

        /// <summary>
        /// 获取分页的html代码(简单不含数据的)
        /// </summary>
        /// <returns>page's html string</returns>
        public string GetPageString (){
            StringBuilder str = new StringBuilder();
            str.Append("<span class=\"page-text\">共"+RowsCount+"条"+PageCount+"页数据.当前是"+PageCurrent+"页</span>");
            str.Append("<a href=\"?page=1\">首页</a>");
            str.Append(PageCurrent == 1 ? "<span>上一页</span>" : "<a href=\"?page=" + (pagecurrent - 1) + "\">上一页</a>");
            str.Append(PageCurrent == PageCount? "<span>下一页</span>" : "<a href=\"?page=" + (pagecurrent +1) + "\">下一页</a>");
            str.Append("<a href=\"?page=" + PageCount + "\">尾页</a>");
            return str.ToString();
        }

        public string GetPageStringPro(string url,bool openAjax=true) {
            StringBuilder str = new StringBuilder();
            int start = 0, end = 0;
            if (PageCurrent <= (PageShow / 2)){
                start = 1;
                end = PageCount;
                if (PageCount > PageShow)
                    end = PageShow;
            }
            else{
                start = PageCurrent - (PageShow / 2);
                end = start +PageShow;
                if ((start + PageShow) > PageCount)
                    end = PageCount;
            }
            str.Append("<span class=\"page-text\">共" + (PageCount>0?PageCount:1) + "页.当前是" + PageCurrent + "页</span>");
            for (int j =start; j <= end;j++ ){
                str.Append("<a " + (openAjax ? "data-" : "") + "href=\"" + url + j + "\" class=\"" + (j == PageCurrent ? "current" : "") + "\">" + j + "</a>");
            }
            return str.ToString();
        }
    }
}
