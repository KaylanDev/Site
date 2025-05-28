using Microsoft.AspNetCore.Mvc;
using SiteMotos.Models;
using SiteMotos.Services.Auth;
using System.Threading.Tasks;

namespace SiteMotos.Controllers
{
    public class AuthController : Controller
    {


        private readonly IAutentificador _autentificador;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAutentificador autentificador, ILogger<AuthController> logger)
        {
          
            _autentificador = autentificador;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(UserAuthViewModel userVW)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Login failed for user: {UserName}", userVW.Name);
                ModelState.AddModelError(string.Empty,"Login Invalido");
                return View(userVW);
            }

            var result = await _autentificador.Autentificacao(userVW);
            if (result is  null)
            {
                _logger.LogError("Login failed for user: {UserName}", userVW.Name);
                ModelState.AddModelError(string.Empty, "Login Invalido");
                return View(userVW);
            }
            //armazena token em cookie
            Response.Cookies.Append("JWT", result.Token,new CookieOptions()
            {
                Secure = true,
                HttpOnly = true,
                SameSite = SameSiteMode.Strict
            });



            return Redirect("/Home/Index");
        }
    }
}
