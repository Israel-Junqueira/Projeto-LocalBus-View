using LocalBus.Context;
using LocalBus.Models;
using LocalBus.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LocalBus.Areas.Administrador.Controllers
{
    [Area("Administrador")]
    [Authorize(Roles = "Member")]
    public class AdministradorController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IPontosRepository _context2;
        private readonly IEscolaRepository _context3;
        private readonly UserManager<MyUser> userManager1;
        public AdministradorController(UserManager<MyUser> userManager, AppDbContext context, IPontosRepository _pontosRepository, IEscolaRepository _escolaRepository)
        {
            userManager1 = userManager;
            _context3 = _escolaRepository;
            _context2 = _pontosRepository;
            _context = context;
        }
        public IActionResult Index()
        {
            var ids = userManager1.GetUserId(User);
            var IdDaEscolaOnlineString = _context.Escola.FirstOrDefault(e => e.MyUserId == ids).EscolaId.ToString();
            var IdDaEscolaOnlineInt = Convert.ToInt32(IdDaEscolaOnlineString);
            ViewData["PontoAtivo2"] = _context.EscolasPontos.Where(p => p.EscolaId.Equals(IdDaEscolaOnlineInt)).Include(c => c.Ponto).Where(c => c.Ponto.AtivoPonto == true).ToArray();
            var pontosDoUsuarioLogado = _context.EscolasPontos.Where(p => p.EscolaId.Equals(IdDaEscolaOnlineInt)).Include(c => c.Ponto).ToArray();
            return View(pontosDoUsuarioLogado);
        }

    
    }
}
