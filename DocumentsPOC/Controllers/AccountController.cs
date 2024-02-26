using DocumentsPOC.Dto;
using DocumentsPOC.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DocumentsPOC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public IActionResult Index()
        {
            return View("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _accountRepository.Login(loginDto);

            return Ok(user);
        }
    }
}
