using DocumentsPOC.Dto;
using DocumentsPOC.Models;

namespace DocumentsPOC.Repository
{
    public interface IAccountRepository
    {

        public Task<User> Login(LoginDto loginDto);
    }
}
