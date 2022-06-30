using LocalBus.Context;
using LocalBus.Models;
using LocalBus.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Options;

namespace LocalBus.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<MyUser> _userManeger;
        private readonly SignInManager<MyUser> _signInManager;
        private readonly AppDbContext _context;
        private readonly ConfigurationImagens _myConfig;
        private readonly IWebHostEnvironment _hostingEvironment;
        public AccountController(IWebHostEnvironment hostEnvironment,IOptions<ConfigurationImagens> myconfiguration,UserManager<MyUser> userManeger, SignInManager<MyUser> signInManager,AppDbContext appDbContext)
        {
            _hostingEvironment = hostEnvironment;
            _myConfig = myconfiguration.Value;
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
                                                                //quando clica em enviar
                                                               //recebe a lista de arquivos 
        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {


        //---------------Faz a verificação se tem arquivos enviados------//
            if (files == null || files.Count == 0)
            {
                ViewData["Error"] = "Erro: Arquivo(s) não selecionado(s)";
                return View(ViewData);
            }
            if (files.Count > 1)
            {
                ViewData["Erro"] = "Error: Envie apenas uma foto";
                return View(ViewData);
            }

        //------------------Soma quantidade de bytes------------//
            long size = files.Sum(f => f.Length);


            var filePathsName = new List<string>(); //nome dos arquivos que sera passado pra view
                                            
        //------------------ Monta o Caminho que sera enviado os arquivos ---------//
            //aqui pega o caminho da wwwroot +, caminho do local de armazenamento das imagens que vc ira enviar  
            var filePaths = Path.Combine(_hostingEvironment.WebRootPath, _myConfig.NomePastaImagensProdutos);

        //------------------Percorre cada arquivo para verificar se e do tipo que foi determinado------------//
            foreach (var formFile in files)
            {
                if(formFile.FileName.Contains(".jpg")|| formFile.FileName.Contains(".png") || formFile.FileName.Contains(".gif"))
                {
                    var fileNamewithPath = string.Concat(filePaths, "\\", formFile.FileName);
                    filePathsName.Add(fileNamewithPath);
                                                                            //aq se o arquivo existir ele sobrescreve se não existir ele cria
                    using (var steam = new FileStream(fileNamewithPath, FileMode.Create))
                    {                      //aq copia a string para a pasta de destino q e a file namewithPath
                        await formFile.CopyToAsync(steam);
                    }
                }
            }

            ViewData["Resultado"] = $"{files.Count} arquivos foram enviados ao servidor," + $"com tamanho total de : {size} bytes";

            ViewBag.Arquivos = filePathsName;
            return View(ViewData);

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
