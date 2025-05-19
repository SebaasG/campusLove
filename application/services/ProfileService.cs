using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using campusLove.domain.entities;
using campusLove.domain.ports;

namespace campusLove.application.services
{
    public class ProfileService
    {
        private readonly IProfileRepository _repository;

        public ProfileService(IProfileRepository repository)
        {
            _repository = repository;
        }

        public Profiles GetNextProfile(string currentUserDoc)
        {
            return _repository.GetNextProfile(currentUserDoc);
        }

        public void LikeProfile(string fromUser, string toUser)
        {
            _repository.RegisterInteraction(fromUser, toUser, 1); // 1 = Like
                                                                  // Ya no se necesita verificar ni crear el match aquí: el trigger lo hace automáticamente.
        }


        public void DislikeProfile(string fromUser, string toUser)
        {
            _repository.RegisterInteraction(fromUser, toUser, 2); // 2 = Dislike
        }
        public string FindDoc(String username)
        {
            return _repository.GetDocByUsername(username);
        }
        public int GetCredits(string doc)
        {
            return _repository.GetCreditsByDoc(doc);
        }
        public void UpdateCredits(string doc, int credits)
        {
            _repository.UpdateCredits(doc, credits);
        }

    }
}
