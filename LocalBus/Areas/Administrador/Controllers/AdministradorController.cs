using LocalBus.Context;
using LocalBus.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LocalBus.Areas.Administrador.Controllers
{
    [Area("Administrador")]
    [Authorize(Roles = "Member")]
    public class AdministradorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    
    }
}
