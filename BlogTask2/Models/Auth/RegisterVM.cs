using System.ComponentModel.DataAnnotations;

namespace BlogTask2.Models.Auth
{
    public class RegisterVM
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        
        [Compare(nameof(ConfirmPassword),ErrorMessage = "Passwords should match!")]
        public string Password{ get; set; }
        public string ConfirmPassword{ get; set; }
    }
}
