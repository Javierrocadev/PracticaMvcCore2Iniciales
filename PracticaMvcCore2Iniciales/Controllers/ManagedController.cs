using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PracticaMvcCore2Iniciales.Repositories;
using System.Security.Claims;
using PracticaMvcCore2Iniciales.Models;

namespace PracticaMvcCore2Iniciales.Controllers
{
    public class ManagedController : Controller
    {
        private RepositoryUsuarios repo;

        public ManagedController(RepositoryUsuarios repo)
        {
            this.repo = repo;
        }

        //--------------------
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login
    (string email, string password)
        {
           
            Usuario usuario = await
                this.repo.LogInEmpleadoAsync(email, password);
            if (usuario != null)
            {
                //SEGURIDAD
                ClaimsIdentity identity =
                    new ClaimsIdentity(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        ClaimTypes.Name, ClaimTypes.Role);
                //CREAMOS EL CLAIM PARA EL NOMBRE (email)
                Claim claimName =
                    new Claim(ClaimTypes.Name, usuario.Email);
                identity.AddClaim(claimName);

                //APELLIDOS
                Claim claimApellido =
                    new Claim("Apellidos", usuario.Apellidos);
                identity.AddClaim(claimApellido);
                //foto
                Claim claimFoto =
                    new Claim("Foto", usuario.Foto);
                identity.AddClaim(claimFoto);
                //nombre
                Claim claimNombre =
                    new Claim("Nombre", usuario.Nombre);
                identity.AddClaim(claimNombre);
                //idusuario
                Claim claimIdUsuario =
                    new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString());
                identity.AddClaim(claimIdUsuario);





                //COMO POR AHORA NO VOY A UTILIZAR NI SE UTILIZAR ROLES
                //NO LO INCLUIMOS
                ClaimsPrincipal userPrincipal =
                    new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    userPrincipal);
                //LO VAMOS A LLEVAR A UNA VISTA QUE TODAVIA NO TENEMOS
                //QUE SERA EL PERFIL DEL EMPLEADO
                return RedirectToAction("PerfilUsuario", "Usuarios");
            }
            else
            {
                ViewData["MENSAJE"] = "Usuario/Password incorrectos";
                return View();
            }
        }






        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync
                (CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }










    }
}
