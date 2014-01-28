using ClientOnBoarding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClientOnBoarding.BAL
{
    public class AuthorizeAdminAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAuthorized = base.AuthorizeCore(httpContext);
            if (!isAuthorized)
            {
                return false;
            }
            if (SessionHelper.UserSession.RoleID == UserRole.SuperAdmin ||
                SessionHelper.UserSession.RoleID == UserRole.Admin)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class AuthorizeAdminAndStaffAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAuthorized = base.AuthorizeCore(httpContext);
            if (!isAuthorized)
            {
                return false;
            }
            if (SessionHelper.UserSession.RoleID == UserRole.SuperAdmin ||
                SessionHelper.UserSession.RoleID == UserRole.Admin ||
                SessionHelper.UserSession.RoleID == UserRole.Staff)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class AuthorizeSuperAdminAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAuthorized = base.AuthorizeCore(httpContext);
            if (!isAuthorized)
            {
                return false;
            }
            
            if (SessionHelper.UserSession.RoleID == UserRole.SuperAdmin )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class AuthorizeNormalAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAuthorized = base.AuthorizeCore(httpContext);
            if (!isAuthorized)
            {
                return false;
            }
            if (SessionHelper.UserSession.RoleID == UserRole.NormalUser)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class AuthorizeStaffAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAuthorized = base.AuthorizeCore(httpContext);
            if (!isAuthorized)
            {
                return false;
            }
            if (SessionHelper.UserSession.RoleID == UserRole.Staff)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}