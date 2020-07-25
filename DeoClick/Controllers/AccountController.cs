using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeoClick.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DeoClick.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Usuario> userManager;
        private readonly SignInManager<Usuario> signInManager;

        public AccountController(UserManager<Usuario> user, SignInManager<Usuario> sign)
        {
            userManager = user;
            signInManager = sign;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            var result = await signInManager.PasswordSignInAsync(user.Email, user.Password, true, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                var u = await userManager.FindByEmailAsync(user.Email);
                var r = await userManager.GetRolesAsync(u);
                if (r[0] == "Administrador")
                    return RedirectToAction("Index", "Admin");
                else
                    return RedirectToAction("Index", "Home");
            }

            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Usuário Bloqueado, aguarde alguns minutos e tente novamente");
                return View();
            }
            else
            {
                ModelState.AddModelError("", "E-mail de Acesso e/ou Senhas Inválidos!!!");
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
