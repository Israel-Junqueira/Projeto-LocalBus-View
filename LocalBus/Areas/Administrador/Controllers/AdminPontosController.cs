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
        public async Task<IActionResult> Index(int IdEscola)
        {
            ViewData["PontoAtivo"] = _context.EscolasPontos.Where(p => p.EscolaId.Equals(IdEscola)).Include(c=>c.Ponto).ToArray();

            var result = await _context.EscolasPontos.Where(p => p.EscolaId.Equals(IdEscola)).ToListAsync();
            var conversao = result.ToArray(); 
            return View(conversao);
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
        public async Task<ActionResult> Create([Bind("PontoId,latitudePonto,LongitudePonto,AtivoPonto,DescriçãoPonto,Nome,EscolaId,PontoId")] Ponto ponto,EscolaPonto escolaPonto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var pontos = ponto;
                    _context.Add(pontos);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(""," Não foi possível salvar as alterações. " +
             "Tente novamente e se o problema persistir " +
             "consulte o administrador do sistema"+ "Error:"+ex);
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
