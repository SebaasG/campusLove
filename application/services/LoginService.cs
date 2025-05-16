using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using campusLove.domain.ports;

namespace campusLove.application.services
{
    public class LoginService
    {
        private readonly ILoginRepository _repo;
        public LoginService(ILoginRepository repo)
        {
            _repo = repo;
        }
        public string login(string username, string password)
        {
            return _repo.login(username, password);
        }
    }
}