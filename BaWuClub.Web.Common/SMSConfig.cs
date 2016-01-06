using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaWuClub.Web.Common
{
    class SMSConfig
    {
        private string gwid, ua, pwd;

        public SMSConfig() {

        }

        public string Gwid { get { return gwid; } set { gwid = value; } }
        public string UA { get { return ua; } set { ua = value; } }
        public string Pwd { get { return pwd; } set { pwd = value; } }
    }
}
