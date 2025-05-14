using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SiteMotos.Models;
using SiteMotos.Services;

namespace SiteMotos.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMotos _motos;
        
        public HomeController(ILogger<HomeController> logger,IMotos moto)
        {
            _logger = logger;
            _motos = moto;
        }

        public async Task<IActionResult> Index()
        {

         

            return View(await _motos.GetAll());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
