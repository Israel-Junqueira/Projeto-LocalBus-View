using Microsoft.AspNetCore.Identity;

namespace LocalBus.Models
{
    public class ValidadorDeSenha<TUser> : IPasswordValidator<TUser> where TUser : class
    {
        public async Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user, string password)
        {
            var username = await manager.GetUserNameAsync(user);

            if (username == password)
            {
                return IdentityResult.Failed(new IdentityError { Description = "USUARIOS E SENHAS NÃO PODEM SER IGUAIS" });
            }
            if (password.Contains("password"))
            {
                return IdentityResult.Failed(new IdentityError { Description = "A SENHA È MUITO OBVIA, NÂO PODE SER PASSWORD" });
            }
            if (password == "12345")
            {
                return IdentityResult.Failed(new IdentityError { Description = $"A SENHA È MUITO OBVIA, NÂO PODE SER {password}" });
            }
       

            return IdentityResult.Success;
        }
    }
}
