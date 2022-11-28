using Microsoft.AspNetCore.Mvc;
using SonDepremler.Models;
using SonDepremler.Models.ViewModels;
using System.Diagnostics;

namespace SonDepremler.Controllers
{
    public class DepremController : Controller
    {
        private readonly ILogger<DepremController> _logger;

        public DepremController(ILogger<DepremController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            Deprem deprem = new Deprem();
            ViewBag.SonDepremler = deprem.Get(0);
            return View();
        }

        public IActionResult About()
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