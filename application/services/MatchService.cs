using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using campusLove.domain.entities;
using campusLove.domain.ports;

namespace campusLove.application.services
{
    public class MatchService
    {
        private readonly IMatchesRepository _matchesRepository;

        public MatchService(IMatchesRepository matchesRepository)
        {
            _matchesRepository = matchesRepository;
        }


        public MacthInfo GetNextProfile(string currentUserDoc)
        {
            return _matchesRepository.GetMatchesForUser(currentUserDoc);
        }
        
        public string  FindDoc(String username)
        {
            return  _matchesRepository.GetDocByUsername(username);
        }

    }
}