using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Biblioteca.Controllers
{
    public class UsuarioController : Controller
    {

        private List<SelectListItem> Acessos()
        {
            return new List<SelectListItem>
                {
                    new SelectListItem { Text = "Usuário", Value = "0"},
                    new SelectListItem { Text = "Administrador", Value = "1"}
                };
        }

        private List<SelectListItem> Filtros()
        {
            return new List<SelectListItem>
                {
                    new SelectListItem { Text = "Usuário", Value = "Usuario"},
                    new SelectListItem { Text = "Nome", Value = "Nome"}
                };
        }

        public IActionResult Cadastro()
        {
            Autenticacao.CheckLogin(this, "admin");
            TB_USUARIO TB_USUARIO = new TB_USUARIO();
            ViewBag.Acessos = Acessos();
            ViewData["Title"] = "Cadastro de usuário";
            return View("Cadastro", TB_USUARIO);
        }

        [HttpPost]
        public IActionResult Cadastro(TB_USUARIO TB_USUARIO)
        {
            Autenticacao.CheckLogin(this, "admin");
            ViewBag.Acessos = Acessos();

            if (ModelState.IsValid)
            {
                using (BibliotecaContext bc = new BibliotecaContext())
                {
                    TB_USUARIO.Senha = Security.GetStringSha256Hash(TB_USUARIO.Senha);
                    bc.TB_USUARIO.Add(TB_USUARIO);
                    bc.SaveChanges();
                }
                return Redirect("Listagem");
            }
            else
            {
                ViewData["Title"] = "Cadastro de usuário";
                return View("Cadastro", TB_USUARIO);
            }
        }

        public IActionResult Listagem(string tipoFiltro, string filtro)
        {
            Autenticacao.CheckLogin(this, "admin");

            List<TB_USUARIO> TB_USUARIO = new List<TB_USUARIO>();

            using (BibliotecaContext bc = new BibliotecaContext())
            {
                if (!string.IsNullOrEmpty(tipoFiltro) && !string.IsNullOrEmpty(filtro))
                {
                    switch (tipoFiltro)
                    {
                        case "Usuario":
                            TB_USUARIO = bc.TB_USUARIO.Where(x => x.Usuario.Contains(filtro)).ToList();
                            break;
                        case "Nome":
                            TB_USUARIO = bc.TB_USUARIO.Where(x => x.Nome.Contains(filtro)).ToList();
                            break;
                    }
                }
                else
                    TB_USUARIO = bc.TB_USUARIO.ToList();
            }

            ViewBag.Acessos = Acessos();
            ViewBag.Filtros = Filtros();
            return View("Listagem", TB_USUARIO);
        }

        public IActionResult Editar(int Id)
        {
            TB_USUARIO TB_USUARIO;
            Autenticacao.CheckLogin(this, "admin");
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                TB_USUARIO = bc.TB_USUARIO.Where(x => x.Id.Equals(Id)).FirstOrDefault();
            }

            ViewBag.Acessos = Acessos();
            ViewBag.Filtros = Filtros();
            ViewData["Title"] = "Edição de usuário";
            return View("Cadastro", TB_USUARIO);
        }

        [HttpPost]
        public IActionResult Editar(TB_USUARIO TB_USUARIO)
        { 
            Autenticacao.CheckLogin(this, "admin");
            ViewBag.Acessos = Acessos();

            if (ModelState.IsValid)
            {
                using (BibliotecaContext bc = new BibliotecaContext())
                {
                    TB_USUARIO.Senha = Security.GetStringSha256Hash(TB_USUARIO.Senha);
                    bc.Entry(TB_USUARIO).State = EntityState.Modified;
                    bc.SaveChanges();
                }
                return Redirect("Listagem");
            }
            else
            {
                ViewData["Title"] = "Edição de usuário";
                return View("Cadastro", TB_USUARIO);
            }
        }

        public IActionResult Apagar(int Id)
        {
            Autenticacao.CheckLogin(this, "admin");
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                bc.TB_USUARIO.Remove(bc.TB_USUARIO.Where(x => x.Id.Equals(Id)).FirstOrDefault());
                bc.SaveChanges();
            }

            return Redirect("Listagem");
        }
    }
}