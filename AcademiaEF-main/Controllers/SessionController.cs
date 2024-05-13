 using Microsoft.AspNetCore.Mvc;
using Academia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;

namespace Academia.Controllers
{
    public class SessionController : Controller
    {
        private static List<Usuario> usuarios = new();
        public IActionResult Index()
        {

            return View(usuarios);
        }
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(Usuario usuario)
        {
            usuarios.Add(usuario);
            usuario.UsuarioID = usuarios.Select(u => u.UsuarioID).Max() + 1;
            return RedirectToAction("Index");
        }
        public IActionResult Login()
        {
            return View();

        }
        [HttpPost]
        public IActionResult Login(string email, string senha)
        {
            var confirma = usuarios.Where(u => u.Email.Equals(email) && u.Senha.Equals(senha)).FirstOrDefault();
            if (confirma != null)
            {
                HttpContext.Session.SetString("usuario_session", confirma.Nome);
                return RedirectToAction("Correto");
            }
            else
            {

                return RedirectToAction("Login");
            }
        }
        public IActionResult Correto()
        {
            return View();
        }
    }
}
