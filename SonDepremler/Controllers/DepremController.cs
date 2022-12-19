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

        [HttpGet]
        //[Route("~/Deprem/Index/{take}")]
        public IActionResult Index(int take)
        {
            if (take == 0)
            {
                take = 100;
            }

            Deprem deprem = new Deprem();
            deprem.GetData(take);
            ViewBag.SonDepremler = deprem.Get(0);
            ViewBag.Take = take;
            return View();
        }

        [HttpGet]
        [Route("/Deprem/Map/{tarih}/{enlem}/{boylam}/{derinlik}/{siddet}/{yer}")]
        public IActionResult Map(string tarih, string enlem, string boylam, string derinlik, string siddet, string yer)
        {
            ViewBag.Tarih = tarih;
            ViewBag.Enlem = enlem.Replace(",", ".");
            ViewBag.Boylam = boylam.Replace(",", "."); ;
            ViewBag.Derinlik = derinlik.Replace(",", "."); ;
            ViewBag.Siddet = siddet.Replace(",", "."); ;
            ViewBag.Yer = yer;

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