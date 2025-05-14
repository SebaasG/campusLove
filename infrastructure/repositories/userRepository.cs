using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using campusLove.domain.entities;
using campusLove.domain.ports;

namespace campusLove.infrastructure.repositories
{
    public class userRepository : IGenericRepository<User>, IUserRepository
    {
         public List<User> GetAll()
        {
            throw new NotImplementedException();
        }
        public void Add(User entity)
        {
            throw new NotImplementedException();
        }
        public void Delete(string id)
        {
            throw new NotImplementedException();
        }
        public void Update(User user)
        {
            throw new NotImplementedException();
        }
       
    }
}