using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Procurement.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using CustomRoles.Sessions;

namespace CustomRoles.Controllers
{
    public class BaseController : Controller
    {

        UserSession _user;


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if (HttpContext.Session.GetObjectFromJson<UserSession>("userObject") != null)
            {
                UserSession sess = (UserSession)HttpContext.Session.GetObjectFromJson<UserSession>("userObject");



                var flag = false;

                var ControllerName = ControllerContext.ActionDescriptor.ControllerName;
                var actionName = ControllerContext.ActionDescriptor.ActionName;

                var Currrentpage = ControllerName + "/" + actionName;



                //SessionExtension.GetObjectFromJson<UserSession>(HttpContext.Session, "userObject");
                var a = HttpContext.Session.GetObjectFromJson<UserSession>("userObject");
                for (int i = 0; i < a.PagePermissions.Count; i++)
                {
                    if (a.PagePermissions[i] == Currrentpage)
                    {
                        flag = true;
                    }
                }

                if (flag == false)
                {

                    filterContext.Result = new RedirectResult("/BusinessUser/AccessDenied/Index");
                }


            }

            else
            {
                
            }

        }

    }
}