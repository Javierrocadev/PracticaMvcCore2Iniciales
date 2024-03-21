using Microsoft.AspNetCore.Mvc;
using PracticaMvcCore2Iniciales.Models;
using PracticaMvcCore2Iniciales.Repositories;

namespace PracticaMvcCore2Iniciales.ViewComponents
{
    public class MenuLibrosViewComponent : ViewComponent
    {
        private RepositoryGeneros repo;

        public MenuLibrosViewComponent(RepositoryGeneros repo)
        {
            this.repo = repo;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Genero> generos = await this.repo.GetGenerosAsync();
            return View(generos);
        }

        

    }
}
