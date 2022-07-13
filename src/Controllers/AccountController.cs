using AppointmentScheduler.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AppointmentScheduler.Controllers
{
    /// <summary>
    /// Authentication controller class.
    /// </summary>
    public class AccountController : Controller
    {
        /// <summary>
        /// Field that hold <see cref="ApplicationDbContext"/> instance.
        /// </summary>
        private readonly ApplicationDbContext _applicationDbContext;

        /// <summary>
        /// Public constructor of <see cref="AccountController"/>.
        /// </summary>
        /// <param name="applicationDbContext">This instance of <see cref="ApplicationDbContext"/>.</param>
        public AccountController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        /// <summary>
        /// Login page.
        /// </summary>
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

        /// <summary>
        /// POST method handler for Login action.
        /// </summary>
        /// <param name="loginViewModel">Instance of <see cref="LoginViewModel"/>.</param>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromForm]LoginViewModel loginViewModel)
        {
            var userAccount = _applicationDbContext.UserAccounts?.FirstOrDefault(x => (x.Username == loginViewModel.Username || x.Email == loginViewModel.Username));
            var passwordMatch = userAccount != null && BCrypt.Net.BCrypt.Verify(loginViewModel.Password, userAccount?.Password);

            if (!passwordMatch)
            {
                ModelState.AddModelError("Error", "User/E-Mail Account does not exist");
                return View();
            }

            var claims = new []
            {
                new Claim(ClaimTypes.NameIdentifier, userAccount!.Id.ToString()),
                new Claim(ClaimTypes.Name, userAccount!.Username),
                new Claim(ClaimTypes.Email, userAccount!.Email)
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, new AuthenticationProperties
            {
                IsPersistent = loginViewModel.RememberMe
            });

            return Redirect("~/");
        }

        /// <summary>
        /// Logout action.
        /// </summary>
        [Authorize]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }
    }
}