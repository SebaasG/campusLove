using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using campusLove.domain.entities;

namespace campusLove.domain.ports
{
    public interface IProfileRepository
    {
        Profiles? GetNextProfile(string currentUserDoc);
        void RegisterInteraction(string fromUser, string toUser, int typeId);
        bool CheckIfMatchExists(string fromUser, string toUser);
        void CreateMatch(string user1, string user2);
        string GetDocByUsername(string username);
    }
}