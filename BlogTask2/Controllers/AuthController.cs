using BlogTask2.Entities;
using BlogTask2.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogTask2.Controllers
{
    public class AuthController : Controller
    {
        private UserManager<AppUser> _userManager  { get; set; }
        private SignInManager<AppUser> _signInManager { get; set; }

        public AuthController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public IActionResult Login()
        {
            return View();
        }


        [HttpPost("{ReturnUrl?}")]
        public async Task<IActionResult> Login(LoginVM model,string? ReturnUrl)
        {
            if(!ModelState.IsValid) return View(model);

            var result = await _signInManager.PasswordSignInAsync(model.UserName,model.Password,false,false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or password is wrong!");
                return View(model);
            }

            if (ReturnUrl != null)
            {
                return LocalRedirect(ReturnUrl);
            }

            return RedirectToAction("Index","Home");
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if(!ModelState.IsValid) return View(model);

            var user = new AppUser()
            {
                Name = model.Name,
                Surname = model.Surname,
                UserName = model.UserName
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("",error.Description);
                }
                return View(model);
            }

            return RedirectToAction(nameof(Login));
        }

    }
}
