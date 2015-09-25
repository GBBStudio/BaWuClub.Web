[33mcommit fe36400134ff5f526734fe2c299acab94223ecfd[m
Author: wesen <mengweifeng@live.cn>
Date:   Fri Sep 11 15:03:12 2015 +0800

    Revert "9.11"
    
    This reverts commit 64085116a75b14a3a3773bf9b97bc0fdc0c4fc32.

[1mdiff --git a/BaWuClub.Web.Dal/ClubModel.edmx b/BaWuClub.Web.Dal/ClubModel.edmx[m
[1mindex de0e08d..22ad932 100644[m
[1m--- a/BaWuClub.Web.Dal/ClubModel.edmx[m
[1m+++ b/BaWuClub.Web.Dal/ClubModel.edmx[m
[36m@@ -235,7 +235,6 @@[m
           <Property Name="Name" Type="nvarchar" MaxLength="50" />[m
           <Property Name="Description" Type="text" />[m
           <Property Name="Cover" Type="nvarchar" MaxLength="50" />[m
[31m-          <Property Name="Icon" Type="nvarchar" MaxLength="50" />[m
           <Property Name="Type" Type="tinyint" />[m
           <Property Name="Variable" Type="nvarchar" MaxLength="50" />[m
           <Property Name="VarDate" Type="datetime" />[m
[36m@@ -487,7 +486,6 @@[m
           <Property Name="FatherDescription" Type="text" />[m
           <Property Name="FatherName" Type="nvarchar" MaxLength="50" />[m
           <Property Name="FatherId" Type="int" Nullable="false" />[m
[31m-          <Property Name="Icon" Type="nvarchar" MaxLength="50" />[m
         </EntityType>[m
         <EntityContainer Name="ClubDBModelStoreContainer">[m
           <EntitySet Name="Activities" EntityType="Self.Activities" Schema="dbo" store:Type="Tables" />[m
[36m@@ -644,8 +642,7 @@[m
     [ViewTopicCategory].[FatherCover] AS [FatherCover], [m
     [ViewTopicCategory].[FatherDescription] AS [FatherDescription], [m
     [ViewTopicCategory].[FatherName] AS [FatherName], [m
[31m-    [ViewTopicCategory].[FatherId] AS [FatherId], [m
[31m-    [ViewTopicCategory].[Icon] AS [Icon][m
[32m+[m[32m    [ViewTopicCategory].[FatherId] AS [FatherId][m[41m[m
     FROM [dbo].[ViewTopicCategory] AS [ViewTopicCategory]</DefiningQuery>[m
           </EntitySet>[m
         </EntityContainer>[m
[36m@@ -1067,7 +1064,6 @@[m
           <Property Name="Type" Type="Byte" />[m
           <Property Name="Variable" Type="String" MaxLength="50" FixedLength="false" Unicode="true" />[m
           <Property Name="VarDate" Type="DateTime" Precision="3" />[m
[31m-          <Property Name="Icon" Type="String" MaxLength="50" FixedLength="false" Unicode="true" />[m
         </EntityType>[m
         <EntityType Name="TopicIndex">[m
           <Key>[m
[36m@@ -1127,7 +1123,6 @@[m
           <Property Name="FatherDescription" Type="String" MaxLength="Max" FixedLength="false" Unicode="false" />[m
           <Property Name="FatherName" Type="String" MaxLength="50" FixedLength="false" Unicode="true" />[m
           <Property Name="FatherId" Type="Int32" Nullable="false" />[m
[31m-          <Property Name="Icon" Type="String" MaxLength="50" FixedLength="false" Unicode="true" />[m
         </EntityType>[m
         <EntityType Name="AdminAccount">[m
           <Key>[m
[36m@@ -1526,7 +1521,6 @@[m
           <EntitySetMapping Name="TopicCategories">[m
             <EntityTypeMapping TypeName="ClubDBModel.TopicCategory">[m
               <MappingFragment StoreEntitySet="TopicCategories">[m
[31m-                <ScalarProperty Name="Icon" ColumnName="Icon" />[m
                 <ScalarProperty Name="VarDate" ColumnName="VarDate" />[m
                 <ScalarProperty Name="Variable" ColumnName="Variable" />[m
                 <ScalarProperty Name="Type" ColumnName="Type" />[m
[36m@@ -1582,7 +1576,6 @@[m
           <EntitySetMapping Name="ViewTopicCategories">[m
             <EntityTypeMapping TypeName="ClubDBModel.ViewTopicCategory">[m
               <MappingFragment StoreEntitySet="ViewTopicCategory">[m
[31m-                <ScalarProperty Name="Icon" ColumnName="Icon" />[m
                 <ScalarProperty Name="FatherId" ColumnName="FatherId" />[m
                 <ScalarProperty Name="FatherName" ColumnName="FatherName" />[m
                 <ScalarProperty Name="FatherDescription" ColumnName="FatherDescription" />[m
[1mdiff --git a/BaWuClub.Web.Dal/ClubModel.edmx.diagram b/BaWuClub.Web.Dal/ClubModel.edmx.diagram[m
[1mindex fcf2556..7712510 100644[m
[1m--- a/BaWuClub.Web.Dal/ClubModel.edmx.diagram[m
[1m+++ b/BaWuClub.Web.Dal/ClubModel.edmx.diagram[m
[36m@@ -34,9 +34,9 @@[m
         <EntityTypeShape EntityType="ClubDBModel.TopicIndex" Width="1.5" PointX="10.125" PointY="10.375" />[m
         <EntityTypeShape EntityType="ClubDBModel.TopicReview" Width="1.5" PointX="0.75" PointY="11.125" />[m
         <EntityTypeShape EntityType="ClubDBModel.Topic" Width="1.5" PointX="2.75" PointY="11.125" />[m
[31m-        <EntityTypeShape EntityType="ClubDBModel.ViewTopicCategory" Width="1.5" PointX="11.75" PointY="3.75" />[m
[31m-        <EntityTypeShape EntityType="ClubDBModel.AdminAccount" Width="1.5" PointX="13.375" PointY="0.75" />[m
[31m-        <EntityTypeShape EntityType="ClubDBModel.AdminType" Width="1.5" PointX="13.375" PointY="4" />[m
[32m+[m[32m        <EntityTypeShape EntityType="ClubDBModel.ViewTopicCategory" Width="1.5" PointX="12.375" PointY="4.75" />[m[41m[m
[32m+[m[32m        <EntityTypeShape EntityType="ClubDBModel.AdminAccount" Width="1.5" PointX="13.875" PointY="0.75" />[m[41m[m
[32m+[m[32m        <EntityTypeShape EntityType="ClubDBModel.AdminType" Width="1.5" PointX="14.375" PointY="4.75" />[m[41m[m
       </Diagram>[m
     </edmx:Diagrams>[m
   </edmx:Designer>[m
[1mdiff --git a/BaWuClub.Web.Dal/TopicCategory.cs b/BaWuClub.Web.Dal/TopicCategory.cs[m
[1mindex d253817..cd12c9f 100644[m
[1m--- a/BaWuClub.Web.Dal/TopicCategory.cs[m
[1m+++ b/BaWuClub.Web.Dal/TopicCategory.cs[m
[36m@@ -21,6 +21,5 @@[m [mpublic partial class TopicCategory[m
         public Nullable<byte> Type { get; set; }[m
         public string Variable { get; set; }[m
         public Nullable<System.DateTime> VarDate { get; set; }[m
[31m-        public string Icon { get; set; }[m
     }[m
 }[m
[1mdiff --git a/BaWuClub.Web.Dal/ViewTopicCategory.cs b/BaWuClub.Web.Dal/ViewTopicCategory.cs[m
[1mindex 4647ee6..11bdfbf 100644[m
[1m--- a/BaWuClub.Web.Dal/ViewTopicCategory.cs[m
[1m+++ b/BaWuClub.Web.Dal/ViewTopicCategory.cs[m
[36m@@ -28,6 +28,5 @@[m [mpublic partial class ViewTopicCategory[m
         public string FatherDescription { get; set; }[m
         public string FatherName { get; set; }[m
         public int FatherId { get; set; }[m
[31m-        public string Icon { get; set; }[m
     }[m
 }[m
[1mdiff --git a/BaWuClub.Web/Areas/bwum/Views/Banner/TypeEdit.cshtml b/BaWuClub.Web/Areas/bwum/Views/Banner/TypeEdit.cshtml[m
[1mdeleted file mode 100644[m
[1mindex 47c6a64..0000000[m
[1m--- a/BaWuClub.Web/Areas/bwum/Views/Banner/TypeEdit.cshtml[m
[1m+++ /dev/null[m
[36m@@ -1,38 +0,0 @@[m
[31m-﻿@{[m
[31m-    Layout = "~/areas/bwum/views/shared/_indexlayout.cshtml";[m
[31m-}[m
[31m-@model BaWuClub.Web.Dal.BannerType[m
[31m-<div class="comm-btns">[m
[31m-    <h2>广告分类创建</h2>[m
[31m-    <a class="btn-back iconfont icon-chexiao" href="#/banner/bannertypeindex">返回广告分类列表</a>[m
[31m-</div>[m
[31m-@Html.Raw(ViewBag.StatusStr)[m
[31m-<div id="input-wrap">[m
[31m-    @{[m
[31m-        string _actionStr = "/bwum/banner/bannertypecreate";[m
[31m-        string _inputStr = string.Empty;[m
[31m-        if (Model!=null && Model.Id > 0)[m
[31m-        {[m
[31m-            _actionStr = "/bwum/banner/bannertypeedit";[m
[31m-            _inputStr = "<input name=\"id\" value=\"" + Model.Id + "\" type=\"hidden\"/>";[m
[31m-        }[m
[31m-    }[m
[31m-    <form method="post" name="frame" action="@_actionStr">[m
[31m-        <p>[m
[31m-            <span>名称：</span>[m
[31m-            <input name="name" value="@Model.Name" type="text" class="input-text input-middle" />[m
[31m-            @Html.Raw(_inputStr)[m
[31m-        </p>[m
[31m-        <p>[m
[31m-            <span>尺寸：</span>[m
[31m-            <input name="size" value="@Model.Size" type="text" class="input-text input-middle" />[m
[31m-        </p>[m
[31m-        <p>[m
[31m-            <span>变量名：</span>[m
[31m-            <input name="variables" value="@Model.Variables" type="text" class="input-text input-middle" />[m
[31m-        </p>[m
[31m-        <p>[m
[31m-            <input class="btn btn-confirm" type="submit" value="提交" />[m
[31m-        </p>[m
[31m-    </form>[m
[31m-</div>[m
\ No newline at end of file[m
[1mdiff --git a/BaWuClub.Web/Areas/bwum/Views/Discuss/Create.cshtml b/BaWuClub.Web/Areas/bwum/Views/Discuss/Create.cshtml[m
[1mindex de91d33..42faf74 100644[m
[1m--- a/BaWuClub.Web/Areas/bwum/Views/Discuss/Create.cshtml[m
[1m+++ b/BaWuClub.Web/Areas/bwum/Views/Discuss/Create.cshtml[m
[36m@@ -1,6 +1,5 @@[m
 ﻿@{[m
     Layout = "~/areas/bwum/views/shared/_indexlayout.cshtml";[m
[31m-    ViewBag.CssAppend = "<link href=\"/content/fonts/category.css\" type=\"text/css\" rel=\"stylesheet\"/>";[m
 }[m
 @model BaWuClub.Web.Dal.TopicCategory[m
 <div class="comm-btns">[m
[36m@@ -8,7 +7,6 @@[m
     <a class="btn-back iconfont icon-chexiao" href="#/discuss/index">返回讨论区分类列表</a>[m
 </div>[m
 @Html.Raw(ViewBag.StatusStr)[m
[31m-[m
 <div id="input-wrap">[m
     @{[m
         string _actionStr = "/bwum/discuss/create";[m
[36m@@ -34,126 +32,11 @@[m
             <textarea name="description" class="input-text input-long" style="height:150px;">@Model.Description</textarea>[m
         </p>[m
         <p>[m
[31m-            <span>图标：</span>[m
[31m-            @{[m
[31m-                var icon = Model.Icon;[m
[31m-                if (!string.IsNullOrEmpty(icon))[m
[31m-                {[m
[31m-                    <i class="font-category @icon"></i>[m
[31m-                }[m
[31m-                else { [m
[31m-                    <a href="#" class="select-icon">选择分类的图标</a>[m
[31m-                }[m
[31m-            }[m
[31m-            <input name="icon" type="hidden" value="@icon" id="icon"/>[m
[32m+[m[32m            <span>图片：</span>[m[41m[m
[32m+[m[32m            <input name="cover" type="hidden" value=""/>[m[41m[m
         </p>[m
         <p>[m
             <input class="btn btn-confirm" type="submit" value="提交" />[m
         </p>[m
     </form>[m
[31m-</div>[m
[31m-<div class="icons-wrap">[m
[31m-    <div class="icons-title"><span style="float:left;">分类图标</span>[m
[31m-    <a title="关闭" style="border:1px solid #666;border-radius:50%;display:block;width:18px;height:18px;text-align:center;line-height:18px;cursor:pointer;float:right;" onclick="closeiconwarp()">X</a></div>[m
[31m-    <ul>[m
[31m-        <li>[m
[31m-            <a class="font-category icon-chuangyejiaoliu" data-icon="icon-chuangyejiaoliu"></a>[m
[31m-        </li>[m
[31m-        <li>[m
[31m-            <a class="font-category icon-dianshangfuwushang" data-icon="icon-dianshangfuwushang"></a>[m
[31m-        </li>[m
[31m-        <li>[m
[31m-            <a class="font-category icon-dianshanghuodong" data-icon="icon-dianshanghuodong"></a>[m
[31m-        </li>[m
[31m-        <li>[m
[31m-            <a class="font-category icon-faburenwu" data-icon="icon-faburenwu"></a>[m
[31m-        </li>[m
[31m-        <li>[m
[31m-            <a class="font-category icon-jianyifankui" data-icon="icon-jianyifankui"></a>[m
[31m-        </li>[m
[31m-        <li>[m
[31m-            <a class="font-category icon-dianshangzatan" data-icon="icon-dianshangzatan"></a>[m
[31m-        </li>[m
[31m-        <li>[m
[31m-            <a class="font-category icon-kefujiaoliu" data-icon="icon-kefujiaoliu"></a>[m
[31m-        </li>[m
[31m-        <li>[m
[31m-            <a class="font-category icon-luntangonggao" data-icon="icon-luntangonggao"></a>[m
[31m-        </li>[m
[31m-        <li>[m
[31m-            <a class="font-category icon-qiuzhizhaopin" data-icon="icon-qiuzhizhaopin"></a>[m
[31m-        </li>[m
[31m-        <li>[m
[31m-            <a class="font-category icon-qiuzhuwenda" data-icon="icon-qiuzhuwenda"></a>[m
[31m-        </li>[m
[31m-        <li>[m
[31m-            <a class="font-category icon-sheyingsheji" data-icon="icon-sheyingsheji"></a>[m
[31m-        </li>[m
[31m-        <li>[m
[31m-            <a class="font-category icon-shichangtuiguang" data-icon="icon-shichangtuiguang"></a>[m
[31m-        </li>[m
[31m-        <li>[m
[31m-            <a class="font-category icon-taobaotianmao" data-icon="icon-taobaotianmao"></a>[m
[31m-        </li>[m
[31m-        <li>[m
[31m-            <a class="font-category icon-zhaotaobaoke" data-icon="icon-zhaotaobaoke"></a>[m
[31m-        </li>[m
[31m-        <li>[m
[31m-            <a class="font-category icon-cangchupeisong" data-icon="icon-cangchupeisong"></a>[m
[31m-        </li>[m
[31m-    </ul>[m
[31m-    <input type="button" value="确定" style="border:none;color:#fff;font-size:16px;padding:8px 35px;background:#ff6a00;cursor:pointer;border-radius:3px;" onclick="confirmselected()" />[m
[31m-</div>[m
[31m-<style>[m
[31m-    .icons-wrap{position:absolute;width:400px;left:15%;z-index:100;background:#fff;padding:20px;border-radius:5px;border:1px solid #666;display:none;}[m
[31m-    .icons-title {margin:10px 0;}[m
[31m-    .icons-wrap ul{clear:both;margin-top:15px;}[m
[31m-    .icons-wrap ul li {float:left;margin:10px 10px;}[m
[31m-    .icons-wrap ul li a{cursor:pointer;font-size:32px;text-decoration:none;}[m
[31m-    .icons-wrap ul li a.selected{color:#ff6a00}[m
[31m-    .mask-wrap{background:#000;width:100%;height:100%;position:fixed;top:0;left:0;filter:alpha(opacity=80);-moz-opacity:0.8;opacity:0.8;z-index:99;}[m
[31m-</style>[m
[31m-<script type="text/javascript">[m
[31m-[m
[31m-    $(function () {[m
[31m-        $("a.select-icon").click(function () {[m
[31m-            var styles=seticonwrapalign()[m
[31m-            $("body").append("<div class=\"mask-wrap\"></div>")[m
[31m-            $(".icons-wrap").attr("style",styles);[m
[31m-        });[m
[31m-[m
[31m-        $(".icons-wrap ul li a").click(function () {[m
[31m-            $(this).toggleClass("selected");[m
[31m-            $(this).parent("li").siblings("li").children("a").removeClass("selected");[m
[31m-        });[m
[31m-[m
[31m-        $(document).on("click", ".mask-wrap", function () {[m
[31m-            //$(".icons-wrap").hide()[m
[31m-            //$(this).remove();[m
[31m-            closeiconwarp();[m
[31m-        });[m
[31m-    })[m
[31m-[m
[31m-    function confirmselected() {[m
[31m-        $(".icons-wrap ul li").each(function () {[m
[31m-            if ($(this).children("a").hasClass("selected")) {[m
[31m-                var _class = $(this).children("a").attr("data-icon");[m
[31m-                $("#icon").val(_class);[m
[31m-                $(".select-icon").hide();[m
[31m-                $("#icon").parent().append("<i class=\"font-category "+_class+"\"></i>")[m
[31m-                closeiconwarp();[m
[31m-            }[m
[31m-        });[m
[31m-    }[m
[31m-[m
[31m-    function seticonwrapalign() {[m
[31m-        var width = ($("body").width() - 400)[m
[31m-        var height = ($("body").height() - $(".icons-wrap").height())[m
[31m-        return "left:" + (width / 2) + "px !important;top:" + (height / 2) + "px  !important;display:block;";[m
[31m-    }[m
[31m-[m
[31m-    function closeiconwarp() {[m
[31m-        $(".icons-wrap").hide()[m
[31m-        $(".mask-wrap").remove();[m
[31m-    }[m
[31m-</script>[m
\ No newline at end of file[m
[32m+[m[32m</div>[m
\ No newline at end of file[m
[1mdiff --git a/BaWuClub.Web/Areas/bwum/Views/Discuss/Index.cshtml b/BaWuClub.Web/Areas/bwum/Views/Discuss/Index.cshtml[m
[1mindex 49adccc..352724f 100644[m
[1m--- a/BaWuClub.Web/Areas/bwum/Views/Discuss/Index.cshtml[m
[1m+++ b/BaWuClub.Web/Areas/bwum/Views/Discuss/Index.cshtml[m
[36m@@ -1,6 +1,5 @@[m
 ﻿@{[m
     Layout = "~/areas/bwum/views/shared/_indexlayout.cshtml";[m
[31m-    ViewBag.CssAppend = "<link href=\"/content/fonts/category.css\" type=\"text/css\" rel=\"stylesheet\"/>";[m
 }[m
 [m
 @model IEnumerable<BaWuClub.Web.Dal.ViewTopicCategory>[m
[36m@@ -16,7 +15,6 @@[m
 <div id="notic-wrap">[m
     @Html.Raw(ViewBag.statusStr)[m
 </div>[m
[31m-[m
 <form name="form" id="form" method="post">[m
     <table>[m
         <thead>[m
[36m@@ -25,7 +23,6 @@[m
                 <td>Id</td>[m
                 <td>名称</td>[m
                 <td>变量</td>[m
[31m-                <td>图标</td>[m
                 <td>更多操作</td>[m
             </tr>[m
         </thead>[m
[36m@@ -35,17 +32,11 @@[m
                 {[m
                     foreach (BaWuClub.Web.Dal.ViewTopicCategory a in Model)[m
                     {[m
[31m-                        string iconStr = string.Empty;[m
[31m-                        if (!string.IsNullOrEmpty(a.Icon))[m
[31m-                        {[m
[31m-                            iconStr="<i class=\"font-category "+a.Icon+"\"></i>";[m
[31m-                        }[m
                         <tr>[m
                             <td><input type="checkbox" name="chk" value="@a.Id" /></td>[m
                             <td>@a.Id</td>[m
                             <td>@a.Name</td>[m
                             <td>@a.Variable</td>[m
[31m-                            <td>@Html.Raw(iconStr)</td>[m
                             <td><a href="#/discuss/edit/@a.Id">编辑</a></td>[m
                         </tr>[m
                     }[m
[1mdiff --git a/BaWuClub.Web/Areas/bwum/Views/Shared/_indexLayout.cshtml b/BaWuClub.Web/Areas/bwum/Views/Shared/_indexLayout.cshtml[m
[1mindex 2bf073e..ceef92b 100644[m
[1m--- a/BaWuClub.Web/Areas/bwum/Views/Shared/_indexLayout.cshtml[m
[1m+++ b/BaWuClub.Web/Areas/bwum/Views/Shared/_indexLayout.cshtml[m
[36m@@ -9,7 +9,6 @@[m
     @Styles.Render("~/Content/css/style.css")[m
     @Scripts.Render("~/Content/scripts/jquery.js")[m
     @Scripts.Render("~/Content/scripts/layout.js")[m
[31m-    @Html.Raw(ViewBag.CssAppend)[m
 </head>[m
 <body>[m
     <div id="container">[m
[1mdiff --git a/BaWuClub.Web/BaWuClub.Web.csproj b/BaWuClub.Web/BaWuClub.Web.csproj[m
[1mindex a088131..9f51228 100644[m
[1m--- a/BaWuClub.Web/BaWuClub.Web.csproj[m
[1m+++ b/BaWuClub.Web/BaWuClub.Web.csproj[m
[36m@@ -711,8 +711,6 @@[m
     <Content Include="Content\Css\rss.css" />[m
     <Content Include="Content\Css\style.css" />[m
     <Content Include="Content\Css\wdialog.css" />[m
[31m-    <Content Include="Content\Fonts\category.css" />[m
[31m-    <Content Include="Content\Fonts\category.svg" />[m
     <Content Include="Content\Fonts\font.svg" />[m
     <Content Include="Content\Fonts\icon.svg" />[m
     <Content Include="Content\Fonts\iconfont.svg" />[m
[36m@@ -935,9 +933,6 @@[m
     <Content Include="Areas\bwum\Views\Role\AccountCreate.cshtml" />[m
     <Content Include="Areas\bwum\Views\Level\Index.cshtml" />[m
     <Content Include="Areas\bwum\Views\Level\Create.cshtml" />[m
[31m-    <Content Include="Content\Fonts\category.eot" />[m
[31m-    <Content Include="Content\Fonts\category.ttf" />[m
[31m-    <Content Include="Content\Fonts\category.woff" />[m
     <None Include="Properties\PublishProfiles\bawuclub.pubxml" />[m
     <Content Include="Views\Column\Index.cshtml" />[m
     <Content Include="Views\Column\Show.cshtml" />[m
[36m@@ -953,8 +948,6 @@[m
     <Content Include="Views\Online\Index.cshtml" />[m
     <Content Include="Views\Forum\Index.cshtml" />[m
     <Content Include="Views\Member\Shared.cshtml" />[m
[31m-    <Content Include="Views\Member\Discuss.cshtml" />[m
[31m-    <Content Include="Views\Member\Message.cshtml" />[m
   </ItemGroup>[m
   <PropertyGroup>[m
     <VisualStudioVersion Condition="'$(VisualStudioVersion)' == ''">10.0</VisualStudioVersion>[m
[1mdiff --git a/BaWuClub.Web/Content/Css/site.css b/BaWuClub.Web/Content/Css/site.css[m
[1mindex 1b33234..7b6fa4e 100644[m
[1m--- a/BaWuClub.Web/Content/Css/site.css[m
[1m+++ b/BaWuClub.Web/Content/Css/site.css[m
[36m@@ -4,6 +4,8 @@[m [mimg{border:none;}[m
 a{text-decoration: none;cursor:pointer}[m
 a:visited{}[m
 input[type=submit],input[type=button]{cursor:pointer}[m
[32m+[m[32m@font-face {font-family: 'font';src: url('../fonts/font.eot');src: url('../fonts/font.eot?#iefix') format('embedded-opentype'),url('../fonts/font.woff') format('woff'), url('../fonts/font.ttf') format('truetype'), url('../fonts/font.svg#iconfont') format('svg'); }[m[41m[m
[32m+[m[32m@font-face {font-family: 'iconfont';src: url('../fonts/iconfont.eot');src: url('../fonts/iconfont.eot?#iefix') format('embedded-opentype'),url('../fonts/iconfont.woff') format('woff'), url('../fonts/iconfont.ttf') format('truetype'), url('../fonts/iconfont.svg#iconfont') format('svg'); }[m[41m[m
 #tips-wrap{font-size:12px;}[m
 .page-wrap{font-size:12px;padding:15px 0;}[m
 .page-wrap span{margin-right:20px;}[m
[36m@@ -13,9 +15,7 @@[m [minput[type=submit],input[type=button]{cursor:pointer}[m
 .clear-padding{padding:0 !important;}[m
 a.more{border-radius:5px;color:#666;display:block;padding:3px 0;width:100%;font-size:14px;background:#F1F1F2 -moz-linear-gradient(center top , #F8F8F9, #E6E6E8) repeat-x scroll 0% 0%;border:1px solid #bbb;text-align:center;}[m
 a.btn-join{background:#007ada;color:#fff;padding:8px 20px;}[m
[31m-/*icons*/  [m
[31m-@font-face {font-family: 'font';src: url('../fonts/font.eot');src: url('../fonts/font.eot?#iefix') format('embedded-opentype'),url('../fonts/font.woff') format('woff'), url('../fonts/font.ttf') format('truetype'), url('../fonts/font.svg#iconfont') format('svg'); }[m
[31m-@font-face {font-family: 'iconfont';src: url('../fonts/iconfont.eot');src: url('../fonts/iconfont.eot?#iefix') format('embedded-opentype'),url('../fonts/iconfont.woff') format('woff'), url('../fonts/iconfont.ttf') format('truetype'), url('../fonts/iconfont.svg#iconfont') format('svg'); }[m
[32m+[m[32m/*icons*/[m[41m    [m
 [class^="icon-"],[class*=" icon-"],[class^="icon1-"],[class*=" icon1-"]{font-size:16px;font-style:normal; -webkit-font-smoothing: antialiased; -webkit-text-stroke-width: 0.2px; -moz-osx-font-smoothing: grayscale;}[m
 [class^="icon-"],[class*=" icon-"]{font-family:"font";}[m
 [class^="icon1-"],[class*=" icon1-"]{font-family:"iconfont";}[m
[36m@@ -32,42 +32,40 @@[m [ma.btn-join{background:#007ada;color:#fff;padding:8px 20px;}[m
 .icon-link:before { content: "";}[m
 .icon-weekhot:before { content: "\e608";}[m
 .icon-dot:before { content: "\e609";}[m
[31m-[m
[31m-.icon1-xin:before { content: "\e600"; }[m
[31m-.icon1-duihua:before { content: "\e601"; }[m
[31m-.icon1-tuijian:before { content: "\e602"; }[m
[31m-.icon1-sousuo:before { content: "\e603"; }[m
[31m-.icon1-fenxiang:before { content: "\e604"; }[m
[31m-.icon1-chexiao:before { content: "\e605"; }[m
[31m-.icon1-zanyang:before { content: "\e606"; }[m
[31m-.icon1-piping:before { content: "\e607"; }[m
[31m-.icon1-xinxi:before { content: "\e608"; }[m
[31m-.icon1-bangzhu:before { content: "\e609"; }[m
[31m-.icon1-shoucang:before { content: "\e60a"; }[m
[31m-.icon1-xiai:before { content: "\e60b"; }[m
[31m-.icon1-cuowu:before { content: "\e60c"; }[m
[31m-.icon1-zhengque:before { content: "\e60d"; }[m
[31m-.icon1-fujian:before { content: "\e60e"; }[m
[31m-.icon1-fasongyoujian:before { content: "\e60f"; }[m
[31m-.icon1-xiangshang:before { content: "\e610"; }[m
[31m-.icon1-xiangxia:before { content: "\e611"; }[m
[31m-.icon1-xinlangweibo:before { content: "\e612"; }[m
[31m-.icon1-tengxunweibo:before { content: "\e613"; }[m
[31m-.icon1-qq:before { content: "\e614"; }[m
[31m-.icon1-anzhuo:before { content: "\e615"; }[m
[31m-.icon1-ditu:before { content: "\e616"; }[m
[31m-.icon1-dianhua:before { content: "\e617"; }[m
[31m-.icon1-weiruan:before { content: "\e618"; }[m
[31m-.icon1-pingguo:before { content: "\e619"; }[m
[31m-.icon1-youzhi:before { content: "\e61a"; }[m
[31m-.icon1-jine:before { content: "\e61b"; }[m
[31m-.icon1-xiangxia1:before { content: "\e61c"; }[m
[31m-.icon1-xiangshang1:before { content: "\e61d"; }[m
[31m-.icon1-re:before { content: "\e61e"; }[m
[31m-.icon1-xin2:before { content: "\e61f"; }[m
[31m-.icon1-re2:before { content: "\e620"; }[m
[31m-.icon1-liulan:before { content: "\e622"; }[m
[31m-.icon1-chakan:before { content: "\e621"; }[m
[32m+[m[32m.icon1-chakan:before { content: "\f02d4"; }[m
[32m+[m[32m.icon1-xiangxia:before { content: "\f02a9"; }[m
[32m+[m[32m.icon1-xiangshang:before { content: "\f02aa"; }[m
[32m+[m[32m.icon1-jine:before { content: "\f0280"; }[m
[32m+[m[32m.icon1-dianhua:before { content: "\f01ef"; }[m
[32m+[m[32m.icon1-ditu:before { content: "\f01ea"; }[m
[32m+[m[32m.icon1-qq:before { content: "\f01c7"; }[m
[32m+[m[32m.icon1-anzhuo:before { content: "\f01c9"; }[m
[32m+[m[32m.icon1-xinlangweibo:before { content: "\f01af"; }[m
[32m+[m[32m.icon1-tengxunweibo:before { content: "\f01b0"; }[m
[32m+[m[32m.icon1-xiangshang1:before { content: "\f016f"; }[m
[32m+[m[32m.icon1-xiangxia1:before { content: "\f0170"; }[m
[32m+[m[32m.icon1-fasongyoujian:before { content: "\f0165"; }[m
[32m+[m[32m.icon1-fujian:before { content: "\f015b"; }[m
[32m+[m[32m.icon1-zhengque:before { content: "\f0156"; }[m
[32m+[m[32m.icon1-cuowu:before { content: "\f0155"; }[m
[32m+[m[32m.icon1-xiai:before { content: "\f0145"; }[m
[32m+[m[32m.icon1-xinxi:before { content: "\f0142"; }[m
[32m+[m[32m.icon1-bangzhu:before { content: "\f0143"; }[m
[32m+[m[32m.icon1-shoucang:before { content: "\f0144"; }[m
[32m+[m[32m.icon1-zanyang:before { content: "\f013c"; }[m
[32m+[m[32m.icon1-piping:before { content: "\f013d"; }[m
[32m+[m[32m.icon1-chexiao:before { content: "\f013a"; }[m
[32m+[m[32m.icon1-fenxiang:before { content: "\f012f"; }[m
[32m+[m[32m.icon1-sousuo:before { content: "\f012c"; }[m
[32m+[m[32m.icon1-tuijian:before { content: "\f00af"; }[m
[32m+[m[32m.icon1-xin:before { content: "\f0021"; }[m
[32m+[m[32m.icon1-duihua:before { content: "\f00ae"; }[m
[32m+[m[32m.icon1-pingguo:before { content: "\f0200"; }[m
[32m+[m[32m.icon1-weiruan:before { content: "\f01ff"; }[m
[32m+[m[32m.icon1-youzhi:before { content: "\f0279"; }[m
[32m+[m[32m.icon1-re:before { content: "\f02b2"; }[m
[32m+[m[32m.icon1-xin2:before { content: "\f02b6"; }[m
[32m+[m[32m.icon1-re2:before { content: "\f02ba"; }[m[41m[m
 [m
 [m
 .fleft{float: left;}[m
[1mdiff --git a/BaWuClub.Web/Content/Fonts/iconfont.eot b/BaWuClub.Web/Content/Fonts/iconfont.eot[m
[1mindex 804092c..5aa6280 100644[m
Binary files a/BaWuClub.Web/Content/Fonts/iconfont.eot and b/BaWuClub.Web/Content/Fonts/iconfont.eot differ
[1mdiff --git a/BaWuClub.Web/Content/Fonts/iconfont.svg b/BaWuClub.Web/Content/Fonts/iconfont.svg[m
[1mindex d2cbe8e..5b7cc46 100644[m
[1m--- a/BaWuClub.Web/Content/Fonts/iconfont.svg[m
[1m+++ b/BaWuClub.Web/Content/Fonts/iconfont.svg[m
[36m@@ -2,7 +2,7 @@[m
 <!DOCTYPE svg PUBLIC "-//W3C//DTD SVG 1.1//EN" "http://www.w3.org/Graphics/SVG/1.1/DTD/svg11.dtd" >[m
 <svg xmlns="http://www.w3.org/2000/svg">[m
 <metadata>[m
[31m-Created by FontForge 20120731 at Fri Sep 11 14:22:55 2015[m
[32m+[m[32mCreated by FontForge 20120731 at Wed Jul 15 23:11:47 2015[m
  By Ads[m
 </metadata>[m
 <defs>[m
[36m@@ -19,7 +19,7 @@[m [mCreated by FontForge 20120731 at Fri Sep 11 14:22:55 2015[m
     bbox="-2 -212 1220.04 812"[m
     underline-thickness="50"[m
     underline-position="-100"[m
[31m-    unicode-range="U+0078-E622"[m
[32m+[m[32m    unicode-range="U+0078-F02D4"[m
   />[m
 <missing-glyph horiz-adv-x="374" [m
 d="M34 0v682h272v-682h-272zM68 34h204v614h-204v-614z" />[m
[36m@@ -33,86 +33,86 @@[m [md="M34 0v682h272v-682h-272zM68 34h204v614h-204v-614z" />[m
 d="M281 543q-27 -1 -53 -1h-83q-18 0 -36.5 -6t-32.5 -18.5t-23 -32t-9 -45.5v-76h912v41q0 16 -0.5 30t-0.5 18q0 13 -5 29t-17 29.5t-31.5 22.5t-49.5 9h-133v-97h-438v97zM955 310v-52q0 -23 0.5 -52t0.5 -58t-10.5 -47.5t-26 -30t-33 -16t-31.5 -4.5q-14 -1 -29.5 -0.5[m
 t-29.5 0.5h-32l-45 128h-439l-44 -128h-29h-34q-20 0 -45 1q-25 0 -41 9.5t-25.5 23t-13.5 29.5t-4 30v167h911zM163 247q-12 0 -21 -8.5t-9 -21.5t9 -21.5t21 -8.5q13 0 22 8.5t9 21.5t-9 21.5t-22 8.5zM316 123q-8 -26 -14 -48q-5 -19 -10.5 -37t-7.5 -25t-3 -15t1 -14.5[m
 t9.5 -10.5t21.5 -4h37h67h81h80h64h36q23 0 34 12t2 38q-5 13 -9.5 30.5t-9.5 34.5q-5 19 -11 39h-368zM336 498v228q0 11 2.5 23t10 21.5t20.5 15.5t34 6h188q31 0 51.5 -14.5t20.5 -52.5v-227h-327z" />[m
[31m-    <glyph glyph-name="uniE600" unicode="&#xe600;" [m
[32m+[m[32m    <glyph glyph-name="uF0021" unicode="&#xf0021;"[m[41m [m
 d="M891 749q29 0 53 -11t42 -29.5t28 -43.5t10 -54v-620q0 -29 -10 -54t-28 -43.5t-42 -29t-53 -10.5h-689q-29 0 -54 10.5t-43.5 29t-29 43.5t-10.5 54v620q0 29 10.5 54t29 43.5t43.5 29.5t54 11h689zM256 557q9 0 27.5 -0.5t43.5 -0.5h52h55q65 0 141 1v64h-126v64h-65[m
 v-64h-128v-64zM378 452q-8 15 -16 29q-6 13 -12 25.5t-9 20.5l-58 -21l32 -77zM255 55l101 144l-66 26l-99 -145zM449 238h126v62h-45h-81v65q23 0 45.5 -1t41.5 -1h41v64h-67q-20 0 -41 1l69 66l-69 32l-20 -98h-258v-63h36h47h54h56v-65h-128v-62h41q18 0 40.5 -0.5[m
 t46.5 0.5v-129q0 -17 -2 -27.5t-10 -15.5t-25 -5t-48 6q26 -32 28 -72q13 2 34 1t40.5 1.5t33.5 12.5t14 35v193zM568 127q-16 19 -29 38l-24 30q-12 15 -18 23l-46 -31l64 -98zM896 432q-20 1 -53 0.5t-64 -1.5q-37 -1 -76 -3v129q50 0 86 4.5t60 9.5q28 7 46 15[m
 q-18 27 -30 46q-5 8 -9.5 15t-7.5 11t-3.5 5t1.5 -5q-22 -16 -57.5 -23.5t-69.5 -10.5t-57.5 -3t-21.5 0q1 -17 1.5 -44.5t0 -78t-1 -129t-0.5 -196.5q0 -13 -3 -33t-10 -40t-20 -35.5t-32 -19.5q13 -3 22 -11.5t17 -18.5t14 -19.5t12 -14.5q12 18 23.5 42t20.5 50t14 52.5[m
 t5 47.5v41v43v47v61h65v-384h63v384q24 -2 36 -1t29 -4v72z" />[m
[31m-    <glyph glyph-name="uniE601" unicode="&#xe601;" horiz-adv-x="1220" [m
[32m+[m[32m    <glyph glyph-name="uF00AE" unicode="&#xf00ae;" horiz-adv-x="1220"[m[41m [m
 d="M829.5 692q79.5 1 150.5 -28.5t123.5 -81.5t84 -122t32.5 -150q1 -76 -26.5 -143t-75 -118.5t-112.5 -85t-140 -41.5q-39 -5 -90.5 -7.5t-133.5 5.5q-46 4 -80 10t-56 12.5t-31 11t-5 5.5q50 9 80 26q17 10 14 28t-17 33q-44 49 -72.5 114.5t-29.5 142.5q-1 80 29 150.5[m
 t82 123.5t123 83.5t150.5 31.5zM705.5 235q27.5 0 46.5 19.5t19 46.5t-19 46.5t-46.5 19.5t-46.5 -19.5t-19 -46.5t19 -46.5t46.5 -19.5zM960.5 237q26.5 0 45.5 18t19 45t-19 46t-45.5 19t-45 -19t-18.5 -46t18.5 -45t45 -18zM382 45q0 -37 -14 -69t-35 -57[m
 q-7 -8 -8.5 -16.5t6.5 -13.5q4 -2 10 -7.5t12.5 -11t14 -10t13.5 -5.5q-24 -6 -46 -3.5t-61 2.5q-20 0 -34.5 0.5t-25 1t-19.5 1.5l-19 2q-37 4 -68.5 19.5t-55 41.5t-37 58.5t-13.5 70.5q0 39 15.5 73.5t41.5 60t61 40t75 14.5q38 0 72.5 -15.5t60 -41.5t40 -61t14.5 -74z[m
 " />[m
[31m-    <glyph glyph-name="uniE602" unicode="&#xe602;" [m
[32m+[m[32m    <glyph glyph-name="uF00AF" unicode="&#xf00af;"[m[41m [m
 d="M825 746q28 0 53 -10.5t43.5 -29t29.5 -43t11 -53.5v-622q0 -29 -11 -53.5t-29.5 -43t-43.5 -29.5t-53 -11h-689q-27 0 -52.5 11t-43.5 29.5t-29 43t-11 53.5v622q0 29 11 53.5t29 43t43.5 29t52.5 10.5h689zM322 250q11 12 23.5 27t25.5 30.5t24 29.5t19 25l353 3v60[m
 h-333l11 28l-61 18v17h126l-3 -60h68l2 60l193 3v64h-193l2 66l-73 2l4 -68h-128l2 63h-69l4 -63h-192v-66l192 -1l-3 -63h-188v-60h203q-39 -46 -76 -84q-32 -34 -67 -67t-62 -48q11 -12 19.5 -26t16.5 -28q27 20 48.5 38t45.5 40v-211l63 -2zM771 300l-319 -2v-64l162 -1[m
 l-53 -33v-30l-177 3v-68l189 -1v-41q0 -2 -7.5 -11.5t-21.5 -9.5h-93l-2 -66q14 -1 32 -1h83q31 0 54 19t23 48v62l129 1v67l-112 -2v14l113 53v63z" />[m
[31m-    <glyph glyph-name="uniE603" unicode="&#xe603;" [m
[32m+[m[32m    <glyph glyph-name="uF012C" unicode="&#xf012c;"[m[41m [m
 d="M930 -16q28 -33 31 -57.5t-19 -48.5q-24 -28 -54 -26t-58 24l-172 166q-46 -30 -98.5 -46.5t-110.5 -16.5q-80 0 -150 30.5t-122 82.5t-82 122t-30 149.5t30 149.5t82 122t122 82t149.5 30t149.5 -30t122 -82t82.5 -122t30.5 -150q0 -60 -17.5 -114t-49.5 -101l34 -33[m
 q24 -25 51.5 -51.5t50 -50t28.5 -29.5zM452.5 107q53.5 0 100.5 20t82 55t55 82t20 100.5t-20 100.5t-55 82t-82 55t-100.5 20t-100.5 -20t-81.5 -55t-54.5 -82t-20 -100.5t20 -100.5t54.5 -82t81.5 -55t100.5 -20z" />[m
[31m-    <glyph glyph-name="uniE604" unicode="&#xe604;" [m
[32m+[m[32m    <glyph glyph-name="uF012F" unicode="&#xf012f;"[m[41m [m
 d="M736 174q33 0 62 -13t50.5 -35t34.5 -51t13 -62.5t-13 -62.5t-34.5 -50.5t-50.5 -34.5t-62.5 -13t-62.5 13t-51 34.5t-35 50.5t-13 63q0 10 1 18.5t3 17.5l-161 136q-14 -7 -30.5 -10t-33.5 -3q-34 0 -63 12.5t-51 34t-35 50.5t-13 63t13 63t35 51t51 34.5t63 12.5[m
 q38 0 72 -17l153 143q-4 13 -4 33q0 33 13 62.5t35 50.5t51 34t62.5 13t62.5 -13t50.5 -34t34.5 -50.5t13 -63t-13 -62.5t-34.5 -51t-50.5 -35t-62 -13q-24 0 -45 6.5t-38 17.5l-146 -136q6 -23 6 -46q0 -28 -9 -53l150 -127q18 11 38.5 16.5t43.5 5.5z" />[m
[31m-    <glyph glyph-name="uniE605" unicode="&#xe605;" [m
[32m+[m[32m    <glyph glyph-name="uF013A" unicode="&#xf013a;"[m[41m [m
 d="M895 238q0 -79 -29.5 -149t-81.5 -123.5t-122 -88t-151 -41.5q-5 -1 -9 -1.5t-9 -0.5h-178q-17 0 -34 6t-30 17t-22 25.5t-9 29.5t8.5 29.5t21.5 24.5t29.5 16.5t33.5 6.5l159 -1q54 0 102.5 20t84 53.5t56 79t20.5 98.5q0 49 -18.5 92t-50.5 76.5t-75 54.5t-93 26[m
 q-2 0 -2.5 -0.5t-2.5 -0.5h-163v-81q0 -17 -14 -25t-45 10q-8 5 -26 18t-42 30.5t-50 36.5l-52 39q-15 12 -26.5 22t-11.5 24q0 12 13 25.5t29 26.5q19 16 44.5 36.5t50.5 40t45 35t28 21.5q25 18 40.5 7.5t16.5 -30.5v-82h163q5 0 10.5 -1t10.5 -2q80 -8 149.5 -42[m
 t121.5 -87.5t81 -123.5t29 -147z" />[m
[31m-    <glyph glyph-name="uniE606" unicode="&#xe606;" [m
[32m+[m[32m    <glyph glyph-name="uF013C" unicode="&#xf013c;"[m[41m [m
 d="M926 347q0 -16 -7 -31t-17.5 -26.5t-26 -18t-31.5 -6.5h-30q34 0 58 -22.5t24 -55t-24 -56.5t-58 -24h-31q35 0 59 -23.5t24 -55.5q0 -17 -9 -31.5t-25 -25.5t-36.5 -17t-41.5 -6q33 0 54 -24t21 -57q0 -21 -11.5 -35.5t-29.5 -24.5t-38.5 -14.5t-36.5 -4.5h-183h-246[m
 q-33 0 -61 11t-49 30.5t-33 47.5t-12 63v313q0 20 1.5 40.5t7.5 38.5t19 33t34 24q21 8 47 24.5t54.5 39t55 50.5t48 59.5t34 65t12.5 68.5q1 31 8.5 51.5t19 33.5t25.5 18.5t28 5.5q37 0 62 -22.5t36 -73.5q2 -44 -6 -94q-6 -42 -21 -92t-47 -99h297q34 0 58 -22t24 -55z[m
 " />[m
[31m-    <glyph glyph-name="uniE607" unicode="&#xe607;" [m
[32m+[m[32m    <glyph glyph-name="uF013D" unicode="&#xf013d;"[m[41m [m
 d="M908 218q0 -31 -22.5 -51t-54.5 -20h-279q30 -47 45 -94.5t20 -86.5q6 -46 5 -87q-10 -48 -33.5 -69.5t-58.5 -21.5q-13 0 -26.5 5.5t-24 17.5t-17.5 31.5t-8 48.5q0 32 -12.5 64t-32.5 61t-45 55.5t-51 47.5t-51 36.5t-44 22.5q-21 9 -32.5 23t-17.5 31.5t-7.5 36.5[m
 t-1.5 38v295q0 33 11.5 58.5t31 44.5t46 29t57.5 10h232h170q16 0 35.5 -4.5t36 -13.5t28 -23.5t11.5 -32.5q0 -32 -20 -54t-52 -22q21 0 40 -6t34 -16t23.5 -23.5t8.5 -29.5q0 -32 -22.5 -54t-54.5 -22h28q32 0 55 -21t23 -53t-23 -52.5t-55 -20.5h28q16 0 30.5 -6.5[m
 t24.5 -17t16 -24.5t6 -30z" />[m
[31m-    <glyph glyph-name="uniE608" unicode="&#xe608;" [m
[32m+[m[32m    <glyph glyph-name="uF0142" unicode="&#xf0142;"[m[41m [m
 d="M512 758q95 0 178.5 -36.5t145.5 -98.5t98.5 -146t36.5 -179t-36.5 -178.5t-98.5 -145.5t-145.5 -98.5t-178.5 -36.5t-179 36.5t-146 98.5t-98.5 145.5t-36.5 178.5t36.5 179t98.5 146t146 98.5t179 36.5zM567 361q0 25 -15.5 42t-39.5 17t-40 -17t-16 -42v-343[m
 q0 -24 16 -39t40 -15t40 15.5t16 39.5zM512 488q30 0 51 21t21 51t-21 51t-51 21t-51.5 -21t-21.5 -51t21.5 -51t51.5 -21z" />[m
[31m-    <glyph glyph-name="uniE609" unicode="&#xe609;" [m
[32m+[m[32m    <glyph glyph-name="uF0143" unicode="&#xf0143;"[m[41m [m
 d="M514 758q95 0 178.5 -36.5t146 -98.5t98.5 -145.5t36 -178.5t-36 -178t-98.5 -145.5t-146 -98.5t-178.5 -36t-178 36t-145.5 98.5t-98.5 145.5t-36 178t36 178.5t98.5 145.5t145.5 98.5t178 36.5zM515 -14q27 0 45 17.5t18 43.5q0 27 -18 45t-45 18t-44.5 -18t-17.5 -45[m
 q0 -26 17.5 -43.5t44.5 -17.5zM567 238q-1 16 20.5 34.5t48.5 41t49.5 50.5t24.5 65q2 40 -8.5 75t-34 60t-61 39.5t-87.5 14.5q-62 0 -103.5 -22t-67 -53t-36 -64.5t-9.5 -55.5q1 -27 17 -39t34.5 -12.5t33.5 10t15 31.5q0 12 8 30t21 34.5t32 27.5t43 11q46 0 73.5 -23[m
 t25.5 -58q0 -17 -10 -32t-26 -29t-34 -27.5t-34 -28.5t-26.5 -32.5t-11.5 -37.5l1 -39q0 -15 14.5 -29t37.5 -15q24 1 37.5 15.5t12.5 32.5v25z" />[m
[31m-    <glyph glyph-name="uniE60A" unicode="&#xe60a;" [m
[32m+[m[32m    <glyph glyph-name="uF0144" unicode="&#xf0144;"[m[41m [m
 d="M781 196q18 -81 31 -146q6 -27 11 -54.5t9.5 -50t7.5 -38.5t4 -20q3 -20 -9 -27t-27 -7q-5 0 -15 3.5t-15 5.5l-266 155q-72 -42 -132 -76q-25 -14 -50 -28.5t-45.5 -26.5t-35 -20.5t-20.5 -10.5q-10 -5 -20.5 -3.5t-19.5 8t-13 15t-2 17.5q1 4 5.5 20t9.5 38t11.5 48.5[m
 t13.5 53.5q15 63 35 143q-60 52 -108 93q-21 17 -40.5 34t-35.5 30.5t-26.5 22.5t-11.5 10q-12 11 -20.5 24.5t-6.5 28t11 22.5t21 10l315 29l117 273q6 17 17.5 28.5t30.5 11.5q10 0 17.5 -4.5t13 -10.5t9 -12.5t5.5 -10.5l114 -273l315 -30q21 -5 29 -12t8 -22t-8.5 -25.5[m
 t-22.5 -25.5z" />[m
[31m-    <glyph glyph-name="uniE60B" unicode="&#xe60b;" [m
[32m+[m[32m    <glyph glyph-name="uF0145" unicode="&#xf0145;"[m[41m [m
 d="M534 544q33 42 71 75q33 28 74.5 50.5t86.5 19.5q63 -5 105.5 -30t68 -63.5t34.5 -87t6 -100t-18 -98t-37 -87t-48.5 -74.5t-53.5 -62q-41 -42 -85.5 -78.5t-85 -62.5t-73.5 -41.5t-52 -15.5q-20 -1 -52 14.5t-69.5 41.5t-79.5 62t-83 76q-27 26 -57.5 59.5t-58 74[m
 t-47 87.5t-21.5 100.5t11.5 100t40 83.5t65.5 62t88 35q25 5 49.5 1.5t48 -12t45 -22t40.5 -27.5q46 -34 87 -81z" />[m
[31m-    <glyph glyph-name="uniE60C" unicode="&#xe60c;" [m
[32m+[m[32m    <glyph glyph-name="uF0155" unicode="&#xf0155;"[m[41m [m
 d="M827.5 616.5q65.5 -65.5 97.5 -147.5t32 -168t-32 -168t-97.5 -147.5t-147.5 -98t-168 -32.5t-168.5 32.5t-148 98t-98 147.5t-32.5 168t32.5 168t98 147.5t148 97.5t168.5 32t168 -32t147.5 -97.5zM720.5 96.5q21.5 21.5 19 49t-23.5 49.5l-108 107l108 108[m
 q21 21 23.5 48.5t-19 49t-51.5 21.5t-52 -21l-107 -107l-104 105q-22 20 -49.5 22.5t-49.5 -17.5q-21 -22 -21 -52.5t21 -52.5l105 -104l-105 -104q-21 -22 -21 -52t21.5 -51.5t49 -19t49.5 23.5l104 105l107 -108q22 -21 52 -21t51.5 21.5z" />[m
[31m-    <glyph glyph-name="uniE60D" unicode="&#xe60d;" [m
[32m+[m[32m    <glyph glyph-name="uF0156" unicode="&#xf0156;"[m[41m [m
 d="M514 753q93 0 174.5 -35.5t142.5 -97t96.5 -143.5t35.5 -175t-35.5 -175.5t-96.5 -144t-142.5 -97t-174.5 -35.5t-175 35.5t-143 97t-96 144t-35 175.5t35 175t96 143.5t143 97t175 35.5zM796 388q18 18 19 45.5t-17.5 46t-45 18t-44.5 -19.5l-289 -289l-100 99[m
 q-18 18 -44.5 18.5t-45 -18t-19 -43.5t17.5 -43l143 -144q18 -18 48 -17.5t48 18.5l-4 -4z" />[m
[31m-    <glyph glyph-name="uniE60E" unicode="&#xe60e;" [m
[32m+[m[32m    <glyph glyph-name="uF015B" unicode="&#xf015b;"[m[41m [m
 d="M925 685q37 -37 54.5 -82.5t17.5 -93.5t-17.5 -93.5t-54.5 -82.5l-393 -389q-36 -37 -90 -61.5t-114 -29t-122.5 16t-115.5 74.5q-52 52 -72.5 113t-16.5 121.5t28 113.5t61 90l348 345q9 9 27 4.5t28 -13.5q8 -9 13 -27t-4 -27l-347 -345q-28 -27 -46.5 -64t-21.5 -79[m
 t12.5 -85t55.5 -83q35 -36 79 -50.5t87 -12t81.5 19t66.5 43.5l391 388q28 28 39.5 57.5t11 59t-13.5 57t-37 51.5q-44 43 -98.5 40t-110.5 -58l-353 -351q-24 -24 -23.5 -52.5t19.5 -46.5q22 -23 51.5 -19t47.5 23l321 318q9 9 27 4.5t27 -13.5t14 -27t-4 -27l-321 -319[m
 q-37 -37 -71 -51t-63.5 -12.5t-55 15.5t-47.5 35q-17 16 -31 41.5t-16 56.5t11 65.5t49 70.5q19 19 33 34l24 24l11 11l286 285q37 36 80.5 57t88.5 24.5t88.5 -12t80.5 -52.5z" />[m
[31m-    <glyph glyph-name="uniE60F" unicode="&#xe60f;" horiz-adv-x="1044" [m
[32m+[m[32m    <glyph glyph-name="uF0165" unicode="&#xf0165;" horiz-adv-x="1044"[m[41m [m
 d="M16 654q-11 8 -14 13.5t-3 14.5v9q0 27 13.5 42.5t45.5 15.5l844 -2q34 0 47.5 -14t13.5 -40l1 -11q0 -9 -2.5 -12.5t-16.5 -15.5l-419 -252q-10 -4 -25 -11t-20 -8q-6 0 -18 5.5t-28 14.5zM646 240q-21 0 -34.5 -9.5t-21 -24t-10 -32t-2.5 -34.5v-71v-13q0 -6 1 -11[m
 h-142q-72 0 -135.5 0.5t-112.5 0.5h-70q-37 0 -60.5 10t-37 25t-18.5 35t-5 42v379q0 21 10 26.5t26 -5.5q5 -3 18.5 -11.5t31.5 -20t38 -24.5t38 -24t31 -19t17 -11q14 -9 16 -22.5t-3 -24.5q-4 -10 -12 -26.5t-17.5 -33t-18 -32t-11.5 -23.5q-5 -15 2 -20t22 4q3 2 17 16[m
 t29.5 31t29 31.5t19.5 18.5q9 8 23.5 11.5t26.5 -2.5q7 -4 19 -12.5t27 -18t31 -19.5l27 -19q12 -8 26 -10t27 -1t23 4.5t15 6.5t19.5 12t31.5 19t32 19.5t22 13.5l129 80q2 1 12.5 8t26.5 17t35 22.5t36 23t31 20t21 13.5q19 12 30 9.5t11 -16.5v-266q-29 23 -56 43.5[m
 t-50 34.5q-22 16 -39 11.5t-28 -20t-16.5 -36t-5.5 -37.5q0 -11 -0.5 -18t-0.5 -12q-1 -5 -1 -8h-15h-105zM1028 132q13 -10 13.5 -28.5t-10.5 -26.5q-15 -13 -35.5 -29.5t-42.5 -34t-43 -35t-39 -30.5q-21 -18 -29.5 -19t-8.5 27v46q0 17 -9 28.5t-23 11.5h-119[m
 q-14 0 -28 10t-14 27v52q0 29 9 35t33 6h20q10 0 21.5 -0.5t28.5 -0.5h43q20 0 29 7t9 26v45q0 18 6 23t22 -8q17 -12 38.5 -29.5t44.5 -35.5t45 -36t39 -31z" />[m
[31m-    <glyph glyph-name="uniE610" unicode="&#xe610;" [m
[32m+[m[32m    <glyph glyph-name="uF016F" unicode="&#xf016f;"[m[41m [m
 d="M854 73l-313 309l-314 -309q-14 -15 -32.5 -22t-37.5 -7t-37 7t-32 22q-30 28 -30 68t30 69l377 374q14 14 34 22t40.5 9t39.5 -5t31 -20l383 -380q29 -29 29 -69t-29 -68q-14 -15 -32.5 -22t-37.5 -7t-37 7t-32 22z" />[m
[31m-    <glyph glyph-name="uniE611" unicode="&#xe611;" horiz-adv-x="1026" [m
[32m+[m[32m    <glyph glyph-name="uF0170" unicode="&#xf0170;" horiz-adv-x="1026"[m[41m [m
 d="M857 588q29 28 69.5 28t68.5 -28q30 -29 30 -69t-30 -68l-383 -381q-12 -15 -30.5 -20.5t-39 -4.5t-40.5 8.5t-34 22.5l-377 375q-30 28 -30 68t30 69q14 14 32 21t37 7t37.5 -7t32.5 -21l314 -310z" />[m
[31m-    <glyph glyph-name="uniE612" unicode="&#xe612;" horiz-adv-x="1138" [m
[32m+[m[32m    <glyph glyph-name="uF01AF" unicode="&#xf01af;" horiz-adv-x="1138"[m[41m [m
 d="M914 294q28 -22 39.5 -52t9.5 -63t-14.5 -65.5t-32.5 -59.5q-34 -48 -78 -81.5t-91.5 -56.5t-95 -35.5t-89.5 -18.5t-75 -7.5t-51.5 -1.5t-49.5 2.5t-68 10t-78 21.5t-80 36.5t-74.5 55t-59.5 78.5q-17 30 -22.5 64t-5.5 86q0 22 7.5 51t27 63.5t53.5 75.5t86 90[m
 q51 49 106 86t117 58q29 10 64 11t57 -11q10 -12 19.5 -23.5t12.5 -28.5q3 -15 0.5 -28t-5.5 -26.5t-5 -26t2 -22.5q17 -2 33.5 1.5t32.5 9.5l30 12q16 6 34 9q27 5 54.5 4.5t49.5 -7.5t36 -22t17 -39q2 -14 -2 -26.5t-8.5 -23t-7 -22t1.5 -23.5q3 -7 15 -13t27 -12.5[m
 t32 -13.5t29 -16zM656 -25q27 17 54 45.5t44 64t19 76t-20 81.5q-20 33 -48 57t-63 37.5t-74.5 19.5t-80.5 6q-78 0 -139 -16t-105.5 -43t-72.5 -60.5t-39 -68.5q-11 -34 -6.5 -67t21 -63t42.5 -53.5t58 -37.5q58 -26 119 -32.5t116.5 -0.5t101 21.5t73.5 33.5zM522 299[m
 q21 -8 39.5 -18.5t32.5 -27.5q10 -12 18 -30.5t11 -39.5t1.5 -42.5t-10.5 -39.5q-8 -18 -19.5 -37t-28.5 -35.5t-40.5 -29t-57.5 -18.5q-61 -12 -117.5 10.5t-88.5 75.5q-12 39 -13 70t16 67q14 27 41 51.5t62 39t75 17t79 -12.5zM359 47q38 -3 58 18.5t21.5 48t-15 47.5[m
 t-52.5 17q-28 -3 -43.5 -23.5t-17.5 -43.5t10 -42.5t39 -21.5zM778 806q75 0 140 -20t113.5 -58t76.5 -92t28 -123q0 -34 -16.5 -51t-35.5 -17.5t-35.5 14.5t-16.5 46q0 41 -22.5 77.5t-59.5 64.5t-84.5 44t-96.5 16q-32 0 -47 15t-14 34t18.5 34.5t51.5 15.5zM778 642[m
 q95 0 144.5 -48.5t49.5 -147.5q0 -30 -10.5 -43t-22.5 -11.5t-22.5 15t-10.5 35.5q0 61 -34.5 96.5t-93.5 35.5q-19 0 -28.5 11t-9.5 23.5t10 23t28 10.5z" />[m
[31m-    <glyph glyph-name="uniE613" unicode="&#xe613;" [m
[32m+[m[32m    <glyph glyph-name="uF01B0" unicode="&#xf01b0;"[m[41m [m
 d="M517 807q92 0 172.5 -35t140.5 -95t94.5 -140t34.5 -171q0 -93 -34.5 -173t-94.5 -140t-140.5 -95t-172.5 -35h-13q-6 0 -12 1h-5q-15 2 -25.5 14t-10.5 28q0 18 12 30t30 12q4 0 12 -2l1 1h11q74 0 139.5 28t114 77t77 114.5t28.5 139t-28.5 139t-77 114t-114 76.5[m
 t-139.5 28t-139.5 -28t-113.5 -76.5t-76 -114t-28 -138.5q0 -76 29 -144q7 -9 7 -22q0 -18 -12.5 -30t-29.5 -12q-12 0 -20.5 4.5t-14.5 12.5q-21 43 -32 90.5t-11 100.5q0 91 34.5 171t94.5 140t140.5 95t171.5 35zM722 366q0 -42 -16 -79.5t-43.5 -65t-65 -44t-80.5 -16.5[m
 q-35 0 -66 10.5t-57 31.5q-11 -14 -28.5 -34t-37 -44.5t-40 -51.5t-37.5 -54q-26 -38 -48 -78.5t-38 -74.5q-20 -40 -36 -78l-105 4q15 44 37 92q20 41 48 91.5t67 100.5q22 27 45.5 55t46 52.5t42 44.5t32.5 33q-29 48 -29 105q0 42 16 79t43.5 65t65 43.5t79.5 15.5[m
 q43 0 80.5 -15.5t65 -43.5t43.5 -65t16 -79z" />[m
[31m-    <glyph glyph-name="uniE614" unicode="&#xe614;" [m
[32m+[m[32m    <glyph glyph-name="uF01C7" unicode="&#xf01c7;"[m[41m [m
 d="M953 145l2 -14v-8v-6v-6l-1 -8l-1 -12l-3 -11l-3 -10l-1 -6l-2 -4l-3 -4l-2 -4l-2 -3l-3 -3l-2 -3l-3 -2l-3 -1l-3 -2l-3 -1h-2h-2l-3 1l-4 2l-2 2l-3 1l-2 3l-2 2l-4 4l-4 6l-4 6l-4 7l-4 6l-6 10l-5 11h-1h-1l-2 -1l-1 -3l-3 -4l-3 -10l-6 -16l-8 -18l-5 -9l-6 -10[m
 l-8 -12l-7 -10l-4 -5l-5 -5l-10 -10l1 -1l1 4l5 -3l22 -12l11 -5l9 -5l9 -7l8 -7l4 -3l3 -4l4 -3l2 -6l2 -4l1 -4l1 -4l1 -4l-1 -3v-3l-1 -3l-1 -3l-1 -2l-2 -4l-4 -6l-4 -4l-3 -3l-2 -3l-6 -4l-7 -4l-8 -3l-7 -4l-10 -3l-5 -2l-4 -1l-10 -2l-10 -3l-12 -2l-11 -1l-12 -1[m
 l-12 -3h-13h-13h-13l-14 1l-13 3l-13 1l-15 1l-14 3l-14 3l-14 3l-14 3l-13 5l-15 4l-6 3l-7 2l-4 1l-4 1h-5h-6l-14 1l-6 1h-8l-6 -4l-7 -5l-11 -5l-11 -6l-7 -3l-7 -3l-16 -6l-9 -3l-9 -2l-12 -3h-7l-10 -1l-8 -1l-9 -1h-10h-11h-21l-23 1l-22 2l-12 2l-11 2l-11 1l-10 3[m
[36m@@ -121,63 +121,58 @@[m [ml-4 9l-3 9l-2 12h-1h-1v1h-1l-1 -1h-1l-1 -2l-1 -3l-1 -2l-1 -3l-6 -9l-2 -4l-3 -5l-[m
 l7 10l7 11l7 10l7 10l6 9l8 8l9 9l3 4l5 4l7 6l7 6l11 11l9 6l3 2l-1 5l-2 7l-1 4v4v6v6l2 6l1 6l2 7l4 7l3 8l6 8v5v6l1 6l2 8l3 8l1 4l2 5l3 3l2 3v6v6v7l2 10l1 12l4 14l4 14l3 7l3 10l3 8l4 8l4 9l4 10l5 9l6 9l3 5l4 5l7 10l7 10l8 10l8 10l9 10l11 10l11 11l8 6l9 7[m
 l9 6l11 6l10 7l10 4l11 5l12 4l12 4l12 4l13 3l12 2l13 2l14 1l13 2l13 1h13h14h14l13 -2l15 -1l13 -2l13 -3l15 -3l13 -3l13 -3l14 -5l12 -5l12 -5l14 -7l11 -7l11 -7l10 -7l4 -3l6 -4l9 -7l7 -8l8 -8l7 -8l6 -8l8 -9l5 -10l5 -8l6 -9l4 -8l8 -18l3 -9l3 -9l3 -9l4 -8l2 -8[m
 l2 -9l4 -15l3 -13l1 -14l2 -10l2 -16l1 -2l1 -4l6 -9l3 -5l3 -6l3 -6l3 -7l3 -9l3 -8l1 -8l1 -5l1 -5v-5l-1 -4v-5l-1 -7l-4 -10l-2 -5l-2 -6v-2l1 -1l3 -5l13 -18l9 -14l4 -8l6 -10l5 -11l6 -11l7 -13l7 -15l4 -9l4 -9l3 -9l3 -8l2 -9l2 -8l3 -16z" />[m
[31m-    <glyph glyph-name="uniE615" unicode="&#xe615;" [m
[32m+[m[32m    <glyph glyph-name="uF01C9" unicode="&#xf01c9;"[m[41m [m
 d="M964 465q23 0 39 -15t16 -36v-299q0 -21 -16 -35.5t-39 -14.5h-19q-23 0 -39 14.5t-16 35.5v299q0 21 16 36t39 15h19zM141 465q23 0 38 -15t15 -36v-299q0 -21 -15 -35.5t-38 -14.5h-19q-23 0 -39.5 14.5t-16.5 35.5v299q0 21 16.5 36t39.5 15h19zM701 700q18 -8 38 -22[m
 t38 -33.5t32 -42t20 -48.5h-577q16 54 51 88.5t71 54.5l-52 82q-2 2 -0.5 7.5t10.5 11.5q8 7 15.5 6.5t9.5 -2.5l52 -83q29 13 60.5 20t64.5 7q69 0 129 -28l54 84q2 2 7.5 2t16.5 -6q11 -5 12.5 -9t-0.5 -6zM417.5 613q13.5 0 22.5 9t9 22.5t-9 22.5t-22.5 9t-22.5 -9[m
 t-9 -22.5t9 -22.5t22.5 -9zM667 617q13 0 22.5 9t9.5 22.5t-9.5 22.5t-22.5 9t-22 -9t-9 -22.5t9 -22.5t22 -9zM829 490l1 -456q0 -23 -15 -38t-36 -15h-14v-142q0 -21 -15 -36t-36 -15h-29q-21 0 -35.5 15t-14.5 36v142h-187v-142q0 -21 -14.5 -36t-36.5 -15h-29[m
 q-20 0 -35 15t-15 36v142h-11q-21 0 -36 15t-15 38v456h573z" />[m
[31m-    <glyph glyph-name="uniE616" unicode="&#xe616;" [m
[32m+[m[32m    <glyph glyph-name="uF01EA" unicode="&#xf01ea;"[m[41m [m
 d="M241 472q6 -24 13.5 -51.5t17.5 -56t22 -56.5t27 -53v-404l-256 -63v640zM317 507q0 5 -0.5 9.5t-0.5 9.5q0 46 18 86t49 70.5t72 47.5t88 17t88 -17t71.5 -47.5t48.5 -70.5t18 -86v-12q0 -6 -1 -12q-6 -55 -24 -104.5t-45.5 -95.5t-63.5 -88.5t-77 -81.5q-7 -7 -15 -7[m
 t-16 7q-42 39 -77.5 82t-63.5 90t-45.5 98t-23.5 105zM542.5 621q-20.5 0 -38 -7.5t-30 -20t-20.5 -30t-8 -37t8 -37t20.5 -30.5t30 -20t38 -7t38.5 7t31 20t20.5 30.5t7.5 37t-7.5 37t-20.5 30t-31 20t-38.5 7.5zM384 172q30 -33 58 -59.5t51 -46.5q26 -24 50 -42[m
 q12 10 27 23q14 11 31 28.5t40 39.5v-327l-257 63v321zM961 493v-642l-256 -63v392q6 6 11.5 13t11.5 13q21 22 38 53.5t31 66.5t25 70.5t19 66.5z" />[m
[31m-    <glyph glyph-name="uniE617" unicode="&#xe617;" horiz-adv-x="1026" [m
[32m+[m[32m    <glyph glyph-name="uF01EF" unicode="&#xf01ef;" horiz-adv-x="1026"[m[41m [m
 d="M705 425q21 -26 46 -52q20 -24 45 -52.5t52 -58.5q34 -39 56 -69.5t34.5 -56t17.5 -47t5 -43.5v-67q0 -30 -10.5 -54.5t-28.5 -42.5t-41 -28t-48 -10h-632q-28 0 -53 9.5t-44 26.5t-30.5 40.5t-11.5 52.5v79q0 25 5 47.5t17 47t33.5 52.5t54.5 62q27 29 55 58t51 53[m
 q26 27 51 53h376zM512 37q27 0 50.5 10t41 27.5t27.5 41.5t10 51t-10 50.5t-27.5 40.5t-41 27.5t-50.5 10.5t-51 -10.5t-41.5 -27.5t-27.5 -40.5t-10 -50.5t10 -51t27.5 -41.5t41.5 -27.5t51 -10zM1025 549v-65q0 -26 -19 -44.5t-45 -18.5h-128q-27 0 -45.5 18.5t-18.5 44.5[m
 v65h-513v-65q0 -26 -18.5 -44.5t-44.5 -18.5h-128q-27 0 -46 18.5t-19 44.5v65v2q0 12 6 21t15.5 16t20.5 12.5t23 12.5q11 6 50.5 27t99 44t136 40t162.5 17q98 0 175.5 -17t134.5 -40t91 -44t47 -27q12 -7 23 -12.5t20.5 -12.5t15 -16t5.5 -21v-2z" />[m
[31m-    <glyph glyph-name="uniE618" unicode="&#xe618;" [m
[32m+[m[32m    <glyph glyph-name="uF01FF" unicode="&#xf01ff;"[m[41m [m
 d="M455 -51l-1 343l455 -2v-410zM454 669l455 68v-379h-455v311zM66 293l325 -1v-337l-325 52v286zM66 358v252l325 52v-304h-325z" />[m
[31m-    <glyph glyph-name="uniE619" unicode="&#xe619;" [m
[32m+[m[32m    <glyph glyph-name="uF0200" unicode="&#xf0200;"[m[41m [m
 d="M837 136q42 -59 96 -77q-24 -72 -75 -151q-79 -119 -156 -119q-28 0 -85 19q-52 20 -91 20t-86 -21q-49 -19 -81 -19q-92 0 -181 157q-89 154 -89 304q0 138 68 226q69 88 172 88q22 0 50 -5.5t57 -20.5q32 -18 52.5 -24.5t31.5 -6.5q14 0 41.5 6t54.5 22q30 17 51.5 25[m
 t44.5 8q71 0 129 -39q30 -20 62 -60q-47 -41 -68 -72q-40 -57 -40 -125q0 -74 42 -135zM631 614q-36 -33 -66 -44q-10 -3 -26 -5.5t-36 -4.5q1 90 47 155.5t151 90.5q2 -10 4 -14v-12q0 -37 -18 -83q-18 -45 -56 -83z" />[m
[31m-    <glyph glyph-name="uniE61A" unicode="&#xe61a;" [m
[32m+[m[32m    <glyph glyph-name="uF0279" unicode="&#xf0279;"[m[41m [m
 d="M891 -148h-691q-57 0 -97 40.5t-40 96.5v622q0 56 40 96.5t97 40.5h691q56 0 94.5 -40t38.5 -97v-622q0 -57 -38.5 -97t-94.5 -40zM319 620q-3 -11 -15.5 -54t-17.5 -58.5t-17 -51.5t-21.5 -56.5t-24.5 -48.5t-32 -52q3 -10 3 -26t-2 -27l-1 -11q1 0 2 1t4.5 4t7 5.5[m
 t8.5 7t9.5 8.5t9.5 9.5t9 10t8 9.5t6 9v-320h64v448q23 47 74 191zM682 609l-43 -39q10 -8 23.5 -19t23 -19t20.5 -17.5t20 -18t17 -16.5q6 7 10.5 22t8 30t6.5 19q-10 10 -31.5 24t-38.5 24zM832 136q-8 2 -31 15.5t-33 13.5v-121h-71q-31 0 -45 19.5t-13 43.5v257h193v63[m
 h-256q1 22 1 52t-0.5 72.5t-0.5 68.5h-65q0 -4 0.5 -49.5t0.5 -87t-1 -56.5h-128v-63h128q0 -8 -1.5 -23t-10 -56.5t-21 -79.5t-39 -83.5t-60.5 -77.5q4 -4 4 -65q3 1 8.5 4t21.5 14t30.5 24.5t32.5 37t32 51.5q21 42 38 105t23 106l7 43v-257q0 -51 19.5 -89.5t60.5 -38.5[m
 h112q41 0 52.5 15.5t11.5 49.5q0 7 -0.5 24t-0.5 28.5t1 39.5z" />[m
[31m-    <glyph glyph-name="uniE61B" unicode="&#xe61b;" horiz-adv-x="1025" [m
[32m+[m[32m    <glyph glyph-name="uF0280" unicode="&#xf0280;" horiz-adv-x="1025"[m[41m [m
 d="M512.5 812q-104.5 0 -199 -40.5t-163.5 -109t-109.5 -163.5t-40.5 -199t40.5 -199t109.5 -163.5t163.5 -109t199 -40.5t199 40.5t163 109t109 163.5t40.5 199t-40.5 199t-109 163.5t-163 109t-199 40.5zM762 319v-100h-200v-48h200v-100h-200v-102h-100v102h-199v100h199[m
 v48h-199v100h187l-208 208l71 71l199 -200l200 200l71 -71l-208 -208h187z" />[m
[31m-    <glyph glyph-name="uniE61C" unicode="&#xe61c;" [m
[32m+[m[32m    <glyph glyph-name="uF02A9" unicode="&#xf02a9;"[m[41m [m
 d="M186 484l-40 -39l366 -366l366 366l-40 39l-326 -326z" />[m
[31m-    <glyph glyph-name="uniE61D" unicode="&#xe61d;" [m
[32m+[m[32m    <glyph glyph-name="uF02AA" unicode="&#xf02aa;"[m[41m [m
 d="M838 79l40 40l-366 365l-366 -365l40 -40l326 326z" />[m
[31m-    <glyph glyph-name="uniE61E" unicode="&#xe61e;" horiz-adv-x="1025" [m
[32m+[m[32m    <glyph glyph-name="uF02B2" unicode="&#xf02b2;" horiz-adv-x="1025"[m[41m [m
 d="M891 748h-691q-56 0 -96.5 -40.5t-40.5 -96.5v-623q0 -28 11 -53t29.5 -43.5t44 -29.5t52.5 -11h691q57 0 95.5 40t38.5 97v623q0 57 -38.5 97t-95.5 40zM318 -14l-66 -1l13 124l60 -1zM359 144q-8 -4 -33.5 -2.5t-30.5 0.5q-2 12 -7.5 21.5t-16 22t-13.5 16.5[m
 q19 -1 32.5 0t21 9t7.5 24v64q-19 -5 -28.5 -8.5t-20.5 -9t-20 -8.5q-6 38 -19 68q22 3 35.5 6.5t28.5 8t24 7.5v64h-15q-7 0 -11 0.5t-7 0.5h-6h-4h-6q-2 0 -5.5 -0.5t-9.5 -0.5v64h64v129h65v-129q8 1 20.5 0.5t20.5 -1t23 0.5v-64h-64v-56l64 16v-64q-64 -8 -64 -16v-72[m
 l1 -64q-5 -20 -26 -27zM439 -18q1 19 5 61t2 63h66q-3 -45 1 -123zM638 -16q-1 21 -3 60.5t-2 63.5h65l9 -121zM829 -13q-4 15 -7.5 33t-6 33t-4.5 27.5t-2 19.5l-1 8l60 2l46 -125zM852 146q-16 0 -33 10.5t-35 30.5q-6 9 -11 23t-7.5 30t-4 34t-1 36t1 35t1.5 32t2.5 26[m
 t2 18t0.5 6h-64q-10 -44 -16 -63.5t-20 -47.5q5 -4 22.5 -21.5t36.5 -32.5l-47 -49q-10 12 -49 47q-57 -81 -120 -124q-29 24 -64 37q86 45 141 126q-9 7 -24 19t-25.5 20t-16.5 12l37 41q3 -2 16 -11t25.5 -17.5t19.5 -13.5q16 32 20 78h-33h-35q-15 0 -32.5 0.5[m
 t-27.5 -0.5v64h62h73q1 89 -2 129q10 -2 26.5 -2t28.5 1l11 1v-57l-7 -72q109 2 129 0q-2 -46 -5 -109.5t-2.5 -72.5t2.5 -64q0 -8 1.5 -14t3.5 -9.5t4 -5t5 -1.5q13 0 27 69q26 -33 57 -37q-28 -101 -74 -101z" />[m
[31m-    <glyph glyph-name="uniE61F" unicode="&#xe61f;" horiz-adv-x="1108" [m
[32m+[m[32m    <glyph glyph-name="uF02B6" unicode="&#xf02b6;" horiz-adv-x="1108"[m[41m [m
 d="M596 300h-9h-22h-31h-34h-32v64h20h20h20q10 0 18.5 0.5t16.5 0.5h14h11h6h3v61q-9 -1 -24 -2t-29 -1.5t-27.5 -1t-21.5 -0.5l-8 -1l69 73l-67 33l-21 -98h-256v-64q28 2 49.5 2t68.5 -1t74 -1v-64h-128v-64h4h10h16h21h24h26h27v-128q0 -7 0.5 -18t0.5 -16.5t-1 -13[m
 t-5.5 -11t-11.5 -6.5t-18.5 -2.5t-28.5 3.5q7 -14 7 -20.5t-4 -20t-3 -23.5q11 2 21.5 2t23 -1t19.5 -1q64 0 64 64v192h128v64zM302 505l33 -76l62 22q-28 53 -37 76zM596 556v64h-128v64h-64v-64h-128v-64h320zM276 172h64v-128h-64v128zM532 172h64v-128h-64v128z[m
 M916 812h-704q-90 0 -147 -69q-45 -54 -45 -123v-640q0 -52 25.5 -96.5t70 -70t96.5 -25.5h704q79 0 135.5 56t56.5 136v640q0 52 -26 96.5t-70 70t-96 25.5zM1043 -10q0 -57 -38 -97t-95 -40h-689q-57 0 -97 40t-40 97v621q0 28 11 53.5t29 43.5t43.5 29t53.5 11h689[m
 q57 0 95 -40t38 -97v-621zM724 428v128q58 0 106 7t66 15l19 7l-50 76q-2 2 1 -4q-48 -36 -206 -37q2 -19 2 -52t-1 -77t-1 -63v-256q0 -22 -5.5 -47t-21 -50.5t-37.5 -30.5q14 -4 26.5 -16.5t22.5 -28t15 -19.5q25 37 44.5 93.5t19.5 98.5v24q0 13 -0.5 24t-0.5 24v25v27[m
 q0 15 0.5 32t0.5 36h64v-384h64v384q16 -1 25 -0.5t18.5 0t20.5 -3.5v72q-31 1 -73.5 0t-80.5 -2z" />[m
[31m-    <glyph glyph-name="uniE620" unicode="&#xe620;" horiz-adv-x="1108" [m
[32m+[m[32m    <glyph glyph-name="uF02BA" unicode="&#xf02ba;" horiz-adv-x="1108"[m[41m [m
 d="M659 111q-1 -15 -2 -40t-1 -36t1 -41l69 2l-2 114zM468 323v64q-44 -10 -64 -15v56h64v64q-10 -1 -33.5 -0.5t-30.5 0.5v128h-64v-128q-14 0 -32.5 -1t-31.5 1v-64q7 3 25 1.5t39 -1.5v-64q-12 -2 -22.5 -5.5t-17 -6t-19.5 -5.5t-29 -5q4 -10 7.5 -21t6 -23t4.5 -24l14 6[m
 q7 3 11 5t10 4.5t14 5t21 5.5v-64q0 -12 -4.5 -19.5t-12.5 -10.5t-19.5 -3.5t-25.5 0.5q3 -4 13.5 -16.5t16 -22t7.5 -21.5q5 1 30.5 0t33.5 2l8 4q4 2 6.5 4.5t4.5 5.5t3.5 5t2 4t0.5 3l1 1l-1 64v72q0 3 16 7t32 6zM272 -14l66 -3l7 126l-60 2zM463 108q3 -26 2.5 -39[m
 t-3.5 -41.5t-3 -46.5l75 3l-6 123zM915 812h-703q-90 0 -148 -69q-44 -54 -44 -123v-640q0 -80 56 -136t136 -56h703q80 0 136 56t56 136v640q0 79 -56 135.5t-136 56.5zM1043 -10q0 -57 -38 -97t-95 -40h-689q-57 0 -97 40t-40 97v621q0 57 40 97t97 40h689q57 0 95 -40[m
 t38 -97v-621zM827 109q2 -67 21 -121l84 -1l-45 124zM888 285q-7 -34 -14 -51.5t-13 -17.5q-12 0 -14 30q-2 55 -2.5 64t2.5 72.5t4 109.5q-19 2 -128 0q5 48 5 80t-2 40l-3 8q-42 -5 -64 0q2 -34 2 -50t-2 -78q-24 -1 -69 -1t-58 1v-64q9 2 55.5 0t71.5 0q-2 -29 -6 -44.5[m
 t-14 -30.5q-10 8 -33.5 22t-27.5 17l-37 -41q22 -14 60 -43q-41 -84 -134 -134q17 -6 33 -15.5t30 -21.5q63 43 120 124q5 -4 14 -11.5t13 -11.5t10 -10.5t12 -12.5l47 48q-17 14 -35 31.5t-27 24.5q16 25 22.5 45t16.5 64h64l-7 -87q-4 -116 23 -153q36 -40 68 -40[m
 q46 0 74 100q-31 4 -57 37z" />[m
[31m-    <glyph glyph-name="uniE621" unicode="&#xe621;" [m
[32m+[m[32m    <glyph glyph-name="uF02D4" unicode="&#xf02d4;"[m[41m [m
 d="M512 96q-30 0 -58.5 6.5t-52.5 17.5t-46 25.5t-39.5 30.5t-32.5 32t-25.5 30.5t-18.5 25.5t-11 17l-4 7q4 8 12.5 21t36.5 46t59 58t80 46t100 21t99.5 -20t81 -48t58 -56t37.5 -48l12 -20q-4 -8 -12.5 -21t-36 -46t-58.5 -58t-80.5 -46t-100.5 -21zM512 416[m
 q-37 0 -72 -13t-58.5 -32t-42 -38t-27.5 -32l-8 -13q8 -14 24 -35.5t70.5 -57t113.5 -35.5q37 0 72.5 13t59 32t41.5 38t27 32l8 13q-3 5 -9 13.5t-26 31t-42.5 39t-58 30.5t-72.5 14zM512 224q-27 0 -45.5 18.5t-18.5 45.5q0 9 2 17t6.5 15t10 13t13 10t15.5 6.5t17 2.5[m
 q13 0 25 -5t20.5 -13.5t13.5 -20.5t5 -25t-5 -25t-13.5 -20.5t-20.5 -13.5t-25 -5z" />[m
[31m-    <glyph glyph-name="uniE622" unicode="&#xe622;" [m
[31m-d="M512 603q-79 0 -161 -30t-142.5 -72.5t-109 -87.5t-74 -75.5t-25.5 -36t25.5 -36.5t74 -75.5t109 -87t142.5 -72t161 -29.5t161 29.5t142 72t109 87t74.5 75.5t25.5 36q0 10 -51 65q-135 144 -290 204q-88 33 -171 33zM512 144q-42 0 -84.5 15.5t-74 37.5t-57 45.5[m
[31m-t-39 39.5t-13.5 19t13.5 19t39 39.5t57 45.5t74 38t84 16t84.5 -16t74.5 -38t57 -45.5t39 -39.5t13.5 -19q0 -8 -39 -46q-108 -107 -221 -111h-8zM512 397q-25 0 -51 -9.5t-45 -23t-34.5 -28t-23.5 -24t-8 -11.5q0 -1 4 -6.5t12 -14t19 -18.5t25.5 -20t30 -18t34.5 -13[m
[31m-t37 -5q25 0 51 9.5t45 23t34.5 27.5t23.5 23.5t8 11.5t-8 11.5t-23.5 24t-34.5 28t-45 23t-51 9.5zM512 248q-5 0 -9.5 1t-8.5 2t-8 3t-7.5 4.5t-6.5 5.5l-6 6q-3 3 -5 7t-3 7.5t-2 8t-1 8.5q0 11 4.5 21t12 17t18 11t22.5 4q24 0 40.5 -15.5t16.5 -37.5q0 -8 -3 -16.5[m
[31m-t-8 -15t-12 -11t-16 -7.5t-18 -3zM512 248z" />[m
   </font>[m
 </defs></svg>[m
[1mdiff --git a/BaWuClub.Web/Content/Fonts/iconfont.ttf b/BaWuClub.Web/Content/Fonts/iconfont.ttf[m
[1mindex 15918e3..39ebad4 100644[m
Binary files a/BaWuClub.Web/Content/Fonts/iconfont.ttf and b/BaWuClub.Web/Content/Fonts/iconfont.ttf differ
[1mdiff --git a/BaWuClub.Web/Content/Fonts/iconfont.woff b/BaWuClub.Web/Content/Fonts/iconfont.woff[m
[1mindex 2d7bd18..0bcd4d7 100644[m
Binary files a/BaWuClub.Web/Content/Fonts/iconfont.woff and b/BaWuClub.Web/Content/Fonts/iconfont.woff differ
[1mdiff --git a/BaWuClub.Web/Content/Fonts/info.txt b/BaWuClub.Web/Content/Fonts/info.txt[m
[1mnew file mode 100644[m
[1mindex 0000000..2b77094[m
[1m--- /dev/null[m
[1m+++ b/BaWuClub.Web/Content/Fonts/info.txt[m
[36m@@ -0,0 +1,2 @@[m
[32m+[m[32mfont->web font[m[41m[m
[32m+[m[32miconfont->web font1[m
\ No newline at end of file[m
[1mdiff --git a/BaWuClub.Web/Controllers/ForumController.cs b/BaWuClub.Web/Controllers/ForumController.cs[m
[1mindex fd0bdc2..9171e09 100644[m
[1m--- a/BaWuClub.Web/Controllers/ForumController.cs[m
[1m+++ b/BaWuClub.Web/Controllers/ForumController.cs[m
[36m@@ -3,28 +3,18 @@[m
 using System.Linq;[m
 using System.Web;[m
 using System.Web.Mvc;[m
[31m-using BaWuClub.Web.Dal;[m
[31m-using BaWuClub.Web.Common;[m
 [m
 namespace BaWuClub.Web.Controllers[m
 {[m
     public class ForumController : BaseController[m
     {[m
[31m-        private ClubEntities club;[m
[32m+[m[32m        //[m[41m[m
[32m+[m[32m        // GET: /Bbs/[m[41m[m
 [m
         public ActionResult Index()[m
         {[m
[31m-            List<TopicCategory> categories = GetDiscussCategory();[m
[31m-            ViewBag.Categories = categories;[m
             return View();[m
         }[m
[31m-        [m
[31m-        private List<TopicCategory> GetDiscussCategory() {[m
[31m-            List<TopicCategory> categories = new List<TopicCategory>();[m
[31m-            using (club = new ClubEntities()) {[m
[31m-                categories = club.TopicCategories.Where(t=>t.Type==1).ToList<TopicCategory>();[m
[31m-            }[m
[31m-            return categories; [m
[31m-        }[m
[32m+[m[41m[m
     }[m
 }[m
[1mdiff --git a/BaWuClub.Web/Controllers/MemberController.cs b/BaWuClub.Web/Controllers/MemberController.cs[m
[1mindex 8c4ded2..fd7887a 100644[m
[1m--- a/BaWuClub.Web/Controllers/MemberController.cs[m
[1m+++ b/BaWuClub.Web/Controllers/MemberController.cs[m
[36m@@ -333,35 +333,6 @@[m [mpublic ActionResult Certification(int? uid)[m
         }[m
         #endregion[m
 [m
[31m-        #region 论坛[m
[31m-        public ActionResult Discuss(int? uid) {[m
[31m-            using (club = new ClubEntities())[m
[31m-            {[m
[31m-                user = GetUser(club, uid);[m
[31m-                if (user == null)[m
[31m-                    return Redirect("/error/notfound");[m
[31m-                ViewBag.User = user;[m
[31m-            }[m
[31m-            ViewBag.User = user;[m
[31m-            return View("~/views/member/discuss.cshtml");[m
[31m-        }[m
[31m-[m
[31m-        #endregion[m
[31m-[m
[31m-        #region 信息中心[m
[31m-        public ActionResult Message(int? uid) {[m
[31m-            using (club = new ClubEntities())[m
[31m-            {[m
[31m-                user = GetUser(club, uid);[m
[31m-                if (user == null)[m
[31m-                    return Redirect("/error/notfound");[m
[31m-                ViewBag.User = user;[m
[31m-            }[m
[31m-            ViewBag.User = user;[m
[31m-            return View("~/views/member/message.cshtml");[m
[31m-        }[m
[31m-        #endregion[m
[31m-[m
         [ChildActionOnly][m
         public ActionResult MemberAvatarWrap(int? uid,string action) {[m
             int useId = uid ?? 0;[m
[1mdiff --git a/BaWuClub.Web/Views/Forum/Index.cshtml b/BaWuClub.Web/Views/Forum/Index.cshtml[m
[1mindex ec5b73e..26b2c47 100644[m
[1m--- a/BaWuClub.Web/Views/Forum/Index.cshtml[m
[1m+++ b/BaWuClub.Web/Views/Forum/Index.cshtml[m
[36m@@ -1,225 +1,7 @@[m
 ﻿@{[m
     Layout = "~/views/shared/_layout.cshtml";[m
     ViewBag.Title = "电商圈-";[m
[31m-    ViewBag.CssAppend = "<link href=\"/content/fonts/category.css\" type=\"text/css\" rel=\"stylesheet\"/>";[m
 }[m
[31m-<style>[m
[31m-    .discuss-category {}[m
[31m-    .discuss-wrap{float:left;overflow:hidden;width:670px;}[m
[31m-    .discuss-title{font-size:14px;padding:10px 0;}[m
[31m-    .discuss-title i{background:url(/Content/Images/discuss.png);width:17px;height:15px;display:inline-block; margin-right:5px;margin-top:1px;}[m
[31m-    .discuss-title span{margin-right:28px}[m
[31m-    .discuss-category{overflow:hidden;margin:20px 0;}[m
[31m-    .discuss-category ul li{width:160px;float:left;margin:7px 0;padding-left: 6px;}[m
[31m-    .discuss-category ul li i{font-size:18px !important;margin-right:5px;color:#ff9000}[m
[31m-    .discuss-category ul li a{font-size:14px;}[m
[31m-    .function-wrap{width:270px;float:right;}[m
[31m-    .area-wrap{clear:both;overflow:hidden;margin:30px 0 20px 0;}[m
[31m-    .discuss-list-title{background:#f5f5f5;height:35px;line-height:35px;width:100%;}[m
[31m-    .discuss-list-title h3{font-weight:400;font-size:16px;margin:0px 18px;float:left;}[m
[31m-    .discuss-list-title a{float:right;font-size:12px;color:#007ada;margin-right:15px;}[m
[31m-    .discuss-list-topic{padding:10px 0;border-bottom:1px dotted #d2d2d2;width:670px;}[m
[31m-    .discuss-list-topic-avatar{float:left;width:40px;height:40px;overflow:hidden}[m
[31m-    .discuss-list-topic-avatar img{width:40px;}[m
[31m-    .discuss-list-topic-titles{margin-left:50px;}[m
[31m-    .topic-title a{color:#336699;font-size:14px;}[m
[31m-    .topic-info{margin:3px 0;}[m
[31m-    .topic-info,  .topic-info a{color:#a0a0a0;font-size:12px;}[m
[31m-    .topic-info span{margin-left:15px;}[m
[31m-    .topic-info span i{font-size:15px;margin-right:2px;}[m
[31m-    .area-title{position:relative;height:285px;margin-right:175px;float:left;}[m
[31m-    .area-title img{position:absolute;top:0;left:0;}[m
[31m-    .area-title h4{position:absolute;top:15px;left:0;color:#fff;background:#ff9000;font-weight:200;width:92px;padding-left:8px;height:40px;line-height:40px;}[m
[31m-    .area-title p{width:175px;height:50px;background:#ff9000;position:absolute;top:220px;text-align:center;color:#fff;font-size:14px;padding-top:15px;}[m
[31m-    .area-title p a{color:#fff;}[m
[31m-    .area-list-item{border:1px solid #e8e8e8;border-left:none;float:left;height:283px;width:274px;}[m
[31m-    .area-item-logo{float:left;margin-left:30px;margin-top:90px;margin-right:25px;}[m
[31m-    .area-item-info{margin-top:90px;}[m
[31m-    .area-item-info h3{font-size:24px;font-weight:400;}[m
[31m-    .area-item-info p{color:#918c8c;font-size:14px;line-height:30px;}[m
[31m-    .area-item-info a{color:#70aaf1;font-size:14px;}[m
[31m-</style>[m
[31m-<div id="main" class="container">[m
[31m-    <div class="discuss-wrap">[m
[31m-        <div class="discuss-title">[m
[31m-            <i></i><span>主题：18135</span><span>昨日：2291</span><span>今日：283</span>[m
[31m-        </div>[m
[31m-        <div class="discuss-category">[m
[31m-            <ul>[m
[31m-                @{[m
[31m-                    var categories=ViewBag.Categories as IEnumerable<BaWuClub.Web.Dal.TopicCategory>;[m
[31m-                       if(categories!=null){[m
[31m-                           foreach (var category in categories) {[m
[31m-                             <li>[m
[31m-                                <i class="font-category @category.Icon"></i><a>@category.Name (2)</a>[m
[31m-                            </li>[m
[31m-                           }[m
[31m-                        }[m
[31m-                }[m
[31m-            </ul>[m
[31m-        </div>[m
[31m-        <div class="discuss-list-wrap">[m
[31m-            <div class="discuss-list-title">[m
[31m-                <h3>最新热帖</h3><a href="#">查看更多 . . .</a>[m
[31m-            </div>[m
[31m-            <div class="discuss-list">[m
[31m-                <div class="discuss-list-topic">[m
[31m-                    <div class="discuss-list-topic-avatar">[m
[31m-                        <a href="#"><img src="~/Content/Images/no-img.jpg" alt="" /></a>[m
[31m-                    </div>[m
[31m-                    <div class="discuss-list-topic-titles">[m
[31m-                        <div class="topic-title"><a href="#">[咨询]蔡崇信亲自出马 阿里体育在下一盘什么棋</a></div>[m
[31m-                        <div class="topic-info">[m
[31m-                            <a href="#">布袋和尚</a>[m
[31m-                            <span class="discuss-list-topic-date">9小时前</span>[m
[31m-                            <span><i class="icon1-liulan"></i>280</span>[m
[31m-                            <span><i class="icon1-xiai" style="font-size:12px;"></i>2</span>[m
[31m-                        </div>[m
[31m-                    </div>[m
[31m-                </div>[m
[31m-                <div class="discuss-list-topic">[m
[31m-                    <div class="discuss-list-topic-avatar">[m
[31m-                        <a href="#"><img src="~/Content/Images/no-img.jpg" alt="" /></a>[m
[31m-                    </div>[m
[31m-                    <div class="discuss-list-topic-titles">[m
[31m-                        <div class="topic-title"><a href="#">[任务]想刷单，有可以帮我的么？</a></div>[m
[31m-                        <div class="topic-info">[m
[31m-                            <a href="#">布袋和尚</a>[m
[31m-                            <span class="discuss-list-topic-date">9小时前</span>[m
[31m-                            <span><i class="icon1-liulan"></i>280</span>[m
[31m-                            <span><i class="icon1-xiai" style="font-size:12px;"></i>2</span>[m
[31m-                        </div>[m
[31m-                    </div>[m
[31m-                </div>[m
[31m-                <div class="discuss-list-topic">[m
[31m-                    <div class="discuss-list-topic-avatar">[m
[31m-                        <a href="#"><img src="~/Content/Images/no-img.jpg" alt="" /></a>[m
[31m-                    </div>[m
[31m-                    <div class="discuss-list-topic-titles">[m
[31m-                        <div class="topic-title"><a href="#">[热帖]论双11打造爆款，木木老师给你揭秘如何打造爆款的秘密，另神秘嘉宾参加哦</a></div>[m
[31m-                        <div class="topic-info">[m
[31m-                            <a href="#">布袋和尚</a>[m
[31m-                            <span class="discuss-list-topic-date">9小时前</span>[m
[31m-                            <span><i class="icon1-liulan"></i>280</span>[m
[31m-                            <span><i class="icon1-xiai" style="font-size:12px;"></i>2</span>[m
[31m-                        </div>[m
[31m-                    </div>[m
[31m-                </div>[m
[31m-                <div class="discuss-list-topic">[m
[31m-                    <div class="discuss-list-topic-avatar">[m
[31m-                        <a href="#"><img src="~/Content/Images/no-img.jpg" alt="" /></a>[m
[31m-                    </div>[m
[31m-                    <div class="discuss-list-topic-titles">[m
[31m-                        <div class="topic-title"><a href="#">[活动]本周有小二的来分享干活，有谁来参加，赶快来报名吧。。。。。？</a></div>[m
[31m-                        <div class="topic-info">[m
[31m-                            <a href="#">布袋和尚</a>[m
[31m-                            <span class="discuss-list-topic-date">9小时前</span>[m
[31m-                            <span><i class="icon1-liulan"></i>280</span>[m
[31m-                            <span><i class="icon1-xiai" style="font-size:12px;"></i>2</span>[m
[31m-                        </div>[m
[31m-                    </div>[m
[31m-                </div>[m
[31m-                <div class="discuss-list-topic">[m
[31m-                    <div class="discuss-list-topic-avatar">[m
[31m-                        <a href="#"><img src="~/Content/Images/no-img.jpg" alt="" /></a>[m
[31m-                    </div>[m
[31m-                    <div class="discuss-list-topic-titles">[m
[31m-                        <div class="topic-title"><a href="#">[任务]想刷单，有可以帮我的么？</a></div>[m
[31m-                        <div class="topic-info">[m
[31m-                            <a href="#">布袋和尚</a>[m
[31m-                            <span class="discuss-list-topic-date">9小时前</span>[m
[31m-                            <span><i class="icon1-liulan"></i>280</span>[m
[31m-                            <span><i class="icon1-xiai" style="font-size:12px;"></i>2</span>[m
[31m-                        </div>[m
[31m-                    </div>[m
[31m-                </div>[m
[31m-                <div class="discuss-list-topic">[m
[31m-                    <div class="discuss-list-topic-avatar">[m
[31m-                        <a href="#"><img src="~/Content/Images/no-img.jpg" alt="" /></a>[m
[31m-                    </div>[m
[31m-                    <div class="discuss-list-topic-titles">[m
[31m-                        <div class="topic-title"><a href="#">[任务]顾家家居价格优势怎么来的？</a></div>[m
[31m-                        <div class="topic-info">[m
[31m-                            <a href="#">布袋和尚</a>[m
[31m-                            <span class="discuss-list-topic-date">9小时前</span>[m
[31m-                            <span><i class="icon1-liulan"></i>280</span>[m
[31m-                            <span><i class="icon1-xiai" style="font-size:12px;"></i>2</span>[m
[31m-                        </div>[m
[31m-                    </div>[m
[31m-                </div>[m
[31m-                <div class="discuss-list-topic">[m
[31m-                    <div class="discuss-list-topic-avatar">[m
[31m-                        <a href="#"><img src="~/Content/Images/no-img.jpg" alt="" /></a>[m
[31m-                    </div>[m
[31m-                    <div class="discuss-list-topic-titles">[m
[31m-                        <div class="topic-title"><a href="#">[任务]第六届浙交会要来了 今年主题跨境电商与服务</a></div>[m
[31m-                        <div class="topic-info">[m
[31m-                            <a href="#">布袋和尚</a>[m
[31m-                            <span class="discuss-list-topic-date">9小时前</span>[m
[31m-                            <span><i class="icon1-liulan"></i>280</span>[m
[31m-                            <span><i class="icon1-xiai" style="font-size:12px;"></i>2</span>[m
[31m-                        </div>[m
[31m-                    </div>[m
[31m-                </div>[m
[31m-                <div class="discuss-list-topic">[m
[31m-                    <div class="discuss-list-topic-avatar">[m
[31m-                        <a href="#"><img src="~/Content/Images/no-img.jpg" alt="" /></a>[m
[31m-                    </div>[m
[31m-                    <div class="discuss-list-topic-titles">[m
[31m-                        <div class="topic-title"><a href="#">[任务]第六届浙交会要来了 今年主题跨境电商与服务</a></div>[m
[31m-                        <div class="topic-info">[m
[31m-                            <a href="#">布袋和尚</a>[m
[31m-                            <span class="discuss-list-topic-date">9小时前</span>[m
[31m-                            <span><i class="icon1-liulan"></i>280</span>[m
[31m-                            <span><i class="icon1-xiai" style="font-size:12px;"></i>2</span>[m
[31m-                        </div>[m
[31m-                    </div>[m
[31m-                </div>[m
[31m-            </div>[m
[31m-        </div>[m
[31m-    </div>[m
[31m-    <div class="function-wrap">[m
[31m-        <div class="function-rw">[m
[31m-            <a href="#"><img src="~/Content/Images/rw.jpg" /></a>[m
[31m-        </div>[m
[31m-    </div>[m
[31m-    <div class="area-wrap">[m
[31m-        <div class="area-title">[m
[31m-            <img src="~/Content/Images/area-1.jpg" alt="" />[m
[31m-            <h4>区域板块</h4>[m
[31m-            <p>申请入住<br /><a href="#">等你来组织></a></p>[m
[31m-        </div>[m
[31m-        <div class="area-list">[m
[31m-            <div class="area-list-item">[m
[31m-                <div class="area-item-logo">[m
[31m-                    <img src="~/Content/Images/area-logo.png" alt="区域图标" />[m
[31m-                </div>[m
[31m-                <div class="area-item-info">[m
[31m-                    <h3>南京圈</h3>[m
[31m-                    <p>区域论坛介绍</p>[m
[31m-                    <a href="#">点击进入</a>[m
[31m-                </div>[m
[31m-            </div>[m
[31m-            <div class="area-list-item">[m
[31m-                <div class="area-item-logo">[m
[31m-                    <img src="~/Content/Images/area-logo.png" alt="区域图标" />[m
[31m-                </div>[m
[31m-                <div class="area-item-info">[m
[31m-                    <h3>海门圈</h3>[m
[31m-                    <p>区域论坛介绍</p>[m
[31m-                    <a href="#">点击进入</a>[m
[31m-                </div>[m
[31m-            </div>[m
[31m-            <div class="area-list-item">[m
[31m-                <div class="area-item-logo">[m
[31m-                    <img src="~/Content/Images/area-logo.png" alt="区域图标" />[m
[31m-                </div>[m
[31m-                <div class="area-item-info">[m
[31m-                    <h3>杭州圈</h3>[m
[31m-                    <p>区域论坛介绍</p>[m
[31m-                    <a href="#">点击进入</a>[m
[31m-                </div>[m
[31m-            </div>[m
[31m-        </div>[m
[31m-    </div>[m
[31m-</div>[m
[32m+[m[32m<div style="text-align:center;font-family:'Segoe UI';font-size:70px;color:#999;margin:250px 0;">[m[41m[m
[32m+[m[32m    Coming Soon[m[41m[m
[32m+[m[32m</div>[m
\ No newline at end of file[m
[1mdiff --git a/BaWuClub.Web/Views/Member/Answers.cshtml b/BaWuClub.Web/Views/Member/Answers.cshtml[m
[1mindex 1c28275..9eb8ed8 100644[m
[1m--- a/BaWuClub.Web/Views/Member/Answers.cshtml[m
[1m+++ b/BaWuClub.Web/Views/Member/Answers.cshtml[m
[36m@@ -12,8 +12,6 @@[m
     <a href="/member/u-@user.Id/answers" class="selected">回答</a>[m
     <a href="/member/u-@user.Id/certification">认证</a>[m
     <a href="/member/u-@user.Id/shared">共享</a>[m
[31m-    <a href="/member/u-@user.Id/discuss">论坛</a>[m
[31m-    <a href="/member/u-@user.Id/message">私信</a>[m
 </div>[m
 <div class="member-main">[m
     <div class="member-main-wrap">[m
[1mdiff --git a/BaWuClub.Web/Views/Member/Ask.cshtml b/BaWuClub.Web/Views/Member/Ask.cshtml[m
[1mindex e8cc911..654f3ff 100644[m
[1m--- a/BaWuClub.Web/Views/Member/Ask.cshtml[m
[1m+++ b/BaWuClub.Web/Views/Member/Ask.cshtml[m
[36m@@ -10,8 +10,6 @@[m
     <a href="/member/u-@user.Id/answers">回答</a>[m
     <a href="/member/u-@user.Id/certification">认证</a>[m
     <a href="/member/u-@user.Id/shared">共享</a>[m
[31m-    <a href="/member/u-@user.Id/discuss">论坛</a>[m
[31m-    <a href="/member/u-@user.Id/message">私信</a>[m
 </div>[m
     <div class="member-main">[m
         <div class="member-ask">[m
[1mdiff --git a/BaWuClub.Web/Views/Member/Asks.cshtml b/BaWuClub.Web/Views/Member/Asks.cshtml[m
[1mindex 663e611..2cd7327 100644[m
[1m--- a/BaWuClub.Web/Views/Member/Asks.cshtml[m
[1m+++ b/BaWuClub.Web/Views/Member/Asks.cshtml[m
[36m@@ -11,8 +11,6 @@[m
     <a href="/member/u-@user.Id/answers">回答</a>[m
     <a href="/member/u-@user.Id/certification">认证</a>[m
     <a href="/member/u-@user.Id/shared">共享</a>[m
[31m-    <a href="/member/u-@user.Id/discuss">论坛</a>[m
[31m-    <a href="/member/u-@user.Id/message">私信</a>[m
 </div>[m
     <div class="member-main">[m
         <div class="member-main-wrap">[m
[1mdiff --git a/BaWuClub.Web/Views/Member/Certification.cshtml b/BaWuClub.Web/Views/Member/Certification.cshtml[m
[1mindex a235416..773da8b 100644[m
[1m--- a/BaWuClub.Web/Views/Member/Certification.cshtml[m
[1m+++ b/BaWuClub.Web/Views/Member/Certification.cshtml[m
[36m@@ -12,8 +12,6 @@[m
     <a href="/member/u-@user.Id/answers">回答</a>[m
     <a href="/member/u-@user.Id/certification" class="selected">认证</a>[m
     <a href="/member/u-@user.Id/shared">共享</a>[m
[31m-    <a href="/member/u-@user.Id/discuss">论坛</a>[m
[31m-    <a href="/member/u-@user.Id/message">私信</a>[m
 </div>[m
 <div class="member-main">[m
     <div class="member-main-wrap">[m
[1mdiff --git a/BaWuClub.Web/Views/Member/Contribute.cshtml b/BaWuClub.Web/Views/Member/Contribute.cshtml[m
[1mindex cc0e616..f4ab874 100644[m
[1m--- a/BaWuClub.Web/Views/Member/Contribute.cshtml[m
[1m+++ b/BaWuClub.Web/Views/Member/Contribute.cshtml[m
[36m@@ -10,8 +10,6 @@[m
     <a href="/member/u-@user.Id/answers">回答</a>[m
     <a href="/member/u-@user.Id/certification">认证</a>[m
     <a href="/member/u-@user.Id/shared">共享</a>[m
[31m-    <a href="/member/u-@user.Id/discuss">论坛</a>[m
[31m-    <a href="/member/u-@user.Id/message">私信</a>[m
 </div>[m
     <div class="member-main">[m
         <div class="member-contributions">[m
[1mdiff --git a/BaWuClub.Web/Views/Member/ContributeList.cshtml b/BaWuClub.Web/Views/Member/ContributeList.cshtml[m
[1mindex 99f6789..36d8bef 100644[m
[1m--- a/BaWuClub.Web/Views/Member/ContributeList.cshtml[m
[1m+++ b/BaWuClub.Web/Views/Member/ContributeList.cshtml[m
[36m@@ -11,8 +11,6 @@[m
     <a href="/member/u-@user.Id/answers">回答</a>[m
     <a href="/member/u-@user.Id/certification">认证</a>[m
     <a href="/member/u-@user.Id/shared">共享</a>[m
[31m-    <a href="/member/u-@user.Id/discuss">论坛</a>[m
[31m-    <a href="/member/u-@user.Id/message">私信</a>[m
 </div>[m
 <div class="member-main">[m
     <div class="member-contributions">[m
[1mdiff --git a/BaWuClub.Web/Views/Member/Discuss.cshtml b/BaWuClub.Web/Views/Member/Discuss.cshtml[m
[1mdeleted file mode 100644[m
[1mindex f1940f9..0000000[m
[1m--- a/BaWuClub.Web/Views/Member/Discuss.cshtml[m
[1m+++ /dev/null[m
[36m@@ -1,22 +0,0 @@[m
[31m-﻿@{[m
[31m-    Layout = "~/views/shared/_layoutmember.cshtml";[m
[31m-    ViewBag.Title = "个人用户中心-论坛";[m
[31m-    BaWuClub.Web.Dal.User user = (BaWuClub.Web.Dal.User)ViewBag.User;[m
[31m-}[m
[31m-@model IEnumerable<BaWuClub.Web.Dal.Article>[m
[31m-<div class="member-title">[m
[31m-    <a href="/member/u-@user.Id/Show">资料</a>[m
[31m-    <a href="/member/u-@user.Id/contributelist">投稿</a>[m
[31m-    <a href="/member/u-@user.Id/asks">问题</a>[m
[31m-    <a href="/member/u-@user.Id/answers">回答</a>[m
[31m-    <a href="/member/u-@user.Id/certification">认证</a>[m
[31m-    <a href="/member/u-@user.Id/shared">共享</a>[m
[31m-    <a href="/member/u-@user.Id/discuss" class="selected">论坛</a>[m
[31m-    <a href="/member/u-@user.Id/message">私信</a>[m
[31m-</div>[m
[31m-<div class="member-main">[m
[31m-    <div class="member-contributions">[m
[31m-    </div>[m
[31m-</div>[m
[31m-<script type="text/javascript" src="~/content/scripts/jquery.js"></script>[m
[31m-<script type="text/javascript" src="~/content/scripts/member.js"></script>[m
[1mdiff --git a/BaWuClub.Web/Views/Member/Index.cshtml b/BaWuClub.Web/Views/Member/Index.cshtml[m
[1mindex e31de15..e96db08 100644[m
[1m--- a/BaWuClub.Web/Views/Member/Index.cshtml[m
[1m+++ b/BaWuClub.Web/Views/Member/Index.cshtml[m
[36m@@ -11,7 +11,6 @@[m
     <a href="/member/u-@user.Id/answers">回答</a>[m
     <a href="/member/u-@user.Id/certification">认证</a>[m
     <a href="/member/u-@user.Id/shared">共享</a>[m
[31m-    <a href="/member/u-@user.Id/discuss">论坛</a>[m
 </div>[m
 @{    [m
     if (User.Identity.IsAuthenticated&&Request.Cookies["bwusers"]["id"].ToString().Equals(user.Id.ToString())){[m
[1mdiff --git a/BaWuClub.Web/Views/Member/Message.cshtml b/BaWuClub.Web/Views/Member/Message.cshtml[m
[1mdeleted file mode 100644[m
[1mindex 8f71c6d..0000000[m
[1m--- a/BaWuClub.Web/Views/Member/Message.cshtml[m
[1m+++ /dev/null[m
[36m@@ -1,22 +0,0 @@[m
[31m-﻿@{[m
[31m-    Layout = "~/views/shared/_layoutmember.cshtml";[m
[31m-    ViewBag.Title = "个人用户中心-信息中心";[m
[31m-    BaWuClub.Web.Dal.User user = (BaWuClub.Web.Dal.User)ViewBag.User;[m
[31m-}[m
[31m-@model IEnumerable<BaWuClub.Web.Dal.Article>[m
[31m-<div class="member-title">[m
[31m-    <a href="/member/u-@user.Id/Show">资料</a>[m
[31m-    <a href="/member/u-@user.Id/contributelist">投稿</a>[m
[31m-    <a href="/member/u-@user.Id/asks">问题</a>[m
[31m-    <a href="/member/u-@user.Id/answers">回答</a>[m
[31m-    <a href="/member/u-@user.Id/certification">认证</a>[m
[31m-    <a href="/member/u-@user.Id/shared">共享</a>[m
[31m-    <a href="/member/u-@user.Id/discuss">论坛</a>[m
[31m-    <a href="/member/u-@user.Id/message" class="selected">私信</a>[m
[31m-</div>[m
[31m-<div class="member-main">[m
[31m-    <div class="member-contributions">[m
[31m-    </div>[m
[31m-</div>[m
[31m-<script type="text/javascript" src="~/content/scripts/jquery.js"></script>[m
[31m-<script type="text/javascript" src="~/content/scripts/member.js"></script>[m
[1mdiff --git a/BaWuClub.Web/Views/Member/Shared.cshtml b/BaWuClub.Web/Views/Member/Shared.cshtml[m
[1mindex 2580108..22b568b 100644[m
[1m--- a/BaWuClub.Web/Views/Member/Shared.cshtml[m
[1m+++ b/BaWuClub.Web/Views/Member/Shared.cshtml[m
[36m@@ -11,8 +11,6 @@[m
     <a href="/member/u-@user.Id/answers">回答</a>[m
     <a href="/member/u-@user.Id/certification">认证</a>[m
     <a href="/member/u-@user.Id/shared" class="selected">共享</a>[m
[31m-    <a href="/member/u-@user.Id/discuss">论坛</a>[m
[31m-    <a href="/member/u-@user.Id/message" >私信</a>[m
 </div>[m
 <div class="member-main">[m
     <div class="member-contributions">[m
[1mdiff --git a/BaWuClub.Web/Views/Shared/_Layout.cshtml b/BaWuClub.Web/Views/Shared/_Layout.cshtml[m
[1mindex 4cadf0b..070b8d2 100644[m
[1m--- a/BaWuClub.Web/Views/Shared/_Layout.cshtml[m
[1m+++ b/BaWuClub.Web/Views/Shared/_Layout.cshtml[m
[36m@@ -11,7 +11,6 @@[m
     <meta name="description" content="@ViewBag.Description@dic["Description"]"/>[m
     <link href="~/Content/Css/site.css" rel="stylesheet" type="text/css"/>[m
     <script type="text/javascript" src="~/Content/Scripts/jquery.js"></script>[m
[31m-    @Html.Raw(ViewBag.CssAppend)[m
 </head>[m
 <body>[m
     <div id="header">[m
[1mdiff --git a/BaWuClub.Web/obj/Release/AspnetCompileMerge/Source/Content/Fonts/iconfont.woff b/BaWuClub.Web/obj/Release/AspnetCompileMerge/Source/Content/Fonts/iconfont.woff[m
[1mdeleted file mode 100644[m
[1mindex 0bcd4d7..0000000[m
Binary files a/BaWuClub.Web/obj/Release/AspnetCompileMerge/Source/Content/Fonts/iconfont.woff and /dev/null differ
[1mdiff --git a/BaWuClub.Web/obj/Release/AspnetCompileMerge/Source/Views/Forum/Index.cshtml b/BaWuClub.Web/obj/Release/AspnetCompileMerge/Source/Views/Forum/Index.cshtml[m
[1mdeleted file mode 100644[m
[1mindex 26b2c47..0000000[m
[1m--- a/BaWuClub.Web/obj/Release/AspnetCompileMerge/Source/Views/Forum/Index.cshtml[m
[1m+++ /dev/null[m
[36m@@ -1,7 +0,0 @@[m
[31m-﻿@{[m
[31m-    Layout = "~/views/shared/_layout.cshtml";[m
[31m-    ViewBag.Title = "电商圈-";[m
[31m-}[m
[31m-<div style="text-align:center;font-family:'Segoe UI';font-size:70px;color:#999;margin:250px 0;">[m
[31m-    Coming Soon[m
[31m-</div>[m
\ No newline at end of file[m
