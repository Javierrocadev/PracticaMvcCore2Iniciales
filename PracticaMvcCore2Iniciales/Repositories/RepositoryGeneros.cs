using Microsoft.EntityFrameworkCore;
using PracticaMvcCore2Iniciales.Data;
using PracticaMvcCore2Iniciales.Models;

namespace PracticaMvcCore2Iniciales.Repositories
{
    public class RepositoryGeneros
    {
        private UsuariosContext context;

        public RepositoryGeneros(UsuariosContext context)
        {
            this.context = context;
        }



        //metodos

        public async Task<List<Genero>> GetGenerosAsync()
        {
            var generos =
                await this.context.Generos.ToListAsync();
            return generos;
        }



    }
}
