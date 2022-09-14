using LocalBus.Context;
using LocalBus.Models;
using LocalBus.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<MyUser> userManager1;
        public AdminPontosController(UserManager<MyUser> userManager, AppDbContext context, IPontosRepository _pontosRepository, IEscolaRepository _escolaRepository)
        {
            userManager1 = userManager;
            _context3 = _escolaRepository;
            _context2 = _pontosRepository;
            _context = context;
        }

        // GET: AdminPontosController
        public async Task<IActionResult> Index()
        {
            var ids = userManager1.GetUserId(User);
            var IdDaEscolaOnlineString = _context.Escola.FirstOrDefault(e => e.MyUserId == ids).EscolaId.ToString();
            var IdDaEscolaOnlineInt = Convert.ToInt32(IdDaEscolaOnlineString);
            ViewData["PontoAtivo"] = _context.EscolasPontos.Where(p => p.EscolaId.Equals(IdDaEscolaOnlineInt)).Include(c=>c.Ponto).Where(c=>c.Ponto.AtivoPonto==true).ToArray();
        
            var pontosDoUsuarioLogado = _context.EscolasPontos.Where(p => p.EscolaId.Equals(IdDaEscolaOnlineInt)).Include(c => c.Ponto).ToArray();
            var result = await _context.EscolasPontos.Where(p => p.EscolaId.Equals(ids)).ToListAsync();
            var conversao = result.ToArray();
            ViewBag.PontosAtivoConvertido = _context.EscolasPontos.Where(p => p.EscolaId.Equals(IdDaEscolaOnlineInt)).Include(c => c.Ponto).ToArray(); 
            return View(pontosDoUsuarioLogado);
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
        public async Task<ActionResult> Create([Bind("PontoId,latitudePonto,LongitudePonto,AtivoPonto,DescriçãoPonto,Nome")] Ponto ponto,EscolaPonto escolaPonto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    
                    var pontos = ponto;
                 
                    _context.Add(pontos);
                    await _context.SaveChangesAsync();

                    var IdPonto = pontos.PontoId;
                    var idUsuarioLogado = userManager1.GetUserId(User);
                    var IdDaEscolaRefenteAoUsuarioLogado = _context3.GetEscolaById(idUsuarioLogado);
                    var IdDaEscola = Convert.ToInt32(IdDaEscolaRefenteAoUsuarioLogado);
                    escolaPonto.PontoId = IdPonto;
                    escolaPonto.EscolaId= IdDaEscola;
                    _context.Add(escolaPonto);
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
        public async Task<ActionResult> Edit(int id)
        {
#pragma warning disable CS0472 // O resultado da expressão é sempre o mesmo, pois um valor deste tipo nunca é 'null' 
            if (id == null)
#pragma warning restore CS0472 // O resultado da expressão é sempre o mesmo, pois um valor deste tipo nunca é 'null' 
            {
                return NotFound();
            }
            var Ponto = await _context.Pontos.FindAsync(id);
#pragma warning disable CS0472 // O resultado da expressão é sempre o mesmo, pois um valor deste tipo nunca é 'null' 
            if (id == null)
#pragma warning restore CS0472 // O resultado da expressão é sempre o mesmo, pois um valor deste tipo nunca é 'null' 
            {
                return NotFound();
            }

            return View(Ponto);
        }

        // POST: AdminPontosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,[Bind("PontoId,latitudePonto,LongitudePonto,AtivoPonto,DescriçãoPonto,Nome")] Ponto ponto)
        {
            if(id != ponto.PontoId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ponto);
                    await _context.SaveChangesAsync();
                }
                catch(DbUpdateConcurrencyException ex)
                {
                    if (!PontoExiste(ponto.PontoId))
                    {
                        return NotFound(ex);
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(ponto);
        }

        // GET: AdminPontosController/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Ponto = await _context.Pontos.Include(p => p.Escola_Ponto).FirstOrDefaultAsync(m => m.PontoId == id);
            if (id == null)
            {
                return NotFound();
            }

            return View(Ponto);
        }

        // POST: AdminPontosController/Delete/5
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var ponto = await _context.Pontos.FindAsync(id);
            _context.Pontos.Remove(ponto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PontoExiste(int id)
        {
            return _context.Pontos.Any(e=>e.PontoId == id);
        }
    }
}
