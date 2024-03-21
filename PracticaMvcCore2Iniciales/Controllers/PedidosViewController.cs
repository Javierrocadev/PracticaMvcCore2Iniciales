using Microsoft.AspNetCore.Mvc;
using PracticaMvcCore2Iniciales.Models;
using PracticaMvcCore2Iniciales.Repositories;
using System.Security.Claims;

namespace PracticaMvcCore2Iniciales.Controllers
{
    public class PedidosViewController : Controller
    {
        private RepositoryVistaPedidos repo;

        public PedidosViewController(RepositoryVistaPedidos repo)
        {
            this.repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            List<VistaPedido> model = await this.repo.GetPedidosAsync();
            return View(model);
        }

        public async Task<IActionResult> PedidosUsuario()
        {
            // Obtener el ClaimsPrincipal del usuario actual
            ClaimsPrincipal user = this.User;

            // Obtener los pedidos del usuario actual
            var model = await this.repo.GetPedidosUserAsync(user);

            return View(model);
        }







    }
}
