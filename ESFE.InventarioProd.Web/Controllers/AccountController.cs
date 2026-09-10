using System.Security.Claims;
using ESFE.GestionProductos.LN;
using ESFE.InventarioProd.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESFE.InventarioProd.Web.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UsuarioLN usuarioLN = new UsuarioLN();

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToAction("Index", "Dashboard");

            ViewData["ReturnUrl"] = returnUrl;
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (!ModelState.IsValid)
                return View(model);

            var resultado = usuarioLN.ValidarLogin(model.Nombre, model.Password);

            if (resultado == null)
            {
                ModelState.AddModelError(string.Empty, "Credenciales inválidas o usuario inactivo.");
                return View(model);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, resultado.Usuario.IdUsuarioPK.ToString()),
                new Claim(ClaimTypes.Name, resultado.Usuario.Nombre ?? string.Empty),
                new Claim(ClaimTypes.Role, resultado.NombreRol),
                new Claim("IdRol", resultado.Usuario.Id_RolFK?.ToString() ?? string.Empty)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Dashboard");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
