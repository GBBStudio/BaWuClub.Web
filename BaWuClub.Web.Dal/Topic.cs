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
    
    public partial class Topic
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Context { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> Category { get; set; }
        public Nullable<int> Views { get; set; }
        public Nullable<int> TopicIndex { get; set; }
        public Nullable<byte> Stick { get; set; }
        public Nullable<byte> Recommend { get; set; }
        public Nullable<byte> Good { get; set; }
        public string Tags { get; set; }
        public string TagIds { get; set; }
        public Nullable<System.DateTime> VarDate { get; set; }
    }
}