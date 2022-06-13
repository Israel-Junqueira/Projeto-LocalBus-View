using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocalBus.Models
{
    public class MyUser : IdentityUser
    {
        public Escola Escola { get; set; }
    }
}
