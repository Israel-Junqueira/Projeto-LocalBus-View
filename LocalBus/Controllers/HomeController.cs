using LocalBus.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LocalBus.Controllers
{
    public class HomeController : Controller
    {
   

        public IActionResult Index()
        {
            return View();
        }
    }
}