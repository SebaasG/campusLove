using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using campusLove.domain.ports;

namespace campusLove.application.services
{
    public class EditProfileService
    {
        private readonly IEditRepository _repo;
        public EditProfileService(IEditRepository repo)
        {
            _repo = repo;
        }

        public void EditGender(int gender, int newGender)
        {
            _repo.editGender(gender, newGender);
        }
        public void EditCity(int userId, int cityId)
        {
            _repo.editCity(userId, cityId);
        }
        public void EditCareer(int userId, int careerId)
        {
            _repo.editCareer(userId, careerId);
        }
        public void EditPhrase(int userId, string phrase)
        {
            _repo.editPhrase(userId, phrase);
        }
        public void EditIsActive(int userId, bool isActive)
        {
            _repo.editIsActive(userId, isActive);
        }
        public void EditInterest(int userId, int interestId)
        {
            _repo.editInterest(userId, interestId);
        }
    }
}