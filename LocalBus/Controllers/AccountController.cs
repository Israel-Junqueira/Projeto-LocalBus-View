using LocalBus.Context;
using LocalBus.Models;
using LocalBus.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LocalBus.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<MyUser> _userManeger;
        private readonly SignInManager<MyUser> _signInManager;
        private readonly AppDbContext _context;

        public AccountController(UserManager<MyUser> userManeger, SignInManager<MyUser> signInManager,AppDbContext appDbContext)
        {
            _context = appDbContext;
            _userManeger = userManeger;
            _signInManager = signInManager;
        }
     

        [HttpGet]
        public async Task<IActionResult> Perfil(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var UsuarioEscola =  _context.Escola.FirstOrDefault(e=> e.MyUserId.Equals(id));
            var iddaEscola = UsuarioEscola.EscolaId;
            ViewBag.UsuarioLogado = _context.Users.Where(c => c.Id.Equals(id));
            if (id == null)
            {
                return NotFound();
            }

            return View(UsuarioEscola);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Perfil(string id, [Bind("EscolaId,NomeEscola,TelefoneDaEscola,EmailDaEscola,CodigoDaEtec,MyUserId")] Escola escola)
        {
            if (id != escola.MyUserId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(escola);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!EscolaExiste(escola.EscolaId))
                    {
                        return NotFound(ex);
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Perfil", "Account");
            }
            return View(escola);
        }
        private bool EscolaExiste(int id)
        {
            return _context.Escola.Any(e => e.EscolaId == id);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel()
            {
                ReturnUrl = returnUrl

            });
        }
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel loginVm)
        {
            if (!ModelState.IsValid)
                return View(loginVm); 

            var user = await _userManeger.FindByNameAsync(loginVm.UserName);

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, loginVm.Password, false, false);
                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(loginVm.ReturnUrl))
                    {
                        return RedirectToAction("Index", "Home");

                    }
                    return Redirect(loginVm.ReturnUrl);
                }
            }
            ModelState.AddModelError("", "Usuario ou senha invalidos");
            return View(loginVm);
        }


        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro(RegistroViewModel registro)
        {
            if (ModelState.IsValid)
            {
                var user = new MyUser { UserName = registro.UserName };
                
               
                var result = await _userManeger.CreateAsync(user, registro.Password);
                var DadosdaEscola = new Escola { EmailDaEscola = registro.Email, TelefoneDaEscola = registro.TelefonedaEscola, NomeEscola = registro.NomedaEscola, CodigoDaEtec = registro.CodigoEtec};
                if (result.Succeeded)
                {
                    await _userManeger.AddToRoleAsync(user, "Member");
                    _context.Add(new Escola { CodigoDaEtec = DadosdaEscola.CodigoDaEtec,EmailDaEscola = DadosdaEscola.EmailDaEscola,NomeEscola=DadosdaEscola.NomeEscola,TelefoneDaEscola=DadosdaEscola.TelefoneDaEscola,MyUserId=user.Id});
                   _context.SaveChanges();
                    return RedirectToAction("Login", "Account");

                }
                else
                {
                    this.ModelState.AddModelError("", "O seu formulario possui erros! verifique a senha ou email e tente novamente. ");
                }
            }
            return View(registro);
        }
        [HttpGet]
        public async Task<IActionResult> Logout1()
        {
            HttpContext.Session.Clear();
            HttpContext.User = null;
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {//a view desse action e uma partial view criada na pasta shered, -adicionar - view - view razor - check criar um modelo de exibição parcial 
            HttpContext.Session.Clear();
            HttpContext.User = null;
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
