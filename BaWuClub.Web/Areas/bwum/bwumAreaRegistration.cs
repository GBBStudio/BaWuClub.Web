using System.Web.Mvc;

namespace BaWuClub.Web.Areas.bwum
{
    public class bwumAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "bwum";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "bwum_default",
                "bwum/{controller}/{action}/{id}",
                new { action = "index",controller="Um", id = UrlParameter.Optional }
            );
        }
    }
}
