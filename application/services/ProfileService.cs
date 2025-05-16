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

        // public void LikeProfile(string fromUser, string toUser)
        // {
        //     _repository.RegisterInteraction(fromUser, toUser, 1); // 1 = Like

        //     if (_repository.CheckIfMatchExists(toUser, fromUser))
        //     {
        //         _repository.CreateMatch(fromUser, toUser);
        //     }
        // }

        // public void DislikeProfile(string fromUser, string toUser)
        // {
        //     _repository.RegisterInteraction(fromUser, toUser, 2); // 2 = Dislike
        // }
    }
}