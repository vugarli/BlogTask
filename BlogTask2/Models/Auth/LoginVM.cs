using System.ComponentModel.DataAnnotations;

namespace BlogTask2.Models.Auth
{
    public class LoginVM
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
