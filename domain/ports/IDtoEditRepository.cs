using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using campusLove.infrastructure.repositories;

namespace campusLove.domain.ports
{
    public interface IDtoEditRepository
    {
        public void editGender(string user, int newGender);
        public void editCity(string user, int newCity);
        public void editCareer(string user, int newCareer);
        public void editPhrase(string user, string newPhrase);
        public void editEmail(string user, string newEmail);
        public void editActive(string user, bool newStatus);

    }
}