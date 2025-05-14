using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using campusLove.domain.entities;
using campusLove.domain.ports;

namespace campusLove.application.services
{
    public class userService
    {
        private readonly IUserRepository _repo;

        public userService(IUserRepository repo)
        {
            _repo = repo;
        }

        public List<User> GetAllUsers()
        {
            return _repo.GetAll();
        }

        public void AddUser(User user)
        {
            _repo.Add(user);
        }

        public void DeleteUser(string id)
        {
            _repo.Delete(id);
        }

        public void UpdateUser(User user)
        {
            _repo.Update(user);
        }
        
    }
}