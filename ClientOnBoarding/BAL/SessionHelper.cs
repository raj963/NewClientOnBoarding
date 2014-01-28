using ClientOnBoarding.Models;
using System.Collections.Generic;
namespace ClientOnBoarding
{
    public static class SessionHelper
    {
        public static SiteSession UserSession
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["SiteSession"] == null)
                {
                    ContactDetails cd = new ContactDetails();
                    return new SiteSession(cd);
                }
                else
                    return (SiteSession)(System.Web.HttpContext.Current.Session["SiteSession"]);
            }
            set { System.Web.HttpContext.Current.Session["SiteSession"] = value; }
        }
    }
}