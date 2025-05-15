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
        [HttpGet]
        public async Task<IActionResult> Index()
        {

         

            return View(await _motos.GetAll());
        }

        [HttpGet]
        public IActionResult Post()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Post(MotosModelView moto)
        {
            if (ModelState.IsValid)
            {
                var response = await _motos.PostAsync(moto);
                if (response is not null)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Put(int id)
        {
            var moto = await _motos.GetByIdAsync(id);
            if (moto is null)
            {
                return NotFound();
            }
            return View(moto);
        }

        [HttpPost]
        public async Task<ActionResult> Put(int id,MotosModelView moto)
        {
            if (ModelState.IsValid)
            {
                var response = await _motos.PutAsync(id,moto);
                if (response)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
