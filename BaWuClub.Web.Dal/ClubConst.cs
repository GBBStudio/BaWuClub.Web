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
        //public const int WebPageSize = 15;
        //public const int WebPageShow = 8;
        public const int WebPageSize = 5;
        public const int WebPageShow = 8;

        public const int WebQuestionPageShow = 8;
        public const int WebQuestionPageSize = 15;

        public const int TopicPageShow = 8;
        public const int TopicPageSize = 12;

        public const int MemberPageSize= 5;
        public const int MemberPageShow = 3;

        public const int AvatarBigWidth = 300;
        public const int AvatarBigHeight = 300;
        public const int AvatarSmallWidth = 75;
        public const int AvatarSmallHeight = 75;

        public readonly static string FilesDir = "/uploads/files/";
        public readonly static string AvatarDir = "/uploads/avatar/";
        public readonly static string AvatarBigDir = "/uploads/avatar/big/";
        public readonly static string AvatarSmallDir = "/uploads/avatar/small/";
        public readonly static string EditorDirectory = "/uploads/editor/";
        public readonly static string BannerDir = "/uploads/banner/";
        public readonly static string ActivityDir = "/uploads/activity/";
        public readonly static string VideoDir = "/uploads/video/";
        public readonly static string VideoCoverDir = "/uploads/video/Cover/";
        public readonly static string UploadTmp = "/uploads/tmps/";
        public readonly static string TopicAactivity = "/uploads/topics/activity/";
        public readonly static string Topics = "/uploads/topics/pics/";

        public readonly static string TextFormatDataUrl = "~/app_data/textconfig.xml";
    }

    public enum State { 
        Disable=0,
        Enable=1
    }

    public enum TopicType { 
        Topic=0,
        Activity=1,
        Task=2
    }

    public enum VideoStatus {
        Disable=0,
        Enable=1,
        Top=2,
        Recommend=3
    }
}
