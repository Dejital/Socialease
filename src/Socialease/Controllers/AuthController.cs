using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Socialease.Models;
using Socialease.ViewModels;

namespace Socialease.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<SocialUser> _signInManager;

        public AuthController(SignInManager<SocialUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("People", "App");
            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel vm, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(vm.Username, vm.Password, true, false);

                if (signInResult.Succeeded)
                {
                    if (string.IsNullOrWhiteSpace(returnUrl))
                    {
                        return RedirectToAction("People", "App");
                    }
                    return Redirect(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Username or password incorrect.");
                }
            }
            return View();
        }

        public async Task<ActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
            }
            return RedirectToAction("Index", "App");
        }
    }
}
