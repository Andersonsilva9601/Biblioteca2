using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Biblioteca.Controllers
{
    public class Autenticacao
    {
        public static void CheckLogin(Controller controller, string user = "")
        {
            controller.ViewBag.User = controller.HttpContext.Session.GetString("acesso");

            if (string.IsNullOrEmpty(controller.HttpContext.Session.GetString("user")))
            {
                controller.Request.HttpContext.Response.Redirect("/Home/Login");
                return;
            }

            if (!string.IsNullOrEmpty(user) && controller.HttpContext.Session.GetString("user") != user)
            {
                controller.Request.HttpContext.Response.Redirect("/Home/Login");
            }
        }
    }
}