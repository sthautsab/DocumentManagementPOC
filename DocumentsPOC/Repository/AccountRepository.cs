using DocumentsPOC.Context;
using DocumentsPOC.Dto;
using DocumentsPOC.Models;
using Microsoft.EntityFrameworkCore;

namespace DocumentsPOC.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DocumentDbContext _context;

        public AccountRepository(DocumentDbContext context)
        {
            _context = context;
        }
        public async Task<User> Login(LoginDto loginDto)
        {
            var user = await _context.Users.Include(x => x.Comments).Where(x => x.UserName == loginDto.UserName).FirstOrDefaultAsync();
            if (user != null)
            {
                return user;
            }
            return null;
        }
    }
}
