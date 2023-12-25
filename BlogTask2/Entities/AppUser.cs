using Microsoft.AspNetCore.Identity;

namespace BlogTask2.Entities
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public override bool TwoFactorEnabled { get => false; }
    }
}
