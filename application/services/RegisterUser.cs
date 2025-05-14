using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using campusLove.domain.dto;
using campusLove.domain.ports;

namespace campusLove.application.services
{
    public class RegisterUser
    {

        private readonly IRegisterRepository _repo;
        public RegisterUser(IRegisterRepository repo)
        {
            _repo = repo;
        }

        public void registerUser(DtoRegisterUser user){
            _repo.register(user);
        }
    }
}