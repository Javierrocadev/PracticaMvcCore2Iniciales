using Microsoft.EntityFrameworkCore;
using PracticaMvcCore2Iniciales.Models;

namespace PracticaMvcCore2Iniciales.Data
{
    public class UsuariosContext : DbContext
    {
        public UsuariosContext(DbContextOptions<UsuariosContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }

    }
}
