using Microsoft.AspNetCore.Mvc;
using PracticaMvcCore2Iniciales.Extensions;
using PracticaMvcCore2Iniciales.Models;
using PracticaMvcCore2Iniciales.Repositories;

namespace PracticaMvcCore2Iniciales.Controllers
{
    public class LibrosController : Controller
    {
        private RepositoryLibros repo;

        public LibrosController(RepositoryLibros repo)
        {
            this.repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            List<Libro> libros = await this.repo.GetLibrosAsync();
            return View(libros);
        }


        public async Task<IActionResult> Details(int idlibro)
        {
            Libro libro = await this.repo.FindLibroAsync(idlibro);
            return View(libro);
        }


        //------------------sesion-----------------

        public async Task<IActionResult> SessionLibros
    (int? idlibro)
        {
            if (idlibro != null)
            {
                //BUSCAMOS AL libro
                Libro libro = await this.repo.FindLibroAsync(idlibro.Value);
                //EN SESSION ALMACENAREMOS UN CONJUNTO DE EMPLEADOS
                List <Libro> librosList;
                //DEBEMOS PREGUNTAR SI TENEMOS libro DENTRO DE 
                //SESSION
                if (HttpContext.Session.GetObject<List<Libro>>("LIBROS") != null)
                {
                    //SI YA TENEMOS EMPLEADOS, LOS RECUPERAMOS DE SESSION
                    librosList =
                        HttpContext.Session.GetObject<List<Libro>>("LIBROS");
                }
                else
                {
                    //SI NO TENEMOS EMPLEADOS, CREAMOS LA COLECCION PARA 
                    //ALMACENAR EL PRIMER EMPLEADO
                    librosList = new List<Libro>();
                }
                //ALMACENAMOS EL NUEVO libro EN SESSION
                librosList.Add(libro);
                //GUARDAMOS LA COLECCION DENTRO DE SESSION
                HttpContext.Session.SetObject("LIBROS", librosList);
                ViewData["MENSAJE"] = "Libro " + libro.Titulo
                    + " almacenado correctamente";
            }
            List<Libro> libros =
                await this.repo.GetLibrosAsync();
            return View(libros);
        }

        public IActionResult LibrosAlmacenados()
        {
            return View();
        }


        public async Task<IActionResult>
            LibrosAlmacenadosOk(int? ideliminar)
        {
            //DEBEMOS RECUPERAR LOS EMPLEADOS QUE ESTEN DENTRO 
            //DE LA COLECCION DE SESSION
            //RECUPERAMOS LOS DATOS DE LA COLECCION DE IDS DE SESSION
            List<int> idsLibros =
                HttpContext.Session.GetObject<List<int>>("IDSLIBROS");
            if (idsLibros != null)
            {
                //DEBEMOS ELIMINAR DE SESSION
                if (ideliminar != null)
                {
                    //NOS HAN ENVIADO UN DATO PARA ELIMINARLO DE SESSION
                    idsLibros.Remove(ideliminar.Value);
                    //DEBEMOS PREGUNTAR SI YA NO TENEMOS EMPLEADOS EN LA 
                    //COLECCION
                    if (idsLibros.Count == 0)
                    {
                        //ELIMINAMOS DIRECTAMENTE SESSION CON SU KEY
                        HttpContext.Session.Remove("IDSLIBROS");
                    }
                    else
                    {
                        //ALMACENAMOS DE NUEVO LOS DATOS EN SESSION
                        HttpContext.Session.SetObject("IDSLIBROS", idsLibros);
                    }
                }
                List<Libro> libros = await
                    this.repo.GetLibrosSessionAsync(idsLibros);
                return View(libros);
            }
            return View();
        }



        public async Task<IActionResult> SessionLibrosOk(int? idlibro)
        {
            if (idlibro != null)
            {
                //ALMACENAREMOS LO MINIMO DEL OBJETO, UNA COLECCION INT
                List<int> idsLibros;
                if (HttpContext.Session.GetString("IDSLIBROS") == null)
                {
                    //TODAVIA NO TENEMOS DATOS EN SESSION Y CREAMOS LA COLECCION
                    idsLibros = new List<int>();
                }
                else
                {
                    //RECUPERAMOS LA COLECCION DE Ids DE SESSION
                    idsLibros =
                        HttpContext.Session.GetObject<List<int>>("IDSLIBROS");
                }
                //ALMACENAMOS EL ID DEL EMPLEADO EN LA COLECCION
                idsLibros.Add(idlibro.Value);
                //ALMACENAMOS LA COLECCION EN SESSION CON LOS CAMBIOS REALIZADOS
                HttpContext.Session.SetObject("IDSLIBROS", idsLibros);
                ViewData["MENSAJE"] = "LIBROS almacenados: "
                    + idsLibros.Count;
            }
            //COMPROBAMOS SI TENEMOS ALGO EN SESSION
            List<int> ids = HttpContext.Session.GetObject<List<int>>("IDSLIBROS");
            if (ids == null)
            {
                List<Libro> libros = await this.repo.GetLibrosAsync();
                return View(libros);
            }
            else
            {
                List<Libro> libros = await
                    this.repo.GetLibrosNotSessionAsync(ids);
                return View(libros);
            }
        }











    }
}
