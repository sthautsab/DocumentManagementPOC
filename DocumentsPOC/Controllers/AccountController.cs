using DocumentsPOC.Dto;
using DocumentsPOC.Models;
using DocumentsPOC.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace DocumentsPOC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            User user = await _accountRepository.Login(loginDto);
            if (user != null)
            {
                //Store user info in the session
                HttpContext.Session.SetString("UserInfo", JsonConvert.SerializeObject(user));

                var info = HttpContext.Session.GetString("UserInfo");

                List<Claim> claims = new List<Claim>() {
                    new Claim(ClaimTypes.NameIdentifier, loginDto.UserName)
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = false
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), properties);

                return RedirectToAction("Index", "Document");
            }
            //ViewData["ValidateMessage"] = "Enter correct username";
            return View("Login");
        }
    }
}
