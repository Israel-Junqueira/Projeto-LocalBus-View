using LocalBus.Context;
using LocalBus.Models;
using LocalBus.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using LocalBus.Services;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Text.Encodings.Web;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace LocalBus.Controllers
{
    [Area("Usuario")]
    public class AccountController : Controller
    {
        private readonly UserManager<MyUser> _userManeger;
        private readonly SignInManager<MyUser> _signInManager;
        private readonly AppDbContext _context;

        private readonly ConfigurationImagens _myConfig;

        private readonly IWebHostEnvironment _hostingEvironment;
        public AccountController( IWebHostEnvironment hostEnvironment,IOptions<ConfigurationImagens> myconfiguration,UserManager<MyUser> userManeger, SignInManager<MyUser> signInManager,AppDbContext appDbContext)
        {
           
            _hostingEvironment = hostEnvironment;
            _myConfig = myconfiguration.Value;
            _context = appDbContext;
            _userManeger = userManeger;
            _signInManager = signInManager;
        }

     
        
        [HttpGet]
        public IActionResult Perfil(string id)
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
        public IActionResult Foto()
        {
            return View();
        }                                
                //recebe a lista de arquivos 
        [HttpPost]
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
                    using (var stream = System.IO.File.Create(fileNamewithPath))
                    {                      //aq copia a string para a pasta de destino q e a file namewithPath
                        await formFile.CopyToAsync(stream);
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
           // await _userManeger.ResetAccessFailedCountAsync(user);//resetada as contagem dos erros de senha
                                                                 //daq pra ca e a parte do bloqueio de usuario
            if (user != null && !await _userManeger.IsLockedOutAsync(user))
            {
                //confirmação do email
                if (!await _userManeger.IsEmailConfirmedAsync(user))
                {
                    ModelState.AddModelError("", "Seu E-mail Ainda Não Foi Válido! ");
                    return View();
                }

                var result = await _signInManager.PasswordSignInAsync(user, loginVm.Password, false, false);
                if (result.Succeeded)
                {
                    await _userManeger.ResetAccessFailedCountAsync(user);//resetada as contagem dos erros de senha

                    if (string.IsNullOrEmpty(loginVm.ReturnUrl))
                    {
                        return RedirectToAction("Index", "Home");

                    }
                    return RedirectToAction("Index", "Home");
                }

                await _userManeger.AccessFailedAsync(user); //o cara tentou acessar e errou a senha 
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("LocalBusEtec@outlook.com"));
                email.To.Add(MailboxAddress.Parse(user.Email));
                email.Subject = "🚌..BIII 🚌..BIII - Tentativa de Login Falhou";
                var data = new DateTime();
                 data = DateTime.Today;
             
                email.Body = new TextPart(TextFormat.Plain)
                {
                    Text = $"Tentativa de acesso login falhou: Data:{data.Date} " +
                    $"Isso Foi você?"
                };

                try
                {
                    using (var smtp = new SmtpClient())
                    {
                        smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
                        smtp.Connect("smtp.office365.com", 587, SecureSocketOptions.StartTls);

                        smtp.Authenticate("LocalBusEtec@outlook.com", "LocalBus123@");
                        smtp.Send(email);
                        smtp.Disconnect(true);
                    }
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException(ex.Message);
                }

                if (await _userManeger.IsLockedOutAsync(user))
                {  //email enviado com sugestão de mudança de senha ou desbloqueio de usuario

                   
                }
                  
            }
            ModelState.AddModelError("", "Usuario ou Senha Invalidos!");
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
                var user = new MyUser { UserName = registro.UserName, Email=registro.Email };
                var verificarEmail = await _userManeger.FindByEmailAsync(registro.Email);
                var verificarUsuario = await _userManeger.FindByNameAsync(registro.UserName);
                if (verificarUsuario != null)
                {
                    ModelState.AddModelError("", "O Usuario já está em uso! ");
                    return View();
                }
                if (verificarEmail != null)
                {
                    ModelState.AddModelError("", "O E-Mail já está em uso! ");
                    return View();
                }
               
                var result = await _userManeger.CreateAsync(user, registro.Password);
                var DadosdaEscola = new Escola { EmailDaEscola = registro.Email, TelefoneDaEscola = registro.TelefonedaEscola, NomeEscola = registro.NomedaEscola, CodigoDaEtec = registro.CodigoEtec};
                if (result.Succeeded)
                {
                    var token = await _userManeger.GenerateEmailConfirmationTokenAsync(user);
                    var confirmacaoEmail = Url.Action("ConfirmEmailAdress","Account",new {token = token ,email = registro.Email},Request.Scheme);

                    var email = new MimeMessage();
                    email.From.Add(MailboxAddress.Parse("LocalBusEtec@outlook.com"));
                    email.To.Add(MailboxAddress.Parse(registro.Email));
                    email.Subject = "BIIII BIII - Pedido de Validação Para o Usuario";
                    email.Body = new TextPart(TextFormat.Plain) { Text =$"Usuario de Nome: {registro.UserName}, Nome da escola {registro.NomedaEscola}, " +
                        $"Numero para contato:{registro.TelefonedaEscola}, E-mail:{registro.Email} " +
                        $"Clique aqui para validar o Usuario {confirmacaoEmail}"};

                    try
                    {
                        using (var smtp = new SmtpClient())
                        {
                            smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
                            smtp.Connect("smtp.office365.com", 587, SecureSocketOptions.StartTls);

                            smtp.Authenticate("LocalBusEtec@outlook.com", "LocalBus123@");
                            smtp.Send(email);
                            smtp.Disconnect(true);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new InvalidOperationException(ex.Message);
                    }
                    await _userManeger.AddToRoleAsync(user, "Member");
                    _context.Add(new Escola { CodigoDaEtec = DadosdaEscola.CodigoDaEtec,EmailDaEscola = DadosdaEscola.EmailDaEscola,NomeEscola=DadosdaEscola.NomeEscola,TelefoneDaEscola=DadosdaEscola.TelefoneDaEscola,MyUserId=user.Id});
                   _context.SaveChanges();

                    return View("Sucesso");
                }
                else
                {
                    foreach (var erro in result.Errors)
                    {
                        ModelState.AddModelError("", erro.Description);
                    }
                    return View();
                }
            }
            return View(registro);
        }
        [HttpGet]
        public async Task<IActionResult> ConfirmEmailAdress(string token,string email)
        {
            var user = await _userManeger.FindByEmailAsync(email);
            if(user != null)
            {
                var result = await _userManeger.ConfirmEmailAsync(user, token);

                if (result.Succeeded)
                {
                    return View("Login");
                }
            }


            return View("Login");
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.User = null;
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> Logout1()
        {//a view desse action e uma partial view criada na pasta shered, -adicionar - view - view razor - check criar um modelo de exibição parcial 
            HttpContext.Session.Clear();
            HttpContext.User = null;
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
         {
           if(ModelState.IsValid)
            {
                var user = await _userManeger.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var token = await _userManeger.GeneratePasswordResetTokenAsync(user);
                    var resetURL = Url.Action("ResetPassword", "Account", new { token = token, email = model.Email }, Request.Scheme);

                    var email = new MimeMessage();
                    email.From.Add(MailboxAddress.Parse("LocalBusEtec@outlook.com"));
                    email.To.Add(MailboxAddress.Parse(model.Email));
                    email.Subject = "BIIII BIII - Esqueci minha senha!";
                    email.Body = new TextPart(TextFormat.Plain)
                    {
                        Text = $"Clique no link para redefinir a senha:{resetURL}"
                    };

                    try
                    {
                        using (var smtp = new SmtpClient())
                        {
                            smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
                            smtp.Connect("smtp.office365.com", 587, SecureSocketOptions.StartTls);

                            smtp.Authenticate("LocalBusEtec@outlook.com", "LocalBus123@");
                            smtp.Send(email);
                            smtp.Disconnect(true);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new InvalidOperationException(ex.Message);
                    }
                    return View("Sucesso");
                }
                else
                {
                    ModelState.AddModelError("", "E-Mail não encontrado");
                    return View();
                }
            }
           return View();

        }
        [HttpGet]
        public async Task<IActionResult> ResetPassword(string token, string email)
        {
            return View(new ResetPasswordModel { Token = token,Email = email});
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManeger.FindByEmailAsync(model.Email);

                if(user != null)
                {
                    var result = await _userManeger.ResetPasswordAsync(user, model.Token, model.Password);
                    if (!result.Succeeded)
                    {
                        foreach (var erro in result.Errors)
                        {
                            ModelState.AddModelError("", erro.Description);
                        }
                        return View();
                    }
                    return View("Login"); //falta implementar
                }
                ModelState.AddModelError("", "Invalid Request");
            }
            return View();

        }

        public IActionResult AccessDenied()
        {
            return View();
        }


    }
}
