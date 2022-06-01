using LocalBus.Context;
using LocalBus.Models;
using LocalBus.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LocalBus.Areas.Administrador.Controllers
{
    [Area("Administrador")]
    [Authorize(Roles = "Member")]
    public class AdminPontosController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IPontosRepository _context2;
        private readonly IEscolaRepository _context3;
        public AdminPontosController(AppDbContext context, IPontosRepository _pontosRepository, IEscolaRepository _escolaRepository)
        {
            _context3 = _escolaRepository;
            _context2 = _pontosRepository;
            _context = context;
        }

        // GET: AdminPontosController
        public async Task<IActionResult> Index()
        {
            ViewData["EscolaPonto"] = _context2.PontosAtivos.ToArray();
            return View(await _context.Pontos.ToListAsync());
        }

        // GET: AdminPontosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminPontosController/Create
        public ActionResult Create()
        {
            ViewBag.Escolas = _context3.EscolaRepository.OrderBy(E => E.EscolaId).ToArray();
            return View();
        }

        // POST: AdminPontosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("PontoId,latitudePonto,LongitudePonto,AtivoPonto,DescriçãoPonto,Nome,EscolaPonto")] Ponto ponto)
        {
            if (ModelState.IsValid)
            {
                var pontos = ponto;
                _context.Add(pontos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ponto);
        }

        // GET: AdminPontosController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdminPontosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminPontosController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminPontosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
