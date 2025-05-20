using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using campusLove.domain.ports;

namespace campusLove.application.services
{
    public class DtoEditService
    {
        private readonly IDtoEditRepository _editRepo;

        public DtoEditService(IDtoEditRepository editRepo)
        {
            _editRepo = editRepo;
        }

        public void EditGender(string userDoc, int newGender)
        {
            _editRepo.editGender(userDoc, newGender);
        }

        public void EditCity(string userDoc, int newCity)
        {
            _editRepo.editCity(userDoc, newCity);
        }

        public void EditCareer(string userDoc, int newCareer)
        {
            _editRepo.editCareer(userDoc, newCareer);
        }

        public void EditPhrase(string userDoc, string newPhrase)
        {
            _editRepo.editPhrase(userDoc, newPhrase);
        }

        public void EditEmail(string userDoc, string newEmail)
        {
            _editRepo.editEmail(userDoc, newEmail);
        }

        public void EditActive(string userDoc, bool newStatus)
        {
            _editRepo.editActive(userDoc, newStatus);
        }
    }
}
