using Microsoft.AspNetCore.Mvc;
using PracticaMvcCore2Iniciales.Filters;
using PracticaMvcCore2Iniciales.Models;
using PracticaMvcCore2Iniciales.Repositories;

namespace PracticaMvcCore2Iniciales.Controllers
{
    public class UsuariosController : Controller
    {
        private RepositoryUsuarios repo;

        public UsuariosController(RepositoryUsuarios repo)
        {
            this.repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            List<Usuario> usuarios = await this.repo.GetUsuariosAsync();
            return View(usuarios);
        }

        public async Task<IActionResult> Details(int idusuario)
        {
            Usuario usuario = await this.repo.FindUsuarioAsync(idusuario);
            return View(usuario);
        }



        //-----------vistas con seguridad--------------

        [AuthorizeUsuarios]
        public IActionResult PerfilUsuario()
        {
            return View();
        }



    }
}
