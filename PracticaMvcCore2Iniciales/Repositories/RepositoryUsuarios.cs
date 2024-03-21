using Microsoft.EntityFrameworkCore;
using PracticaMvcCore2Iniciales.Data;
using PracticaMvcCore2Iniciales.Models;

namespace PracticaMvcCore2Iniciales.Repositories
{
    public class RepositoryUsuarios
    {
        private UsuariosContext context;

        public RepositoryUsuarios(UsuariosContext context)
        {
            this.context = context;
        }

        //metodos


        public async Task<List<Usuario>> GetUsuariosAsync()
        {
            return await this.context.Usuarios.ToListAsync();
        }

        public async Task<Usuario> FindUsuarioAsync(int idUsuario)
        {
            return await this.context.Usuarios
                .FirstOrDefaultAsync(x => x.IdUsuario == idUsuario);
        }


        //--------validar usuario------------------


        public async Task<Usuario> LogInEmpleadoAsync
            (string email, string password)
        {
            Usuario usuario =
                await this.context.Usuarios
                .Where(z => z.Email == email
                && z.Pass == password).FirstOrDefaultAsync();
            return usuario;
        }

    }
}
