using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using campusLove.domain.entities;

namespace campusLove.domain.ports
{
    public class IUserRepository : IGenericRepository<User>
    {
       public void Add(User entity)
        {
            
        }

        public void Delete(string id)
        {
            
        }

        public List<User> GetAll()
        {
            return null;
        }

        public void Update(User entity)
        {
            
        }
    }

}