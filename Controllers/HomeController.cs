using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Biblioteca.Models;
using Microsoft.AspNetCore.Http;

namespace Biblioteca.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            Autenticacao.CheckLogin(this);
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string login, string senha)
        {
            TB_USUARIO Usuario = null;
            if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(senha))
            {
                using (BibliotecaContext bc = new BibliotecaContext())
                {
                    Usuario = bc.TB_USUARIO.Where(x => x.Usuario.Equals(login) &&
                                                       x.Senha.Equals(Security.GetStringSha256Hash(senha)))
                                           .FirstOrDefault();
                }
            }

            if (Usuario == null)
            {
                ViewData["Erro"] = "Senha inválida";

                /*
                               using (BibliotecaContext bc = new BibliotecaContext())
                               {
                                   bc.TB_USUARIO.Add(new TB_USUARIO
                                   {
                                       Nome = "Administrator",
                                       Acesso = 1,
                                       Senha = Security.GetStringSha256Hash("admin"),
                                       Usuario = "admin"
                                   });
                                   bc.SaveChanges();
                               }
                               */

                return View();
            }
            else
            {
                string acesso = "";
                switch (Usuario.Acesso)
                {
                    case 0: // Usuário
                        acesso = "user";
                        break;
                    case 1: // Administrador
                        acesso = "admin";
                        break;
                }
                HttpContext.Session.SetString("user", Usuario.Usuario);
                HttpContext.Session.SetString("acesso", acesso);
                return RedirectToAction("Index");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Logoff()
        {
            HttpContext.Session.SetString("user", "");
            HttpContext.Session.SetString("acesso", "");
            return RedirectToAction("Index");
        }
    }
}
