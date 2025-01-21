using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
//using WarhammerForum.Models;

namespace WarhammerForum.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController()
        {

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
