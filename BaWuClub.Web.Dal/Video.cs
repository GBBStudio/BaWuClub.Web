//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace BaWuClub.Web.Dal
{
    using System;
    using System.Collections.Generic;
    
    public partial class Video
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string LocalUrl { get; set; }
        public string Teacher { get; set; }
        public Nullable<int> Views { get; set; }
        public string Cover { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<byte> Status { get; set; }
        public Nullable<int> Sort { get; set; }
        public Nullable<byte> Mode { get; set; }
        public Nullable<System.DateTime> VarDate { get; set; }
        public Nullable<int> AdminId { get; set; }
    }
}
