using Microsoft.EntityFrameworkCore;
using PracticaMvcCore2Iniciales.Data;

namespace PracticaMvcCore2Iniciales.Repositories
{
    public class RepositoryLibros
    {
        private UsuariosContext context;

        public RepositoryLibros(UsuariosContext context)
        {
            this.context = context;
        }



        //metodos


        public async Task<List<Libro>> GetLibrosAsync()
        {
            var libros =
                await this.context.Libros.ToListAsync();
            return libros;
        }

        public async Task<Libro> FindLibroAsync(int idLibro)
        {
            return await
                this.context.Libros.FirstOrDefaultAsync
                (z => z.IdLibro == idLibro);
        }




        public async Task<List<Libro>>
           GetLibrosSessionAsync(List<int> ids)
        {
            //PARA REALIZAR UN IN DENTRO DE LINQ, DEBEMOS HACERLO 
            //CON Collection.Contains(dato a buscar)
            //select * from EMP where EMP_NO in (7777,8888,9999)
            var consulta = from datos in this.context.Libros
                           where ids.Contains(datos.IdLibro)
                           select datos;
            if (consulta.Count() == 0)
            {
                return null;
            }
            else
            {
                return await consulta.ToListAsync();
            }
        }


        public async Task<List<Libro>>
           GetLibrosNotSessionAsync(List<int> ids)
        {
            
            var consulta = from datos in this.context.Libros
                           where ids.Contains(datos.IdLibro) == false
                           select datos;
            if (consulta.Count() == 0)
            {
                return null;
            }
            else
            {
                return await consulta.ToListAsync();
            }
        }


    }
}
