using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaWuClub.Web.Dal
{
    public static class ClubConst
    {
        public const int AdminPageSize=30;
        public const int AdminPageShow = 1;
        public const int WebPageSize = 15;
        public const int WebPageShow = 8;

        public const int WebQuestionPageShow = 8;
        public const int WebQuestionPageSize = 15;

        public const int MemberPageSize= 5;
        public const int MemberPageShow = 3;

        public const int AvatarBigWidth = 300;
        public const int AvatarBigHeight = 300;
        public const int AvatarSmallWidth = 75;
        public const int AvatarSmallHeight = 75;

        public readonly static string AvatarDir = "/uploads/avatar/";
        public readonly static string AvatarBigDir = "/uploads/avatar/big/";
        public readonly static string AvatarSmallDir = "/uploads/avatar/small/";
        public readonly static string EditorDirectory = "/uploads/editor/";
        public readonly static string BannerDir = "/uploads/banner/";
        public readonly static string ActivityDir = "/uploads/activity/";
    }
}
