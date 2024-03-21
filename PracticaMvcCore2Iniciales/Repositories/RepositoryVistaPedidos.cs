using Microsoft.EntityFrameworkCore;
using PracticaMvcCore2Iniciales.Data;
using PracticaMvcCore2Iniciales.Models;
using System.Security.Claims;

namespace PracticaMvcCore2Iniciales.Repositories
{
    public class RepositoryVistaPedidos
    {
        private UsuariosContext context;

        public RepositoryVistaPedidos(UsuariosContext context)
        {
            this.context = context;
        }


        public async Task<List<VistaPedido>> GetPedidosAsync()
        {
            var consulta = from datos in this.context.VistaPedidos
                           select datos;
            return await consulta.ToListAsync();
        }

        public async Task<List<VistaPedido>> GetPedidosUserAsync(ClaimsPrincipal user)
        {
            // Obtener el IdUsuario del claim
            string idUsuario = user.FindFirstValue(ClaimTypes.NameIdentifier);

            // Consulta para obtener los pedidos del usuario
            var consulta = from datos in this.context.VistaPedidos
                           where datos.IdUsuario == int.Parse(idUsuario)
                           select datos;

            return await consulta.ToListAsync();
        }




    }
}

