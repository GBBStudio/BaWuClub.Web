using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaWuClub.Web.Models
{
    public class SettingModel
    {
        public string Title { get; set; }
        public string Keywords { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public string QQ { get; set; }
        public string Email{get;set;}
        public string Fax { get; set; }
        public string Address { get; set; }
        public string CopyRight { get; set; }
    }
}