using ClientOnBoarding.Models;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ClientOnBoarding.Controllers
{
    public class AccountController : Controller
    {
        #region -- External Login --

        // GET
        [AllowAnonymous]
        public ActionResult ExternalLogin(string returnUrl)
        {
            if (SessionHelper.UserSession.RoleID == UserRole.SuperAdmin ||
                        SessionHelper.UserSession.RoleID == UserRole.Admin)
                return RedirectToAction("Index", "Search");
            else if (SessionHelper.UserSession.RoleID == UserRole.NormalUser)
                return RedirectToAction("Index", "SetupCustomer");
            else
            {
                ViewBag.ReturnUrl = returnUrl;
                return View();
            }
        }
                        
        [HttpPost]
        [AllowAnonymous]
        public JsonResult JsonExternalLogin(LoginModel model, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                //Step 1: Get data from Sp and check it
                AccountBL Ab = new AccountBL();
                ContactDetails cd = new ContactDetails();
                cd = Ab.CheckLogin(model.UserName, model.Password);
                if (cd.CustomerID > 0)
                {
                    //cd.CustomerID = 0;
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    SiteSession siteSession = new SiteSession(cd);
                    SessionHelper.UserSession = siteSession;

                    UrlHelper u = new UrlHelper(HttpContext.Request.RequestContext);
                    string url = string.Empty;
                    if (SessionHelper.UserSession.RoleID == UserRole.SuperAdmin ||
                        SessionHelper.UserSession.RoleID == UserRole.Admin||
                    SessionHelper.UserSession.RoleID == UserRole.Staff)
                        url = u.Action("Index", "Search", null);
                    else
                        url = u.Action("Index", "SetupCustomer", null);

                    return Json(new { success = true, redirect = string.IsNullOrEmpty(ReturnUrl) ? url : ReturnUrl });
                }
                else
                {
                    ModelState.AddModelError("", "Please provide valid User Name/Password.");
                }
            }
            return Json(new { errors = KeyValue.GetErrorsFromModelState(ViewData) });
        }

        #endregion -- External Login --

        #region -- Logoff --

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            int culture = SiteSession.CurrentUICulture;
            SessionHelper.UserSession = null;
            return RedirectToAction("ExternalLogin", "Account");
        }

        #endregion -- Logoff --

        #region -- error --

        public ActionResult GlobalError()
        {
            return PartialView("Error");
        }

        #endregion -- error --
    }
}