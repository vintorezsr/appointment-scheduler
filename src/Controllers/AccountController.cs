using AppointmentScheduler.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AppointmentScheduler.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public AccountController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            var isAuthenticated = User.Identity?.IsAuthenticated ?? false;
            if (isAuthenticated)
            {
                return Redirect("~/");
            }

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromForm]LoginViewModel loginViewModel)
        {
            var userAccount = _applicationDbContext.UserAccounts?.FirstOrDefault(x => x.Username == loginViewModel.Username);
            var passwordMatch = userAccount != null && BCrypt.Net.BCrypt.Verify(loginViewModel.Password, userAccount?.Password);

            if (!passwordMatch)
            {
                var viewModel = new LoginViewModel
                {
                    Success = false
                };
                return View(viewModel);
            }

            var claims = new []
            {
                new Claim(ClaimTypes.NameIdentifier, userAccount!.Id.ToString()),
                new Claim(ClaimTypes.Name, userAccount!.Username),
                new Claim(ClaimTypes.Email, userAccount!.Email)
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            return Redirect("~/");
        }

        [Authorize]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }
    }
}