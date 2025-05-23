using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using campusLove.domain.entities;

namespace campusLove.domain.ports
{
    public interface IMatchesRepository
    {
        List<MacthInfo> GetMatchesForUser(string userDoc);
        string GetDocByUsername(string username);

        bool MatchExists(string user1, string user2); 
        
        string getEmailByUsername(string username);
    

    }
    
}