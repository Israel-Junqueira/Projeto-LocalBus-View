using LocalBus.Context;
using LocalBus.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LocalBus.Areas.Administrador.Controllers
{
    [Area("Administrador")]
    public class AdministradorController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<MyUser> _userManeger;

        public AdministradorController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

    
    }
}
