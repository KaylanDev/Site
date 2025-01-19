using Microsoft.AspNetCore.Mvc;

namespace SiteMotos.Controllers
{
    public class TesteController : Controller
    {
        public IActionResult Index()
        {
            List<string> ola = new List<string>(){
        "MT-07",
        "MT-09",
        "MT-15",
        "MT-10",

    };



            return View(ola);
        }
    }
}
