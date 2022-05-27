using LocalBus.Models;
using LocalBus.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LocalBus.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManeger;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManeger, SignInManager<IdentityUser> signInManager)
        {
            _userManeger = userManeger;
            _signInManager = signInManager;
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
                var user = new IdentityUser { UserName = registro.UserName };
                var DadosdaEscola = new Escola { EmailDaEscola = registro.Email, TelefoneDaEscola = registro.TelefonedaEscola, NomeEscola = registro.NomedaEscola};

                var result = await _userManeger.CreateAsync(user, registro.Password);

                if (result.Succeeded)
                {
                    await _userManeger.AddToRoleAsync(user, "Member");
                    return RedirectToAction("Login", "Account");

                }
                else
                {
                    this.ModelState.AddModelError("", "O seu formulario possui erros! verifique a senha ou email e tente novamente. ");
                }
            }
            return View(registro);
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
