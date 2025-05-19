using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace campusLove.domain.ports
{
    public interface IEditRepository
    {
        public void editGender(int userId, int genderId);
        public void editCity(int userId, int cityId);
        public void editCareer(int userId, int careerId);
        public void editPhrase(int userId, string phrase);
        public void editIsActive(int userId, bool isActive);
        public void editInterest(int userId, int interestId);
        
    }
}